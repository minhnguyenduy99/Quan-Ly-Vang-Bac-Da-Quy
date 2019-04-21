using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// The view model of <see cref="Expander"/> control
    /// </summary>
    public class ExpandTabViewModel : TabViewModel, IExpandableVM
    {
        private bool isExpanded;

        /// <summary>
        /// Indicating weither the subtab is expanded
        /// </summary>
        public bool IsExpanded
        {
            get => isExpanded;
            set => SetProperty(ref isExpanded, value);
        }

        /// <summary>
        /// The children of the expand tab
        /// </summary>
        public List<BaseContentViewModel> Children { get; set; }

        public ExpandTabViewModel() : base()
        {
           // this.IsExpanded = false;
        }
        protected override void OnTabSelected(string tabName)
        {
            this.IsExpanded = true;
            base.OnTabSelected(tabName);
        }

        /// <summary>
        /// Create an instance of TabViewModel class 
        /// </summary>
        /// <param name="tabName">The name of tab</param>
        /// <param name="iconSourceKey">The source key of the icon in the source dictionary</param>
        /// <param name="state">The state of tab</param>
        /// <returns></returns>
        public static ExpandTabViewModel CreateExpandTabViewModel(string tabName, string iconSourceKey, TabState state,
            List<BaseContentViewModel> childrenTab)
        {
            return new ExpandTabViewModel()
            {
                TabName = tabName,
                IconSource = (string)Application.Current.FindResource(iconSourceKey),
                State = state,
                Background = (Brush)Application.Current.FindResource("RoyalBlue"),
                Foreground = Brushes.White,
                FocusBackground = (Brush)Application.Current.FindResource("AzureBlue"),
                IsChecked = false,
                Children = childrenTab
            };
        }
    }
}
