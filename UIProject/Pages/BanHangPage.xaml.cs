using BaseMVVM_Service.BaseMVVM;
using ModelProject;
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
            ViewModel.ThucThiThemKhachHang += ViewModel_ThucThiThemKhachHang;
            PART_LoaiSanPham.DataContext = ViewModel.LocSanPhamVM;
           // PART_TimKiemKhachHang.DataContext = ViewModel.TimKiemKhachHangVM;
        }

        private void ViewModel_ThucThiThemKhachHang(object sender, ViewModels.AddingWindowViewModel<BaseSubmitableModel> e)
        {
            CustomerAddingDialogWindow customerWindow = new CustomerAddingDialogWindow();
        }

        private void ViewModel_SanPhamDaCo(object sender, Events.ItemEventArgs<ChiTietBanModel> e)
        {
            MessageBox.Show("Sản phẩm này đã được chọn");
        }

        private async void BanHangPage_Loaded(object sender, RoutedEventArgs e)
        {
            await this.FadeIn(0.5f, 0.5f);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomerAddingDialogWindow customerWindow = new CustomerAddingDialogWindow()
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            customerWindow.DataContext = ViewModel.ThemKhachHangVM;

            customerWindow.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenPrintViewer();
        }

        private void OpenPrintViewer()
        {
            if (File.Exists("hoadon.xps"))
            {
                File.Delete("hoadon.xps");
            }
            using (XpsDocument xpsDoc = new XpsDocument("hoadon.xps", FileAccess.ReadWrite))
            {
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDoc);
                var printTicket = new PrintTicket();
                printTicket.PagesPerSheet = 1;
                printTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.NorthAmericaLetter, 500, 500);
                writer.Write(GetDocumentPage().DocumentPaginator);

                //var fixedDoc = xpsDoc.GetFixedDocumentSequence();

                var fixedDoc = (FlowDocument)GetDocumentPage();

                DocumentPrintViewerWindow printWnd = new DocumentPrintViewerWindow(fixedDoc);
                printWnd.ShowDialog();
            }
        }

        private IDocumentPaginatorSource GetDocumentPage()
        {
            return (IDocumentPaginatorSource)FindResource("HoaDonPrintTemplate");
        }
    }
}
