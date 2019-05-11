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
        private List<Func<ItemViewModel<T>,bool>> filterCallbacks;
        private bool isFilterEnabled;

        /// <summary>
        /// The filter callback that will be executed 
        /// </summary>
        public List<Func<ItemViewModel<T>, bool>> FilterCallBacks
        {
            get => filterCallbacks;
            set => SetProperty(ref filterCallbacks, value);
        }


        public BaseFilterViewModel(List<Func<ItemViewModel<T>, bool>> filterCallbacks) : base()
        {
            if (filterCallbacks == null)
                this.FilterCallBacks = new List<Func<ItemViewModel<T>, bool>>();
            else
                this.FilterCallBacks = filterCallbacks;
        }

        public BaseFilterViewModel() : this(null) { }


        public event EventHandler<object> FilterExecuted;


        protected virtual void OnFilterExecuted(FilterEventArgs<T> e)
        {
            FilterExecuted?.Invoke(this, e);
        }
    }
}
