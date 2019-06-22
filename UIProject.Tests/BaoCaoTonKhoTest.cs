using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UIProject.Tests
{
    public class BaoCaoTonKhoTest
    {
        public static IEnumerable<PhieuBanModel> DanhSachPhieuBan = DataAccess.LoadPhieuBan();
        public static IEnumerable<ChiTietBanModel> DanhSachChiTietBan = DataAccess.LoadChiTietBan();

        [Fact]
        public void LoadingTest()
        {
            // Arrange 
            var join = from phieuBan in DanhSachPhieuBan
                       join chiTiet in DanhSachChiTietBan
                       on phieuBan.MaPhieu equals chiTiet.MaPhieuBan
                       select new
                       {
                           MaSP = chiTiet.MaSP,
                           SoLuong = chiTiet.SoLuong
                       };
        }
    }
}
