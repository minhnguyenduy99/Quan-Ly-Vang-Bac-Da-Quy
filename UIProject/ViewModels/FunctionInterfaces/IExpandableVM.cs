using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.FunctionInterfaces
{
    /// <summary>
    /// Provides functionalities for aan expandable view model 
    /// </summary>
    public interface IExpandableVM
    {
        /// <summary>
        /// Indicating weither the view model is expanded
        /// </summary>
        bool IsExpanded { get; set; }

        /// <summary>
        /// Children of expandable view model
        /// </summary>
        List<BaseContentViewModel> Children { get; set; }


    }
}
