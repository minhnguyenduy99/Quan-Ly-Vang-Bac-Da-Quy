using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UIProject.Converters
{
    [ValueConversion(typeof(int), typeof(Visibility))]
    public class EmptyListDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int castCount = (int)value;
                // empty list
                if (castCount == 0)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
            catch { return Binding.DoNothing; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
