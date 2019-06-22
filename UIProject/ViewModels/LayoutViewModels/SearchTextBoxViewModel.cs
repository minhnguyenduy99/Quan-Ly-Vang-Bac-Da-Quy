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
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// A view model provides functionalities for data searching and item selection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchTextBoxViewModel<T> : BaseViewModelObject<T>, ISearcher, IAsyncCurrentSelectedItem, IConditionalDisplayCollection<ItemViewModel<T>>
    {
        #region Private Fields
        private ItemViewModel<T> selectedItem;
        private ItemCollectionViewModel<T> itemsSource;
        private bool isTextChangedFromUI = true;
        #endregion

        /// <summary>
        /// Represents the search mode of <see cref="SearchTextBoxViewModel{T}"/>
        /// </summary>
        public SearchMode SearchMode { get; set; }

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
                if (!DisplayItems.Equals(value))
                    OnDisplayItemsChanged(new DisplayItemsChangedEventArgs<T>(value));
                if (value.Count() == 0)
                    OnDisplayItemsEmpty();
            }
        }
        
        /// <summary>
        /// The item which is selected
        /// </summary>
        public ISelectable SelectedItem
        {
            get => selectedItem;
            set
            {
                if (selectedItem != null)
                {
                    // The item is currently selected
                    if (value != null && value.Equals(selectedItem))
                    {
                        // Trigger for notification of selected item changes
                        selectedItem = null;
                    }
                    else
                    {
                        selectedItem.IsSelected = false;
                    }
                }
                SetProperty(ref selectedItem, (ItemViewModel<T>)value);
                if (value != null)
                {
                    selectedItem.IsSelected = true;
                }
                OnSelectedItem(new SelectedItemChangedEventArgs((ItemViewModel<T>)value));
            }
        }


        /// <summary>
        /// All filters applied to the searching
        /// </summary>
        public List<Func<ItemViewModel<T>, bool>> Filters { get; private set; }

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
                if (isTextChangedFromUI)
                    OnTextPropertyChanged(oldValue, value);
                else
                {
                    isTextChangedFromUI = true;
                }
            }
        }

        /// <summary>
        /// Get the value path for searching
        /// </summary>
        public string SelectedValuePath
        {
            get => GetPropertyValue<string>();
            set => SetProperty(value);
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
        public SearchTextBoxViewModel(IEnumerable<T> itemsSource) : this(new ObservableCollection<T>(itemsSource)) { }

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

        /// <summary>
        /// Refresh the items source 
        /// </summary>
        /// <param name="itemsSource">The new items source applied to the <see cref="SearchTextBoxViewModel{T}"/></param>
        public void RefreshItemSource(IEnumerable<T> itemsSource)
        {
            this.itemsSource = new ItemCollectionViewModel<T>(itemsSource);
            if (itemsSource != null)
                foreach (var item in itemsSource)
                {
                    this.itemsSource.Add(new ItemViewModel<T>(item));
                }

            Reload();
        }
        #endregion

        #region Method executes the Event
        protected virtual void OnTextPropertyChanged(string oldValue, string newValue)
        {
            // Avoid the case that the changed text does not match any item
            SelectedItem = null;

            if (!string.IsNullOrEmpty(newValue))
            {
                this.DisplayItems = new ObservableCollection<ItemViewModel<T>>(itemsSource.Filter(Filters));                 
            }
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

        protected virtual void OnSelectedItem(SelectedItemChangedEventArgs e)
        {
            var castSelectedItem = SelectedItem as ItemViewModel<T>;
            if (castSelectedItem != null)
            {
                isTextChangedFromUI = false;
                if (string.IsNullOrEmpty(SelectedValuePath))
                    Text = string.Empty;
                else
                    Text = ObservableObject.GetPropValue(castSelectedItem.Model, SelectedValuePath).ToString();             
            }
            SelectedItemChanged?.Invoke(this, e);
        }

        protected virtual void OnDisplayItemsChanged(DisplayItemsChangedEventArgs<T> e)
        {
            DisplayItemsChanged?.Invoke(this, e);
        }



        protected override void LoadComponentsInternal()
        {
            Filters = new List<Func<ItemViewModel<T>, bool>>();
            DisplayItems = new ObservableCollection<ItemViewModel<T>>();
            ReloadComponentsInternal();
        }

        protected override void ReloadComponentsInternal()
        {
            Text = string.Empty;
            SelectedItem = null;
            if (DisplayItems != null)
                DisplayItems.Clear();
            else
            {
                DisplayItems = new ObservableCollection<ItemViewModel<T>>();
            }
        }
        #endregion

        #region Event Declaration
        public event EventHandler<TextValueChangedEventArgs> TextChanged;
        public event EventHandler DisplayItemsEmpty;
        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;
        public event EventHandler<DisplayItemsChangedEventArgs<T>> DisplayItemsChanged;
        #endregion


    }
}

