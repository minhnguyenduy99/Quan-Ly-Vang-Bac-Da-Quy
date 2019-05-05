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
    public class ObservableCollectionViewModel<T> : ItemCollectionViewModel<T>, IAsyncCurrentSelectedItem
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

        public ICommand RemoveItemCommand
        {
            get => removeItemCmd ?? new BaseCommand<object>(OnRemoveItemCommandExecute);
            set => removeItemCmd = value;
        }

        public ObservableCollectionViewModel(): base() { }
        public ObservableCollectionViewModel(IEnumerable<T> itemsSource) : base(itemsSource)
        {
        }

        public event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;

        protected virtual void OnSelectedItemChanged(SelectedItemChangedEventArgs e)
        {
            SelectedItemChanged?.Invoke(this, e);    
        }


        protected virtual void OnRemoveItemCommandExecute(object item)
        {
            ItemViewModel<T> castItem = item as ItemViewModel<T>;
            if (castItem != null)
            {
                Items.Remove(castItem);
            }
        }
    }
}
