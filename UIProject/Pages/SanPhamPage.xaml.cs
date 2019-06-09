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
using UIProject.ViewModels.LayoutViewModels;
using UIProject.Views;

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for SanPhamPage.xaml
    /// </summary>
    public partial class SanPhamPage : Page
    {
        public SanPhamPage()
        {
            InitializeComponent();
            this.Loaded += SanPhamPage_Loaded;
        }

        private async void SanPhamPage_Loaded(object sender, RoutedEventArgs e)
        {
            await this.SlideFromRightToLeftAndFadeIn(0.7f);
        }

        private void BtnEditProductInfo_Click(object sender, RoutedEventArgs e)
        {
            EditProductInfoWindow editPropWnd = new EditProductInfoWindow();
            editPropWnd.Activator = btnEditProductInfo;
            btnEditProductInfo.CommandParameter = editPropWnd;
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            DialogPopupWindow notifyDeleteWnd = new DialogPopupWindow(new DialogWindowViewModel()
            {
                DialogType = DialogWindowType.YesNo,
                MessageText = "Bạn muốn xóa sản phẩm này ?",
                YesText = "Có",
                NoText = "Không"
            });

            this.btnDeleteProduct.CommandParameter = notifyDeleteWnd;
        }
    }
}
