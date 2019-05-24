using Services.PrintService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UIProject.ServiceProviders;
using UIProject.ViewModels;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for DocumentPrintViewerWindow.xaml
    /// </summary>
    public partial class DocumentPrintViewerWindow : Window, IWindow
    {
        public DocumentPrintViewerWindow(FlowDocument document)
        {
            InitializeComponent();

            this.DataContext = new PrintWindowViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
