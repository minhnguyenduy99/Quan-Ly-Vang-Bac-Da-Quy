using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using UIProject.ServiceProviders;

namespace UIProject.ServiceProviders
{
    /// <summary>
    /// Provides <see cref="Page"/> extension  
    /// </summary>
    public static class PageControlExtension
    {
        /// <summary>
        /// Sliding the page from right to left
        /// </summary>
        /// <param name="page"></param>
        /// <param name="seconds"></param>
        public static async Task SlideFromRightToLeft(this Page page, double seconds, double decelerationRatio = 0.9f)
        {
            Storyboard sb = new Storyboard();
            ThicknessAnimation slideAnimation = AnimationHelper.CreateSlideAnimation(
                page.ActualWidth, 
                page.ActualHeight, 
                SlideAnimationMode.RightToLeft,
                seconds,
                decelerationRatio);

            sb.Children.Add(slideAnimation);

            page.Visibility = Visibility.Hidden;
            sb.Begin(page);
            page.Visibility = Visibility.Visible;            
        }

        /// <summary>
        /// Sliding the page from right to left and fade in
        /// </summary>
        /// <param name="page"></param>
        /// <param name="seconds">Timespan of animation</param>
        public static async Task SlideFromRightToLeftAndFadeIn(this Page page, double seconds, double decelerationRatio = 0.9f)
        {
            Storyboard sb = new Storyboard();

            ThicknessAnimation slideAnimation = AnimationHelper.CreateSlideAnimation(
                page.ActualWidth,
                page.ActualHeight,
                SlideAnimationMode.RightToLeft,
                seconds,
                decelerationRatio);
            DoubleAnimation fadeInAnimation = AnimationHelper.CreateFadeAnimation(0, 1, seconds, decelerationRatio);

            sb.Children.Add(slideAnimation);
            sb.Children.Add(fadeInAnimation);

            page.Visibility = Visibility.Hidden;
            sb.Begin(page);
            page.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Sliding the page from right to left and fade in
        /// </summary>
        /// <param name="page"></param>
        /// <param name="seconds">Timespan of animation</param>
        public static async Task FadeIn(this Page page, double seconds, double decelerationRatio = 0.9f)
        {
            Storyboard sb = new Storyboard();

            DoubleAnimation fadeInAnimation = AnimationHelper.CreateFadeAnimation(0, 1, seconds, decelerationRatio);

            sb.Children.Add(fadeInAnimation);

            page.Visibility = Visibility.Hidden;
            sb.Begin(page);
            page.Visibility = Visibility.Visible;
        }
    }
}
