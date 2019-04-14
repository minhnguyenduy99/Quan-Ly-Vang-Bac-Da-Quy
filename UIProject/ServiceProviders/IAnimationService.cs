using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Projects.ServiceProviders
{
    /// <summary>
    /// Indicating the animation mode of UI 
    /// </summary>
    public enum AnimationMode
    {
        TopToBottom = 0,
        BottomToTop = 1,
        LeftToRight = 2,
        RightToLeft = 3,
        None = 4
    }

    /// <summary>
    /// Provides the ability to have slide effect on UI controls 
    /// </summary>
    public interface IAnimationService
    {
        /// <summary>
        /// Perform sliding animation on control
        /// </summary>
        /// <param name="control">The control applies the animation</param>
        /// <param name="mode">The animation mode to be displayed</param>
        /// <returns>The task represents the animation action</returns>
        void PerformAnimation(FrameworkElement control, AnimationMode mode);

    }
}
