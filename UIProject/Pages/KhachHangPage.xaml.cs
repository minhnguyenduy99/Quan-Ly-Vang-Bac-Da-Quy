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
        }

        private void OpenAddNewCustomerDialog(object sender, RoutedEventArgs e)
        {
            var addCustomerBtn = sender as Button;

            CustomerAddingDialogWindow dialog = new CustomerAddingDialogWindow();
            Point currentPosition = 
                addCustomerBtn.TransformToAncestor(this).Transform(new Point(0, 0));
            dialog.Left = currentPosition.X - 200;
            dialog.Top = currentPosition.Y + 100 ;
            dialog.ShowDialog();           
        }
    }
}
