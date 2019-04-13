using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Projects
{
    /// <summary>
    /// Provides extend attached properties
    /// </summary>
    public class AttachedPropertyExtension
    {
        #region Icon image attached property

        public static readonly DependencyProperty SourceProperty = DependencyProperty.RegisterAttached(
            "Source",
            typeof(ImageSource),
            typeof(AttachedPropertyExtension));
        
        public static ImageSource GetSource(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(SourceProperty);
        }

        public static void SetSource(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(SourceProperty, value);
        }
        #endregion
    }
}
