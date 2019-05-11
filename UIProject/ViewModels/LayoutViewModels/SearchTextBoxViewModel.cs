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
    public class SearchTextBoxViewModel<T> : BaseViewModel, IAsyncCurrentSelectedItem, IConditionalDisplayCollection<ItemViewModel<T>>
    {
        #region Private Fields
        private ItemViewModel<T> selectedItem;
        private ItemCollectionViewModel<T> itemsSource;
        private bool isTextChangedFromUI = true;
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
                    selectedItem.IsSelected = false;
                SetProperty(ref selectedItem, (ItemViewModel<T>)value);
                if (value != null)
                {
                    selectedItem.IsSelected = true;
                    OnSelectedItem(new SelectedItemChangedEventArgs((ItemViewModel<T>)value));
                }
            }
        }

        /// <summary>
        /// The default filter callback for <see cref="SearchTextBoxViewModel{T}"/>
        /// </summary>
        public Func<ItemViewModel<T>, bool> DefaultFilter { get; set; }

        /// <summary>
        /// The array of filter callbacks added for filtering 
        /// </summary>
        public List<Func<ItemViewModel<T>, bool>> AdditionFilters { get; set; }

        /// <summary>
        /// All filters applied to the searching
        /// </summary>
        public IEnumerable<Func<ItemViewModel<T>, bool>> Filters
        {
            get => GetAllFilters();
            set { }
        }

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
        public SearchTextBoxViewModel(IEnumerable<T> itemsSource) : this(null) { }

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

            AdditionFilters = new List<Func<ItemViewModel<T>, bool>>();
        }

        #endregion

        #region Method executes the Event
        protected virtual void OnTextPropertyChanged(string oldValue, string newValue)
        {
            if (!string.IsNullOrEmpty(newValue))
            {
                this.DisplayItems = new ObservableCollection<ItemViewModel<T>>(itemsSource.Filter(Filters.ToArray()));                  
            }
            else
            {
                this.DisplayItems.Clear();
            }
            TextChanged?.Invoke(this, new TextValueChangedEventArgs(oldValue, newValue));
        }

        /// <summary>
        /// Combines the DefaultFilter and AdditionFilters into one array
        /// </summary>
        /// <returns></returns>
        private Func<ItemViewModel<T>, bool>[] GetAllFilters()
        {
            if (AdditionFilters == null)
                return new Func<ItemViewModel<T>, bool>[] { DefaultFilter };

            Func<ItemViewModel<T>, bool>[] filters = new Func<ItemViewModel<T>, bool>[AdditionFilters.Count + 1];
            filters[0] = DefaultFilter;
            for (int i=1;i< filters.Length; i++)
            {
                filters[i] = AdditionFilters[i - 1];
            }

            return filters;
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
        #endregion

        #region Event Declaration
        public event EventHandler<TextValueChangedEventArgs> TextChanged;
        public event EventHandler DisplayItemsEmpty;
        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;
        public event EventHandler<DisplayItemsChangedEventArgs<T>> DisplayItemsChanged;
        #endregion
    }




}

