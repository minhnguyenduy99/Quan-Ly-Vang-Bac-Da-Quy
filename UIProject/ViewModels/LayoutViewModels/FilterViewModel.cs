using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// Base view model for filter functionality 
    /// </summary>
    public abstract class BaseFilterViewModel<T> : BaseViewModel
    {
        private Func<ItemViewModel<T>,bool> filterCallback;
        private bool isFilterEnabled;

        /// <summary>
        /// The filter callback that will be executed 
        /// </summary>
        public Func<ItemViewModel<T>, bool> FilterCallBack
        {
            get => filterCallback;
            set => SetProperty(ref filterCallback, value);
        }

        /// <summary>
        /// Indicating weither the filter is enabled
        /// </summary>
        public bool IsFilterEnabled
        {
            get => isFilterEnabled;
            set
            {
                SetProperty(ref isFilterEnabled, value);
                if (value == false)
                    FilterCallBack = (item) => true;
            }
        }

        public BaseFilterViewModel(Func<ItemViewModel<T>, bool> filterCallback) : base()
        {
            this.FilterCallBack = filterCallback;
        }

        public event EventHandler<object> FilterExecuted;


        protected virtual void OnFilterExecuted(FilterEventArgs<T> e)
        {
            FilterExecuted?.Invoke(this, e);
        }
        
    }
}
