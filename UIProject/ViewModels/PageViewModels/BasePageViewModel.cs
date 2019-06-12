using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    /// <summary>
    /// A base view model of page
    /// </summary>
    public abstract class BasePageViewModel : BaseViewModelObject, INavigatable
    {
        #region Private Fields
        private bool takeFullScreen;
        private bool isNavigated;
        #endregion

        /// <summary>
        /// Indicating weither the page should take all window area
        /// </summary>
        public bool TakeFullScreen
        {
            get => takeFullScreen;
            set => SetProperty(ref takeFullScreen, value);
        }
        
        /// <summary>
        /// Indicate if the page instance is currently navigated
        /// </summary>
        public bool IsNavigated
        {
            get => isNavigated;
            set
            {
                SetProperty(ref isNavigated, value);
                if (value == true)
                    OnNavigated();
            }
        } 

        /// <summary>
        /// The navigator that holds this page 
        /// </summary>
        public INavigator Navigator { get; set; }

        /// <summary>
        /// Creat an instance of <see cref="BasePageViewModel"/>
        /// </summary>
        public BasePageViewModel() : this(null)
        {
        }

        /// <summary>
        /// Create an instance of <see cref="BasePageViewModel"/>
        /// </summary>
        /// <param name="navigator">The navigator that holds this page</param>
        public BasePageViewModel(INavigator navigator) : base()
        {
            this.Navigator = navigator;
            takeFullScreen = false;
        }

        /// <summary>
        /// Event occurs when the page is navigated
        /// </summary>
        public event EventHandler Navigated;

        protected virtual void OnNavigated()
        {
            Reload();
            Navigated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Load components of page 
        /// </summary>
        protected abstract override void LoadComponentsInternal();
        protected abstract override void ReloadComponentsInternal();
    }
}
