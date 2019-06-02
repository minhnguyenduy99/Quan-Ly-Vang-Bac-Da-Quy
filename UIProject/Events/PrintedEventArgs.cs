using Services.PrintService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace UIProject.Events
{
    /// <summary>
    /// Provides information of the print process when it is finished
    /// </summary>
    public class PrintedEventArgs : EventArgs
    {
        public IPrinter Printer { get; private set; }
        public IDocumentPaginatorSource Document { get; private set; } = null;
        public Visual Visual { get; private set; } = null;
        public bool? PrintResult { get; private set; }
        public PrintedEventArgs(IPrinter printer, IDocumentPaginatorSource document, bool? printResult = null)
        {
            Printer = printer;
            Document = document;
            PrintResult = printResult;
        }
        public PrintedEventArgs(IPrinter printer, Visual visual, bool printResult)
        {
            Printer = printer;
            Visual = visual;
            PrintResult = printResult;
        }
    }
}
