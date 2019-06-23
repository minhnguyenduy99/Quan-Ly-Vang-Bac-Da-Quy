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
using UIProject.Views;

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for DanhSachPhieuDichVuPage.xaml
    /// </summary>
    public partial class DanhSachPhieuDichVuPage : Page
    {
        public DanhSachPhieuDichVuPage()
        {
            InitializeComponent();

            this.Loaded += DanhSachPhieuDichVuPage_Loaded;
            this.Unloaded += DanhSachPhieuDichVuPage_Unloaded;
        }

        private void DanhSachPhieuDichVuPage_Unloaded(object sender, RoutedEventArgs e)
        {
            AnimationHelper.Fade(this, 1.0f, 0f);
        }

        private async void DanhSachPhieuDichVuPage_Loaded(object sender, RoutedEventArgs e)
        {
            await this.SlideFromRightToLeftAndFadeIn(0.7f);
        }

    }
}
