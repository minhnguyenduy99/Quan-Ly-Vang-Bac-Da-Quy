using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// The View Model for <see cref="System.Windows.Controls.DataGrid"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataGridViewModel<T> : BaseViewModel
    {
        private ObservableCollection<ItemViewModel<T>> listItems;
        private T selectedItem;

        private ICommand addItemCmd;
        private ICommand removeItemCmd;
        private ICommand selectItemCmd;

        /// <summary>
        /// The item collection in the <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public ObservableCollection<ItemViewModel<T>> Items
        {
            get => listItems;
        }

        /// <summary>
        /// The item which is selected
        /// </summary>
        public T SelectedItem
        {
            get => selectedItem;
            set
            {
                SetProperty(ref selectedItem, value);
            }
        }
        
        public ICommand RemoveItemCommand
        {
            get => removeItemCmd ?? new BaseCommand<object>(OnRemoveItemCommandExecute);
            set => this.removeItemCmd = value;
        }

        public DataGridViewModel() : base()
        {
            this.listItems = new ObservableCollection<ItemViewModel<T>>();
        }


        /// <summary>
        /// Remove item from the <see cref="DataGridViewModel{T}"/>
        /// </summary>
        /// <param name="item">The item needed to add/param>
        public void Add(ItemViewModel<T> item)
        {
            if (!Contains(item))
                Items.Add(item);
            else
            {
                OnContainsItemModel(new ItemEventArgs<T>(item));
            }
            OnItemAdded(new ItemAddedEventArgs<T>(item));
        }

        public void Add(T itemModel)
        {
            ItemViewModel<T> item = null;
            if (!Contains(itemModel))
            {
                item = new ItemViewModel<T>(itemModel);
                Items.Add(item);
            }
            else
            {
                var containedItem = Items.Where(currrentItem => currrentItem.Model.Equals(itemModel)).ElementAt(0);
                OnContainsItemModel(new ItemEventArgs<T>(containedItem));
            }
            OnItemAdded(new ItemAddedEventArgs<T>(item));
        }

        /// <summary>
        /// Indicating weither the item collection contains a specified item model
        /// </summary>
        /// <param name="item">The specified item</param>
        /// <returns></returns>
        public bool Contains(ItemViewModel<T> item)
        {
            return Contains(item.Model);
        }

        public bool Contains(T item)
        {
            var resultCount = Items.Where(currentItem => currentItem.Model.Equals(item)).Count();
            if (resultCount == 0)
                return false;
            return true;
        }

        /// <summary>
        /// Remove item from the <see cref="DataGridViewModel{T}"/>
        /// </summary>
        /// <param name="item">The item needed to remove</param>
        /// <returns>Result indicating weither the item has been successfully removed</returns>
        public bool Remove(ItemViewModel<T> item)
        {
            bool removeSuccess = Items.Remove(item);
            OnItemRemoved(new ItemRemovedEventArgs<T>(item, removeSuccess));
            return removeSuccess;
        }

        public bool Remove(T itemModel)
        {
            ItemViewModel<T> item = Items.Where(currentItem => currentItem.Model.Equals(itemModel)).ElementAt(0);
            return Remove(item);
        }

        /// <summary>
        /// Event occurs when item is added to <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public event EventHandler<ItemAddedEventArgs<T>> ItemAdded;

        /// <summary>
        /// Event occurs when item is removed from <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public event EventHandler<ItemRemovedEventArgs<T>> ItemRemoved;
        
        /// <summary>
        /// Evenr occurs when model of item is already in the <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public event EventHandler<ItemEventArgs<T>> ContainsItemModel;


        protected virtual void OnItemAdded(ItemAddedEventArgs<T> e)
        {
            ItemAdded?.Invoke(this, e);
        }
        protected virtual void OnItemRemoved(ItemRemovedEventArgs<T> e)
        {
            ItemRemoved?.Invoke(this, e);
        }
        protected virtual void OnRemoveItemCommandExecute(object item)
        {
            var itemVM = item as ItemViewModel<T>;
            if (itemVM != null)
                this.Remove(itemVM);
        }
        protected virtual void OnContainsItemModel(ItemEventArgs<T> e)
        {
            ContainsItemModel?.Invoke(this, e);
        }
    }
}
