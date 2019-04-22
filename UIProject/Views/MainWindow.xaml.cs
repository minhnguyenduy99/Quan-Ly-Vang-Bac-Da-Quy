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
using UIProject.ViewModels.LayoutViewModels;

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
            this.Closed += MainWindow_Closed;

            
        }

        private async void MainWindow_Closed(object sender, EventArgs e)
        {
            await AnimationHelper.FadeAsync(this, 1f, 0f);
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetupBindingTabControl();
            await AnimationHelper.FadeAsync(this, 0f, 1f);
        }

        private void SetupBindingTabControl()
        {
            var tabs = TabsContainer.Children;
            int count = 0;
            for(int i=0;i<tabs.Count;i++)
            {
                if (tabs[i] is RadioButton)
                {                  
                    var radioButtonTab = tabs[i] as RadioButton;
                    radioButtonTab.DataContext = radioButtonTab.Content = (DataContext as HomePageWindowViewModel).ListTabs[count];
                    count++;
                }
                else
                {
                    if (tabs[i] is StackPanel)
                    { 
                        SetupExpander(tabs[i] as StackPanel, count - 1);
                        count++;
                    }
                }
            }
        }

        private void SetupExpander(StackPanel subtab, int viewModelIndex)
        {
            var tabs = subtab.Children.OfType<RadioButton>();
            var expanderTabVM = (DataContext as HomePageWindowViewModel).ListTabs[viewModelIndex] as ExpandTabViewModel;
            for (int i = 0; i < tabs.Count(); i++)
            {
                tabs.ElementAt(i).DataContext = tabs.ElementAt(i).Content = expanderTabVM.Children[i];
            }
        }

        private void Collapsed_tab(object sender, RoutedEventArgs e)
        {
            IconGrid.Visibility = Visibility.Collapsed;
        }
    }
}
