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
    public class PrintWindowViewModel : BaseWindowViewModel, IPrintSupporter
    {
        private ICommand print;
        public IPrinter Printer { get; set; }
       
        public IDocumentPaginatorSource DocumentSource
        {
            get => GetPropertyValue<IDocumentPaginatorSource>();
            set => SetProperty(value);
        }

        public ICommand Print
        {
            get => print ?? new BaseCommand<IDocumentPaginatorSource>(OnPrintCommandExecute);
            set => print = value;
        }

        public ICommand PrintVisual
        {
            get => new BaseCommand<Visual>(OnPrintVisualExecute);
        }

        public PrintWindowViewModel(IPrinter printer) : base()
        {
            Printer = printer;
        }
        public PrintWindowViewModel() : this(null) { }


        public event EventHandler<PrintedEventArgs> PrintFinished;
        protected virtual void OnPrintCommandExecute(IDocumentPaginatorSource document)
        {
            Printer?.PrintDocument(document);
          
            OnPrintFinished(new PrintedEventArgs(Printer, document));
        }
        protected virtual void OnPrintVisualExecute(Visual visual)
        {
            Printer?.PrintVisual(visual);

            OnPrintFinished(new PrintedEventArgs(Printer, visual));
        }
        protected virtual void OnPrintFinished(PrintedEventArgs e)
        {
            PrintFinished?.Invoke(this, e);
        }
    }
}
