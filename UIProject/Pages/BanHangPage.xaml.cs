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

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for BanHangPage.xaml
    /// </summary>
    public partial class BanHangPage : Page
    {
        public BanHangPage()
        {
            InitializeComponent();

            var listProducts = new ObservableCollection<Product>()
            {
                new Product(),
                new Product(),
                new Product()
            };

            this.ProductView.ItemsSource = listProducts;
            this.ProductDisplayed.ItemsSource = listProducts;
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
