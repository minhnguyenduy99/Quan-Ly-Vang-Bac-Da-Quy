using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        #region Private fields and resources
        private ICommand thanhToanCmd;
        private ICommand themKhachHangCmd;

        private IEnumerable<SanPhamModel> dsSanPham;
        private IEnumerable<KhachHangModel> dsKhachHang;
        private IEnumerable<LoaiSanPhamModel> dsLoaiSanPham;
        #endregion

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
            get => thanhToanCmd ?? new BaseCommand<IWindow>(OnThanhToanCommandExecute);
            set => thanhToanCmd = value;
        }

        /// <summary>
        /// Command thêm khách hàng
        /// </summary>
        public ICommand ThemKhachHangCommand
        {
            get => themKhachHangCmd ??  new BaseCommand<IWindowExtension>(OnThemKhachHangCommandExecute);
            set => themKhachHangCmd = value;
        }

        public BanHangPageVM() : base() { }
        public BanHangPageVM(INavigator navigator) : base(navigator) { }


        public event EventHandler<ItemEventArgs<ChiTietBanModel>> SanPhamDaTonTai
        {
            add { HoaDonVM.SanPhamDaTonTai += value; }
            remove { HoaDonVM.SanPhamDaTonTai -= value; }
        }

        private void SetUpHoaDonVM()
        {
            HoaDonVM = new HoaDonViewModel();
            HoaDonVM.SanPhamKhongDu += HoaDonVM_SanPhamKhongDuHandler ;

            // local function
            void HoaDonVM_SanPhamKhongDuHandler(object sender, EventArgs e)
            {
                MessageBox.Show("Không đủ số lượng cho sản phẩm này");
            }
        }


        private void SetUpBolocTimKiemSanPham()
        {
            TimKiemSanPhamVM = new SearchTextBoxViewModel<SanPhamModel>(dsSanPham);
            
            TimKiemSanPhamVM.Filters.Add(LocTenSanPhamCallBack);
            TimKiemSanPhamVM.Filters.Add(LocSanPhamVM.FilterCallBack);
            
            TimKiemSanPhamVM.SelectedItemChanged += TimKiemSanPhamVM_SelectionChanged;

            // local function
            void TimKiemSanPhamVM_SelectionChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var sanPhamDaChon = e.SelectedItem as ItemViewModel<SanPhamModel>;
                if (sanPhamDaChon != null)
                {
                    var chiTietBan = new ChiTietBanModel(HoaDonVM.PhieuBan, sanPhamDaChon.Model);
                    HoaDonVM.ThemChiTietBan(chiTietBan);
                }
            }
            bool LocTenSanPhamCallBack(ItemViewModel<SanPhamModel> sanPham)
            {
                return sanPham.Model.TenSP.ToLower().Contains(TimKiemSanPhamVM.Text.ToLower());
            }
        }
        private void SetUpBoLocLoaiSanPham()
        {
            LocSanPhamVM = new EnumFilterViewModel<SanPhamModel>(
                LocLoaiSanPhamCallBack,
                dsLoaiSanPham);

            LocSanPhamVM.NonApplyFilterItem.Model = new LoaiSanPhamModel() { TenLoaiSP = "Lọc tất cả", MaLoaiSP = null };

            // local function
            bool LocLoaiSanPhamCallBack(ItemViewModel<SanPhamModel> sanPham)
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
        }
        private void SetUpBoLocTimKiemKhachHang()
        {
            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>(dsKhachHang);
            TimKiemKhachHangVM.Filters.Add(LocTenKhachHangCallBack);
            TimKiemKhachHangVM.SelectedValuePath = "TenKH";

            TimKiemKhachHangVM.SelectedItemChanged += TimKiemKhachHangVM_SelectedItemChanged;

            // local function
            void TimKiemKhachHangVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var khachHangDaChon = e.SelectedItem as ItemViewModel<KhachHangModel>;
                if (khachHangDaChon != null)
                {
                    HoaDonVM.KhachHang = khachHangDaChon.Model;
                }
            }
            bool LocTenKhachHangCallBack(ItemViewModel<KhachHangModel> khachHang)
            {
                return khachHang.Model.TenKH.ToLower().Contains(TimKiemKhachHangVM.Text.ToLower());
            }
        }

        #region Command Execution 
        protected virtual void OnThemKhachHangCommandExecute(IWindowExtension window)
        {
            var addingCustomerVM = new AddingWindowViewModel<KhachHangModel>();
            addingCustomerVM.AdditionData.Add(DataAccess.LoadKhuVuc());

            window.DataContext = addingCustomerVM;
            window.Closing += (sender, e) => e.Cancel = true;

            // thêm khách hàng thành công
            if (window.ShowDialog(-500, 0) == true)
            {
                Reload();
            }
        }

        protected virtual void OnThanhToanCommandExecute(IWindow window)
        {
            if (window.ShowDialog() == true)
            {
                bool submitSuccess = HoaDonVM.Submit();
                Reload();
            }
        }


        #endregion

        #region Setup components

        private void RefreshSource()
        {
            dsKhachHang = DataAccess.LoadKhachHang();
            dsSanPham = DataAccess.LoadSanPham();
            dsLoaiSanPham = DataAccess.LoadLoaiSanPham();
        }

        protected override void LoadComponentsInternal()
        {
            RefreshSource();

            // Thứ tự hàm quan trọng
            SetUpBoLocLoaiSanPham();
            SetUpBolocTimKiemSanPham();
            SetUpBoLocTimKiemKhachHang();
            SetUpHoaDonVM();
        }

        protected override void ReloadComponentsInternal()
        {
            RefreshSource();

            TimKiemKhachHangVM.Reload();
            TimKiemKhachHangVM.RefreshItemSource(dsKhachHang);
            TimKiemSanPhamVM.Reload();
            TimKiemSanPhamVM.RefreshItemSource(dsSanPham);

            HoaDonVM.Reload();
        }
        #endregion
    }
}
