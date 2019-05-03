using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.Events
{
    public class SelectedItemChangedEventArgs : EventArgs
    {
        public object SelectedItem { get; private set; }
        public SelectedItemChangedEventArgs(object selectedItem)
        {
            this.SelectedItem = selectedItem;
        }
    }
}
