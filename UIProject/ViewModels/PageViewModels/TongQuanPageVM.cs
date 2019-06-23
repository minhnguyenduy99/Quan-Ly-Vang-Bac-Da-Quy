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
using UIProject.ViewModels.DataViewModels;
using System.Diagnostics;

namespace UIProject.ViewModels.PageViewModels
{
    /// <summary>
    /// View Model của trang Tổng quan
    /// </summary>
    public class TongQuanPageVM : BasePageViewModel
    {

        #region Private Fields
        private const string TimHieuThemURL = "https://github.com/minhnguyenduy99/Quan-Ly-Vang-Bac-Da-Quy";
        private IEnumerable<PhieuBanModel> dsPhieuBan;
        private IEnumerable<KhachHangModel> dsKhachHang;

        private ICommand navigateNhapHangPageCmd;
        private ICommand navigateNhaCungCapPageCmd;
        private ICommand navigateDichVuPageCmd;
        private ICommand navigateBanHangPageCmd;
        private ICommand navigateTimHieuThemCmd;
        #endregion

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

        public ICommand NavigateTimHieuThemCommand
        {
            get => navigateTimHieuThemCmd ?? new BaseCommand(OnNavigateTimHieuThemCommandExecute);
            set => navigateTimHieuThemCmd = value;
        }

        public ThongTinTongQuanViewModel TongQuanVM { get; private set; }
        public TongQuanPageVM() : base() { }
        public TongQuanPageVM(INavigator navigator) : base(navigator) { }


        protected virtual void OnNavigateNhapHangPageCommandExecute()
        {
            this.Navigator.Navigate("Nhập hàng");
        }

        protected virtual void OnNavigateTimHieuThemCommandExecute()
        {
            Process.Start("chrome.exe", TimHieuThemURL);
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
            TongQuanVM = new ThongTinTongQuanViewModel();
        }

        protected override void ReloadComponentsInternal()
        {
            TongQuanVM.Load();
        }
    }
}
