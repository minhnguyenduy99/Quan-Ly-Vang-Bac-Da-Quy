using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.Events
{
    public class FilterEventArgs<T> : EventArgs
    {
        public Func<T, bool> FilterCallback { get; private set; }

        public FilterEventArgs(Func<T, bool> filterCallback)
        {
            FilterCallback = filterCallback;
        }
    }
}
