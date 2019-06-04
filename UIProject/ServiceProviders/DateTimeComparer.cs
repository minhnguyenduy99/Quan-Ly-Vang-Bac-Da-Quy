using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelProject;
using UIProject.ViewModels.LayoutViewModels;
using System.Globalization;
using System.ComponentModel;
using UIProject.Converters;

namespace UIProject.ServiceProviders
{


    public class DateTimeComparer : ICustomSorter
    { 
        public ListSortDirection SortDirection { get; set; }
        public int Compare(object x, object y)
        {
            var castX = (x as ItemViewModel<PhieuBanModel>).Model;
            var castY = (y as ItemViewModel<PhieuBanModel>).Model;
            if (castX != null && castY != null)
            {
                try
                {
                    var dateX = (DateTime)new ToShortDateConverter().ConvertBack(castX.NgayLap, typeof(DateTime), null, null);
                    var dateY = (DateTime)new ToShortDateConverter().ConvertBack(castY.NgayLap, typeof(DateTime), null, null);
                    if (SortDirection == ListSortDirection.Ascending)
                        return dateX.CompareTo(dateY);
                    else
                        return dateX < dateY ? 1 : dateX == dateY ? 0 : -1;
                }
                catch
                {
                     throw new InvalidCastException($"Cannot cast from {typeof(string)} to {typeof(DateTime)}");
                }
            }
            throw new ArgumentException($"Argument is not of type {typeof(ItemViewModel<PhieuBanModel>)}");
        }
    }
}
