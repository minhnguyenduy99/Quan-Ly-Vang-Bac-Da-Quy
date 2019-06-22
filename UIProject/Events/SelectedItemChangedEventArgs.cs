using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.Events
{
    public class SelectedItemChangedEventArgs : EventArgs
    {
        public ISelectable SelectedItem { get; private set; }
        public SelectedItemChangedEventArgs(ISelectable selectedItem)
        {
            this.SelectedItem = selectedItem;
        }
    }
}
