using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UIProject.ServiceProviders;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for OrderFilterWindow.xaml
    /// </summary>
    public partial class OrderFilterWindow : Window, IClosable
    {
        
        public OrderFilterWindow()
        {
            InitializeComponent();
            DataContext = new DialogWindowViewModel()
            {
                NavigationBarVisibility = Visibility.Collapsed,
                CanMaximized = false,
                CanMinimized = false,
                WindowState = WindowState.Normal,
                Title = string.Empty
            };
        }
    }
}
