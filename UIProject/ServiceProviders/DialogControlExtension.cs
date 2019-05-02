using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UIProject.ServiceProviders
{
    /// <summary>
    /// Indicating the position specification type
    /// </summary>
    public enum PositionType
    {
        /// <summary>
        /// The position of control is shown below its source activator
        /// </summary>
        BelowActivator = 0,
        /// <summary>
        /// The position of control is customizable
        /// </summary>
        Custom = 1
    }

    public static class DialogControlExtension
    {
        /// <summary>
        /// Show window in dialog mode at a specified position
        /// </summary>
        /// <param name=""></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool? ShowDialog(this Window window, Point position)
        {
            window.Left = position.X;
            window.Top = position.Y;
            return window.ShowDialog();
        }

        /// <summary>
        /// Shows window in dialog mode at a relative position to the activator
        /// </summary>
        /// <param name="window"></param>
        /// <param name="activator">The control that activates the window</param>
        /// <param name="customDentaX">The x-coordinate denta from the x-coordinate position of activator</param>
        /// <param name="customDentaY">The y-coordinate denta from the y-coordinate position of activator</param>
        /// <returns></returns>
        public static bool? ShowDialog(this Window window, FrameworkElement activator, double customDentaX = double.NaN, double customDentaY = double.NaN)
        {
            var activatorLocation = activator.PointToScreen(new Point(0, 0));
            window.Left = activatorLocation.X + customDentaX;
            window.Top = activatorLocation.Y + customDentaY;
            return window.ShowDialog();
        }
    }
}
