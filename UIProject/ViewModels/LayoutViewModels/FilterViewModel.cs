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

        /// <summary>
        /// The filter callback that will be executed 
        /// </summary>
        public Func<ItemViewModel<T>, bool> FilterCallBack
        {
            get => filterCallback;
            set => SetProperty(ref filterCallback, value);
        }


        public BaseFilterViewModel(Func<ItemViewModel<T>, bool> filterCallback) : base()
        {
            if (filterCallback == null)
                this.FilterCallBack = new Func<ItemViewModel<T>, bool>((item) => true);
            else
                this.FilterCallBack = filterCallback;
        }

        public BaseFilterViewModel() : this(null) { }



        public event EventHandler<object> FilterExecuted;
        protected virtual void OnFilterExecuted(FilterEventArgs<T> e)
        {
            FilterExecuted?.Invoke(this, e);
        }
    }
}
