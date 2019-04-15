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
    /// Provides animation performance and <see cref="Timeline"/> type creation 
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
        public async Task PerformAnimationAsync(FrameworkElement control, SlideAnimationMode mode)
        {
            PerformAnimation(control, mode);
        }

        public void PerformAnimation(FrameworkElement control, SlideAnimationMode mode)
        {
            if (mode == SlideAnimationMode.None)
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

        /// <summary>
        /// Provides slide effect
        /// </summary>
        /// <param name="controlWidth">The width of the control applying the animation</param>
        /// <param name="animationMode">The animation mode of sliding</param>
        /// /// <param name="decelerationRatio">The ratio of timespan spent to decelerate</param>
        /// <returns>The animation</returns>
        public ThicknessAnimation CreateSlideAnimation(
            double controlWidth, 
            double controlHeight, 
            SlideAnimationMode animationMode, 
            double seconds,
            double decelerationRatio = 0.9f)
        {
            GetThicknessAnimateValue(controlWidth, controlHeight, animationMode, out Thickness from, out Thickness to);

            var slideAnimation = new ThicknessAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(System.TimeSpan.FromSeconds(seconds)),
                DecelerationRatio = decelerationRatio
            };

            Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));

            return slideAnimation;         
        }

        /// <summary>
        /// Provides fade animation
        /// </summary>
        /// <param name="from">the first opacity value</param>
        /// <param name="to">The last opacity value</param>
        /// <param name="seconds">The timespan of animation</param>
        /// <param name="decelerationRatio">The ratio of timespan spent to decelerate</param>
        /// <returns>The animation</returns>
        public DoubleAnimation CreateFadeAnimation(double from, double to, double seconds, double decelerationRatio = 0.9f)
        {
            var fadeAnimation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(System.TimeSpan.FromSeconds(seconds)),
                DecelerationRatio = decelerationRatio
            };

            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath("Opacity"));

            return fadeAnimation;
        }


        private void GetThicknessAnimateValue(
            double controlWidth, 
            double controlHeight,
            SlideAnimationMode mode, 
            out Thickness firstThickness, 
            out Thickness lastThickness)
        {
            switch (mode)
            {
                case SlideAnimationMode.LeftToRight:
                    firstThickness = new Thickness(-controlWidth, 0, controlWidth, 0);
                    break;
                case SlideAnimationMode.RightToLeft:
                    firstThickness = new Thickness(controlWidth, 0, -controlWidth, 0);
                    break;
                case SlideAnimationMode.TopToBottom:
                    firstThickness = new Thickness(0, -controlHeight, 0, controlHeight);
                    break;
                case SlideAnimationMode.BottomToTop:
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
