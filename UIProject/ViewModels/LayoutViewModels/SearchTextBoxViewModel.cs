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
    public class SearchTextBoxViewModel<T> : BaseViewModel
    {
        private ObservableCollection<ItemViewModel<T>> itemsSource;
        private ItemViewModel<T> selectedItem;

        public IEnumerable<ItemViewModel<T>> DisplayItems
        {
            get => GetPropertyValue<IEnumerable<ItemViewModel<T>>>();
            private set
            {
                SetProperty(value);
                if (value.Count() == 0)
                    OnDisplayItemsEmpty();
            }
        }
        
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

        public Func<ItemViewModel<T>,bool> Filter { get; set; } 

        public string Text
        {
            get => GetPropertyValue<string>();
            set
            {
                string oldValue = GetPropertyValue<string>();
                SetProperty(value);
                OnTextPropertyChanged(oldValue, value);
            }
        }

        public string EmptySourceNotifyText
        {
            get => GetPropertyValue<string>();
            set => SetProperty(value);
        }

        public SearchTextBoxViewModel(): this(null)
        {
        }

        public SearchTextBoxViewModel(ObservableCollection<T> itemsSource) : base()
        {
            this.itemsSource = new ObservableCollection<ItemViewModel<T>>();
            if (itemsSource != null)
                foreach(var item in itemsSource)
                {
                    this.itemsSource.Add(new ItemViewModel<T>(item));
                }
        }
        protected virtual void OnTextPropertyChanged(string oldValue, string newValue)
        {
            if (!string.IsNullOrEmpty(newValue))
                this.DisplayItems = itemsSource.Where(Filter);
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

        public event EventHandler<TextValueChangedEventArgs> TextChanged;
        public event EventHandler DisplayItemsEmpty;
        public event EventHandler<ItemSelectedEventArgs<T>> SelectItem;
    }

    public class SelectedItemChangedEventArgs : EventArgs
    {
        public object SelectedItem { get; private set; }
        public SelectedItemChangedEventArgs(object selectedItem)
        {
            this.SelectedItem = selectedItem;
        }
    }


}

