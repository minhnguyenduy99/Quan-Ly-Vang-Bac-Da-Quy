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
            get => new BaseCommand<IWindowExtension>(OnChinhSuaThongTinCommandExecute);
        }

        /// <summary>
        /// Command cho việc xóa khách hàng
        /// </summary>
        public ICommand XoaKhachHangCommand
        {
            get => new BaseCommand<IWindowExtension>(OnXoaKhachHangCommandExecute);
        }


        public KhachHangPageVM() : base() { }
        public KhachHangPageVM(INavigator navigator) : base(navigator) { }


        protected override void LoadPageComponents()
        {
            SetUpDanhSachKhachHangVM();
            SetUpTimKiemKhachHangVM();
            SetUpLocKhuVucVM();
        }


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
                new List<Func<ItemViewModel<KhachHangModel>, bool>>()
                {
                    LocKhachHangTheoKhuVuc
                },
                DataAccess.LoadKhuVuc());

            // Thêm 1 lựa chọn tất cả vào bộ lọc
            LocKhuVucVM.NonApplyFilterItem.Model = new KhuVucModel() { MaKhuVuc = "-1", TenKhuVuc = "Chọn tất cả" };

            (DanhSachKhachHangVM.Filters as List<Func<ItemViewModel<KhachHangModel>, bool>>)
                .Add(LocKhuVucVM.FilterCallBacks[0]);

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
        private void OnXoaKhachHangCommandExecute(IWindowExtension window)
        {
            if (window.ShowDialog() == true)
            {
                var deleteKhachHangItem = DanhSachKhachHangVM.SelectedItem as ItemViewModel<KhachHangModel>;
                if (deleteKhachHangItem == null)
                    return;
                DataAccess.RemoveKhachHang(deleteKhachHangItem.Model);
                DanhSachKhachHangVM.RefreshItemsSource(DataAccess.LoadKhachHang());
                DanhSachKhachHangVM.SelectedItem = null;
            }
        }
        private void OnThemKhachHangCommandExecute(IWindowExtension window)
        {
            if (window.ShowDialog(-500, 0) == true)
            {
                DanhSachKhachHangVM.RefreshItemsSource(DataAccess.LoadKhachHang());
            }
        }

        private void OnChinhSuaThongTinCommandExecute(IWindowExtension window)
        {
            EditWindowViewModel<KhachHangModel> chinhSuaThongTin
                = new EditWindowViewModel<KhachHangModel>(KhachHangDuocChon.Model);

            if (window.ShowDialog(-400, -700) == true)
            {
                DanhSachKhachHangVM.RefreshItemsSource(DataAccess.LoadKhachHang());
                DanhSachKhachHangVM.SelectedItem = null;
            }
        }


        #endregion
    }
}
