using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;

namespace ModelProject
{
    public class DataAccess
    {
        //San pham
        public static List<SanPhamModel> LoadSanPham()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SanPhamModel>("select * from sanpham", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveSanPham(SanPhamModel sanPham)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into  SanPham (MASP,TENSP,MALOAISP,DONGIAMUAVAO) values (@MaSP,@TenSP,@MaLoaiSP,@DonGiaMuaVao)", sanPham);
            }
        }
        //Chi tiet ban
        public static List<ChiTietBanModel> LoadChiTietBan()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ChiTietBanModel>("select * from chitietban", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveChiTietBan(ChiTietBanModel ctb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into chitietban(MAPHIEUMUAHANG,MASP,SOLUONG,THANHTIEN,DONGIAMUAVAO,CHIETKHAU,THUE) values (@MaPhieuMuaHang,@MaSP,@SoLuong,@ThanhTien,@DonGiaMuaVao,@ChietKhau,@Thue", ctb);
            }
        }
        //Chi tiet dich vu
        public static List<ChiTietDichVuModel> LoadChiTietDichVu()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ChiTietDichVuModel>("select * from ChiTietDichVu", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveChiTietDichVu(ChiTietDichVuModel ctdv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into ChiTietDichVu(MAPHIEU,MALOAIDV,CHIPHIRIENG,SOLUONG,THANHTIEN,TRATRUOC,NGAYGIAO,MATINHTRANG) values (@MaPhieu,@MaLoaiDV,@ChiPhiRieng,@SoLuong,@ThanhTien,@TraTruoc,@NgayGiao,@MaTinhTrang ", ctdv);
            }
        }

        //Chi tiet mua

        public static List<ChiTietMuaModel> LoadChiTietMua()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ChiTietMuaModel>("select * from ChiTietMua", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveChiTietMua(ChiTietMuaModel ctm)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into ChiTietMua(MAPHIEUMUAHANG,MASP,SOLUONG,DONGIA) values (@MaPhieuMuaHang,@MaSP,@SoLuong,@DonGia) ", ctm);
            }
        }
        // Don vi tinh

        public static List<DonViTinhModel> LoadDonViTinh()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DonViTinhModel>("select * from DonViTinh", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveDonViTinh(DonViTinhModel dvt)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into DonViTinh(MADVT,TENDVT) values (@MaDVT,@TenDVT) ", dvt);
            }
        }
        // Loai dich vu 
        public static List<LoaiDichVuModel> LoadLoaiDichVu()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<LoaiDichVuModel>("select * from LoaiDichVu", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveLoaiDichVu(LoaiDichVuModel loaiDV)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into LoaiDichVu(MALOAIDV,TENLOAIDV,DONGIADV) values (@MaLoaiDV,@TenLoaiDV,@DonGiaDV) ", loaiDV);
            }
        }
        // Loai san pham

        public static List<LoaiSanPhamModel> LoadLoaiSanPham()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<LoaiSanPhamModel>("select * from LoaiSanPham", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveLoaiSanPham(LoaiSanPhamModel loaiSP)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into LoaiSanPham(MALOAISP,MADVT,PHANTRAMLOINHUAN) values (@MaLoaiSP,@MaDVT,@PhanTramLoiNhuan) ",loaiSP);
            }
        }
        // Nha cung cap

        public static List<PhieuBanModel> LoadPhieuBan()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PhieuBanModel>("select * from PhieuBan", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SavePhieuBan(PhieuBanModel phieuBan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into PhieuBan(MAPHIEU,SOPHIEU,NGAYLAP,MAKH) values (@MaPhieu,@SoPhieu,@NgayLap,@MaKH) ", phieuBan);
            }
        }
        // Phieu Mua
        public static List<PhieuMuaModel> LoadPhieuMua()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PhieuMuaModel>("select * from PhieuMua", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SavePhieuMua(PhieuMuaModel PhieuMua)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into PhieuMua(MAPHIEU,SOPHIEU,NGAYLAP,MANCC) values (@MaPhieu,@SoPhieu,@NgayLap,@MaNCC) ", PhieuMua);
            }
        }
        //Phieu dich vu
        public static List<PhieuDichVuModel> LoadPhieuDichVu()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PhieuDichVuModel>("select * from PhieuDichVu", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SavePhieuDichVu(PhieuDichVuModel PhieuDichVu)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into PhieuDichVu(MAPHIEU,SOPHIEU,NGAYLAP,MAKH,TONGTIEN,TONGTIENTRATRUOC) values (@MaPhieu,@SoPhieu,@NgayLap,@MaKH,@TongTien,@TongTienTraTruoc) ", PhieuDichVu);
            }
        }
        // Tinh trang
        public static List<TinhTrangModel> LoadTinhTrang()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TinhTrangModel>("select * from TinhTrang", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveTinhTrang(TinhTrangModel TinhTrang)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into TinhTrang(MATINHTRANG,TENTINHTRANG) values (@MaTinhTrang,@TenTinhTrang) ", TinhTrang);
            }
        }

        // khach hang 
        public static List<KhachHangModel> LoadKhachHang()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<KhachHangModel>("select * from KhachHang", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveKhachHang(KhachHangModel khachHang)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into KhachHang(MAKH,TENKH,DIACHI,SDT,CONGNO,MAKHUVUC,EMAIL) values (@MaKH,@TenKH,@DiaChi,@SDT,@CongNo,@MaKhuVuc,@Email) ", khachHang);
            }
        }

        // khu vuc
        public static List<KhuVucModel> LoadKhuVuc()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<KhuVucModel>("select * from KhuVuc", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void SaveKhuVuc(KhuVucModel khuVuc)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into KhuVuc(MAKHUVUC,TENKHUVUC) values (@MaKhuVuc,@TenKhuVuc) ", khuVuc);
            }
        }
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
