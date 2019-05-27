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
    public class PrintWindowViewModel : BaseWindowViewModel
    {
        PrintViewModel PrintViewModel { get; set; }

        public PrintWindowViewModel() : base()
        {
            this.PrintViewModel = new PrintViewModel();
        }
    }
}
