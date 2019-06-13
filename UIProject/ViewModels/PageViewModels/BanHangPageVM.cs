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
using UIProject.ViewModels.DataViewModels;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class BanHangPageVM : BasePageViewModel
    {
        private ICommand thanhToanCommand;
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
        /// View model của hóa đơn
        /// </summary>
        public HoaDonViewModel HoaDonVM { get; set; }


        /// <summary>
        /// View model của việc tìm kiếm sản phẩm
        /// </summary>
        public SearchTextBoxViewModel<SanPhamModel> TimKiemSanPhamVM { get; set; }

        /// <summary>
        /// View model của việc tìm kiếm khách hàng
        /// </summary>
        public SearchTextBoxViewModel<KhachHangModel> TimKiemKhachHangVM { get; set; }

        /// <summary>
        /// Định nghĩa bộ lọc sản phẩm
        /// </summary>
        public EnumFilterViewModel<SanPhamModel> LocSanPhamVM { get; set; }
        
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
            get => new BaseCommand<IWindowExtension>(OnThemKhachHangCommandExecute);
        }

        public BanHangPageVM() : base() { }
        public BanHangPageVM(INavigator navigator) : base(navigator) { }


        public event EventHandler<ItemEventArgs<ChiTietBanModel>> SanPhamDaTonTai
        {
            add { HoaDonVM.SanPhamDaTonTai += value; }
            remove { HoaDonVM.SanPhamDaTonTai -= value; }
        }

        #region Setup components
        protected override void LoadComponentsInternal()
        {
            SetUpBolocTimKiemSanPham();
            SetUpBoLocTimKiemKhachHang();
            SetUpHoaDonVM();
        }

        protected override void ReloadComponentsInternal()
        {
            TimKiemKhachHangVM.Reload();
            TimKiemSanPhamVM.Reload();
            HoaDonVM.DanhSachChiTietBan.Reload();        
        }
        #endregion




        private void SetUpHoaDonVM()
        {
            HoaDonVM = new HoaDonViewModel();
        }

        private void SetUpBolocTimKiemSanPham()
        {
            var sanPhamSource = DataAccess.LoadSanPham();

            TimKiemSanPhamVM = new SearchTextBoxViewModel<SanPhamModel>(sanPhamSource);

            LocSanPhamVM = new EnumFilterViewModel<SanPhamModel>(
                LocLoaiSanPhamCallBack,
                new ObservableCollection<LoaiSanPhamModel>(DataAccess.LoadLoaiSanPham()));

            LocSanPhamVM.NonApplyFilterItem.Model = new LoaiSanPhamModel() { TenLoaiSP = "Lọc tất cả", MaLoaiSP = null };

            TimKiemSanPhamVM.Filters.Add(LocTenSanPhamCallBack);
            TimKiemSanPhamVM.Filters.Add(LocSanPhamVM.FilterCallBack);
            
            TimKiemSanPhamVM.SelectedItemChanged += TimKiemSanPhamVM_SelectionChanged;
        }

        private void SetUpBoLocTimKiemKhachHang()
        {
            var khachHangSource = DataAccess.LoadKhachHang();
            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>(khachHangSource);
            TimKiemKhachHangVM.Filters.Add(new Func<ItemViewModel<KhachHangModel>, bool>(LocTenKhachHangCallBack));
            TimKiemKhachHangVM.SelectedValuePath = "TenKH";

            TimKiemKhachHangVM.SelectedItemChanged += TimKiemKhachHangVM_SelectedItemChanged;
        }



        #region Event Handler
        private void TimKiemSanPhamVM_SelectionChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var sanPhamDaChon = e.SelectedItem as ItemViewModel<SanPhamModel>;
            if (sanPhamDaChon != null)
            {
                var chiTietBan = new ChiTietBanModel(HoaDonVM.PhieuBan, sanPhamDaChon.Model, 1);
                HoaDonVM.ThemChiTietBan(chiTietBan);
            }
        }

        private void TimKiemKhachHangVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var khachHangDaChon = e.SelectedItem as ItemViewModel<KhachHangModel>;
            if (khachHangDaChon != null)
            {
                HoaDonVM.KhachHang = khachHangDaChon.Model;
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

            if (castLoaiSanPhamDaChon.MaLoaiSP == null)
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
        protected virtual void OnThemKhachHangCommandExecute(IWindowExtension window)
        {
            var addingCustomerVM = new AddingWindowViewModel<KhachHangModel>();
            addingCustomerVM.AdditionData.Add(DataAccess.LoadKhuVuc());

            window.DataContext = addingCustomerVM;
            window.Closing += (sender, e) => e.Cancel = true;

            if (window.ShowDialog(-400, 0) == true)
            {
                TimKiemKhachHangVM.RefreshItemSource(DataAccess.LoadKhachHang());
            }
        }

        protected virtual void OnThanhToanCommandExecute(IWindow window)
        {
            if (window.ShowDialog() == true)
            {
                bool submitSuccess = HoaDonVM.Submit();
            }
        }
        #endregion
    }
}
