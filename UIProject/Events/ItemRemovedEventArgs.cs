using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Events
{
    public class ItemRemovedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// The removed item (if it has been removed sucessfully)
        /// </summary>
        public ItemViewModel<T> RemovedItem { get; private set; }
        /// <summary>
        /// Indicating weither the item has been successfully removed
        /// </summary>
        public bool Result { get; private set; }

        public ItemRemovedEventArgs(ItemViewModel<T> removedItem)
        {
            RemovedItem = removedItem;
            if (removedItem == null)
                Result = false;
            else
                Result = true;
        }

        public ItemRemovedEventArgs(ItemViewModel<T> removedItem, bool result)
        {
            RemovedItem = removedItem;
            Result = result;
        }
    }
}
