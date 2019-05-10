using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UIProject.ViewModels.PageViewModels
{
    /// <summary>
    /// A base view model of page
    /// </summary>
    public abstract class BasePageViewModel : BaseViewModel
    {
        private bool takeFullScreen = false;

        /// <summary>
        /// Indicating weither the page should take all window area
        /// </summary>
        public bool TakeFullScreen
        {
            get => takeFullScreen;
            set => SetProperty(ref takeFullScreen, value);
        }

        public BasePageViewModel() : base()
        {
            LoadPageComponents();
        }

        /// <summary>
        /// Load components of page 
        /// </summary>
        protected abstract void LoadPageComponents();
    }
}
