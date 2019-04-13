using ModelProject.DataViewModels;
using UIProject.ViewModels.LayoutViewModels;
using UIProject.ViewModels.PageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using static UIProject.ViewModels.TabViewModel;
using ModelProject.Models;

namespace UIProject.ViewModels
{
    /// <summary>
    /// View model for home page window
    /// </summary>
    public class HomePageWindowViewModel : BaseWindowViewModel
    {
        #region List tab names of the HomePage window
        public static readonly List<string> ListTabNames = new List<string>()
        {
            "Tổng quan",
            "Bán hàng",
            "Đơn hàng",
            "Khách hàng",
            "Sản phẩm",
            "Dịch vụ",
            "Báo cáo",
            "Cấu hình"
        };
        #endregion

        #region Private Fields
        private TabViewModel currentTabVM;
        private BasePageViewModel currentPageVM;
        private NhanVienVM nhanVienVM;
        #endregion

        /// <summary>
        /// Set of tabs represents the multi-tab layout of Homepage Window
        /// </summary>
        public Dictionary<string, BasePageViewModel> TabPageVM { get; set; }
        
        /// <summary>
        /// List of tab view models represents the menu tab on window
        /// </summary>
        public List<TabViewModel> ListTabs { get; private set; }

        /// <summary>
        /// Current page displayed on the window
        /// </summary>
        public TabViewModel CurrentTabVM
        {
            get => currentTabVM;
            set => SetProperty(ref currentTabVM, value);
        }

        /// <summary>
        /// The current page displayed on the Main Window
        /// </summary>
        public BasePageViewModel CurrentPageVM
        {
            get => currentPageVM;
            set => SetProperty(ref currentPageVM, value);
        }

        public HomePageWindowViewModel() : this(new NhanVienVM(new NhanVienModel()
        {
            FullName = "Nguyen Duy Minh",
            DateOfBirth = DateTime.Now,
            CMND = "225818043"
        })) { }

        public HomePageWindowViewModel(NhanVienVM nhanVienVM)
        {
            if (nhanVienVM == null)
                throw new ArgumentNullException("Account view model cannot be null");
            this.nhanVienVM = nhanVienVM;

            SetUpWindowLayout();

            InitializeTabs();

            ListTabs[0].IsChecked = true;
        }

        private void SetUpWindowLayout()
        {
            CanMaximized = true;
            CanMinimized = true;
            WindowState = WindowState.Maximized;
            IconSource = (string)Application.Current.FindResource("SoftwareIcon");
            BackgroundSource = (string)Application.Current.FindResource("LoginBackground");
        }
        

        private void OnCurrentTabChanged(object sender, TabSelectedEventArgs e)
        {
            UpdateCurrentPageWithSelectedTab(e.TabName);
        }

        private void UpdateCurrentPageWithSelectedTab(string tabName)
        {
            if (TabPageVM.ContainsKey(tabName))
            {
                CurrentTabVM = ListTabs.Where(tab => tab.TabName == tabName).ElementAt(0);
                CurrentPageVM = TabPageVM[tabName];
            }
        }

        /// <summary>
        /// Initialize view models of tabs
        /// </summary>
        protected virtual void InitializeTabs()
        {
            this.ListTabs = new List<TabViewModel>()
            {
                CreateTabViewModel(ListTabNames[0], "Tab_TongQuan", TabState.New),
                CreateTabViewModel(ListTabNames[1], "SoftwareIcon", TabState.New),
                CreateTabViewModel(ListTabNames[2], "SoftwareIcon", TabState.New),
                CreateTabViewModel(ListTabNames[3], "SoftwareIcon", TabState.New),
                CreateTabViewModel(ListTabNames[4], "SoftwareIcon", TabState.New),
                CreateTabViewModel(ListTabNames[5], "SoftwareIcon", TabState.New),
                CreateTabViewModel(ListTabNames[6], "SoftwareIcon", TabState.New),
                CreateTabViewModel(ListTabNames[7], "SoftwareIcon", TabState.New),
            };


            TabPageVM = new Dictionary<string, BasePageViewModel>();

            TabPageVM.Add(ListTabs[0].TabName, new TongQuanPageVM());
            TabPageVM.Add(ListTabs[1].TabName, null);
            TabPageVM.Add(ListTabs[2].TabName, null);
            TabPageVM.Add(ListTabs[3].TabName, null);
            TabPageVM.Add(ListTabs[4].TabName, null);
            TabPageVM.Add(ListTabs[5].TabName, null);
            TabPageVM.Add(ListTabs[6].TabName, null);
            TabPageVM.Add(ListTabs[7].TabName, null);


            //  Subcribe the FocusTabChanged event of tab by hooking the OnCurrentTabChanged method 
            foreach (var tab in ListTabs)
            {
                tab.TabSelected += OnCurrentTabChanged;
            }
        }
    }
}
