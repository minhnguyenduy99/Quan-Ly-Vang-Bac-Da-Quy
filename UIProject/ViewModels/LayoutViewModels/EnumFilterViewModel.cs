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
        #region Private Fields
        private ObservableCollectionViewModel<object> collection;
        private bool isApplyNonFilterItem;
        #endregion


        /// <summary>
        /// Indicating weither a non-filter item is used in the filtering
        /// </summary>
        public bool IsApplyNonFilterItem
        {
            get => isApplyNonFilterItem;
            set
            {
                SetProperty(ref isApplyNonFilterItem, value);
                if (value == true)
                {
                    Collection?.Add(NonApplyFilterItem);
                }
                else
                {
                    Collection?.Remove(NonApplyFilterItem);
                }
            }
        }

        /// <summary>
        /// The source item of the filter
        /// </summary>
        public ObservableCollectionViewModel<object> Collection
        {
            get => collection;
            set
            {
                SetProperty(ref collection, value);
                Collection.Add(NonApplyFilterItem);
            }
        }

        /// <summary>
        /// The non-apply filter item
        /// </summary>
        public ItemViewModel<object> NonApplyFilterItem { get; private set; } = new ItemViewModel<object>(new object());

        /// <summary>
        /// Create an instance of <see cref="EnumFilterViewModel{T}"/>, allow collection filter
        /// </summary>
        /// <param name="filterCallback">The filter callback applied to this filter</param>
        /// <param name="itemsSource">The source collection of the filter</param>
        public EnumFilterViewModel(Func<ItemViewModel<T>, bool> filterCallback, IEnumerable<object> itemsSource) : base(filterCallback)
        {
            if (itemsSource == null)
                Collection = new ObservableCollectionViewModel<object>();
            else
                Collection = new ObservableCollectionViewModel<object>(itemsSource);
        }

        /// <summary>
        /// Create an instance of <see cref="EnumFilterViewModel{T}"/>
        /// </summary>
        public EnumFilterViewModel() : this(null, null) { }

        /// <summary>
        /// Refresh the items source of the <see cref="EnumFilterViewModel{T}"/>
        /// </summary>
        /// <param name="newSource">The new source to applied</param>
        public void RefreshItemsSource(IEnumerable<object> newSource)
        {
            Collection.Clear();
            foreach(var newItem in newSource)
            {
                Collection.Add(newItem);
            }
            if (IsApplyNonFilterItem)
            {
                Collection.Add(NonApplyFilterItem);
            }
        }

        /// <summary>
        /// Event occurs when the selected item of the collection changed
        /// </summary>
        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged
        {
            add { Collection.SelectedItemChanged += value; }
            remove { Collection.SelectedItemChanged -= value; }
        }

        
        protected override void LoadComponentsInternal()
        {
            if (collection != null)
                Collection.Load();
        }
        protected override void ReloadComponentsInternal()
        {
            if (Collection != null)
                Collection.Reload();
        }
    }
}
