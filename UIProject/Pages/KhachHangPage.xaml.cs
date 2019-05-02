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
using UIProject.Test;
using UIProject.Views;

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for KhachHangPage.xaml
    /// </summary>
    public partial class KhachHangPage : Page
    {
        public KhachHangPage()
        {
            InitializeComponent();

            PART_CustomerList.Content = new List<Customer>()
            {
                new Customer(),
                new Customer(),
                new Customer()
            };

            this.Loaded += KhachHangPage_Loaded;
        }

        private async void KhachHangPage_Loaded(object sender, RoutedEventArgs e)
        {
            await this.FadeIn(0.5f, 0.5f);
        }

        private void OpenAddNewCustomerDialog(object sender, RoutedEventArgs e)
        {
            var addCustomerBtn = sender as Button;

            CustomerAddingDialogWindow dialog = new CustomerAddingDialogWindow();
            dialog.ShowDialog(addCustomerBtn);        
        }
    }
}
