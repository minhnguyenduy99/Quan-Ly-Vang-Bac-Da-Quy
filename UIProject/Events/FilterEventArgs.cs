using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Events
{
    public class FilterEventArgs<T> : EventArgs
    {
        public List<Func<ItemViewModel<T>, bool>> FilterCallbacks { get; private set; }

        public FilterEventArgs(List<Func<ItemViewModel<T>, bool>> filterCallbacks)
        {
            FilterCallbacks = filterCallbacks;
        }
    }
}
