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
                }
                OnSelectedItemChanged(new SelectedItemChangedEventArgs(selectedItem));
            }
        }      

        /// <summary>
        /// The items displayed in after the filter
        /// </summary>
        public ObservableCollection<ItemViewModel<T>> DisplayItems
        {
            get
            {
                return GetPropertyValue<ObservableCollection<ItemViewModel<T>>>();
            }
            private set
            {
                SetProperty(value);
                OnDisplayItemsChanged(new DisplayItemsChangedEventArgs<T>(value));
            }
        }

        /// <summary>
        /// The filter to applied to the collection
        /// </summary>
        public List<Func<ItemViewModel<T>, bool>> Filters
        {
            get => GetPropertyValue<List<Func<ItemViewModel<T>, bool>>>();
            private set
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

        public ObservableCollectionViewModel(): base()
        {
            Filters = new List<Func<ItemViewModel<T>, bool>>();
        }

        public ObservableCollectionViewModel(IEnumerable<T> itemsSource) : base(itemsSource)
        {
            Filters = new List<Func<ItemViewModel<T>, bool>>();
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


        /// <summary>
        /// Filter the collection with available <see cref="ObservableCollectionViewModel{T}.Filters"/>
        /// </summary>
        public void Filter()
        {
            if (Filters == null)
                DisplayItems = Items;
            else
                DisplayItems = GetUpdatedDisplayItems();
        }

        /// <summary>
        /// Remove the currently selected item in the collection
        /// </summary>
        /// <returns></returns>
        public bool RemoveCurrentItem()
        {
            return this.Remove(this.SelectedItem as ItemViewModel<T>);
        }

        /// <summary>
        /// Refresh the item source with new source
        /// </summary>
        /// <param name="itemsSource">The new source</param>
        public void RefreshItemsSource(IEnumerable<T> itemsSource)
        {
            ObservableCollection<ItemViewModel<T>> newItems = new ObservableCollection<ItemViewModel<T>>();
            Items.Clear();
            foreach (var item in itemsSource)
            {
                Items.Add(new ItemViewModel<T>(item));
            }
            OnItemsSourceRefresh();
        }

        protected virtual void OnItemsSourceRefresh()
        {
            Filter();
            Reload();
        }

        // Everytime an item is added to or remove from the collection, update the DisplayItems property
        #region Override add and move internal handler
        protected override void OnItemAdded(ItemAddedEventArgs<T> e)
        {
            base.OnItemAdded(e);
            if (Filters == null)
                return;
            Filter();
        }

        protected override void OnItemRemoved(ItemRemovedEventArgs<T> e)
        {
            base.OnItemRemoved(e);
            if (Filters == null)
                return;
            Filter();
        }
        #endregion



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
            Filter();
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


        protected new void LoadComponentsInternal()
        {
            SelectedItem = null;           
        }

        protected new void ReloadComponentsInternal()
        {
            SelectedItem = null;
        }


        private ObservableCollection<ItemViewModel<T>> GetUpdatedDisplayItems()
        {
            return new ObservableCollection<ItemViewModel<T>>(this.Filter(Filters));
        }
    }
}
