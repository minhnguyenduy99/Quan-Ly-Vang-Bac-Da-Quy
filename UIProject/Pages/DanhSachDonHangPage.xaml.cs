using MaterialDesignThemes.Wpf;
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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIProject.ViewModels.LayoutViewModels;
using UIProject.Views;

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for DanhSachDonHangPage.xaml
    /// </summary>
    public partial class DanhSachDonHangPage : Page
    {
        public DanhSachDonHangPage()
        {
            InitializeComponent();

            
        }

        private void OpenFilterWindow(object sender, RoutedEventArgs e)
        {
            OrderFilterDialogWindow orderFilterWnd = new OrderFilterDialogWindow();
            orderFilterWnd.ShowDialog();


        }
    }
}
