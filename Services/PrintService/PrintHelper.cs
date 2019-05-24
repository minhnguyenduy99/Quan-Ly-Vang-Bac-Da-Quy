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
    /// <summary>
    /// Provides the printing functionalities 
    /// </summary>
    public class PrintHelper : IPrinter
    {
        private PageMediaSize pageSizer;
        private PrintDialog printDlg;

        public string DocumentName { get; set; }
        public double? PageWidth 
        {
            get => pageSizer?.Width;
        }
        public double? PageHeight
        {
            get => pageSizer?.Height;
        }

        /// <summary>
        /// Create an instance of <see cref="PrintHelper"/>
        /// </summary>
        /// <param name="document">The document source to print</param>
        public PrintHelper() : this(double.NaN, double.NaN)
        {
            pageSizer = new PageMediaSize(PageMediaSizeName.ISOA4);
        }

        /// <summary>
        /// Create an instance of <see cref="PrintHelper"/>
        /// </summary>
        /// <param name="document">The document source to print</param>
        /// <param name="pageWidth">The width of page to print out</param>
        /// <param name="pageHeight">The height of page to print out</param>
        public PrintHelper(double pageWidth, double pageHeight)
                       : this(pageWidth, pageHeight, null) { }

        /// <summary>
        /// Create an instance of <see cref="PrintHelper"/>
        /// </summary>
        /// <param name="document">The document source to print</param>
        /// <param name="pageWidth">The width of page to print out</param>
        /// <param name="pageHeight">The height of page to print out</param>
        /// <param name="title">The title of the print file</param>
        public PrintHelper(double pageWidth, double pageHeight, string title)
        {
            printDlg = new PrintDialog();

            pageSizer = new PageMediaSize(pageWidth, pageHeight);

            this.DocumentName = title;
        }

        public PrintHelper(Visual visual, double width, double height)
        {
            printDlg = new PrintDialog();

            var printerCabilities = printDlg.PrintQueue.GetPrintCapabilities();
            double heightScale = height / printerCabilities.PageImageableArea.ExtentHeight;
            double widthScale = width / printerCabilities.PageImageableArea.ExtentWidth;       
        }

        /// <summary>
        /// Activate the print method
        /// </summary>
        public void PrintDocument(IDocumentPaginatorSource document)
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintTicket.PageMediaSize = pageSizer;
            if (printDlg.ShowDialog() == true)
            {
                printDlg.PrintDocument(document.DocumentPaginator, DocumentName);
            }
        }

        public void PrintVisual(Visual visual)
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintTicket.PageMediaSize = pageSizer;
            if (printDlg.ShowDialog() == true)
            {
                printDlg.PrintVisual(visual, DocumentName);
            }
        }

        public void Print(IPrintable printableObject)
        {
            PrintDocument(printableObject.ConvertToPrintableObject());
        }

        private FixedDocumentSequence GetFixedDocumentFromVisual(Visual v)
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
