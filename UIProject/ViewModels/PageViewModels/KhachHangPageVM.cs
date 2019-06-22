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
using UIProject.UIConnector;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    class KhachHangPageVM : BasePageViewModel
    {
        #region Resources from database
        IEnumerable<KhachHangModel> dsKhachHang;
        IEnumerable<KhuVucModel> dsKhuVuc;
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
        /// View model của việc tìm kiếm khách hàng
        /// </summary>
        public TextBasedSearchViewModel<KhachHangModel> TimKiemKhachHangVM { get; set; }

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
            DanhSachKhachHangVM = new ObservableCollectionViewModel<KhachHangModel>(dsKhachHang);

            DanhSachKhachHangVM.Filters.Add(LocKhachHangTheoTen);
        }

        private void SetUpTimKiemKhachHangVM()
        {
            TimKiemKhachHangVM = new TextBasedSearchViewModel<KhachHangModel>(SearchMode.LikelyIgnoreCase);
            TimKiemKhachHangVM.TextChanged += TimKiemKhachHangVM_TextChanged;
        }

        private void SetUpLocKhuVucVM()
        {
            LocKhuVucVM = new EnumFilterViewModel<KhachHangModel>(
                LocKhachHangTheoKhuVuc,
                dsKhuVuc);

            // Thêm 1 lựa chọn tất cả vào bộ lọc
            LocKhuVucVM.NonApplyFilterItem.Model = new KhuVucModel() { MaKhuVuc = null, TenKhuVuc = "Chọn tất cả" };

            DanhSachKhachHangVM.Filters.Add(LocKhuVucVM.FilterCallBack);

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
                //DataAccess.RemoveKhachHang(deleteKhachHangItem.Model);
                deleteKhachHangItem.Model.Submit(SubmitType.Delete);
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
            var khachHangDuocChon = DanhSachKhachHangVM.SelectedItem as ItemViewModel<KhachHangModel>;
            EditWindowViewModel<KhachHangModel> chinhSuaThongTin
                = new EditWindowViewModel<KhachHangModel>(khachHangDuocChon?.Model);

            chinhSuaThongTin.AdditionData.Add(dsKhuVuc);

            window.DataContext = chinhSuaThongTin;

            window.Closing += (sender, e) => e.Cancel = true;

            if (window.ShowDialog(-400, -600) == true)
            {
                Reload();
            }
        }

        private void RefreshSource()
        {
            dsKhachHang = DataAccess.LoadKhachHang();
            dsKhuVuc = DataAccess.LoadKhuVuc();
        }

        protected override void LoadComponentsInternal()
        {
            RefreshSource();

            SetUpDanhSachKhachHangVM();
            SetUpTimKiemKhachHangVM();
            SetUpLocKhuVucVM();
        }

        protected override void ReloadComponentsInternal()
        {
            RefreshSource();

            DanhSachKhachHangVM.RefreshItemsSource(dsKhachHang);
            DanhSachKhachHangVM.Reload();
            LocKhuVucVM.RefreshItemsSource(dsKhuVuc);
            LocKhuVucVM.Reload();
            TimKiemKhachHangVM.Reload();
        }


        #endregion
    }
}
