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
        public const string DATE_ERROR_STATEMENT = "Ngày không hợp lệ";
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
            if (value == null)
                return Binding.DoNothing;
            try
            {
                var castValue = (DateTime)value;
                var day = castValue.Day;
                var month = castValue.Month;
                var year = castValue.Year;
                string dayStr = day.ToString();
                string monthStr = month.ToString();
                string yearStr = year.ToString();
                if (day < 10)
                    dayStr = $"0{day}";
                if (month < 10)
                    monthStr = $"0{month}";

                return $"{dayStr}/{monthStr}/{yearStr}";
            }
            catch { return DATE_ERROR_STATEMENT; }
        }
    }
}
