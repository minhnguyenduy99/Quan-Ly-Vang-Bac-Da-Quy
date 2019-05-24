using Services.PrintService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;

namespace UIProject.ViewModels.FunctionInterfaces
{
    /// <summary>
    /// supports the cability to manipulate the print process
    /// </summary>
    interface IPrintSupporter
    {
        IPrinter Printer { get; set; }

        ICommand Print { get; set; }

        event EventHandler<PrintedEventArgs> PrintFinished;
    }
}
