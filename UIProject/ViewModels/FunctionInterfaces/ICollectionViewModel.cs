using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.Events;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.FunctionInterfaces
{
    /// <summary>
    /// Represents a view model of collection
    /// </summary>
    public interface ICollectionViewModel<T>
    {
        ObservableCollection<ItemViewModel<T>> Items { get; }
        void Add(ItemViewModel<T> item);
        bool Contains(ItemViewModel<T> item);
        bool Remove(ItemViewModel<T> item);

        event EventHandler<ItemAddedEventArgs<T>> ItemAdded;

        event EventHandler<ItemRemovedEventArgs<T>> ItemRemoved;

        event EventHandler<ItemEventArgs<T>> ContainsItemModel;

    }
}
