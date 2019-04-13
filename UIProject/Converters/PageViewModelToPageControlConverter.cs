using UIProject.ViewModels;
using UIProject.ViewModels.PageViewModels;
using UIProject.Pages;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace UIProject.Converters
{
    /// <summary>
    /// Provides conversion from current selected tab to the corresponding page
    /// </summary>)
    class PageViewModelToPageControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "Sorry page not found";
            var objectType = value.GetType();
            if (objectType == typeof(TongQuanPageVM))
            {
                return new TongQuanPage() { DataContext = value };
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the page instance corresponding to the tab name
        /// </summary>
        /// <param name="currentTab">The tab to be converted</param>
        /// <param name="listDefaultTabNames">The list tab names constantly available</param>
        /// <returns></returns>
        private Page GetPageInstance(TabViewModel currentTab, List<string> listDefaultTabNames)
        {
            //string tabName = currentTab.TabName;
            //if (tabName == listDefaultTabNames[0])
            //    return new TongQuanPage() { DataContext = currentTab.PageViewModel };
            return null;          
        }
    }
}
