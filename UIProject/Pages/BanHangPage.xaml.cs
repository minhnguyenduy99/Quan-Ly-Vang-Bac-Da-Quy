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

            this.DataContext = ViewModel;
            //PART_ProductSearch.DataContext = ViewModel.TimKiemSanPhamVM;
            ViewModel.SanPhamDaCo += ViewModel_SanPhamDaCo;
            PART_LoaiSanPham.DataContext = ViewModel.LoaiSanPhamFilterVM;
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
}
