using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.Events;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.FunctionInterfaces
{
    interface IAsyncCurrentSelectedItem
    {
        ISelectable SelectedItem { get; set; }

        event EventHandler<SelectedItemChangedEventArgs> SelectedItemChanged;
    }
}
