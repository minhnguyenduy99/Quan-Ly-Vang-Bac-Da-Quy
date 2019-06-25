using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UIProject.Events;
using UIProject.UIConnector;
using UIProject.ViewModels.DataViewModels;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class DichVuPageVM : BasePageViewModel
    {
        private IEnumerable<PhieuDichVuModel> dsPhieuDichVu;
        private IEnumerable<TinhTrangModel> dsTinhTrang;
        private IEnumerable<ChiTietDichVuModel> dsChiTietDichVu;
        private DateTime? locNgayLap;

        private ICommand xoaPhieuDichVuCmd;
        private ICommand chinhSuaPhieuDichVuCmd;
        private ICommand navigateTaoPhieuDichVuPageCmd;
        private ICommand themDichVuCmd;
        private ICommand navigateTongQuanPageCmd;

        public ICommand XoaPhieuDichVuCommand
        {
            get => xoaPhieuDichVuCmd ?? new BaseCommand<IWindow>(OnXoaPhieuDichVuCommand);
            set => xoaPhieuDichVuCmd = value;
        }
        public ICommand ChinhSuaPhieuDichVuCommand
        {
            get => chinhSuaPhieuDichVuCmd ?? new BaseCommand<IWindowExtension>(OnChinhSuaPhieuDichVuCommand);
            set => chinhSuaPhieuDichVuCmd = value;
        }
        public ICommand NavigateTaoPhieuDichVuPageCommand
        {
            get => navigateTaoPhieuDichVuPageCmd ?? new BaseCommand(OnNavigateTaoPhieuDichVuPageCommandExecute);
            set => navigateTaoPhieuDichVuPageCmd = value;
        }
        public ICommand NavigateTongQuanPageCommand
        {
            get => navigateTongQuanPageCmd ?? new BaseCommand(OnNavigateTongQuanPageCommandExecute);
            set => navigateTongQuanPageCmd = value;
        }
        public ICommand HienThiTatCaCommand
        {
            get => new BaseCommand(() => LocNgayLap = null);
        }

        public SearchTextBoxViewModel<PhieuDichVuModel> TimKiemPhieuDichVuVM { get; set; }
        public EnumFilterViewModel<PhieuDichVuModel> LocTinhTrangPhieuDichVuVM { get; set; }
        public ObservableCollectionViewModel<PhieuDichVuModel> DanhSachPhieuDichVuVM { get; set; }
        public ObservableCollectionViewModel<ChiTietDichVuModel> DanhSachChiTietDichVuVM { get; set; }
        public DateTime? LocNgayLap
        {
            get => locNgayLap;
            set
            {
                SetProperty(ref locNgayLap, value);
                if (DanhSachPhieuDichVuVM != null)
                {
                    DanhSachPhieuDichVuVM.Filter();
                }
            }
        }



        public DichVuPageVM() : base()
        {
            TakeFullScreen = true;
        }
        public DichVuPageVM(INavigator navigator) : base(navigator)
        {
            TakeFullScreen = true;
        }


        private void SetUpDanhSachPhieuDichVuVM()
        {
            DanhSachPhieuDichVuVM = new ObservableCollectionViewModel<PhieuDichVuModel>(dsPhieuDichVu);
            DanhSachPhieuDichVuVM.Filters.Add(LocTinhTrangPhieuDichVuVM.FilterCallBack);
            DanhSachPhieuDichVuVM.Filters.Add(TimKiemTheoMaDichVu);
            DanhSachPhieuDichVuVM.Filters.Add(LocTheoThoiGianLapPhieu);
            DanhSachPhieuDichVuVM.SelectedItemChanged += DanhSachPhieuDichVuVM_SelectedItemChanged;


            // local function
            bool TimKiemTheoMaDichVu(ItemViewModel<PhieuDichVuModel> phieuDVItem)
            {
                if (phieuDVItem == null || phieuDVItem.Model == null)
                    return true;
                return phieuDVItem.Model.MaPhieu.ToString().Contains(TimKiemPhieuDichVuVM.Text);
            }
            bool LocTheoThoiGianLapPhieu(ItemViewModel<PhieuDichVuModel> phieuDVItem)
            {
                if (phieuDVItem == null || phieuDVItem.Model == null)
                    return true;
                if (LocNgayLap == null)
                    return true;
                var ngayLapPhieuDV = phieuDVItem.Model.NgayLapDateTime;
                return ngayLapPhieuDV.Date == LocNgayLap?.Date;
            }
            void DanhSachPhieuDichVuVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var phieuDVDaChon = e.SelectedItem as ItemViewModel<PhieuDichVuModel>;
                DanhSachChiTietDichVuVM.Clear();

                if (phieuDVDaChon == null || phieuDVDaChon.Model == null)
                    return;

                var dsChiTiet = dsChiTietDichVu.Where(chiTiet => chiTiet.MaPhieu == phieuDVDaChon.Model.MaPhieu);
                DanhSachChiTietDichVuVM.RefreshItemsSource(dsChiTiet);
            }
        }
        private void SetUpDanhSachChiTietDichVuVM()
        {
            DanhSachChiTietDichVuVM = new ObservableCollectionViewModel<ChiTietDichVuModel>();
        }
        private void SetUpTimKiemPhieuDichVuVM()
        {
            TimKiemPhieuDichVuVM = new SearchTextBoxViewModel<PhieuDichVuModel>(dsPhieuDichVu);
            TimKiemPhieuDichVuVM.TextChanged += TimKiemPhieuDichVuVM_TextChanged;

            // local function
            void TimKiemPhieuDichVuVM_TextChanged(object sender, TextValueChangedEventArgs e)
            {
                DanhSachPhieuDichVuVM?.Filter();
            }
        }



        private void SetUpLocTinhTrangPhieuDichVuVM()
        {
            LocTinhTrangPhieuDichVuVM = new EnumFilterViewModel<PhieuDichVuModel>(
                LocTinhTrangPhieuDichVu,
                dsTinhTrang);

            // Lựa chọn "Chọn tất cả"
            LocTinhTrangPhieuDichVuVM.NonApplyFilterItem.Model
                = new TinhTrangModel() { MaTinhTrang = null, TenTinhTrang = "Chọn tất cả" };

            LocTinhTrangPhieuDichVuVM.SelectedItemChanged += LocTinhTrangPhieuDichVuVM_SelectedItemChanged;

            // local function
            void LocTinhTrangPhieuDichVuVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                if (e.SelectedItem == null)
                    return;



                // Gọi tới hàm filter mặc định để lọc lại
                this.DanhSachPhieuDichVuVM.Filter();
            }
            bool LocTinhTrangPhieuDichVu(ItemViewModel<PhieuDichVuModel> phieuDichVuItem)
            {
                var phieuDV = phieuDichVuItem.Model as PhieuDichVuModel;
                var tinhTrangDaChonItem = LocTinhTrangPhieuDichVuVM.Collection.SelectedItem as ItemViewModel<object>;

                if (phieuDV == null)
                    return true;
                if (tinhTrangDaChonItem == null)
                    return true;

                var tinhTrangDaChon = tinhTrangDaChonItem.Model as TinhTrangModel;
                var nonApplyItem = LocTinhTrangPhieuDichVuVM.NonApplyFilterItem?.Model as TinhTrangModel;

                // trong trường hợp người dùng lựa chọn "Chọn tất cả"
                if (nonApplyItem != null && nonApplyItem.MaTinhTrang == tinhTrangDaChon.MaTinhTrang)
                {
                    return true;
                }



                return phieuDV.TinhTrang.Equals(tinhTrangDaChon?.MaTinhTrang);
            }
        }

        private void OnXoaPhieuDichVuCommand(IWindow dialogWindow)
        {
            // Chấp nhận xóa 
            if (dialogWindow.ShowDialog() == true)
            {
                this.DanhSachPhieuDichVuVM.RemoveCurrentItem();

                // Remove chi tiết dịch vụ


                // Load lại DanhSachPhieuDichVu
                this.DanhSachPhieuDichVuVM.RefreshItemsSource(DataAccess.LoadPhieuDichVu());
            }
        }

        private void OnNavigateTaoPhieuDichVuPageCommandExecute()
        {
            this.Navigator.Navigate("Tạo phiếu dịch vụ");
        }
        private void OnChinhSuaPhieuDichVuCommand(IWindowExtension window)
        {
            var phieuDichVuDaChon = (DanhSachPhieuDichVuVM.SelectedItem as ItemViewModel<PhieuDichVuModel>)?.Model;
            EditWindowViewModel<PhieuDichVuModel> editPhieuDVVM
                = new EditWindowViewModel<PhieuDichVuModel>(phieuDichVuDaChon);

            editPhieuDVVM.AdditionData.Add(DataAccess.LoadTinhTrang());

            window.DataContext = editPhieuDVVM;
            window.Closing += (sender, e) => e.Cancel = true;
            
            if (window.ShowDialog(-500, 100) == true)
            {
                this.DanhSachPhieuDichVuVM.RefreshItemsSource(DataAccess.LoadPhieuDichVu());

            }
        }
        private void OnNavigateTongQuanPageCommandExecute()
        {
            this.Navigator.Navigate("Tổng quan");
        }
        private void RefreshResource()
        {
            dsPhieuDichVu = DataAccess.LoadPhieuDichVu();
            dsTinhTrang = DataAccess.LoadTinhTrang();
            dsChiTietDichVu = DataAccess.LoadChiTietDichVu();
        }



        protected override void LoadComponentsInternal()
        {
            RefreshResource();

            SetUpTimKiemPhieuDichVuVM();
            SetUpLocTinhTrangPhieuDichVuVM();
            SetUpDanhSachPhieuDichVuVM();
            SetUpDanhSachChiTietDichVuVM();
            LocNgayLap = null;
        }

        protected override void ReloadComponentsInternal()
        {
            RefreshResource();

            TimKiemPhieuDichVuVM.Reload();

            DanhSachPhieuDichVuVM.RefreshItemsSource(dsPhieuDichVu);
            DanhSachPhieuDichVuVM.Reload();

            DanhSachChiTietDichVuVM.Reload();
        }
    }
}
