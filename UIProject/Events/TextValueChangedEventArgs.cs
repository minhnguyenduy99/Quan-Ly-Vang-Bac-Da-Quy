using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.Events
{
    public class TextValueChangedEventArgs : EventArgs
    {
        public string OldValue { get; private set; }
        public string NewValue { get; private set; }

        public TextValueChangedEventArgs(string oldValue, string newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}
