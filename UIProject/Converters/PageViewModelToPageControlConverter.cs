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
        Dictionary<object, Page> PageDict = new Dictionary<object, Page>();
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
            if (PageDict.ContainsKey(pageVM))
            {
                return PageDict[pageVM];
            }
            else
            {
                switch (pageVM)
                {
                    case TongQuanPageVM vm:
                        PageDict.Add(pageVM, new TongQuanPage());break;
                    case BanHangPageVM vm:
                        PageDict.Add(pageVM, new BanHangPage()); break;
                    case LamDichVuPageVM vm:
                        PageDict.Add(pageVM, new LamDichVuPage()); break;
                    case DanhSachDonHangPageVM vm:
                        PageDict.Add(pageVM, new DanhSachDonHangPage()); break;
                    case KhachHangPageVM vm:
                        PageDict.Add(pageVM, new KhachHangPage()); break;
                    case NhapHangPageVM vm:
                        PageDict.Add(pageVM, new NhapHangPage()); break;
                    case NhaCungCapPageVM vm:
                        PageDict.Add(pageVM, new DoiTacPage()); break;
                    case DichVuPageVM vm:
                        PageDict.Add(pageVM, new DanhSachPhieuDichVuPage());break;
                    case SanPhamPageVM vm:
                        PageDict.Add(pageVM, new SanPhamPage()); break;
                    case BaoCaoTonKhoPageVM vm:
                        PageDict.Add(pageVM, new BaoCaoTonKhoPage()); break;
                }

                PageDict[pageVM].DataContext = pageVM;
                return PageDict[pageVM];
            }
        }
    }
}
