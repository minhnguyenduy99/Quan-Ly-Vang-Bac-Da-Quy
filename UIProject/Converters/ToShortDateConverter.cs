using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UIProject.Converters
{
    /// <summary>
    /// Convert from a string to date time value
    /// </summary>
    [ValueConversion(typeof(string),typeof(DateTime))]
    public class ToShortDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DateTime castShortDate = (DateTime)value;
                return castShortDate.ToString("dd/MM/yyyy");
            }
            catch { return Binding.DoNothing; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                DateTime castShortDate = DateTime.ParseExact(value.ToString(), "dd/MM/yyyy", null);
                return castShortDate;
            }
            catch { return Binding.DoNothing; }
        }
    }
}
