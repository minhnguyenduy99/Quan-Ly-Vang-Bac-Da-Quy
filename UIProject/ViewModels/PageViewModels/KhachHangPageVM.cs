using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.UIConnector;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    class KhachHangPageVM : BasePageViewModel
    {
        #region Resources from database
        IEnumerable<KhachHangModel> dsKhachHang;
        IEnumerable<SanPhamModel> dsSanPham;
        #endregion

        /// <summary>
        /// Danh sách khách hàng
        /// </summary>
        public ObservableCollectionViewModel<KhachHangModel> DanhSachKhachHangVM { get; set; }

        /// <summary>
        /// View model của bộ lọc khu vực
        /// </summary>
        public EnumFilterViewModel<KhachHangModel> LocKhuVucVM { get; set; }
        

        /// <summary>
        /// Khách hàng đang được chọn
        /// </summary>
        public ItemViewModel<KhachHangModel> KhachHangDuocChon { get; private set; }


        /// <summary>
        /// View model của việc tìm kiếm khách hàng
        /// </summary>
        public SearchTextBoxViewModel<KhachHangModel> TimKiemKhachHangVM { get; set; }

        /// <summary>
        /// Command cho việc thêm khách hàng
        /// </summary>
        public ICommand ThemKhachHangCommand
        {
            get => new BaseCommand<IWindowExtension>(OnThemKhachHangCommandExecute);
        }

        /// <summary>
        /// Command cho việc chỉnh sửa thông tin
        /// </summary>
        public ICommand ChinhSuaThongTinCommand
        {
            get => new BaseCommand<IWindowExtension>
                (OnChinhSuaThongTinCommandExecute,
                window => DanhSachKhachHangVM?.SelectedItem != null);
        }

        /// <summary>
        /// Command cho việc xóa khách hàng
        /// </summary>
        public ICommand XoaKhachHangCommand
        {
            get => new BaseCommand<IWindow>(
                OnXoaKhachHangCommandExecute,
                window => DanhSachKhachHangVM?.SelectedItem != null);
        }


        public KhachHangPageVM() : base() { }
        public KhachHangPageVM(INavigator navigator) : base(navigator) { }


        private void SetUpDanhSachKhachHangVM()
        {
            var khachHangSource = DataAccess.LoadKhachHang();
            DanhSachKhachHangVM = new ObservableCollectionViewModel<KhachHangModel>(khachHangSource);
            DanhSachKhachHangVM.SelectedItemChanged += DanhSachKhachHangVM_SelectedItemChanged;

            var temp = DanhSachKhachHangVM.Filters as List<Func<ItemViewModel<KhachHangModel>, bool>>;
            temp.Add(LocKhachHangTheoTen);
        }

        private void DanhSachKhachHangVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var khachHangDuocChon = e.SelectedItem as ItemViewModel<KhachHangModel>;
            if (khachHangDuocChon != null)
            {
                KhachHangDuocChon = khachHangDuocChon;
            }
        }

        private void SetUpTimKiemKhachHangVM()
        {
            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>(null);
            TimKiemKhachHangVM.TextChanged += TimKiemKhachHangVM_TextChanged;
        }
        private void SetUpLocKhuVucVM()
        {
            LocKhuVucVM = new EnumFilterViewModel<KhachHangModel>(
                LocKhachHangTheoKhuVuc,
                DataAccess.LoadKhuVuc());

            // Thêm 1 lựa chọn tất cả vào bộ lọc
            LocKhuVucVM.NonApplyFilterItem.Model = new KhuVucModel() { MaKhuVuc = null, TenKhuVuc = "Chọn tất cả" };

            (DanhSachKhachHangVM.Filters as List<Func<ItemViewModel<KhachHangModel>, bool>>)
                .Add(LocKhuVucVM.FilterCallBack);

            LocKhuVucVM.SelectedItemChanged += LocKhuVucVM_SelectedItemChanged;
        }



        #region Filter callbacks
        private bool LocKhachHangTheoTen(ItemViewModel<KhachHangModel> khachHang)
        {
            if (string.IsNullOrEmpty(TimKiemKhachHangVM.Text))
                return true;

            return khachHang.Model.TenKH.ToLower().StartsWith(TimKiemKhachHangVM.Text.ToLower());
        }

        private bool LocKhachHangTheoKhuVuc(ItemViewModel<KhachHangModel> khachHang)
        {
            var khuVucDaChon = LocKhuVucVM.Collection.SelectedItem as ItemViewModel<object>;
            if (khuVucDaChon == null)
            {
                return true;
            }
            var castLoaiKhuVuc = khuVucDaChon.Model as KhuVucModel;
            var chonTatCa = LocKhuVucVM.NonApplyFilterItem.Model as KhuVucModel;

            // lựa chọn "Chọn tất cả" đã được chọn
            if (chonTatCa != null && castLoaiKhuVuc.MaKhuVuc == chonTatCa.MaKhuVuc)
                return true;

            return khachHang.Model.MaKhuVuc.Equals(castLoaiKhuVuc.MaKhuVuc);
        }

        #endregion

        #region Event handler

        private void TimKiemKhachHangVM_TextChanged(object sender, TextValueChangedEventArgs e)
        {
            if (e.NewValue == null || e.NewValue.Equals(e.OldValue))
                return;
            
            DanhSachKhachHangVM.Filter();
        }

        private void LocKhuVucVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                DanhSachKhachHangVM.Filter();
            }
        }

        #endregion

        #region Command execution
        private void OnXoaKhachHangCommandExecute(IWindow window)
        {
            DialogWindowViewModel notifyDeleteWnd = new DialogWindowViewModel()
            {
                DialogType = DialogWindowType.YesNo,
                YesText = "Có",
                NoText = "Không",
                MessageText = "Bạn muốn xóa khách hàng này?"
            };

            notifyDeleteWnd.ButtonPressed += NotifyDeleteWnd_ButtonPressed;
            window.DataContext = notifyDeleteWnd;


            if (window.ShowDialog() == true)
            {
                var deleteKhachHangItem = DanhSachKhachHangVM.SelectedItem as ItemViewModel<KhachHangModel>;
                if (deleteKhachHangItem == null)
                    return;
                DataAccess.RemoveKhachHang(deleteKhachHangItem.Model);
                Reload();
            }

            // local function for event handler
            void NotifyDeleteWnd_ButtonPressed(object sender, DialogButtonPressedEventArgs e)
            {
                if (e.DialogResult == DialogResult.Yes)
                    window.DialogResult = true;
                if (e.DialogResult == DialogResult.No)
                    window.DialogResult = false;

                window.Close();
            }
        }

        private void OnThemKhachHangCommandExecute(IWindowExtension window)
        {
            var themKhachHangWnd = new AddingWindowViewModel<KhachHangModel>();
            themKhachHangWnd.AdditionData.Add(DataAccess.LoadKhuVuc());

            window.DataContext = themKhachHangWnd;

            window.Closing += (sender, e) => e.Cancel = true;

            if (window.ShowDialog(-500, 0) == true)
            {
                DanhSachKhachHangVM.RefreshItemsSource(DataAccess.LoadKhachHang());
            }
        }

        private void OnChinhSuaThongTinCommandExecute(IWindowExtension window)
        {
            EditWindowViewModel<KhachHangModel> chinhSuaThongTin
                = new EditWindowViewModel<KhachHangModel>(KhachHangDuocChon.Model);

            chinhSuaThongTin.AdditionData.Add(DataAccess.LoadKhuVuc());

            window.DataContext = chinhSuaThongTin;

            window.Closing += (sender, e) => e.Cancel = true;

            if (window.ShowDialog(-400, -600) == true)
            {
                DanhSachKhachHangVM.RefreshItemsSource(DataAccess.LoadKhachHang());
                DanhSachKhachHangVM.Reload();
            }
        }

        protected override void LoadComponentsInternal()
        {
            SetUpDanhSachKhachHangVM();
            SetUpTimKiemKhachHangVM();
            SetUpLocKhuVucVM();
        }

        protected override void ReloadComponentsInternal()
        {
            dsKhachHang = DataAccess.LoadKhachHang();
            dsSanPham = DataAccess.LoadSanPham();

            DanhSachKhachHangVM.Reload();
            DanhSachKhachHangVM.RefreshItemsSource(dsKhachHang);
            LocKhuVucVM.Reload();
            TimKiemKhachHangVM.Reload();
        }


        #endregion
    }
}
