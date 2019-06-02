using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using ModelProject.AggregationalModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.ServiceProviders;
using UIProject.UIConnector;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class BanHangPageVM : BasePageViewModel
    {
        private ICommand thanhToanCommand;
        private PhieuBanModel phieuBan;
        private List<ChiTietBanModel> dsChiTietBan;

        /// <summary>
        /// Thông tin khách hàng 
        /// </summary>
        public KhachHangModel KhachHang
        {
            get
            {
                var khachHangDaChon = TimKiemKhachHangVM.SelectedItem as ItemViewModel<KhachHangModel>;
                if (khachHangDaChon == null)
                    return null;
                return khachHangDaChon.Model;
            }
        }

        /// <summary>
        /// Hóa đơn tương ứng với phiếu bán
        /// </summary>
        public HoaDonModel HoaDon
        {
            get
            {
                return new HoaDonModel(this.PhieuBan)
                {
                    DSChiTietBan = DanhSachChiTietBan?.Models.ToList()
                };
            }
        }


        /// <summary>
        /// View model của việc tìm kiếm sản phẩm
        /// </summary>
        public SearchTextBoxViewModel<SanPhamModel> TimKiemSanPhamVM { get; set; }

        /// <summary>
        /// View model của việc tìm kiếm khách hàng
        /// </summary>
        public SearchTextBoxViewModel<KhachHangModel> TimKiemKhachHangVM { get; set; }

        /// <summary>
        /// Danh sách chi tiết phiếu bán
        /// </summary>
        public ObservableCollectionViewModel<ChiTietBanModel> DanhSachChiTietBan { get; set; }

        /// <summary>
        /// Model của phiếu bán hàng
        /// </summary>
        public PhieuBanModel PhieuBan
        {
            get => phieuBan;
            set => SetProperty(ref phieuBan, value);
        }

        /// <summary>
        /// Định nghĩa bộ lọc sản phẩm
        /// </summary>
        public EnumFilterViewModel<SanPhamModel> LocSanPhamVM { get; set; }
        
        /// <summary>
        /// Số tiền khách hàng đã trả
        /// </summary>
        public long SoTienKhachTra
        {
            get => GetPropertyValue<long>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Số tiền thối lại
        /// </summary>
        public long SoTienThoiLai { get; set; }

        /// <summary>
        /// Danh sách chi tiết bán
        /// </summary>
        public List<ChiTietBanModel> DSChiTietBan
        {
            get => this.dsChiTietBan;
        }

        /// <summary>
        /// Command thực hiện việc tính toán
        /// </summary>
        public ICommand ThanhToanCommand
        {
            get => thanhToanCommand ?? new BaseCommand<IWindow>(OnThanhToanCommandExecute);
            set => thanhToanCommand = value;
        }

        /// <summary>
        /// Command thêm khách hàng
        /// </summary>
        public ICommand ThemKhachHangCommand
        {
            get => new BaseCommand<IWindow>(OnThemKhachHangCommandExecute);
        }

        /// <summary>
        /// Event xảy ra khi chọn 1 sản phẩm đã có từ trước
        /// </summary>
        public event EventHandler<ItemEventArgs<ChiTietBanModel>> SanPhamDaCo
        {
            add { DanhSachChiTietBan.ContainsItemModel += value; }
            remove { DanhSachChiTietBan.ContainsItemModel -= value; }
        }

        public BanHangPageVM() : base() { }
        public BanHangPageVM(INavigator navigator) : base(navigator) { }

        protected override void LoadPageComponents()
        {
            PhieuBan = new PhieuBanModel();

            SetUpBolocTimKiemSanPham();
            SetUpBoLocTimKiemKhachHang();

            DanhSachChiTietBan = new ObservableCollectionViewModel<ChiTietBanModel>(HoaDon.DSChiTietBan);
        }
        private void SetUpBolocTimKiemSanPham()
        {
            var sanPhamSource = DataAccess.LoadSanPham();

            TimKiemSanPhamVM = new SearchTextBoxViewModel<SanPhamModel>(sanPhamSource);

            TimKiemSanPhamVM.DefaultFilter = new Func<ItemViewModel<SanPhamModel>, bool>(LocTenSanPhamCallBack);

            LocSanPhamVM = new EnumFilterViewModel<SanPhamModel>(
                new List<Func<ItemViewModel<SanPhamModel>, bool>>()
                {
                    new Func<ItemViewModel<SanPhamModel>, bool>(LocLoaiSanPhamCallBack)
                },
                new ObservableCollection<LoaiSanPhamModel>(DataAccess.LoadLoaiSanPham()));

            LocSanPhamVM.NonApplyFilterItem.Model = new LoaiSanPhamModel() { TenLoaiSP = "Lọc tất cả", MaLoaiSP = "LSP-1" };

            TimKiemSanPhamVM.AdditionFilters = LocSanPhamVM.FilterCallBacks;

            TimKiemSanPhamVM.SelectedItemChanged += TimKiemSanPhamVM_SelectionChanged;
        }
        private void SetUpBoLocTimKiemKhachHang()
        {
            var khachHangSource = DataAccess.LoadKhachHang();
            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>(khachHangSource);
            TimKiemKhachHangVM.DefaultFilter = new Func<ItemViewModel<KhachHangModel>, bool>(LocTenKhachHangCallBack);
            TimKiemKhachHangVM.SelectedValuePath = "TenKH";
        }



        #region Event Handler
        private void TimKiemSanPhamVM_SelectionChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var sanPhamDaChon = e.SelectedItem as ItemViewModel<SanPhamModel>;
            if (sanPhamDaChon != null)
            {
                DanhSachChiTietBan.Add(new ChiTietBanModel(PhieuBan, sanPhamDaChon.Model, 1));
            }
        }
        #endregion


        #region Callback cho bộ lọc tìm kiếm 
        private bool LocLoaiSanPhamCallBack(ItemViewModel<SanPhamModel> sanPham)
        {
            var loaiSanPhamDaChon = LocSanPhamVM.Collection.SelectedItem as ItemViewModel<object>;
            if (loaiSanPhamDaChon == null)
            {
                return true;
            }
            var castLoaiSanPhamDaChon = loaiSanPhamDaChon.Model as LoaiSanPhamModel;
            var chonTatCa = LocSanPhamVM.NonApplyFilterItem.Model as LoaiSanPhamModel;

            if (chonTatCa == null || castLoaiSanPhamDaChon.MaLoaiSP == chonTatCa.MaLoaiSP)
                return true;
            return sanPham.Model.MaLoaiSP.Equals(castLoaiSanPhamDaChon.MaLoaiSP);
        }
        private bool LocTenSanPhamCallBack(ItemViewModel<SanPhamModel> sanPham)
        {
            return sanPham.Model.TenSP.ToLower().Contains(TimKiemSanPhamVM.Text.ToLower());
        }
        private bool LocTenKhachHangCallBack(ItemViewModel<KhachHangModel> khachHang)
        {
            return khachHang.Model.TenKH.ToLower().Contains(TimKiemKhachHangVM.Text.ToLower());
        }
        #endregion


        #region Command Execution 
        protected virtual void OnThemKhachHangCommandExecute(IWindow window)
        {
            window.DataContext = new AddingWindowViewModel<KhachHangModel>();
            window.ShowDialog();
        }

        protected virtual void OnThanhToanCommandExecute(IWindow window)
        {
            window.DataContext = new PrintWindowViewModel();
            if (window.ShowDialog() == true)
            {
                HoaDon.Submit(SubmitType.Add);
            }
        }
        #endregion
    }
}
