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
    /// <summary>
    /// The filter view model for combobox-like filter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumFilterViewModel<T> : BaseFilterViewModel<T>
    {
        private ObservableCollectionViewModel<object> collection;
        public ObservableCollectionViewModel<object> Collection
        {
            get => collection;
            set
            {
                SetProperty(ref collection, value);
                Collection.Add(NonApplyFilterItem);
            }
        }

        public ItemViewModel<object> NonApplyFilterItem { get; private set; } = new ItemViewModel<object>(new object());

        public EnumFilterViewModel(List<Func<ItemViewModel<T>, bool>> filterCallbacks, IEnumerable<object> itemsSource) : base(filterCallbacks)
        {
            if (itemsSource == null)
                Collection = new ObservableCollectionViewModel<object>();
            else
                Collection = new ObservableCollectionViewModel<object>(itemsSource);

            this.Collection.Add(NonApplyFilterItem);
        }

        public EnumFilterViewModel() : this(null, null) { }

        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged
        {
            add { Collection.SelectedItemChanged += value; }
            remove { Collection.SelectedItemChanged -= value; }
        }
    }
}
