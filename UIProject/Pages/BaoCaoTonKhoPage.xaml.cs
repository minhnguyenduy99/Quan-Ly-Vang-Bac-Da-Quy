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
using UIProject.ViewModels.PageViewModels;
using UIProject.Views;

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for BaoCaoTonKhoPage.xaml
    /// </summary>
    public partial class BaoCaoTonKhoPage : Page
    {
        public BaoCaoTonKhoPage()
        {
            InitializeComponent();

            this.Loaded += BaoCaoTonKhoPage_Loaded;
        }

        private async void BaoCaoTonKhoPage_Loaded(object sender, RoutedEventArgs e)
        {
            await this.SlideFromRightToLeftAndFadeIn(0.7f);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();
            dlg.PageRangeSelection = PageRangeSelection.AllPages;
            dlg.ShowDialog();
        }

        private void BtnLoadBaoCao_Click(object sender, RoutedEventArgs e)
        {
            DialogPopupWindow loadingWindow = new DialogPopupWindow();
            btnLoadBaoCao.CommandParameter = loadingWindow;
        }

        private void BtnIn_Click(object sender, RoutedEventArgs e)
        {
            InitializePrintViewWindow();
        }
        private void InitializePrintViewWindow()
        {
            var baoCaoTonKhoPrintDoc = (FlowDocument)GetDocumentPage();
            baoCaoTonKhoPrintDoc.DataContext = (DataContext as BaoCaoTonKhoPageVM).BaoCaoTonKhoVM;
            DocumentPrintViewerWindow printWnd = new DocumentPrintViewerWindow(baoCaoTonKhoPrintDoc);
            btnIn.CommandParameter = printWnd;
        }

        private IDocumentPaginatorSource GetDocumentPage()
        {
            return (IDocumentPaginatorSource)FindResource("BaoCaoTonKhoPrintTemplate");
        }
    }
}
