using ModelProject.Models;
using ModelProject.DataViewModels;
using UIProject.ViewModels.PageViewModels;
using UIProject.ViewModels.LayoutViewModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;


namespace UIProject.ViewModels
{
    /// <summary>
    /// View model of tab control
    /// </summary>
    public class TabViewModel : BaseContentViewModel
    {


        #region Private Fields
        private string tabName;
        private TabState state;
        private bool isChecked;
        #endregion


        /// <summary>
        /// Indicating the status of tab in a specified window
        /// </summary>
        public enum TabState
        {
            New,
            Idle,
            Process
        }

        /// <summary>
        /// The name of tab control
        /// </summary>
        public string TabName
        {
            get => this.tabName;
            set => SetProperty(ref tabName, value);
        }

        /// <summary>
        /// Status of the tab
        /// </summary>
        public TabState State
        {
            get => state;
            set => SetProperty(ref state, value);
        }

        /// <summary>
        /// Weither the tab is selected
        /// </summary>
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                SetProperty(ref isChecked, value);
                if (isChecked == true)
                    OnTabSelected(this.tabName);
            }
        }

        /// <summary>
        /// Create an instance of <see cref="TabViewModel"/>
        /// </summary>
        public TabViewModel() : base()
        {
        }

        /// <summary>
        /// Create an instance of TabViewModel class 
        /// </summary>
        /// <param name="tabName">The name of tab</param>
        /// <param name="iconSourceKey">The source key of the icon in the source dictionary</param>
        /// <param name="state">The state of tab</param>
        /// <param name="nhanVienVM"></param>
        /// <returns></returns>
        public static TabViewModel CreateTabViewModel(string tabName, string iconSourceKey, TabState state)
        {
            return new TabViewModel()
            {
                TabName = tabName,
                IconSource = (string)Application.Current.FindResource(iconSourceKey),
                State = state,
                Background = (Brush)Application.Current.FindResource("RoyalBlue"),
                Foreground = Brushes.White,
                FocusBackground = (Brush)Application.Current.FindResource("AzureBlue"),
                IsChecked = false
            };
        }

        /// <summary>
        /// Executes the TabSelected event
        /// </summary>
        /// <param name="tabName"></param>
        protected virtual void OnTabSelected(string tabName)
        {
            TabSelected?.Invoke(this, new TabSelectedEventArgs(tabName));
        }


        /// <summary>
        /// Event raises when the focus tab changed
        /// </summary>
        public event EventHandler<TabSelectedEventArgs> TabSelected;
    }


    /// <summary>
    /// Provides information about TabFocusChanged event
    /// </summary>
    public class TabSelectedEventArgs: EventArgs
    {
        /// <summary>
        /// The tab that raises the event
        /// </summary>
        public string TabName { get; private set; }

        public TabSelectedEventArgs(string tabName)
        {
            this.TabName = tabName;
        }
    }
}
