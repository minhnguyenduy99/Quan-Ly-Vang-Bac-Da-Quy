using System.Windows.Controls;
using System.Windows.Documents;
using UIProject.ServiceProviders;
using UIProject.ViewModels.PageViewModels;
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

        private void BtnThemPhieuMua_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            InitializePrintViewWindow();
        }

        private void InitializePrintViewWindow()
        {
            var phieuMuaPrintDoc = (FlowDocument)GetDocumentPage();
            phieuMuaPrintDoc.DataContext = (DataContext as NhapHangPageVM).NhapHangVM;
            DocumentPrintViewerWindow printWnd = new DocumentPrintViewerWindow(phieuMuaPrintDoc);
            btnThemPhieuMua.CommandParameter = printWnd;
        }

        private IDocumentPaginatorSource GetDocumentPage()
        {
            return (IDocumentPaginatorSource)FindResource("PhieuMuaHangPrintTemplate");
        }
    }
}
