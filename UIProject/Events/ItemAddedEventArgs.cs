using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Events
{
    public class ItemAddedEventArgs<T> : EventArgs
    {
        public ItemViewModel<T> AddedItem { get; private set; }

        public ItemAddedEventArgs(ItemViewModel<T> addedItem)
        {
            this.AddedItem = addedItem;
        }
    }
}
