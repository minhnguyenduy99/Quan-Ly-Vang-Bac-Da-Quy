using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// Base view model of <see cref="HeaderedContentControl"/>
    /// </summary>
    public class BaseHeaderViewModel: BaseViewModel
    {
        private string header;
        private string iconSource;
        private Brush foreground;
        private Brush background;


        /// <summary>
        /// The content of header
        /// </summary>
        public string Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }

        /// <summary>
        /// The image source of icon 
        /// </summary>
        public string IconSource
        {
            get => iconSource;
            set => SetProperty(ref iconSource, value);
        }

        /// <summary>
        /// The foreground of header
        /// </summary>
        public Brush Foreground
        {
            get => foreground;
            set => SetProperty(ref foreground, value);
        }

        /// <summary>
        /// The background of applied control
        /// </summary>
        public Brush Background
        {
            get => background;
            set => SetProperty(ref background, value);
        }
    }
}
