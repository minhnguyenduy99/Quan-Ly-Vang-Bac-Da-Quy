using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIProject.ServiceProviders
{
    public class IconButtonExtension : DependencyObject
    {
        [AttachedPropertyBrowsableForType(typeof(Button))]
        public static string GetIconKind(DependencyObject obj)
        {
            return (string)obj.GetValue(IconKindProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(Button))]
        public static void SetIconKind(DependencyObject obj, string value)
        {
            obj.SetValue(IconKindProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(Button))]
        public static Brush GetIconBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(IconBackgroundProperty);
        }

        [AttachedPropertyBrowsableForType(typeof(Button))]
        public static void SetIconBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(IconBackgroundProperty, value);
        }

        #region Register Attached Properties
        // Using a DependencyProperty as the backing store for IconKind.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconKindProperty =
            DependencyProperty.RegisterAttached(
                "IconKind",
                typeof(string),
                typeof(IconButtonExtension),
                new PropertyMetadata(string.Empty));
       
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconBackgroundProperty =
            DependencyProperty.RegisterAttached(
                "IconBackground", 
                typeof(Brush), 
                typeof(IconButtonExtension), 
                new PropertyMetadata(Brushes.White));

        #endregion

    }
}
