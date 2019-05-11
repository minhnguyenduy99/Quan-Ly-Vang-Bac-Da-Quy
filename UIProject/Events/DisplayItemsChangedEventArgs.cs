using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Events
{
    public class DisplayItemsChangedEventArgs<T> : EventArgs
    {
        public ObservableCollection<ItemViewModel<T>> Items { get; private set; }

        public DisplayItemsChangedEventArgs(ObservableCollection<ItemViewModel<T>> items)
        {
            Items = items;
        }
    }
}
