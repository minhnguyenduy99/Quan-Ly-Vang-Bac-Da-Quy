using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.PageViewModels;

namespace UIProject.ViewModels.FunctionInterfaces
{
    /// <summary>
    /// Provides the ability to navigate <see cref="BasePageViewModel"/>
    /// </summary>
    public interface INavigator
    {
        BasePageViewModel CurrentNavigatedPage { get; set; }
        
        Dictionary<object, BasePageViewModel> NavigatedPages { get; set; }

        void Navigate(object key);
    }
}
