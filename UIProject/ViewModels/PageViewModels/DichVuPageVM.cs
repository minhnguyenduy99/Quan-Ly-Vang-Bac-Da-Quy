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
    public class DichVuPageVM : BasePageViewModel
    {
        private ICommand navigatePhieuDichVuCmd;
        private ICommand xoaPhieuDichVuCmd;
        private ICommand chinhSuaPhieuDichVuCmd;


        public ICommand NavigateTaoPhieuDichVuCommand
        {
            get => navigatePhieuDichVuCmd ?? new BaseCommand(OnNavigateTaoPhieuDichVuCommand);
            set => navigatePhieuDichVuCmd = value;
        }
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


        public SearchTextBoxViewModel<KhachHangModel> TimKiemKhachHangVM { get; set; }
        public EnumFilterViewModel<PhieuDichVuModel> LocTinhTrangPhieuDichVuVM { get; set; }
        public ObservableCollectionViewModel<PhieuDichVuModel> DanhSachPhieuDichVuVM { get; set; }
        public ObservableCollectionViewModel<ChiTietDichVuModel> DSChiTietDichVuVM { get; set; }


        public DichVuPageVM() : base() { }
        public DichVuPageVM(INavigator navigator) : base(navigator) { }

        private void SetUpDanhSachPhieuDichVuVM()
        {
            var dsPhieuDichVu = DataAccess.LoadPhieuDichVu();
            DanhSachPhieuDichVuVM = new ObservableCollectionViewModel<PhieuDichVuModel>(dsPhieuDichVu);      
        }
        private void SetUpTimKiemKhachHangVM()
        {
            var dsKhachHang = DataAccess.LoadKhachHang();
            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>(dsKhachHang);
            TimKiemKhachHangVM.SelectedValuePath = "Model.TenKH";
            TimKiemKhachHangVM.Filters.ToList().Add(TimKiemTheoTenCallBack);

            // local function
            bool TimKiemTheoTenCallBack(ItemViewModel<KhachHangModel> khachHangItem)
            {
                return (bool)khachHangItem?.Model.TenKH.ToLower().StartsWith(TimKiemKhachHangVM.Text.ToLower());
            }
        }

        private void SetUpLocTinhTrangPhieuDichVuVM()
        {
            LocTinhTrangPhieuDichVuVM = new EnumFilterViewModel<PhieuDichVuModel>(
                LocTinhTrangPhieuDichVu,
                DataAccess.LoadTinhTrang());

            // Lựa chọn "Chọn tất cả"
            LocTinhTrangPhieuDichVuVM.NonApplyFilterItem.Model
                = new TinhTrangModel() { MaTinhTrang = null, TenTinhTrang = "Chọn tất cả" };

            LocTinhTrangPhieuDichVuVM.SelectedItemChanged += UpdateDanhSachPhieuBan;
        }

        private void UpdateDanhSachPhieuBan(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            // Gọi tới hàm filter mặc định để lọc lại
            this.DanhSachPhieuDichVuVM.Filter();
        }

        private bool LocTinhTrangPhieuDichVu(ItemViewModel<PhieuDichVuModel> phieuDichVuItem)
        {
            var phieuDV = phieuDichVuItem.Model as PhieuDichVuModel;
            var tinhTrangDaChon = LocTinhTrangPhieuDichVuVM.Collection.SelectedItem as ItemViewModel<TinhTrangModel>;

            if (phieuDV == null)
                return true;
            if (tinhTrangDaChon == null)
                return true;

            var nonApplyItem = LocTinhTrangPhieuDichVuVM.NonApplyFilterItem?.Model as TinhTrangModel;

            // trong trường hợp người dùng lựa chọn "Chọn tất cả"
            if (nonApplyItem != null && nonApplyItem.MaTinhTrang.Equals(phieuDV.TinhTrang)){
                return true;
            }

            return phieuDV.TinhTrang.Equals(tinhTrangDaChon?.Model?.MaTinhTrang);
        }

        private void OnNavigateTaoPhieuDichVuCommand()
        {
            this.Navigator.Navigate("Tạo phiếu dịch vụ");
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

        protected override void LoadComponentsInternal()
        {
            SetUpDanhSachPhieuDichVuVM();
            SetUpTimKiemKhachHangVM();
            SetUpLocTinhTrangPhieuDichVuVM();
        }

        protected override void ReloadComponentsInternal()
        {
            TimKiemKhachHangVM.Reload();
            DanhSachPhieuDichVuVM.Reload();
            DSChiTietDichVuVM.Reload();
        }
    }
}
