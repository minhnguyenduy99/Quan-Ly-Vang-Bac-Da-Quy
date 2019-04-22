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
    /// Converts a too-long-to-display string into shorten version by adding triple dot at the end
    /// </summary>
    public class LongStringToThreeDotConverter : IValueConverter
    {
        public int MaxLength { get; set; } = 30;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strVal = (string)value;
            if (strVal.Length > MaxLength)
            {
                return new StringBuilder(strVal.Substring(0, MaxLength - 3)).Append("...").ToString();
            }
            return strVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
