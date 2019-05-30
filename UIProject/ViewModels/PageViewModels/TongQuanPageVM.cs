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
            get => logoutCommand ?? (logoutCommand = new BaseCommand(OnLogOut));
        }

        public ICommand NavigateNhapHangPageCommand
        {
            get => navigateNhapHangPageCmd ?? new BaseCommand(OnNavigateNhapHangPageCommandExecute);
            set => navigateNhapHangPageCmd = value;
        }

        public TongQuanPageVM() : base() { }
        public TongQuanPageVM(INavigator navigator) : base(navigator) { }

        private void OnLogOut()
        {
            
        }


        private void OpenInfoDialog()
        {

        }

        protected virtual void OnNavigateNhapHangPageCommandExecute()
        {
            this.Navigator.Navigate("Nhập hàng");
        }

        protected override void LoadPageComponents()
        {
            // Load all models needed for page
            dsPhieuBan = DataAccess.LoadPhieuBan();
            dsKhachHang = DataAccess.LoadKhachHang();
        }
    }
}
