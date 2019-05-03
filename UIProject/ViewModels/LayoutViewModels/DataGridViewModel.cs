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
        #region Private Fields
        private ItemCollectionViewModel<T> listItems;
        private T selectedItem;

        private ICommand addItemCmd;
        private ICommand removeItemCmd;
        private ICommand selectItemCmd;
        #endregion
        
        public ItemViewModel<T> this[int index] => listItems[index];

        /// <summary>
        /// The item collection in the <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public ObservableCollection<ItemViewModel<T>> Items
        {
            get => listItems.Items;
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
        
        /// <summary>
        /// Command executes for remove the item. It requires an <see cref="object"/> needed to remove
        /// </summary>
        public ICommand RemoveItemCommand
        {
            get => removeItemCmd ?? new BaseCommand<object>(OnRemoveItemCommandExecute);
            set => this.removeItemCmd = value;
        }

        #region Constructors
        public DataGridViewModel() : base()
        {
            this.listItems = new ItemCollectionViewModel<T>();
        }

        public DataGridViewModel(IEnumerable<T> itemsSource)
        {
            listItems = new ItemCollectionViewModel<T>(itemsSource);
        }

        #endregion


        #region Methods provides functionalities for DataGridViewModel
        /// <summary>
        /// Add an <see cref="ItemViewModel{T}"/> to the <see cref="DataGridViewModel{T}"/>
        /// </summary>
        /// <param name="item">The item needed to add/param>
        public void Add(ItemViewModel<T> item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Create an instance of <see cref="ItemViewModel{T}"/> with specified <see cref="T"/> model
        /// </summary>
        /// <param name="itemModel"></param>
        public void Add(T itemModel)
        {
            listItems.Add(itemModel);
        }

        /// <summary>
        /// Indicating weither the item collection contains a specified <see cref="ItemViewModel{T}"/>
        /// </summary>
        /// <param name="item">The specified item</param>
        /// <returns></returns>
        public bool Contains(ItemViewModel<T> item)
        {
            return listItems.Contains(item);
        }

        /// <summary>
        /// Indicating weither the item collection contains a specified item model <see cref="T"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return listItems.Contains(item);
        }

        /// <summary>
        /// Remove item from the <see cref="DataGridViewModel{T}"/>
        /// </summary>
        /// <param name="item">The item needed to remove</param>
        /// <returns>Result indicating weither the item has been successfully removed</returns>
        public bool Remove(ItemViewModel<T> item)
        {
            return listItems.Remove(item);
        }

        public bool Remove(T itemModel)
        {
            return listItems.Remove(itemModel);
        }
        #endregion


        #region Event Declaration
        /// <summary>
        /// Event occurs when item is added to <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public event EventHandler<ItemAddedEventArgs<T>> ItemAdded
        {
            add { listItems.ItemAdded += value; }
            remove { listItems.ItemAdded -= value; }
        }

        /// <summary>
        /// Event occurs when item is removed from <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public event EventHandler<ItemRemovedEventArgs<T>> ItemRemoved
        {
            add { listItems.ItemRemoved += value; }
            remove { listItems.ItemRemoved -= value; }
        }
        
        /// <summary>
        /// Evenr occurs when model of item is already in the <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public event EventHandler<ItemEventArgs<T>> ContainsItemModel
        {
            add { listItems.ContainsItemModel += value; }
            remove { listItems.ContainsItemModel -= value; }
        }
        #endregion

        #region Method executes the command
        protected virtual void OnRemoveItemCommandExecute(object item)
        {
            var itemVM = item as ItemViewModel<T>;
            if (itemVM != null)
                this.Remove(itemVM);
        }
        #endregion
    }
}
