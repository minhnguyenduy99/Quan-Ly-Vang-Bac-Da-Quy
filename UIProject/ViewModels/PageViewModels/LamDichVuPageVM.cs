using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.ViewModels.FunctionInterfaces;

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
        private ICommand chinhSuaThongTinLienHeCmd;
        private ICommand xemDSDichVuCmd;
        private ICommand themDichVuCmd;
        private ICommand submitPhieuDichVuCmd;
        #endregion


        public LamDichVuPageVM() : base() { }
        public LamDichVuPageVM(INavigator navigator) : base(navigator) { }
        protected override void LoadComponentsInternal()
        {
            this.TakeFullScreen = true;
        }

        protected override void ReloadComponentsInternal()
        {
            throw new NotImplementedException();
        }
    }
}
