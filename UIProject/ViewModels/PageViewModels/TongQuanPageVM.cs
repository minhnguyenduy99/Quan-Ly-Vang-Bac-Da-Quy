using UIProject.ViewModels.LayoutViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.PageViewModels
{
    /// <summary>
    /// View Model của trang Tổng quan
    /// </summary>
    public class TongQuanPageVM : BasePageViewModel
    {

        #region Private Fields
        private List<PhieuBanModel> dsPhieuBan;
        private List<KhachHangModel> dsKhachHang;
        private long doanhThu;
        private ICommand getMoreInfoCommand;
        private ICommand logoutCommand;

        private ICommand navigateNhapHangPageCmd;
        private ICommand navigateNhaCungCapPageCmd;
        private ICommand navigateDichVuPageCmd;
        private ICommand navigateBanHangPageCmd;
        #endregion

        public int SoLuongHoaDon
        {
            get => this.dsPhieuBan.Count;
        }

        public int SoLuongKhachHangMoi
        {
            get => this.dsKhachHang.Count;
        }

        public long DoanhThu
        {
            get => this.doanhThu;
        }


        /// <summary>
        /// Command hiển thị thông tin nhân viên
        /// </summary>
        public ICommand GetMoreInfoCommand
        {
            get => getMoreInfoCommand ?? (getMoreInfoCommand = new BaseCommand(OpenInfoDialog));
        }

        public ICommand LogOutCommand
        {
            get => logoutCommand ?? (logoutCommand = new BaseCommand(OnLogOutCommandExecute));
        }

        public ICommand NavigateNhapHangPageCommand
        {
            get => navigateNhapHangPageCmd ?? new BaseCommand(OnNavigateNhapHangPageCommandExecute);
            set => navigateNhapHangPageCmd = value;
        }

        public ICommand NavigateNhaCungCapPageCommand
        {
            get => navigateNhaCungCapPageCmd ?? new BaseCommand(OnNavigateNhaCungCapPageCommandExecute);
            set => navigateNhaCungCapPageCmd = value;
        }



        public ICommand NavigateDichVuPageCommand
        {
            get => navigateDichVuPageCmd ?? new BaseCommand(OnNavigateDichVuPageCommandExecute);
            set => navigateDichVuPageCmd = value;
        }


        public ICommand NavigateBanHangPageCommand
        {
            get => navigateBanHangPageCmd ?? new BaseCommand(OnNavigateBanPageCommandExecute);
            set => navigateBanHangPageCmd = value;
        }


        public TongQuanPageVM() : base() { }
        public TongQuanPageVM(INavigator navigator) : base(navigator) { }



        private void OpenInfoDialog()
        {

        }

        protected void OnLogOutCommandExecute()
        {

        }


        protected virtual void OnNavigateNhapHangPageCommandExecute()
        {
            this.Navigator.Navigate("Nhập hàng");
        }


        protected virtual void OnNavigateBanPageCommandExecute()
        {
            this.Navigator.Navigate("Bán hàng");
        }

        protected virtual void OnNavigateDichVuPageCommandExecute()
        {
            this.Navigator.Navigate("Tạo phiếu dịch vụ");
        }

        protected virtual void OnNavigateNhaCungCapPageCommandExecute()
        {
            this.Navigator.Navigate("Nhà cung cấp");
        }

        protected override void LoadComponentsInternal()
        {
            // Load all models needed for page
            dsPhieuBan = DataAccess.LoadPhieuBan();
            dsKhachHang = DataAccess.LoadKhachHang();
        }

        protected override void ReloadComponentsInternal()
        {
            
        }
    }
}
