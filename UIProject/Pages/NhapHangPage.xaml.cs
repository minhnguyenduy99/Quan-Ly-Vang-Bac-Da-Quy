using System.Windows.Controls;
using UIProject.ServiceProviders;
using UIProject.Views;

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for NhapHangPage.xaml
    /// </summary>
    public partial class NhapHangPage : Page
    {
        public NhapHangPage()
        {
            InitializeComponent();

            this.Loaded += NhapHangPage_Loaded;
        }

        private async void NhapHangPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.SlideFromRightToLeftAndFadeIn(0.7f);
        }

        private void BtnAddProduct_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ProductAddingDialogWindow productAddingWnd = new ProductAddingDialogWindow(btnAddProduct);
            btnAddProduct.CommandParameter = productAddingWnd;
        }
    }
}
