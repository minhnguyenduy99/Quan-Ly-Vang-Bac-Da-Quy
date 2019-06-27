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
                char[] splits = new char[] { '/', '-', ':', ' '};
                string[] dateSplit = value.ToString().Split(splits, StringSplitOptions.RemoveEmptyEntries); ;
                int month = int.Parse(dateSplit[0]);
                int day = int.Parse(dateSplit[1]);
                int year = int.Parse(dateSplit[2]);
                if (dateSplit.Length <= 3)
                {
                    return new DateTime(year, month, day);
                }
                int hour = int.Parse(dateSplit[3]);
                int minute = int.Parse(dateSplit[4]);
                int second = int.Parse(dateSplit[5]);
                return new DateTime(year, month, day, hour, minute, second);
            }
            catch { return DATE_ERROR_STATEMENT; }
        }
    }
}
