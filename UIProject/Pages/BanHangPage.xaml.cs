using ModelProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UIProject.ViewModels.LayoutViewModels;
using UIProject.ViewModels.PageViewModels;
using UIProject.Views;

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for BanHangPage.xaml
    /// </summary>
    public partial class BanHangPage : Page
    {
        public BanHangPageVM ViewModel { get; private set; }
        public BanHangPage()
        {
            InitializeComponent();

            this.Loaded += BanHangPage_Loaded;

            ViewModel = new BanHangPageVM();

            PART_ProductSearch.DataContext = ViewModel.TimKiemSanPhamVM;
            ProductView.DataContext = ViewModel.DanhSachChiTietBan;

            ViewModel.SanPhamDaCo += ViewModel_SanPhamDaCo;
        }

        private void ViewModel_SanPhamDaCo(object sender, Events.ItemEventArgs<ChiTietBanModel> e)
        {
            MessageBox.Show("Sản phẩm này đã được chọn");
        }

        private async void BanHangPage_Loaded(object sender, RoutedEventArgs e)
        {
            await this.FadeIn(0.5f, 0.5f);
        }

    }

    public class Product
    {
        public int No { get; set; }
        public string OrderCode { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public long SinglePrice { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public long TotalMoney { get; set; }

        public string ProductImageSource { get; set; }
        public Product()
        {
            No = 1;
            OrderCode = "PD001";
            ProductName = "Jewelnafnakgnaknglanlgkaetea";
            ProductType = "Gold";
            Unit = "a";
            Quantity = 0;
            SinglePrice = 30000;
            Discount = 0.04;
            Tax = 0.05;
            TotalMoney = 20000;
            ProductImageSource = (string)Application.Current.FindResource("SoftwareIcon");
        }
    }
}
