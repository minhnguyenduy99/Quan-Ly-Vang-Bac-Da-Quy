using BaseMVVM_Service.BaseMVVM;
using Services.PrintService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using UIProject.Events;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels
{
    public class PrintWindowViewModel : BaseWindowViewModel, IPrintWindowViewModel
    {
        public IPrintSupporter PrintVM { get; set; }
        public IDocumentPaginatorSource Document { get; set; }
        public bool? PrintResult { get; private set; }
        public PrintWindowViewModel() : base()
        {
            this.PrintVM = new PrintViewModel();
            this.PrintVM.PrintFinished += PrintVM_PrintFinished;
        }

        public PrintWindowViewModel(IDocumentPaginatorSource document)
        {
            var pageSize = new
            {
                Width = document.DocumentPaginator.PageSize.Width,
                Height = document.DocumentPaginator.PageSize.Height
            };


            if (document is FlowDocument)
            {
                pageSize = new
                {
                    Width = ((FlowDocument)document).PageWidth,
                    Height = ((FlowDocument)document).PageHeight
                };
            }

            this.PrintVM = new PrintViewModel(new PrintHelper(pageSize.Width, pageSize.Height));

            this.Document = document;
        }

        public event EventHandler<PrintedEventArgs> PrintFinished
        {
            add { PrintVM.PrintFinished += value; }
            remove { PrintVM.PrintFinished -= value; }
        }

        private void PrintVM_PrintFinished(object sender, PrintedEventArgs e)
        {
            PrintResult = e.PrintResult;
        }

       

        public ICommand PrintCommand
        {
            get => PrintVM.Print;
        }
    }
}
