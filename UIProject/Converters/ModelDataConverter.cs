using ModelProject;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UIProject.ViewModels.FunctionInterfaces;

namespace UIProject.Converters
{
    public class MaKhuVucToKhuVucConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string castMaKhuVuc = (string)value;
            if (string.IsNullOrEmpty(castMaKhuVuc))
                return Binding.DoNothing;
            return DataAccess.LoadKhuVucByMKV(castMaKhuVuc);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            KhuVucModel castKhuVuc = value as KhuVucModel;
            if (castKhuVuc == null)
                return Binding.DoNothing;
            return castKhuVuc.MaKhuVuc;
        }
    }

    public class MaLoaiSanPhamToLoaiSanPhamConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var castMaLoaiSP = (string)value;
            if (string.IsNullOrEmpty(castMaLoaiSP))
                return Binding.DoNothing;
            return DataAccess.LoadLoaiSanPhamByMaLSP(castMaLoaiSP);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var castLoaiSP = value as LoaiSanPhamModel;
            if (castLoaiSP == null)
                return Binding.DoNothing;
            return castLoaiSP.MaLoaiSP;
        }
    }


    /// <summary>
    /// A converter functions to indicate weither there is a currently selected item
    /// </summary>
    public class SelectedItemToEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var castSelectedItem = value as ISelectable;
            if (castSelectedItem == null)
                return false;
            return true;          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
