using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using Services.PrintService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Printing;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using UIProject.Converters;
using UIProject.ServiceProviders;
using UIProject.ViewModels;
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
        public FlowDocument HoaDonPrintDoc { get; set; }
        public BanHangPage()
        {
            InitializeComponent();

            this.Loaded += BanHangPage_Loaded;
        }

        private void ViewModel_SanPhamDaCo(object sender, Events.ItemEventArgs<ChiTietBanModel> e)
        {
            MessageBox.Show("Sản phẩm này đã được chọn");
        }

        private async void BanHangPage_Loaded(object sender, RoutedEventArgs e)
        {
            (this.DataContext as BanHangPageVM).SanPhamDaTonTai += ViewModel_SanPhamDaCo;
            await this.SlideFromRightToLeftAndFadeIn(0.7f);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InitializePrintViewWindow();
        }

        private void InitializePrintViewWindow()
        {
            HoaDonPrintDoc = (FlowDocument)GetDocumentPage();
            HoaDonPrintDoc.DataContext = (DataContext as BanHangPageVM).HoaDonVM;
            DocumentPrintViewerWindow printWnd = new DocumentPrintViewerWindow(HoaDonPrintDoc);
            btnThanhToan.CommandParameter = printWnd;           
        }

        private IDocumentPaginatorSource GetDocumentPage()
        {
            return (IDocumentPaginatorSource)FindResource("HoaDonPrintTemplate");
        }

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerAddingDialogWindow customerAddingWnd = new CustomerAddingDialogWindow(btnAddCustomer);
            this.btnAddCustomer.CommandParameter = customerAddingWnd;
        }
    }
}
