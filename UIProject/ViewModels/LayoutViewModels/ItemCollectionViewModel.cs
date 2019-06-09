using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.Events;
using UIProject.ServiceProviders;

namespace UIProject.ViewModels.LayoutViewModels
{
    public class ItemCollectionViewModel<Model> : BaseViewModel
    {
        protected ObservableCollection<ItemViewModel<Model>> items;
       
        public ObservableCollection<ItemViewModel<Model>> Items
        {
            get => items;
        }

        /// <summary>
        /// Retrieves the list of models only 
        /// </summary>
        public List<Model> Models
        {
            get
            {
                List<Model> models = new List<Model>();
                foreach (var item in Items)
                {
                    models.Add(item.Model);
                }
                return models;
            }
        }

        public ItemViewModel<Model> this[int index]
        {
            get => Items[index];
        }

        #region Constructors
        public ItemCollectionViewModel() : this(null)
        {
        }


        public ItemCollectionViewModel(IEnumerable<Model> itemsSource) : base()
        {
            this.items = new ObservableCollection<ItemViewModel<Model>>();
            if (itemsSource != null)
                foreach (var item in itemsSource)
                {
                    this.items.Add(new ItemViewModel<Model>(item));
                }
        }

        #endregion

        #region Methods provides functionalities for DataGridViewModel
        /// <summary>
        /// Add an <see cref="ItemViewModel{T}"/> to the <see cref="DataGridViewModel{T}"/>
        /// </summary>
        /// <param name="item">The item needed to add/param>
        public void Add(ItemViewModel<Model> item)
        {
            if (!Contains(item))
                Items.Add(item);
            else
            {
                OnContainsItemModel(new ItemEventArgs<Model>(item));
            }
            OnItemAdded(new ItemAddedEventArgs<Model>(item));
        }

        /// <summary>
        /// Create an instance of <see cref="ItemViewModel{T}"/> with specified <see cref="T"/> model
        /// </summary>
        /// <param name="itemModel"></param>
        public void Add(Model itemModel)
        {
            ItemViewModel<Model> item = null;
            if (!Contains(itemModel))
            {
                item = new ItemViewModel<Model>(itemModel);
                Items.Add(item);
                OnItemAdded(new ItemAddedEventArgs<Model>(item));
            }
            else
            {
                var containedItem = Items.Where(currrentItem => currrentItem.Model.Equals(itemModel)).ElementAt(0);
                OnContainsItemModel(new ItemEventArgs<Model>(containedItem));
            }

        }

        /// <summary>
        /// Indicating weither the item collection contains a specified <see cref="ItemViewModel{T}"/>
        /// </summary>
        /// <param name="item">The specified item</param>
        /// <returns></returns>
        public bool Contains(ItemViewModel<Model> item)
        {
            return Contains(item.Model);
        }

        /// <summary>
        /// Indicating weither the item collection contains a specified item model <see cref="T"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(Model item)
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
        public bool Remove(ItemViewModel<Model> item)
        {
            bool removeSuccess = Items.Remove(item);
            OnItemRemoved(new ItemRemovedEventArgs<Model>(item, removeSuccess));
            return removeSuccess;
        }

        public bool Remove(Model itemModel)
        {
            ItemViewModel<Model> item = Items.Where(currentItem => currentItem.Model.Equals(itemModel)).ElementAt(0);
            return Remove(item);
        }

        public IEnumerable<ItemViewModel<Model>> Filter(Func<ItemViewModel<Model>, bool> filterFunction)
        {
            return FilterHelper.Filter(items, filterFunction);
        }

        public IEnumerable<ItemViewModel<Model>> Filter(params Func<ItemViewModel<Model>, bool>[] filters)
        {
            return FilterHelper.Filter(items, filters);
        }

        #endregion

        #region Event Declaration
        /// <summary>
        /// Event occurs when item is added to <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public event EventHandler<ItemAddedEventArgs<Model>> ItemAdded;

        /// <summary>
        /// Event occurs when item is removed from <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public event EventHandler<ItemRemovedEventArgs<Model>> ItemRemoved;

        /// <summary>
        /// Evenr occurs when model of item is already in the <see cref="DataGridViewModel{T}"/>
        /// </summary>
        public event EventHandler<ItemEventArgs<Model>> ContainsItemModel;
        #endregion


        #region Methods invoke the events 
        protected virtual void OnItemAdded(ItemAddedEventArgs<Model> e)
        {
            ItemAdded?.Invoke(this, e);
        }
        protected virtual void OnItemRemoved(ItemRemovedEventArgs<Model> e)
        {
            ItemRemoved?.Invoke(this, e);
        }
        protected virtual void OnContainsItemModel(ItemEventArgs<Model> e)
        {
            ContainsItemModel?.Invoke(this, e);
        }
        #endregion

    }
}
