using ModelProject;
using Services.PrintService;
using System;
using System.Collections.Generic;
using System.Data;
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
using UIProject.UIConnector;
using UIProject.ViewModels;
using UIProject.ViewModels.DataViewModels;
using UIProject.ViewModels.FunctionInterfaces;
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

            var printWndVM = new PrintWindowViewModel(document);

            DataContext = printWndVM;

            this.documentViewer.Document = printWndVM.Document;

            this.Loaded += DocumentPrintViewerWindow_Loaded;
            printWndVM.PrintFinished += PrintWndVM_PrintFinished;
        }

        private async void DocumentPrintViewerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //UpdateBindingToLabel();
            ConvertDataGridToTable();
            await AnimationHelper.FadeAsync(this, 0f, 1.0f);
        }

        private void PrintWndVM_PrintFinished(object sender, Events.PrintedEventArgs e)
        {
            DialogPopupWindow notifyWnd = new DialogPopupWindow();
            DialogWindowViewModel notifyWndVM = new DialogWindowViewModel()
            {
                DialogType = DialogWindowType.Info,
                MessageText = "In thành công",
                OKText = "OK"
            };
            notifyWnd.DataContext = notifyWndVM;
            notifyWndVM.ButtonPressed += ButtonPressedHandler;

            notifyWnd.ShowDialog();
            this.DialogResult = e.PrintResult;
            this.Close();

            void ButtonPressedHandler(object s, DialogButtonPressedEventArgs ev)
            {
                notifyWnd.DialogResult = true;
                notifyWnd.Close();
            }
        }

        private void ConvertDataGridToTable()
        {
            var document = documentViewer.Document as FlowDocument;
            if (document == null)
                return;

            var table = LogicalTreeHelper.FindLogicalNode(document, "dataTable") as Table;
            if (table == null)
                return;

            // Clear all the previous data in the document before adding new one
            if (table.RowGroups.Count > 1)
                table.RowGroups.RemoveRange(1, table.RowGroups.Count - 1);      

            (document.DataContext as ITableConvertable).ConvertDataToTable(table);
        }

    }
}
