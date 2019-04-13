using ModelProject.Models;
using ModelProject.DataViewModels;
using UIProject.ViewModels.LayoutViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseMVVM_Service.BaseMVVM;

namespace UIProject.ViewModels.PageViewModels
{
    /// <summary>
    /// View Model của trang Tổng quan
    /// </summary>
    public class TongQuanPageVM : BasePageViewModel
    {

        private NhanVienVM nhanVienVM;

        private ICommand getMoreInfoCommand;
        private ICommand logoutCommand;
        
        /// <summary>
        /// Tên nhân viên hiển thị trên trang Tổng quan
        /// </summary>
        public string StaffName
        {
            get => nhanVienVM.ModelData.FullName;
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

        private void OnLogOut()
        {
            
        }


        private void OpenInfoDialog()
        {
            Views.InfoDialogWindow infoDialogWnd = new Views.InfoDialogWindow()
            {
                DataContext = new BaseWindowViewModel<NhanVienModel>(nhanVienVM.ModelData)
                {
                    CanMinimized = false,
                    CanMaximized = false,
                    NavigationBarVisibility = System.Windows.Visibility.Collapsed,
                    WindowState = System.Windows.WindowState.Normal
                }
            };
            infoDialogWnd.ShowDialog();
        }

        public TongQuanPageVM() { }


        #region Internal methods for Data query
        private void QueryGeneralInformation()
        {
            throw new NotImplementedException();
        }

        private void QueryStaffInformation()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
