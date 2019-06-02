using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.FunctionInterfaces
{
    public interface IPrintWindowViewModel
    {
        IPrintSupporter PrintVM { get; set; }
        bool? PrintResult { get;  }

    }
}
