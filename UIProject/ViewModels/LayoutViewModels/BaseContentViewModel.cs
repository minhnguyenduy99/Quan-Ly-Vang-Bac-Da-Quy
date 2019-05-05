using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace UIProject.ViewModels.LayoutViewModels
{

    /// <summary>
    /// Base view model for <see cref="System.Windows.Controls.ContentControl"/>
    /// </summary>
    public class BaseContentViewModel<T> : BaseViewModel<T>
    {
        private string iconSource;

        /// <summary>
        /// The source image of icon of tab control
        /// </summary>
        public string IconSource
        {
            get => iconSource;
            set => SetProperty(ref iconSource, value);
        }

        /// <summary>
        /// Backgorund color of the tab
        /// </summary>
        public Brush Background { get; set; } = Brushes.White;

        /// <summary>
        /// Color of the tab name
        /// </summary>
        public Brush Foreground { get; set; } = Brushes.Black;

        /// <summary>
        /// The background of tab when it is focused or hover
        /// </summary>
        public Brush FocusBackground { get; set; } = Brushes.LightGray;


        /// <summary>
        /// The default style for the view model
        /// </summary>
        protected virtual void SetDefaultStyle()
        {
            this.Background = (Brush)Application.Current.FindResource("RoyalBlue");
            this.Foreground = Brushes.White;
        }
    }
}
