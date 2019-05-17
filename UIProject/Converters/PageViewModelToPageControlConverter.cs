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
    /// Provides conversion from current page view model to corresponding page control
    /// </summary>)
    class PageViewModelToPageControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "Trang này đang được cập nhật";
            return GetPageInstance(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get page instance corresponding with the page view model
        /// </summary>
        /// <param name="pageVM">The view model of page</param>
        /// <returns></returns>
        private Page GetPageInstance(object pageVM)
        {
            if (pageVM is TongQuanPageVM)
                return new TongQuanPage() { DataContext = pageVM };
            if (pageVM is BanHangPageVM)
                return new BanHangPage();
            if (pageVM is LamDichVuPageVM)
                return new LamDichVuPage();
            if (pageVM is DanhSachDonHangPageVM)
                return new DanhSachDonHangPage();
            if (pageVM is KhachHangPageVM)
                return new KhachHangPage();
            if (pageVM is NhapHangPageVM)
                return new NhapHangPage();
            if (pageVM is NhaCungCapPageVM)
                return new DoiTacPage();
            if (pageVM is SanPhamPageVM)
                return new SanPhamPage();
            if (pageVM is BaoCaoTonKhoPageVM)
                return new BaoCaoTonKhoPage();

            return null;
        }
    }
}
