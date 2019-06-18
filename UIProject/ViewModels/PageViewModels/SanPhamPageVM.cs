using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using System;
using System.Collections.Generic;
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
    public class SanPhamPageVM : BasePageViewModel
    {
        #region Resources of page
        private IEnumerable<SanPhamModel> dsSanPham;
        private IEnumerable<LoaiSanPhamModel> dsLoaiSanPham;
        private IEnumerable<NhaCungCapModel> dsNhaCungCap;
        #endregion
        /// <summary>
        /// Danh sách sản phẩm
        /// </summary>
        public ObservableCollectionViewModel<SanPhamModel> DanhSachSanPhamVM { get; set; }

        /// <summary>
        /// View model của bộ lọc khu vực
        /// </summary>
        public EnumFilterViewModel<SanPhamModel> LocLoaiSanPhamVM { get; set; }

        /// <summary>
        /// View model của bộ lọc nhà cung cấp
        /// </summary>
        public EnumFilterViewModel<SanPhamModel> LocNhaCungCapVM { get; set; }


        /// <summary>
        /// View model của việc tìm kiếm khách hàng
        /// </summary>
        public SearchTextBoxViewModel<SanPhamModel> TimKiemSanPhamVM { get; set; }

        /// <summary>
        /// Command cho việc thêm khách hàng
        /// </summary>
        public ICommand ThemSanPhamCommand
        {
            get => new BaseCommand(OnThemSanPhamCommandExecute);
        }

        /// <summary>
        /// Command cho việc chỉnh sửa thông tin
        /// </summary>
        public ICommand ChinhSuaThongTinCommand
        {
            get => new BaseCommand<IWindowExtension>(
                OnChinhSuaThongTinCommandExecute,
                window => DanhSachSanPhamVM?.SelectedItem != null);
        }

        /// <summary>
        /// Command cho việc xóa khách hàng
        /// </summary>
        public ICommand XoaSanPhamCommand
        {
            get => new BaseCommand<IWindow>(
                OnXoaSanPhamCommandExecute,
                window => DanhSachSanPhamVM?.SelectedItem != null);
        }


        public SanPhamPageVM() : base() { }
        public SanPhamPageVM(INavigator navigator) : base(navigator) { }

        private void SetUpDanhSachSanPhamVM()
        {
            DanhSachSanPhamVM = new ObservableCollectionViewModel<SanPhamModel>(dsSanPham);
            DanhSachSanPhamVM.Filters.Add(LocSanPhamTheoTen);
            DanhSachSanPhamVM.Filters.Add(LocSanPhamTheoLoaiSanPham);
            DanhSachSanPhamVM.Filters.Add(LocSanPhamTheoNhaCungCap);
        }


        private void SetUpTimKiemSanPhamVM()
        {
            TimKiemSanPhamVM = new SearchTextBoxViewModel<SanPhamModel>(null);
            TimKiemSanPhamVM.TextChanged += TimKiemSanPhamVM_TextChanged;

            // local function
            void TimKiemSanPhamVM_TextChanged(object sender, TextValueChangedEventArgs e)
            {
                if (e.NewValue == null || e.NewValue.Equals(e.OldValue))
                    return;

                DanhSachSanPhamVM.Filter();
            }
        }

        private void SetUpLocLoaiSanPhamVM()
        {
            LocLoaiSanPhamVM = new EnumFilterViewModel<SanPhamModel>(
                LocSanPhamTheoLoaiSanPham,
                dsLoaiSanPham);

            // Thêm 1 lựa chọn tất cả vào bộ lọc
            LocLoaiSanPhamVM.IsApplyNonFilterItem = true;
            LocLoaiSanPhamVM.NonApplyFilterItem.Model 
                = new LoaiSanPhamModel() { MaLoaiSP = null, TenLoaiSP = "Chọn tất cả" };
            LocLoaiSanPhamVM.SelectedItemChanged += LocSanPhamVM_SelectedItemChanged;

            // local function
            void LocSanPhamVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                if (e.SelectedItem != null)
                {
                    DanhSachSanPhamVM.Filter();
                }
            }

        }

        private void SetUpLocNhaCungCapVM()
        {
            LocNhaCungCapVM = new EnumFilterViewModel<SanPhamModel>(
                LocSanPhamTheoNhaCungCap,
                DataAccess.LoadNhaCungCap());

            // Thêm 1 lựa chọn tất cả vào bộ lọc
            LocNhaCungCapVM.IsApplyNonFilterItem = true;
            LocNhaCungCapVM.NonApplyFilterItem.Model 
                = new NhaCungCapModel() { MaNCC = null, TenNCC = "Chọn tất cả" };

            LocNhaCungCapVM.SelectedItemChanged += LocNhaCungCapVM_SelectedItemChanged;

            void LocNhaCungCapVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                DanhSachSanPhamVM.Filter();
            }
        }

        #region Filter callbacks
        private bool LocSanPhamTheoTen(ItemViewModel<SanPhamModel> sanPham)
        {
            if (string.IsNullOrEmpty(TimKiemSanPhamVM.Text))
                return true;

            return sanPham.Model.TenSP.ToLower().StartsWith(TimKiemSanPhamVM.Text.ToLower());
        }
        private bool LocSanPhamTheoLoaiSanPham(ItemViewModel<SanPhamModel> sanPham)
        {
            var loaiSanPhamDaChon = LocLoaiSanPhamVM.Collection.SelectedItem as ItemViewModel<object>;
            if (loaiSanPhamDaChon == null)
            {
                return true;
            }
            var castLoaiSP = loaiSanPhamDaChon.Model as LoaiSanPhamModel;

            if (castLoaiSP == null || castLoaiSP.MaLoaiSP == null)
                return true;
            return sanPham.Model.MaLoaiSP == castLoaiSP.MaLoaiSP;
        }
        private bool LocSanPhamTheoNhaCungCap(ItemViewModel<SanPhamModel> sanPham)
        {
            var nhaCCDaChon = LocNhaCungCapVM.Collection.SelectedItem as ItemViewModel<object>;
            if (nhaCCDaChon == null)
            {
                return true;
            }
            var castNhaCC = nhaCCDaChon.Model as NhaCungCapModel;

            // nhà cung cấp đã chọn là item "Chọn tất cả" vì mã null
            if (castNhaCC == null || castNhaCC.MaKhuVuc == null)
                return true;
            return sanPham.Model?.MaNCC == castNhaCC?.MaNCC;
        }

        #endregion


        #region Command execution
        private void OnXoaSanPhamCommandExecute(IWindow window)
        {
            DialogWindowViewModel notifyDeleteWnd = new DialogWindowViewModel()
            {
                DialogType = DialogWindowType.YesNo,
                YesText = "Có",
                NoText = "Không",
                MessageText = "Bạn muốn xóa sản phẩm này ?"
            };

            notifyDeleteWnd.ButtonPressed += NotifyDeleteWnd_ButtonPressed;
            window.DataContext = notifyDeleteWnd;

            if (window.ShowDialog() == true)
            {
                var deleteSanPhamItem = DanhSachSanPhamVM.SelectedItem as ItemViewModel<SanPhamModel>;
                if (deleteSanPhamItem == null)
                    return;
                deleteSanPhamItem.Model.Submit(SubmitType.Delete);

                // page reload
                Reload();
            }

            // local functon
            void NotifyDeleteWnd_ButtonPressed(object sender, DialogButtonPressedEventArgs e)
            {
                if (e.DialogResult == DialogResult.Yes)
                    window.DialogResult = true;
                if (e.DialogResult == DialogResult.No)
                    window.DialogResult = false;

                window.Close();
            }
        }

        private void OnThemSanPhamCommandExecute()
        {
            this.Navigator.Navigate("Nhập hàng");
        }

        private void OnChinhSuaThongTinCommandExecute(IWindowExtension window)
        {
            var sanPhamDaChon = DanhSachSanPhamVM.SelectedItem as ItemViewModel<SanPhamModel>;

            EditWindowViewModel<SanPhamModel> chinhSuaThongTin
                = new EditWindowViewModel<SanPhamModel>(sanPhamDaChon.Model);

            chinhSuaThongTin.AdditionData.Add(dsLoaiSanPham);

            window.DataContext = chinhSuaThongTin;
            window.Closing += (sender, e) => e.Cancel = true;
    
            // Update thông tin sản phẩm thành công
            if (window.ShowDialog(-500, -200) == true)
            {
                Reload();
            }
        }

        private void RefreshResource()
        {
            dsSanPham = DataAccess.LoadSanPham();
            dsLoaiSanPham = DataAccess.LoadLoaiSanPham();
            dsNhaCungCap = DataAccess.LoadNhaCungCap();
        }

        protected override void LoadComponentsInternal()
        {
            RefreshResource();

            SetUpTimKiemSanPhamVM();
            SetUpLocLoaiSanPhamVM();
            SetUpLocNhaCungCapVM();
            SetUpDanhSachSanPhamVM();
        }

        protected override void ReloadComponentsInternal()
        {
            RefreshResource();

            DanhSachSanPhamVM.Reload();
            DanhSachSanPhamVM.RefreshItemsSource(dsSanPham);

            TimKiemSanPhamVM.Reload();

            LocLoaiSanPhamVM.Reload();
            LocLoaiSanPhamVM.RefreshItemsSource(dsLoaiSanPham);

            LocNhaCungCapVM.Reload();
            LocNhaCungCapVM.RefreshItemsSource(dsNhaCungCap);
        }


        #endregion
    }
}
