using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIProject.ViewModels.FunctionInterfaces
{
    /// <summary>
    /// Provides selection ability for any view model
    /// </summary>
    public interface ISelectable
    {
        bool IsSelected { get; set; }
        ICommand SelectItemCommand { get; set; }
        void Select();

        event EventHandler<EventArgs> Selected;
    }
}
