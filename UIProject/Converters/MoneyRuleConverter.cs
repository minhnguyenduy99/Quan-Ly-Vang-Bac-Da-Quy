using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace UIProject.Converters
{
    public class MoneyRuleConverter : IValueConverter
    {
        public static string InvalidMoneyCast = "Số tiền không hợp lệ";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return Binding.DoNothing;
            decimal castMoney = System.Convert.ToDecimal(value.ToString());
            if (castMoney < 0)
                return InvalidMoneyCast;
            return string.Format("{0:N0}", System.Convert.ToDecimal(value.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return System.Convert.ToDecimal(value);
            }
            catch
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// Try converting an object to money format
    /// </summary>
    public class TryMoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (string.IsNullOrEmpty(value.ToString()))
                    return value;
                decimal castMoney = System.Convert.ToDecimal(value.ToString());
                if (castMoney < 0)
                    return value;
                return string.Format("{0:N0}", System.Convert.ToDecimal(value.ToString()));
            }
            catch { return value; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
