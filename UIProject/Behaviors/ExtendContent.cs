using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace UIProject.Behaviors
{
    public class ExtendContent
    {
        #region ImageSource Property
        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.RegisterAttached(
                "ImageSource",
                typeof(ImageSource),
                typeof(ExtendContent),
                new PropertyMetadata(null));

        public static ImageSource GetImageSource(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(ImageSourceProperty);
        }

        public static void SetImageSource(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(ImageSourceProperty, value);
        }
        #endregion


        #region Text Property
        public static string GetTextContent(DependencyObject obj)
        {
            return (string)obj.GetValue(TextContentProperty);
        }

        public static void SetTextContent(DependencyObject obj, string value)
        {
            obj.SetValue(TextContentProperty, value);
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextContentProperty =
            DependencyProperty.RegisterAttached(
                "TextContent",
                typeof(string),
                typeof(ExtendContent),
                new PropertyMetadata(null));

        #endregion
    }
}

