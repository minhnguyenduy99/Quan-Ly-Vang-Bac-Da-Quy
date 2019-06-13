using ModelProject;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIProject.ServiceProviders;
using UIProject.ViewModels;
using UIProject.Views;

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for DoiTacPage.xaml
    /// </summary>
    public partial class DoiTacPage : Page
    {
        public DoiTacPage()
        {
            InitializeComponent();

            this.Loaded += DoiTacPage_Loaded;
        }

        private async void DoiTacPage_Loaded(object sender, RoutedEventArgs e)
        {
            await this.SlideFromRightToLeftAndFadeIn(0.7f);
        }

        private void BtnAddProvider_Click(object sender, RoutedEventArgs e)
        {
            ProviderAddingDialogWindow providerAddWnd = new ProviderAddingDialogWindow(btnAddProvider);
            btnAddProvider.CommandParameter = providerAddWnd;
        }

        private void BtnEditProvider_Click(object sender, RoutedEventArgs e)
        {
            EditProviderInfoWindow providerEditWnd = new EditProviderInfoWindow(btnEditProvider);
            btnEditProvider.CommandParameter = providerEditWnd;
        }

        private void BtnDeleteProvider_Click(object sender, RoutedEventArgs e)
        {
            DialogPopupWindow deleteNotifyWnd = new DialogPopupWindow();
            btnDeleteProvider.CommandParameter = deleteNotifyWnd;
        }
    }
}
