using UIProject.ViewModels;
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
    /// Provides mechanism for converting from Tab Name to the TabViewModel object
    /// </summary>
    [ValueConversion(typeof(string),typeof(TabViewModel))]
    public class TabNameToTabViewModelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tabs = value as Dictionary<string, TabViewModel>;
            string tabName = (string)parameter;
            if (tabs != null)
            {
                return tabs[tabName];
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
