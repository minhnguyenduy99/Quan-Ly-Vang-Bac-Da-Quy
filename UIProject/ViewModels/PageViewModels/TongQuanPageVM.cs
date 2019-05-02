using UIProject.ViewModels.LayoutViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseMVVM_Service.BaseMVVM;
using ModelProject;


namespace UIProject.ViewModels.PageViewModels
{
    /// <summary>
    /// View Model của trang Tổng quan
    /// </summary>
    public class TongQuanPageVM : BasePageViewModel
    {

        #region Models
        private List<PhieuBanModel> dsPhieuBan;
        private List<KhachHangModel> dsKhachHang;
        private long doanhThu;
        #endregion

        private ICommand getMoreInfoCommand;
        private ICommand logoutCommand;
       
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

        public TongQuanPageVM() : base() { }

        private void OnLogOut()
        {
            
        }


        private void OpenInfoDialog()
        {

        }

        protected override void LoadPageComponents()
        {
            // Load all models needed for page
            dsPhieuBan = DataAccess.LoadPhieuBan();
            dsKhachHang = DataAccess.LoadKhachHang();
        }
    }
}
