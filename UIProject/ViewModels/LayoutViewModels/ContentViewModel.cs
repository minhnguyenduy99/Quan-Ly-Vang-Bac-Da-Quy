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
    public class ContentViewModel : BaseViewModel
    {
        private string iconSource;
        private object content;

        /// <summary>
        /// The source image of icon of tab control
        /// </summary>
        public string IconSource
        {
            get => iconSource;
            set => SetProperty(ref iconSource, value);
        }
        
        /// <summary>
        /// The content of the view model
        /// </summary>
        public object Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }
    }
}
