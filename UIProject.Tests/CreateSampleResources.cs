using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.Tests
{
    public static class CreateSampleResources
    {
        public static List<KhuVucModel> GetDSKhuVucSample()
        {
            return new List<KhuVucModel>()
            {
                new KhuVucModel(){MaKhuVuc = 1, TenKhuVuc = "Thủ đức - TP HCM"},
                new KhuVucModel(){MaKhuVuc = 2, TenKhuVuc = "Bình thạnh - TP HCM"},
                new KhuVucModel(){MaKhuVuc = 3, TenKhuVuc = "Quận 9 - TP HCM"},
            };
        }
        public static List<NhaCungCapModel> GetDSNhaCungCapSample()
        {
            return new List<NhaCungCapModel>()
            {
                new NhaCungCapModel(){MaNCC = 1, TenNCC = "NCC A", DiaChi = "Dia chi A", MaKhuVuc = 1},
                new NhaCungCapModel(){MaNCC = 2, TenNCC = "NCC B", DiaChi = "Dia chi B", MaKhuVuc = 2},
                new NhaCungCapModel(){MaNCC = 3, TenNCC = "NCC C", DiaChi = "Dia chi C", MaKhuVuc = 3},
                new NhaCungCapModel(){MaNCC = 4, TenNCC = "NCC D", DiaChi = "Dia chi D", MaKhuVuc = 1},
                new NhaCungCapModel(){MaNCC = 5, TenNCC = "NCC E", DiaChi = "Dia chi E", MaKhuVuc = 2},
            };
        }
        public static List<DonViTinhModel> GetDSDonViTinhSample()
        {
            return new List<DonViTinhModel>()
            {
                new DonViTinhModel(){MaDVT = 1, TenDVT = "DVT 1"},
                new DonViTinhModel(){MaDVT = 2, TenDVT = "DVT 2"},
                new DonViTinhModel(){MaDVT = 3, TenDVT = "DVT 3"},
            };
        }
        public static List<LoaiSanPhamModel> GetDSLoaiSPSample()
        {
            return new List<LoaiSanPhamModel>()
            {
                new LoaiSanPhamModel(){MaLoaiSP = 1, MaDVT = 1, TenLoaiSP = "LOAISP 1", PhanTramLoiNhuan = 10},
                new LoaiSanPhamModel(){MaLoaiSP = 2, MaDVT = 2, TenLoaiSP = "LOAISP 2", PhanTramLoiNhuan = 5},
                new LoaiSanPhamModel(){MaLoaiSP = 3, MaDVT = 3, TenLoaiSP = "LOAISP 3", PhanTramLoiNhuan = 20},
                new LoaiSanPhamModel(){MaLoaiSP = 4, MaDVT = 1, TenLoaiSP = "LOAISP 1", PhanTramLoiNhuan = 25},
            };
        }
        public static List<PhieuMuaModel> GetDSPhieuMuaSample()
        {
            return new List<PhieuMuaModel>()
            {
                new PhieuMuaModel(){MaPhieu = 1, NgayLap = "6/10/2018", MaNCC = 1},
                new PhieuMuaModel(){MaPhieu = 2, NgayLap = "5/10/2018", MaNCC = 2},
                new PhieuMuaModel(){MaPhieu = 3, NgayLap = "4/10/2018", MaNCC = 3},
            };
        }
        public static List<ChiTietMuaModel> GetDSChiTietMuaSample()
        {
            return new List<ChiTietMuaModel>()
            {
                new ChiTietMuaModel(){MaPhieuMua = 1, MaSP = 1, SoLuong = 5},
                new ChiTietMuaModel(){MaPhieuMua = 1, MaSP = 2, SoLuong = 3},
                new ChiTietMuaModel(){MaPhieuMua = 1, MaSP = 2, SoLuong = 10},
                new ChiTietMuaModel(){MaPhieuMua = 2, MaSP = 2, SoLuong = 4},
                new ChiTietMuaModel(){MaPhieuMua = 2, MaSP = 3, SoLuong = 2},
                new ChiTietMuaModel(){MaPhieuMua = 2, MaSP = 3, SoLuong = 1},
                new ChiTietMuaModel(){MaPhieuMua = 3, MaSP = 3, SoLuong = 15},
                new ChiTietMuaModel(){MaPhieuMua = 3, MaSP = 4, SoLuong = 9},
                new ChiTietMuaModel(){MaPhieuMua = 3, MaSP = 4, SoLuong = 8},
                new ChiTietMuaModel(){MaPhieuMua = 3, MaSP = 4, SoLuong = 5},
            };
        }
        public static List<SanPhamModel> GetDSSanPhamSample()
        {
            return new List<SanPhamModel>()
            {
                new SanPhamModel(){MaSP = 1, MaLoaiSP = 1, DonGiaMuaVao = 10000, SoLuong = 5},
                new SanPhamModel(){MaSP = 2, MaLoaiSP = 2, DonGiaMuaVao = 20000, SoLuong = 17},
                new SanPhamModel(){MaSP = 3, MaLoaiSP = 3, DonGiaMuaVao = 30000, SoLuong = 18},
                new SanPhamModel(){MaSP = 4, MaLoaiSP = 1, DonGiaMuaVao = 40000, SoLuong = 22},
            };
        }
    }
}
