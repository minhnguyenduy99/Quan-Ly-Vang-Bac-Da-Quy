using ModelProject.Models;
using UIProject.ViewModels;
using UIProject.ServiceProviders;
using UIProject.Pages;

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

using static UIProject.ViewModels.TabViewModel;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IClosable
    {
        public HomePageWindowViewModel HomePageWindowVM { get; private set; }
        public MainWindow()
        {

            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetupTabState();
        }

        private void SetupTabState()
        {
            var tabs = TabsContainer.Children.OfType<RadioButton>();
            for(int i=0;i<tabs.Count();i++)
            {
                tabs.ElementAt(i).DataContext = tabs.ElementAt(i).Content = (DataContext as HomePageWindowViewModel).ListTabs[i];
            }
        }
    }
}
