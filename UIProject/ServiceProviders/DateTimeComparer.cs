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
using UIProject.Behaviors;
using ModelProject.ExtensionFunctions;
using BaseMVVM_Service.BaseMVVM;

namespace UIProject.ServiceProviders
{


    public class DateTimeComparer : ICustomSorter
    { 
        public ListSortDirection SortDirection { get; set; }

        public int Compare(object x, object y)
        {
            var castDateX = ObservableObject.GetPropValue(x, "Model.NgayLapDate").ToString();
            var castDateY = ObservableObject.GetPropValue(y, "Model.NgayLapDate").ToString();
            if (castDateX != null && castDateY != null)
            {
                try
                {
                    var dateX = DateTime.ParseExact(castDateX, "dd/MM/yyyy", null);
                    var dateY = DateTime.ParseExact(castDateY, "dd/MM/yyyy", null);
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
