using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// The View Model for <see cref="System.Windows.Controls.DataGrid"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollectionViewModel<T> : ItemCollectionViewModel<T>, IAsyncCurrentSelectedItem, IConditionalDisplayCollection<ItemViewModel<T>>
    {
        private ItemViewModel<T> selectedItem;

        private ICommand removeItemCmd;

        /// <summary>
        /// The current selected item 
        /// </summary>
        public ISelectable SelectedItem
        {
            get => selectedItem;
            set
            {
                if (selectedItem != null)
                    selectedItem.IsSelected = false;
                SetProperty(ref selectedItem, (ItemViewModel<T>)value);

                if (value != null)
                {
                    selectedItem.IsSelected = true;
                    OnSelectedItemChanged(new SelectedItemChangedEventArgs(selectedItem));
                }
            }
        }      

        /// <summary>
        /// The items displayed in after the filter
        /// </summary>
        public ObservableCollection<ItemViewModel<T>> DisplayItems
        {
            get
            {
                if (Filters == null)
                    return items;
                return GetUpdatedDisplayItems();
            }
            private set
            {
                OnDisplayItemsChanged(new DisplayItemsChangedEventArgs<T>(value));
            }
        }

        /// <summary>
        /// The filter to applied to the collection
        /// </summary>
        public IEnumerable<Func<ItemViewModel<T>, bool>> Filters
        {
            get => GetPropertyValue<IEnumerable<Func<ItemViewModel<T>, bool>>>();
            set
            {
                SetProperty(value);
                OnFilterUpdated();
            }
        }


        /// <summary>
        /// The command executes to remove the item out of the collection
        /// </summary>
        public ICommand RemoveItemCommand
        {
            get => removeItemCmd ?? new BaseCommand<object>(OnRemoveItemCommandExecute);
            set => removeItemCmd = value;
        }

        public ObservableCollectionViewModel(): base() { }

        public ObservableCollectionViewModel(IEnumerable<T> itemsSource) : base(itemsSource)
        {
        }

        /// <summary>
        /// Event occurs when the selected item changed
        /// </summary>
        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;
        
        /// <summary>
        /// The event occurs when the display items changed
        /// </summary>
        public event EventHandler<DisplayItemsChangedEventArgs<T>> DisplayItemsChanged;

        /// <summary>
        /// The event occurs when the filters changed
        /// </summary>
        public event EventHandler FiltersUpdated;

        public void UpdateDisplayItems()
        {
            DisplayItems = GetUpdatedDisplayItems();
        }

        protected virtual void OnSelectedItemChanged(SelectedItemChangedEventArgs e)
        {
            SelectedItemChanged?.Invoke(this, e);    
        }

        protected virtual void OnDisplayItemsChanged(DisplayItemsChangedEventArgs<T> e)
        {
            DisplayItemsChanged?.Invoke(this, e);
        }

        protected virtual void OnFilterUpdated()
        {
            UpdateDisplayItems();
            FiltersUpdated?.Invoke(this, EventArgs.Empty);
        }


        protected virtual void OnRemoveItemCommandExecute(object item)
        {
            ItemViewModel<T> castItem = item as ItemViewModel<T>;
            if (castItem != null)
            {
                this.Remove(castItem);
            }
        }



        private ObservableCollection<ItemViewModel<T>> GetUpdatedDisplayItems()
        {
            return (ObservableCollection<ItemViewModel<T>>)this.Filter(Filters.ToArray());
        }
    }
}
