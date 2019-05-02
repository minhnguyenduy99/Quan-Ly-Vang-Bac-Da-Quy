using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Events
{
    public class ItemEventArgs<T> : EventArgs
    {
        public ItemViewModel<T> Item { get; private set; }

        public ItemEventArgs(ItemViewModel<T> item)
        {
            Item = item;
        }
    }
}
