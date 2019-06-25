using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace Services.PrintService
{
    public interface IPrinter
    {        
        bool? PrintResult { get; }
        void PrintDocument(IDocumentPaginatorSource documentSource);
        void PrintVisual(Visual visual);
        void Print(IPrintable printableObject);
    }
}
