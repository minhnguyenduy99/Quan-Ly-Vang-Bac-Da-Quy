using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UIProject.Events
{
    /// <summary>
    /// Event information when Selected event <see cref="ListBox"/> occurs 
    /// </summary>
    public class SelectedEventArgs : EventArgs
    {
        public object SelectedItem { get; private set; }
        public SelectedEventArgs(object selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
}
