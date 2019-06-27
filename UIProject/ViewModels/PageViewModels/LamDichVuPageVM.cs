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
using UIProject.ViewModels.DataViewModels;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class LamDichVuPageVM : BasePageViewModel
    {
        #region Resources
        private IEnumerable<KhachHangModel> dsKhachHang;
        private IEnumerable<LoaiDichVuModel> dsDichVu;
        #endregion

        #region Private Fields
        private ICommand themKhachHangCmd;
        private ICommand xemDSDichVuCmd;
        private ICommand themDichVuCmd;
        private ICommand submitPhieuDichVuCmd;
        private ICommand quayLaiTrangTongQuanCmd;
        #endregion

        #region Commands
        public ICommand ThemKhachHangCommand
        {
            get => themKhachHangCmd ?? new BaseCommand<IWindowExtension>(OnThemKhachHangCommandExecute);
            set => themKhachHangCmd = value;
        }     
        public ICommand XemDanhSachDichVuCommand
        {
            get => xemDSDichVuCmd ?? new BaseCommand<IWindow>(OnXemDanhSachDichVuCommandExecute);
            set => xemDSDichVuCmd = value;
        }
        public ICommand ThemDichVuCommand
        {
            get => themDichVuCmd ?? new BaseCommand<IWindowExtension>(OnThemDichVuCommandExecute);
            set => themDichVuCmd = value;
        }
        public ICommand SubmitPhieuDichVuCommand
        {
            get => submitPhieuDichVuCmd ?? new BaseCommand<IWindow>(OnSubmitPhieuDichVuCommandExecute);
            set => submitPhieuDichVuCmd = value;
        }
        public ICommand QuayLaiTrangTongQuanCommand
        {
            get => quayLaiTrangTongQuanCmd ?? new BaseCommand(OnQuayLaiTrangTongQuanCommandExecute);
            set => quayLaiTrangTongQuanCmd = value;
        }



        #endregion

        public SearchTextBoxViewModel<KhachHangModel> TimKiemKhachHangVM { get; private set; }
        public SearchTextBoxViewModel<LoaiDichVuModel> TimKiemDichVuVM { get; private set; }

        public PhieuDichVuViewModel PhieuDichVuVM { get; private set; }

        public LamDichVuPageVM() : base()
        {
            TakeFullScreen = true;
        }
        public LamDichVuPageVM(INavigator navigator) : base(navigator)
        {
            TakeFullScreen = true;
        }
        

        private void SetUpPhieuDichVuVM()
        {
            PhieuDichVuVM = new PhieuDichVuViewModel();
        }

        private void SetUpTimKiemKhachHangVM()
        {
            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>(dsKhachHang);
            TimKiemKhachHangVM.SelectedValuePath = "TenKH";
            TimKiemKhachHangVM.Filters.Add(LocKhachHangTheoTen);
            TimKiemKhachHangVM.SelectedItemChanged += TimKiemKhachHangVM_SelectedItemChanged;
            // local function
            bool LocKhachHangTheoTen(ItemViewModel<KhachHangModel> khachHangItem)
            {
                if (khachHangItem == null || khachHangItem.Model == null)
                    return true;
                return khachHangItem.Model.TenKH.ToLower().StartsWith(TimKiemKhachHangVM.Text.ToLower());
            }


            // local function
            void TimKiemKhachHangVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var khachHangDaChonItem = e.SelectedItem as ItemViewModel<KhachHangModel>;
                if (khachHangDaChonItem == null || khachHangDaChonItem.Model == null)
                    return;

                KhachHangModel khachHang = khachHangDaChonItem.Model;
                CapNhatKhachHangDaChon(khachHang);
            }

            // Cập nhật thông tin liên hệ của khách hàng đã chọn vào phiếu dịch vụ
            void CapNhatKhachHangDaChon(KhachHangModel khachHang)
            {
                PhieuDichVuVM.PhieuDichVu.MaKH = khachHang.MaKH;
                PhieuDichVuVM.SoDienThoai = khachHang.SDT;
                PhieuDichVuVM.DiaChi = $"{khachHang.DiaChi}, {khachHang.TenKhuVuc}";
            }
        }



        private void SetUpTimKiemDichVuVM()
        {
            TimKiemDichVuVM = new SearchTextBoxViewModel<LoaiDichVuModel>(dsDichVu);
            TimKiemDichVuVM.SelectedValuePath = "TenLoaiDV";
            TimKiemDichVuVM.Filters.Add(LocDichVuTheoTen);
            TimKiemDichVuVM.SelectedItemChanged += TimKiemDichVuVM_SelectedItemChangedHandler;

            // local function
            bool LocDichVuTheoTen(ItemViewModel<LoaiDichVuModel> dichVuItem)
            {
                if (dichVuItem == null || dichVuItem.Model == null)
                    return true;

                return dichVuItem.Model.TenLoaiDV.ToLower().Contains(TimKiemDichVuVM.Text.ToLower());
            }

            void TimKiemDichVuVM_SelectedItemChangedHandler(object sender, SelectedItemChangedEventArgs e)
            {
                var dichVuDaChon = e.SelectedItem as ItemViewModel<LoaiDichVuModel>;
                if (dichVuDaChon == null || dichVuDaChon.Model == null)
                    return;

                ChiTietDichVuModel chiTietDichVu = new ChiTietDichVuModel(PhieuDichVuVM.PhieuDichVu, dichVuDaChon.Model);
                PhieuDichVuVM.AddChiTietDichVu(chiTietDichVu);
            }
        }




        #region Command execution
        private void OnSubmitPhieuDichVuCommandExecute(IWindow window)
        {
            if (window.ShowDialog() == true)
            {
                PhieuDichVuVM.Submit();
                Reload();
            }
        }

        private void OnQuayLaiTrangTongQuanCommandExecute()
        {
            this.Navigator.Navigate("Tổng quan");
        }

        private void OnThemDichVuCommandExecute(IWindowExtension window)
        {
            AddingWindowViewModel<LoaiDichVuModel> themDichVuVM = new AddingWindowViewModel<LoaiDichVuModel>();

            // Trigger event for manual handler
            window.Closing += (sender, e) => e.Cancel = true;
            window.DataContext = themDichVuVM;

            // thêm dịch vụ thành công
            if (window.ShowDialog() == true)
            {
                // update lại danh sách dịch vụ
                RefreshResource();

                TimKiemDichVuVM.RefreshItemSource(dsDichVu);
                TimKiemDichVuVM.Reload();
            }
        }
        private void OnXemDanhSachDichVuCommandExecute(IWindow window)
        {
            ItemCollectionViewModel<LoaiDichVuModel> DanhSachDichVuVM = new ItemCollectionViewModel<LoaiDichVuModel>(dsDichVu);
            window.DataContext = DanhSachDichVuVM;
            window.ShowDialog();
        }

        private void OnThemKhachHangCommandExecute(IWindowExtension window)
        {
            AddingWindowViewModel<KhachHangModel> themKhachHangVM = new AddingWindowViewModel<KhachHangModel>();
            themKhachHangVM.AdditionData.Add(DataAccess.LoadKhuVuc());

            // Trigger the closing for manual handler
            window.Closing += (sender, e) => e.Cancel = true;

            window.DataContext = themKhachHangVM;
            if (window.ShowDialog(100,-100) == true)
            {
                // Load lại danh sách khách hàng sau khi thêm khách hàng mới thành công
                dsKhachHang = DataAccess.LoadKhachHang();
                TimKiemKhachHangVM.RefreshItemSource(dsKhachHang);
                TimKiemKhachHangVM.Reload();
            }
        }
        #endregion


        private void RefreshResource()
        {
            dsKhachHang = DataAccess.LoadKhachHang();
            dsDichVu = DataAccess.LoadLoaiDichVu();
        }

        protected override void LoadComponentsInternal()
        {
            RefreshResource();
            SetUpPhieuDichVuVM();
            SetUpTimKiemKhachHangVM();
            SetUpTimKiemDichVuVM();
        }

        protected override void ReloadComponentsInternal()
        {
            RefreshResource();

            TimKiemDichVuVM.RefreshItemSource(dsDichVu);
            TimKiemDichVuVM.Reload();

            TimKiemKhachHangVM.RefreshItemSource(dsKhachHang);
            TimKiemKhachHangVM.Reload();

            PhieuDichVuVM.Reload();
        }
    }
}
