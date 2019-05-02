using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UIProject.ViewModels.LayoutViewModels
{
    public class SearchAndSelectViewModel<T> : BaseViewModel
    {
        private ICollectionView items;
        private T selectedItem;

        public ICollectionView Items
        {
            get => items;
        }

        public T SelectedItem
        {
            get => SelectedItem;
            set
            {
                if (!selectedItem.Equals(value))
                    selectedItem = value;
            }
        }

        public SearchAndSelectViewModel() : this(null) { }
        public SearchAndSelectViewModel(IEnumerable<T> itemsSource) : base()
        {
            SetupItemSource(itemsSource);
        }

        

        protected virtual void SetupItemSource(IEnumerable<T> itemsSource)
        {
            this.items = CollectionViewSource.GetDefaultView(itemsSource);

            this.items.Filter = FilterSource;
            ((ICollectionViewLiveShaping)items).IsLiveFiltering = true;
        }
        
        protected virtual bool FilterSource(object obj)
        {
            T castObj = (T)obj;
            return items.Cast<T>().Contains(castObj);   
        }
    }
}
