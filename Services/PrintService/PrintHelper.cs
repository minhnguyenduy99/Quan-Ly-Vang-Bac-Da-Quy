using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace Services.PrintService
{
    public class PrintHelper : IPrinter
    {
        private PageMediaSize pageSizer;
        public DocumentPaginator Document { get; set; }


        public string DocumentName { get; set; }
        public double PageWidth { get; set; }
        public double PageHeight { get; set; }

        public PrintHelper(IDocumentPaginatorSource document) : this(document, double.NaN, double.NaN)
        {
            pageSizer = new PageMediaSize(PageMediaSizeName.ISOA4);
        }

        public PrintHelper(IDocumentPaginatorSource document, double pageWidth, double pageHeight)
                       : this(document, pageWidth, pageHeight, null) { }

        public PrintHelper(IDocumentPaginatorSource document, double pageWidth, double pageHeight, string title)
        {
            if (document != null)
                Document = document.DocumentPaginator;
            Document.PageSize = new System.Windows.Size(PageWidth, PageHeight);
            PageWidth = pageWidth;
            PageHeight = pageHeight;

            pageSizer = new PageMediaSize(PageWidth, PageHeight);

            this.DocumentName = title;
        }

        public void Print()
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintTicket.PageMediaSize = pageSizer;
            if (printDlg.ShowDialog() == true)
            {
                printDlg.PrintDocument(Document, DocumentName);
            }
        }

        public FixedDocumentSequence GetFixedDocumentFromVisual(Visual v)
        {
            if (File.Exists("sample.xps"))
                File.Delete("sample.xps");
            using (XpsDocument doc = new XpsDocument("sample.xps", FileAccess.ReadWrite))
            {
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

                writer.Write(v, new PrintTicket() { PageMediaSize = pageSizer });
                return doc.GetFixedDocumentSequence();
            }
        }

        private Visual PerformTransform(Visual visual, PrintTicket ticket)
        {
            ContainerVisual root = new ContainerVisual();
            const double inch = 96;

            // Set the margins.
            double xMargin = 1.25 * inch;
            double yMargin = 1 * inch;

            Double printableWidth = ticket.PageMediaSize.Width.Value;
            Double printableHeight = ticket.PageMediaSize.Height.Value;

            Double xScale = (printableWidth - xMargin * 2) / printableWidth;
            Double yScale = (printableHeight - yMargin * 2) / printableHeight;
            root.Children.Add(visual);
            root.Transform = new MatrixTransform(xScale, 0, 0, yScale, xMargin, yMargin);
           
            return root;
        }
    }
}
