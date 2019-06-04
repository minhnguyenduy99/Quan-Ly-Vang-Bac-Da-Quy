using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    class Program
    {
        static void Main(string[] args)
        {

            SanPhamModel spModel = new SanPhamModel()
            {
                MaSP = "MASP1",
                MaLoaiSP = "MALOAISP1",
                DonGiaMuaVao = 100000,
                TenSP = "TENSP1"
            };

            DonViTinhModel donViTinhModel = new DonViTinhModel()
            {
                MaDVT = "MaDVT",
                TenDVT = "TENDVT1"
            };

            KhachHangModel khachHangModel = new KhachHangModel()
            {
                MaKH = "MAKH1",
                TenKH = "TENKH1",
                DiaChi = "DIACHI1 ",
                SDT = "123",
                CongNo = 12312,
                Email = "email",
                MaKhuVuc = "MKV "
            };

            KhuVucModel khuVucModel = new KhuVucModel()
            {
                MaKhuVuc = "MAKV1",
                TenKhuVuc = "TENKV1"
            };

            TinhTrangModel tinhTrangModel = new TinhTrangModel()
            {
                MaTinhTrang = "MATT1",
                TenTinhTrang = "TENTT1"
            };

            LoaiDichVuModel loaiDichVuModel = new LoaiDichVuModel()
            {
                MaLoaiDV = "MALOAIDV1",
                TenLoaiDV = "TENLOADV1",
                DonGiaDV = 1
            };

            ChiTietBanModel chiTietBanModel = new ChiTietBanModel();


            ChiTietMuaModel chiTietMuaModel = new ChiTietMuaModel()
            {
                MaPhieuMuaHang = "MPMH1",
                MaSP = "MASP1",
                SoLuong = 10
            };

            ChiTietDichVuModel chiTietDichVuModel = new ChiTietDichVuModel()
            {
                ChiPhiRieng = 10,
                MaLoaiDV = "sda",
                MaPhieu = "adsasd",
                MaTinhTrang = "dasda",
                NgayGiao = "sadasd",
                SoLuong = 133333333,
                ThanhTien = 10000,
                TraTruoc = 10
            };

            LoaiSanPhamModel loaiSanPhamModel = new LoaiSanPhamModel()
            {
                MaLoaiSP = "cfsadfsaf",
                MaDVT = "safsadf",
                PhanTramLoiNhuan = 100,
                TenLoaiSP = "safsdefas"
            };

            PhieuBanModel phieuBanModel = new PhieuBanModel()
            {
                SoPhieu = "adsadadsad",
                MaKH = "sadas",
                MaPhieu = "asdasd",
                NgayLap = "asdas"
            };

            PhieuDichVuModel phieuDichVuModel = new PhieuDichVuModel()
            {
                MaKH = "sfdsaf",
                MaPhieu = "sdfsdaf",
                NgayLap = "sfdsaf",
                SoPhieu = "asfdsa",
                TongTien = 1000,
                TongTienTraTruoc = 10000,
                TinhTrang = 1

            };

            DataAccess.SaveKhachHang(khachHangModel);

            List<KhachHangModel> list = new List<KhachHangModel>();
            list = DataAccess.LoadKhachHang();

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].MaKH);
            }




            Console.ReadKey();
        }
    }
}
