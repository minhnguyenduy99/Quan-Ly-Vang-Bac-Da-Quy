using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UIProject.UIConnector
{
    /// <summary>
    /// Provides functionalities for <see cref="Window"/> class simulation at View Model
    /// </summary>
    public interface IWindow
    {

        object DataContext { get; set; }

        void Show();
        bool? ShowDialog();
        void Close();

    }
}
