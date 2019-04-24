using UIProject.ViewModels.LayoutViewModels;
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
    /// A converter from DialogWindowType to boolean, represents the visibility of buttons in Dialog Window
    /// </summary>
    [ValueConversion(typeof(DialogWindowType), typeof(System.Windows.Visibility))]
    class DialogWindowTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DialogWindowType dialogType = (DialogWindowType)value;
            string buttonName = (string)parameter;
            return GetVisibilityStateFrom(dialogType, buttonName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        private System.Windows.Visibility GetVisibilityStateFrom(DialogWindowType dialogType,string buttonName)
        {
            string valueInStr = dialogType.ToString();
            if (valueInStr.Contains(buttonName))
                return System.Windows.Visibility.Visible;
            return System.Windows.Visibility.Collapsed;
        }
    }
}
