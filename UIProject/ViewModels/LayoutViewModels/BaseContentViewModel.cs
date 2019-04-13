using BaseMVVM_Service.BaseMVVM;
using ModelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// Base view model for content control with a specifiec Data Model
    /// </summary>
    public abstract class BaseContentViewModel<T> : BaseViewModel<T> where T:BaseDataModel
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

        public BaseContentViewModel(T modelData)
        {
            this.ModelData = modelData;
        }

    }

    /// <summary>
    /// Base view model for ContentControl without a specified Data Model
    /// </summary>
    public abstract class BaseContentViewModel : BaseViewModel
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
    }
}
