using BaseMVVM_Service.BaseMVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.Events;

namespace UIProject.ViewModels.FunctionInterfaces
{
    /// <summary>
    /// Provides submitation ability to view model
    /// </summary>
    public interface ISubmitViewModel
    {
        ISubmitable Data { get; set; }

        bool IsDataValid { get; }

        event EventHandler<SubmitedDataEventArgs> SubmitedData;

        bool Submit();
    }
}
