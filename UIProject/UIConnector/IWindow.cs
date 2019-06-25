using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UIProject.ServiceProviders;


namespace UIProject.UIConnector
{
    /// <summary>
    /// Provides functionalities for <see cref="Window"/> class simulation at View Model
    /// </summary>
    public interface IWindow 
    {
        object DataContext { get; set; }
        bool? DialogResult { get; set; }

        void Show();
        bool? ShowDialog();
        void Close();

        event CancelEventHandler Closing;
    }

    /// <summary>
    /// The extension interface version of <see cref="IWindow"/>
    /// </summary>
    public interface IWindowExtension : IWindow
    {
        FrameworkElement Activator { get; set; }
        bool? ShowDialog(Point position);
        bool? ShowDialog(double dentaX, double dentaY);
    }

    public class WindowEventArgs: EventArgs
    {
        public IWindow Window { get; private set; }
        public WindowEventArgs(IWindow window)
        {
            Window = window;
        }
    }
}
