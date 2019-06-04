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
        public readonly string[] DateTimeFormats =
        {
            "dd/MM/yyyy",
            "dd/M/yyyy",
            "d/M/yyyy",
            "d/MM/yyyy"
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Binding.DoNothing;
            try
            {
                string castDateStr = ((DateTime)value).ToString("dd/MM/yyyy");
                return castDateStr;
            }
            catch { }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime castShortDate;
            string castDateStr = (string)value;
            bool parseSuccess = DateTime.TryParseExact(castDateStr, DateTimeFormats, null, DateTimeStyles.None, out castShortDate);
            if (parseSuccess)
                return castShortDate;
            return Binding.DoNothing;
        }
    }
}
