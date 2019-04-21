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
using static UIProject.ViewModels.LayoutViewModels.ExpandTabViewModel;
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
            "Tạo đơn hàng",
            "Danh sách đơn hàng",
            "Quản lý giao hàng",
            "Khách trả hàng",
            "Khách hàng & đối tác",
            "Khách hàng",
            "Đối tác",
            "Sản phẩm",
            "Danh sách sản phẩm",
            "Quản lý kho",
            "Dịch vụ",
        };
        #endregion

        #region Private Fields
        private TabViewModel currentTabVM;
        private BasePageViewModel currentPageVM;
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

        public HomePageWindowViewModel()
        {
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
        /// Initialize default view models of tabs
        /// </summary>
        protected virtual void InitializeTabs()
        {
            //  Initialize the content present layout of tabs
            this.ListTabs = new List<TabViewModel>()
            {
                CreateTabViewModel(ListTabNames[0], "Tab_TongQuan", TabState.New),
                CreateTabViewModel(ListTabNames[1], "SoftwareIcon", TabState.New),
                CreateExpandTabViewModel(ListTabNames[2], "SoftwareIcon", TabState.New,
                    new List<BaseContentViewModel>()
                    {
                        CreateTabViewModel(ListTabNames[3], "SoftwareIcon", TabState.New),
                        CreateTabViewModel(ListTabNames[4], "SoftwareIcon", TabState.New),
                        CreateTabViewModel(ListTabNames[5], "SoftwareIcon", TabState.New),
                        CreateTabViewModel(ListTabNames[6], "SoftwareIcon", TabState.New),
                    }),
                CreateExpandTabViewModel(ListTabNames[7], "SoftwareIcon", TabState.New,
                    new List<BaseContentViewModel>()
                    {
                        CreateTabViewModel(ListTabNames[8], "SoftwareIcon", TabState.New),
                        CreateTabViewModel(ListTabNames[9], "SoftwareIcon", TabState.New),
                    }),
                CreateExpandTabViewModel(ListTabNames[10], "SoftwareIcon", TabState.New,
                    new List<BaseContentViewModel>()
                    {
                        CreateTabViewModel(ListTabNames[11], "SoftwareIcon", TabState.New),
                        CreateTabViewModel(ListTabNames[12], "SoftwareIcon", TabState.New),
                    }),
            };

            //  Set up pages to corresponding tabs
            TabPageVM = new Dictionary<string, BasePageViewModel>();

            TabPageVM.Add(ListTabs[0].TabName, new TongQuanPageVM());
            TabPageVM.Add(ListTabs[1].TabName, null);
            TabPageVM.Add(((TabViewModel)((ExpandTabViewModel)ListTabs[2]).Children[0]).TabName, null);
            TabPageVM.Add(((TabViewModel)((ExpandTabViewModel)ListTabs[2]).Children[1]).TabName, null);
            TabPageVM.Add(((TabViewModel)((ExpandTabViewModel)ListTabs[2]).Children[2]).TabName, null);
            TabPageVM.Add(((TabViewModel)((ExpandTabViewModel)ListTabs[2]).Children[3]).TabName, null);
            TabPageVM.Add(((TabViewModel)((ExpandTabViewModel)ListTabs[3]).Children[0]).TabName, null);
            TabPageVM.Add(((TabViewModel)((ExpandTabViewModel)ListTabs[3]).Children[1]).TabName, null);
            TabPageVM.Add(((TabViewModel)((ExpandTabViewModel)ListTabs[4]).Children[0]).TabName, null);
            TabPageVM.Add(((TabViewModel)((ExpandTabViewModel)ListTabs[4]).Children[1]).TabName, null);

            SubcribeTabChangedEvent();
        }

        /// <summary>
        /// Subcribe the <see cref="TabViewModel.TabSelected"/> event for updating the <see cref="CurrentTabVM"/> and <see cref="currentPageVM"/>
        /// </summary>
        private void SubcribeTabChangedEvent()
        {
            //  Subcribe the FocusTabChanged event of tab by hooking the OnCurrentTabChanged method 
            foreach (var tab in ListTabs)
            {
                if (tab is TabViewModel)
                    tab.TabSelected += OnCurrentTabChanged;
                else
                {
                    foreach (var subtab in ((ExpandTabViewModel)tab).Children)
                    {
                        ((TabViewModel)subtab).TabSelected += OnCurrentTabChanged;
                    }
                }
            }
        }
    }
}
