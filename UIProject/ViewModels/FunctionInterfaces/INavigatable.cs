using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.FunctionInterfaces
{
    /// <summary>
    /// Provides functionalities for a view model to be navigated
    /// </summary>
    public interface INavigatable
    {
        bool IsNavigated { get; set; }
        INavigator Navigator { get; set; }

        event EventHandler Navigated;
    }
}
