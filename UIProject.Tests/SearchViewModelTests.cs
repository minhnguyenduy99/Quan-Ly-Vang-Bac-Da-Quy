using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.ServiceProviders;
using UIProject.ViewModels;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;
using Xunit;

namespace UIProject.Tests
{
    
    public class SearchViewModelTests
    {
        public static IEnumerable<KhachHangModel> dsKhachHang = new List<KhachHangModel>()
        {
            new KhachHangModel()
            {
                TenKH = "Nguyễn Văn A",
            },
            new KhachHangModel()
            {
                TenKH = "Nguyễn Văn B",
            },
            new KhachHangModel()
            {
                TenKH = "Nguyễn Văn C",
            },
            new KhachHangModel()
            {
                TenKH = "Nguyễn Văn A",
            },
        };

        public static TextBasedSearchViewModel<KhachHangModel> timKiemKhachHang
            = new TextBasedSearchViewModel<KhachHangModel>();

        [Fact]
        public void Search_Exactly()
        {
            // Arrange
            var expected_1 = dsKhachHang.Where(kh => kh.TenKH.Equals("Nguyễn Văn A"));
            var expected_2 = dsKhachHang.Where(kh => kh.TenKH.Equals("Nguyễn Văn B"));
            
            // Act
            var actual_1 = Search_ValueExactly("Nguyễn Văn A", "TenKH");
            var actual_2 = Search_ValueExactly("Nguyễn Văn B", "TenKH");
            
            // Assert
            Assert.Equal(expected_1.Count(), actual_1.Count());
            Assert.Equal(expected_2.Count(), actual_2.Count());
        }

        [Fact]
        public void Search_Likely()
        {
            // Arrange
            var expected_1 = dsKhachHang.Where(kh => kh.TenKH.Contains("Văn A"));
            var expected_2 = dsKhachHang.Where(kh => kh.TenKH.Contains("Nguyễn"));

            // Act
            var actual_1 = Search_ValueLikely("Văn A", "TenKH");
            var actual_2 = Search_ValueLikely("Nguyễn", "TenKH");

            // Assert
            Assert.Equal(expected_1.Count(), actual_1.Count());
            Assert.Equal(expected_2.Count(), actual_2.Count());
        }

        [Fact]
        public void Search_LikelyIgnoreCase()
        {
            // Arrange
            var expected_1 = 2;
            var expected_2 = 4;
            var expected_3 = 4;
            var expected_4 = 4;

            // Act
            var actual_1 = Search_ValueLikelyIgnoreCase("vĂn a", "TenKH");
            var actual_2 = Search_ValueLikelyIgnoreCase("nGUYễN vĂN", "TenKH");
            var actual_3 = Search_ValueLikelyIgnoreCase(string.Empty, "TenKH");
            var actual_4 = Search_ValueLikelyIgnoreCase(null, "TenKH");

            // Assert
            Assert.Equal(expected_1, actual_1.Count());
            Assert.Equal(expected_2, actual_2.Count());
            Assert.Equal(expected_3, actual_3.Count());
            Assert.Equal(expected_4, actual_4.Count());
        }

        public IEnumerable<KhachHangModel> Search_ValueExactly(string textSearch, string propertyName)
        {
            timKiemKhachHang.SearchMode = SearchMode.Exactly;
            timKiemKhachHang.Text = textSearch;
            return timKiemKhachHang.Search(dsKhachHang, propertyName);
        }
        public IEnumerable<KhachHangModel> Search_ValueLikely(string textSearch, string propertyName)
        {
            timKiemKhachHang.SearchMode = SearchMode.Likely;
            timKiemKhachHang.Text = textSearch;
            return timKiemKhachHang.Search(dsKhachHang, propertyName);
        }
        public IEnumerable<KhachHangModel> Search_ValueLikelyIgnoreCase(string textSearch, string propertyName)
        {
            timKiemKhachHang.SearchMode = SearchMode.LikelyIgnoreCase;
            timKiemKhachHang.Text = textSearch;
            return timKiemKhachHang.Search(dsKhachHang, propertyName);
        }
    }
}
