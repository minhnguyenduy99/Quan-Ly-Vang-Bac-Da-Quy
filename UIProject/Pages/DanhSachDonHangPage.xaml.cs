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
            this.Opacity = 0.4;
            this.Background = Brushes.Black;
            this.Effect = new BlurEffect();

            var filterWnd = new OrderFilterWindow();

            filterWnd.ShowDialog();

            this.Opacity = 1;
            this.Background = Brushes.WhiteSmoke;
        }
    }
}
