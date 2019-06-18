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
    /// Interaction logic for LamDichVuPage.xaml
    /// </summary>
    public partial class LamDichVuPage : Page
    {
        public LamDichVuPage()
        {
            InitializeComponent();
            this.Loaded += LamDichVuPage_Loaded;
        }

        private async void LamDichVuPage_Loaded(object sender, RoutedEventArgs e)
        {
            await this.SlideFromRightToLeftAndFadeIn(0.7f);
        }

        private void BtnChinhSuaThongTinLienHe_Click(object sender, RoutedEventArgs e)
        {
            AddressEditDialogWindow addressEditWnd = new AddressEditDialogWindow(this.btnChinhSuaThongTinLienHe);
            btnChinhSuaThongTinLienHe.CommandParameter = addressEditWnd;
        }

        private void BtnXemDanhSachDV_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnThemDichVu_Click(object sender, RoutedEventArgs e)
        {
            ServiceAddingDialogWindow serviceAddWnd = new ServiceAddingDialogWindow(btnThemDichVu);
            btnThemDichVu.CommandParameter = serviceAddWnd;
        }

        private void BtnSubmitPhieuDichVu_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnThemKhachHang_Click(object sender, RoutedEventArgs e)
        {
            CustomerAddingDialogWindow customerAddWnd = new CustomerAddingDialogWindow(btnThemKhachHang);
            btnThemKhachHang.CommandParameter = customerAddWnd;
        }
    }
}
