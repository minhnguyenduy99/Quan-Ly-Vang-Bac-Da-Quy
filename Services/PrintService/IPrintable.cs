using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace Services.PrintService
{
    public interface IPrintable
    {
        IDocumentPaginatorSource ConvertToPrintableObject();
    }
}
