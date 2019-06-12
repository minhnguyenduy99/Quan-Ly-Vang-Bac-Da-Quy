using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.FunctionInterfaces
{
    /// <summary>
    /// Represents a view model object
    /// </summary>
    public interface IViewModelObject
    {
        void Load();
        void Reload();

        event EventHandler Loaded;
        event EventHandler Reloaded;
    }
}
