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
        public Func<ItemViewModel<T>, bool> FilterCallback { get; private set; }

        public FilterEventArgs(Func<ItemViewModel<T>, bool> filterCallback)
        {
            FilterCallback = filterCallback;
        }
    }
}
