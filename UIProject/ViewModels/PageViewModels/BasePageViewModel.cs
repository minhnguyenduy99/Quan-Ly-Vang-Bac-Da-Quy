using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UIProject.UIConnector;
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
        private bool firstLoaded = true;
        private bool isLoadAsync = false;
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

        public bool IsLoadAsync
        {
            get => isLoadAsync;
            set => SetProperty(ref isLoadAsync, value);
        }

        /// <summary>
        /// The navigator that navigates this page 
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
        /// Perform loading and loading window asynchorously
        /// </summary>
        /// <param name="loadingWindow"></param>
        public async void LoadAsync(IWindow loadingWindow)
        {
            if (loadingWindow == null)
                Load();
            Task loadingTask = new Task(() => this.Load());
            loadingTask.Start();
            loadingWindow.Show();
            await loadingTask;
            loadingWindow.Close();
        }

        public async void ReloadAsync(IWindow loadingWindow)
        {
            if (loadingWindow == null)
                Load();
            Task loadingTask = new Task(() => this.Reload());
            loadingTask.Start();
            loadingWindow.Show();
            await loadingTask;
            loadingWindow.Close();
        }

        /// <summary>
        /// Event occurs when the page is navigated
        /// </summary>
        public event EventHandler Navigated;

        /// <summary>
        /// Internal handler when the page is navigated
        /// </summary>
        protected virtual void OnNavigated()
        {
            if (!firstLoaded)
            {
                Reload();
            }
            else
                firstLoaded = false;
            Navigated?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Load components of page 
        /// </summary>
        protected abstract override void LoadComponentsInternal();
        protected abstract override void ReloadComponentsInternal();
    }
}
