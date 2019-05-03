using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.Pages;
using UIProject.ServiceProviders;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// A view model provides functionalities for data searching and item selection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchTextBoxViewModel<T> : BaseViewModel
    {
        #region Private Fields
        private ItemViewModel<T> selectedItem;
        private ItemCollectionViewModel<T> itemsSource;
        #endregion

        /// <summary>
        /// The items satisfied the Filter
        /// </summary>
        public ObservableCollection<ItemViewModel<T>> DisplayItems
        {
            get
            {
                if (itemsSource == null)
                    return new ObservableCollection<ItemViewModel<T>>();
                return GetPropertyValue<ObservableCollection<ItemViewModel<T>>>();
            }
            private set
            {
                SetProperty(value);
                if (value.Count() == 0)
                    OnDisplayItemsEmpty();
            }
        }
        
        /// <summary>
        /// The item which is selected
        /// </summary>
        public ItemViewModel<T> SelectedItem
        {
            get => selectedItem;
            set
            {
                if (selectedItem != null)
                    selectedItem.IsSelected = false;
                SetProperty(ref selectedItem, value);
                if (value != null)
                {
                    selectedItem.IsSelected = true;
                    OnSelectedItem(new ItemSelectedEventArgs<T>(value));
                }
            }
        }

        /// <summary>
        /// The callback represents the filter function
        /// </summary>
        public Func<ItemViewModel<T>,bool>[] Filters { get; set; }

        /// <summary>
        /// The text for searching
        /// </summary>
        public string Text
        {
            get
            {
                return GetPropertyValue<string>() ?? string.Empty;
            }
            set
            {
                string oldValue = GetPropertyValue<string>();
                SetProperty(value);
                if (string.IsNullOrEmpty(value))
                    selectedItem = null;
                OnTextPropertyChanged(oldValue, value);
            }
        }

        /// <summary>
        /// The text notified the state of empty displayed items
        /// </summary>
        public string EmptySourceNotifyText
        {
            get => GetPropertyValue<string>();
            set => SetProperty(value);
        }

        #region Constructors
        public SearchTextBoxViewModel(): this(null)
        {
        }

        /// <summary>
        /// Create an instance of <see cref="SearchTextBoxViewModel{T}"/> 
        /// </summary>
        /// <param name="itemsSource">The item source of <see cref="SearchTextBoxViewModel{T}"/></param>
        public SearchTextBoxViewModel(ObservableCollection<T> itemsSource) : base()
        {
            this.itemsSource = new ItemCollectionViewModel<T>(itemsSource);
            if (itemsSource != null)
                foreach(var item in itemsSource)
                {
                    this.itemsSource.Add(new ItemViewModel<T>(item));
                }
        }

        #endregion

        #region Method executes the Event
        protected virtual void OnTextPropertyChanged(string oldValue, string newValue)
        {
            if (!string.IsNullOrEmpty(newValue))
                this.DisplayItems = new ObservableCollection<ItemViewModel<T>>(itemsSource.Filter(Filters));
            else
            {
                this.DisplayItems.Clear();
            }
            TextChanged?.Invoke(this, new TextValueChangedEventArgs(oldValue, newValue));
        }

        protected virtual void OnDisplayItemsEmpty()
        {
            DisplayItemsEmpty?.Invoke(this, EventArgs.Empty);              
        }

        protected virtual void OnSelectedItem(ItemSelectedEventArgs<T> e)
        {
            if (!string.IsNullOrEmpty(Text))
                Text = string.Empty;
            SelectItem?.Invoke(this, e);
        }
        #endregion

        #region Event Declaration
        public event EventHandler<TextValueChangedEventArgs> TextChanged;
        public event EventHandler DisplayItemsEmpty;
        public event EventHandler<ItemSelectedEventArgs<T>> SelectItem;
        #endregion
    }




}

