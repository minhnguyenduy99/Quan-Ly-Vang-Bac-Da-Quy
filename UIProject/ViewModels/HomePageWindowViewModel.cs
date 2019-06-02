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
using UIProject.ViewModels.FunctionInterfaces;
using System.Collections.ObjectModel;

namespace UIProject.ViewModels
{
    /// <summary>
    /// View model for home page window
    /// </summary>
    public class HomePageWindowViewModel : BaseWindowViewModel, INavigator
    {
        #region List tab names of the HomePage window
        public static readonly List<string> ListTabNames = new List<string>()
        {
            "Tổng quan",
            "Bán hàng",
            "Tạo phiếu dịch vụ",
            "Danh sách đơn hàng",
            "Khách hàng",
            "Nhà cung cấp",
            "Sản phẩm",
            "Dịch vụ",
            "Nhập hàng",
            "Báo cáo tồn kho"
        };

        public static readonly List<string> ListTabHeaderNames = new List<string>()
        {
            "Tạo phiếu",
            "Danh sách",
            "Quản lý",
        };
        #endregion

        #region Private Fields
        private TabViewModel currentTabVM;
        private BasePageViewModel currentPageVM;
        #endregion

        public List<HeaderViewModel> ListTabHeaders { get; private set; }
        
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
        /// The current object which is currently navigated
        /// </summary>
        public BasePageViewModel CurrentNavigatedPage
        {
            get => GetPropertyValue<BasePageViewModel>();
            set => SetProperty(value);
        }

        /// <summary>
        /// A dictionary to store the navigated objects
        /// </summary>
        public Dictionary<object, BasePageViewModel> NavigatedPages { get; set; }

        public HomePageWindowViewModel()
        {
            SetUpWindowLayout();

            InitializeTabHeaders();

            InitializeTabs();

            //  Set up pages to corresponding tabs
            NavigatedPages = new Dictionary<object, BasePageViewModel>();

            ListTabs[0].Select();
        }

        /// <summary>
        /// Navigate to the page corresponding with the key
        /// </summary>
        /// <param name="key">The value of key which represents for the navigated page</param>
        public void Navigate(object key)
        {
            if (!NavigatedPages.ContainsKey(key))
            {
                NavigatedPages.Add(key, GetCorrespondingPage((string)key));
            }
            CurrentNavigatedPage = NavigatedPages[key];
            CurrentTabVM = ListTabs.Where(tab => tab.TabName == (string)key).ElementAt(0);
            if (!CurrentTabVM.IsSelected)
                CurrentTabVM.Select();
        }


        private void OnCurrentTabChanged(object sender, TabSelectedEventArgs e)
        {
            Navigate(e.TabName);
        }

        private void SetUpWindowLayout()
        {
            CanMaximized = true;
            CanMinimized = true;
            WindowState = WindowState.Maximized;
            IconSource = (string)Application.Current.FindResource("SoftwareIcon");
            BackgroundSource = (string)Application.Current.FindResource("LoginBackground");
        }
        private void InitializeTabHeaders()
        {
            ListTabHeaders = new List<HeaderViewModel>()
            {
                new HeaderViewModel()
                {
                    Header = ListTabHeaderNames[0],
                    IconSource = (string)Application.Current.FindResource("SoftwareIcon"),
                },
                new HeaderViewModel()
                {
                    Header = ListTabHeaderNames[1],
                    IconSource = (string)Application.Current.FindResource("SoftwareIcon"),
                },
                new HeaderViewModel()
                {
                    Header = ListTabHeaderNames[2],
                    IconSource = (string)Application.Current.FindResource("SoftwareIcon"),
                }
            };

            foreach(var header in ListTabHeaders)
            {
                header.Background = (Brush)Application.Current.FindResource("RoyalBlue");
                header.Foreground = Brushes.White;
                header.FocusBackground = (Brush)Application.Current.FindResource("AzureBlue");
            }
        }
        private void InitializeTabs()
        {
            //  Initialize the content present layout of tabs
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
                CreateTabViewModel(ListTabNames[8], "SoftwareIcon", TabState.New),
                CreateTabViewModel(ListTabNames[9], "SoftwareIcon", TabState.New),
            };

            SubcribeTabChangedEvent();
        }    
        private void SubcribeTabChangedEvent()
        {
            //  Subcribe the FocusTabChanged event of tab by hooking the OnCurrentTabChanged method 
            foreach (var tab in ListTabs)
            {
                tab.TabSelected += OnCurrentTabChanged;
            }
        }
        private BasePageViewModel GetCorrespondingPage(string tabName)
        {
            if (tabName.Equals(ListTabNames[0]))
                return new TongQuanPageVM(this);
            if (tabName.Equals(ListTabNames[1]))
                return new BanHangPageVM(this);
            if (tabName.Equals(ListTabNames[2]))
                return new LamDichVuPageVM(this);
            if (tabName.Equals(ListTabNames[3]))
                return new DanhSachDonHangPageVM(this);
            if (tabName.Equals(ListTabNames[4]))
                return new KhachHangPageVM(this);
            if (tabName.Equals(ListTabNames[5]))
                return new NhaCungCapPageVM(this);
            if (tabName.Equals(ListTabNames[6]))
                return new SanPhamPageVM(this);
            if (tabName.Equals(ListTabNames[7]))
                return new DichVuPageVM(this);
            if (tabName.Equals(ListTabNames[8]))
                return new NhapHangPageVM(this);
            if (tabName.Equals(ListTabNames[9]))
                return new BaoCaoTonKhoPageVM(this);
            return null;
        }


    }
}
