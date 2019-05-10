using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UIProject.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class InnerControlHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ContentControl castElement = value as ContentControl;
            if (castElement != null)
            {
                if (!double.IsNaN(castElement.ActualHeight))
                    return castElement.ActualHeight - castElement.Padding.Top - castElement.Padding.Bottom;
                return Binding.DoNothing;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
