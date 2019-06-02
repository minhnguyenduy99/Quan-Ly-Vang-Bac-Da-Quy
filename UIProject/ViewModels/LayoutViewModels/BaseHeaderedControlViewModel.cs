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
    /// Base view model of <see cref="HeaderedContentControl"/> class 
    /// </summary>
    public abstract class BaseHeaderedControlViewModel
    {
        /// <summary>
        /// The background value of the control when it is focused
        /// </summary>
        public Brush FocusBackground { get; set; }
        
        /// <summary>
        /// The header view model of base  <see cref="HeaderedContentControl"/>
        /// </summary>
        public HeaderViewModel HeaderVM { get; set; }
        
        /// <summary>
        /// The contents involved in the <see cref="BaseHeaderedControlViewModel"/>
        /// </summary>
        public List<BaseContentViewModel> Contents { get; set; }

    }
}
