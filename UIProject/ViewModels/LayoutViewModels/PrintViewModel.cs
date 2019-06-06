using BaseMVVM_Service.BaseMVVM;
using Services.PrintService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using UIProject.Events;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.LayoutViewModels
{
    public class PrintViewModel : BaseViewModel, IPrintSupporter
    {
        private ICommand print;
        public IPrinter Printer { get; set; }
        public ICommand Print
        {
            get => print ?? new BaseCommand<IDocumentPaginatorSource>(OnPrintCommandExecute);
            set => print = value;
        }


        public PrintViewModel(IPrinter printer) : base()
        {
            Printer = printer;
        }

        public PrintViewModel() : this(null) { }


        public event EventHandler<PrintedEventArgs> PrintFinished;


        protected virtual void OnPrintCommandExecute(IDocumentPaginatorSource documentSource)
        {
            try
            {
                Printer.PrintDocument(documentSource);
            }
            catch
            {
                //  Print failed for some reasons
                OnPrintFinished(new PrintedEventArgs(Printer, documentSource, false));
            }
            OnPrintFinished(new PrintedEventArgs(Printer, documentSource, true));
        }
        protected virtual void OnPrintFinished(PrintedEventArgs e)
        {
            PrintFinished?.Invoke(this, e);
        }
    }
}
