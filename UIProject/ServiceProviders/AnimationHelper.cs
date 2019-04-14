using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace UIProject.ServiceProviders
{
    /// <summary>
    /// Provides implementation for animation sliding effect
    /// </summary>
    public class AnimationHelper : IAnimationService
    {
        /// <summary>
        /// The time span of the animation, in milisecond unit
        /// </summary>
        public static int TimeSpan { get; set; } = 400;

        /// <summary>
        /// Perform the animation on control in asynchorous mode
        /// </summary>
        /// <param name="control"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task PerformAnimationAsync(FrameworkElement control, AnimationMode mode)
        {
            PerformAnimation(control, mode);
        }

        public void PerformAnimation(FrameworkElement control, AnimationMode mode)
        {
            if (mode == AnimationMode.None)
                return;

            control.Visibility = Visibility.Hidden;

            GetThicknessAnimateValue(control.ActualWidth,control.ActualHeight, mode, out Thickness from, out Thickness to);

            var slideAnimation = new ThicknessAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(System.TimeSpan.FromMilliseconds(TimeSpan)),
                DecelerationRatio = 0.9f
            };

            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));

            var sb = new Storyboard();
            sb.Children.Add(slideAnimation);            

            sb.Begin(control);

            control.Visibility = Visibility.Visible;          
        }
      
        private void GetThicknessAnimateValue(
            double controlWidth, 
            double controlHeight,
            AnimationMode mode, 
            out Thickness firstThickness, 
            out Thickness lastThickness)
        {
            switch (mode)
            {
                case AnimationMode.LeftToRight:
                    firstThickness = new Thickness(-controlWidth, 0, controlWidth, 0);
                    break;
                case AnimationMode.RightToLeft:
                    firstThickness = new Thickness(controlWidth, 0, -controlWidth, 0);
                    break;
                case AnimationMode.TopToBottom:
                    firstThickness = new Thickness(0, -controlHeight, 0, controlHeight);
                    break;
                case AnimationMode.BottomToTop:
                    firstThickness = new Thickness(0, controlHeight, 0, -controlHeight);
                    break;
                default:
                    firstThickness = new Thickness(0);
                    break;
            }
            lastThickness = new Thickness(0);
        }
    }
}
