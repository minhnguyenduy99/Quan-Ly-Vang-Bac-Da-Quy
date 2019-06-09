using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UIProject.Events;

namespace UIProject.ViewModels.FunctionInterfaces
{
    public interface ISearcher
    {
        string Text { get; set; }

        event EventHandler<TextValueChangedEventArgs> TextChanged;
    }
}
