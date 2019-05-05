using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UIProject.Events;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.LayoutViewModels
{
    public class EnumFilterViewModel<T> : BaseFilterViewModel<T>
    {
        public ObservableCollectionViewModel<object> Collection { get; set; }

        public EnumFilterViewModel(Func<ItemViewModel<T>, bool> filterCallback, IEnumerable<object> itemsSource) : base(filterCallback)
        {
            Collection = new ObservableCollectionViewModel<object>(itemsSource);

        }

        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged
        {
            add { Collection.SelectedItemChanged += value; }
            remove { Collection.SelectedItemChanged -= value; }
        }
    }
}
