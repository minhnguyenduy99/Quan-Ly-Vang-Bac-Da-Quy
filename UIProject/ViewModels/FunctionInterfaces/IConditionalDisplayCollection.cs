using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.FunctionInterfaces
{
    /// <summary>
    /// Provides the ability to display a collection under filters
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IConditionalDisplayCollection<T>
    {
        ObservableCollection<T> DisplayItems { get; }
        
        List<Func<T, bool>> Filters { get ; }

    }
}
