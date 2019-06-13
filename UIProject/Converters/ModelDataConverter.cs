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
            try
            {
                long? castMaKhuVuc = (long?)value;
                return DataAccess.LoadKhuVucByMKV(castMaKhuVuc);
            }
            catch { return Binding.DoNothing; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            KhuVucModel castKhuVuc = value as KhuVucModel;
            if (castKhuVuc == null)
                return Binding.DoNothing;
            return castKhuVuc.MaKhuVuc;
        }
    }

    public class KhuVucToTenKhuVucConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            KhuVucModel khuVuc = value as KhuVucModel;
            if (khuVuc == null)
                return Binding.DoNothing;
            return khuVuc.TenKhuVuc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new Exception("Cannot convert back");
        }
    }

    public class MaLoaiSanPhamToLoaiSanPhamConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var castMaLoaiSP = (long)value;
                return DataAccess.LoadLoaiSanPhamByMaLSP(castMaLoaiSP);
            }
            catch { return Binding.DoNothing; }
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
