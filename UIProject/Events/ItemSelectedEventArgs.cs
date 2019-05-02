using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Events
{
    /// <summary>
    /// Provides information about <see cref="ItemViewModel.Selected"/> event
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ItemSelectedEventArgs<T> : EventArgs
    {
        /// <summary>
        /// The item which is selected
        /// </summary>
        public ItemViewModel<T> Item { get; private set; }
        
        public ItemSelectedEventArgs(ItemViewModel<T> item)
        {
            Item = item;
        }
    }
}
