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
        //Đã test
        #region SANPHAM_DataAcess
        public static List<SanPhamModel> LoadSanPham()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SanPhamModel>("select * from SANPHAM", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void SaveSanPham(SanPhamModel sanPham)        //Đã check
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into  SANPHAM (MASP,TENSP,MALOAISP,DONGIAMUAVAO) values (@MaSP,@TenSP,@MaLoaiSP,@DonGiaMuaVao)", sanPham);
            }
           
        }

        public static void UpdateSanPham(SanPhamModel sanPham)      //Đã check
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update SANPHAM set TENSP = @tenSP, MALOAISP = @MaLoaiSP, DONGIAMUAVAO = @DonGiaMuaVao ", sanPham);
            }
        }

        #endregion SANPHAM_DataAcess 

        //Đã test
        #region DONVITINH_DataAcess

        public static List<DonViTinhModel> LoadDonViTinh()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DonViTinhModel>("select * from DONVITINH", new DynamicParameters());
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
        public static void UpdateDonViTinh(DonViTinhModel dvt)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update DonViTinh set TENDVT = @TenDVT ", dvt);
            }
        }

        #endregion DONVITINH_DataAcess

        //Chưa test
        #region KHACHHANG_DataAcess

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
                cnn.Execute("insert into KhachHang(MAKH,TENKH,DIACHI,SDT,CONGNO,MAKHUVUC,EMAIL) values (@MaKH,@TenKH,@DiaChi,@SDT1,@CongNo,@MaKhuVuc,@Email) ", khachHang);
            }
        }

        public static void UpdateKhachHang(KhachHangModel KhachHang)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update KhachHang set TENKH = @TenKH,DIACHI = @DiaChi ,SDT = @SDT1,CONGNO = @CongNo ,MAKHUVUC = @MaKhuVuc ,EMAIL = @Email ) ", KhachHang);
            }
        }

        #endregion KHACHHANG_DataAcess

        //Đã test
        #region KHUVUC_DataAcess
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
        public static void UpdateKhuVuc(KhuVucModel KhuVuc)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update KhuVuc set TENKHUVUC= @TenKhuVuc ", KhuVuc);
            }
        }

        #endregion KHUVUC_DataAcess

        //Đã test
        #region TINHTRANG_DataAcess
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
                cnn.Execute("insert into TinhTrang(MATINHTRANG,TENTINHTRANG) values (@MaTinhTrang, @TenTinhTrang) ", TinhTrang);
            }
        }

        public static void UpdateTinhTrang(TinhTrangModel TinhTrang)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update TinhTrang set TENTINHTRANG = @TenTinhTrang ", TinhTrang);
            }
        }

        #endregion TINHTRANG_DataAcess

        //Đã test
        #region LOAIDICHVU_DataAcess
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

        public static void UpdateLoaiDichVu(LoaiDichVuModel loaiDV)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update LoaiDichVu set TENLOAIDV = @TenLoaiDV , DONGIADV = @DonGiaDV  ", loaiDV);
            }
        }
        #endregion LOAIDICHVU_DataAcess

        //Đã test
        #region CHITIETBAN_DataAcess
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
                cnn.Execute("insert into chitietban(MAPHIEUMUAHANG,MASP,SOLUONG,THANHTIEN,DONGIAMUAVAO,CHIETKHAU,THUE) values (@MaPhieuMuaHang,@MaSP,@SoLuong,@ThanhTien,@DonGiaMuaVao,@ChietKhau,@Thue)", ctb);
            }
        }

        public static void UpdateChiTietBan(ChiTietBanModel ctb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update chitietban set SOLUONG=@SoLuong,THANHTIEN=@ThanhTien,DONGIAMUAVAO=@DonGiaMuaVao,CHIETKHAU=@ChietKhau,THUE=@Thue", ctb);
            }
        }
        #endregion CHITIETBAN_DataAcess

        //Đã test
        #region CHITIETMUA_DataAcess
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

        public static void UpdateChiTietMua(ChiTietMuaModel ctm)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update ChiTietMua set SOLUONG = @SoLuong ,DONGIA = @DonGia ", ctm);
            }
        }

        #endregion CHITIETMUA_DataAcess

        //Đã test
        #region CHITIETDICHVU_DataAcess
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
                cnn.Execute("insert into ChiTietDichVu(MAPHIEU,MALOAIDV,CHIPHIRIENG,SOLUONG,THANHTIEN,TRATRUOC,NGAYGIAO,MATINHTRANG) values (@MaPhieu,@MaLoaiDV,@ChiPhiRieng,@SoLuong,@ThanhTien,@TraTruoc,@NgayGiao,@MaTinhTrang) ", ctdv);
            }
        }

        public static void UpdateChiTietDichVu(ChiTietDichVuModel ctdv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update ChiTietDichVu set CHIPHIRIENG=@ChiPhiRieng,SOLUONG=@SoLuong,THANHTIEN=@ThanhTien,TRATRUOC=@TraTruoc,NGAYGIAO=@NgayGiao,MATINHTRANG=@MaTinhTrang" , ctdv);
            }
        }

        #endregion CHITIETDICHVU_DataAcess

        //Đã test
        #region LOAISANPHAM_DataAcess

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
                cnn.Execute("insert into LoaiSanPham(MALOAISP,MADVT,PHANTRAMLOINHUAN, TENLOAISP) values (@MaLoaiSP,@MaDVT,@PhanTramLoiNhuan,@TenLoaiSP) ", loaiSP);
            }
        }

        public static void UpdateLoaiSanPham(LoaiSanPhamModel loaiSP)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update LoaiSanPham set MADVT=@MaDVT,PHANTRAMLOINHUAN=@PhanTramLoiNhuan, TENLOAISP = @TenLoaiSP ", loaiSP);
            }
        }

        #endregion LOAISANPHAM_DataAcess

        //Đã test
        #region PHIEUBAN_DataAcess

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

        public static void UpdatePhieuBan(PhieuBanModel phieuBan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update PhieuBan set SOPHIEU=@SoPhieu,NGAYLAP=@NgayLap,MAKH=@MaKH ", phieuBan);
            }
        }

        #endregion PHIEUBAN_DataAcess

        //Đã test
        #region PHIEUMUA_DataAcess
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
        public static void UpdatePhieuMua(PhieuMuaModel PhieuMua)

        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update PhieuMua set SOPHIEU=@SoPhieu,NGAYLAP=@NgayLap,MANCC=@MaNCC ", PhieuMua);
            }
        }

        #endregion PHIEUMUA_DataAcess

        //Đã test
        #region PHIEUDICHVU_DataAcess
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
                cnn.Execute("insert into PhieuDichVu(MAPHIEU,SOPHIEU,NGAYLAP,MAKH,TONGTIEN,TONGTIENTRATRUOC, TINHTRANG) values (@MaPhieu,@SoPhieu,@NgayLap,@MaKH,@TongTien,@TongTienTraTruoc,@TinhTrang) ", PhieuDichVu);
            }
        }

        public static void UpdateLoaiPhieuDichVu(PhieuDichVuModel PhieuDichVu)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update PhieuDichVu set SOPHIEU=@SoPhieu,NGAYLAP=@NgayLap,MAKH=@MaKH,TONGTIEN=@TongTien,TONGTIENTRATRUOC=@TongTienTraTruoc ", PhieuDichVu);
            }
        }
        #endregion PHIEUDICHVU_DataAcess


        //Đã test 
        public static List<SanPhamModel> FilterSPByName(string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SanPhamModel>("select * from SanPham where TENSP like @n ", new { n = '%' +query + '%' });
                return output.ToList();
            }
        }

        //Loc theo ma loai sp 
        public static List<SanPhamModel> FilterSPByKind(string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SanPhamModel>("select * " +
                    "from SanPham SP join LOAISANPHAM LSP " +
                    "on SP.MALOAISP = LSP.MALOAISP " +
                    "where LSP.TENLOAISP like @q ", new { q = '%' + query + '%' });
                return output.ToList();
            }
        }

        //Loc theo ten 

        public static List<KhachHangModel> FilterKHByName(string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<KhachHangModel>("select * from KhachHang where TENKH like @q ", new { q = '%' + query + '%' });
                return output.ToList();
            }
        }

        //loc theo khu vuc 
        public static List<KhachHangModel> FilterKHByKhuVuc(string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<KhachHangModel>("select * " +
                    "from KhachHang kh join KhuVuc kv " +
                    "on kh.MAKHUVUC = kv.MAKHUVUC" +
                    " where kv.TENKHUVUC like @q ", new { q = '%' + query + '%' });
                return output.ToList();
            }
        }

        //loc theo sdt 
        public static List<KhachHangModel> FilterKHBySDT(string query)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<KhachHangModel>("select * " +
                    "from KhachHang " +
                    "where SDT like @q ", new { q = '%' + query + '%' });
                return output.ToList();
            }
        }

        //Đã test
        public static SanPhamModel LoadSPByMaSP(string masp)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<SanPhamModel>("select * " +
                    "from SanPham " +
                    "where MASP=@n ", new { n = masp});

                return output. ElementAt<SanPhamModel>(0);
            }
        }

        //Đã test
        public static KhachHangModel LoadKHByMaKH(string makh)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<KhachHangModel>("select * " +
                    "from KhachHang " +
                    "where MAKH=@m ",new { m = makh });
                return output.ElementAt<KhachHangModel>(0);
            }
        }


        //Đã test
        public static NhaCungCapModel LoadNCCByMaNCC(string mancc)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<NhaCungCapModel>("select * " +
                    "from NhaCungCap " +
                    "where MANCC=@m ",new { m = mancc });
                return output.ElementAt<NhaCungCapModel>(0);
            }
        }


        public static KhuVucModel LoadKhuVucByMKV(string makv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<KhuVucModel>("select * " +
                    "from KhuVuc " +
                    "where MAKHUVUC=@m ", new { m = makv });
                return output.ElementAt<KhuVucModel>(0);
            }
        }

        public static PhieuBanModel LoadPhieuBanByMaPhieuBan(string mapb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PhieuBanModel>("select * " +
                    "from PhieuBan " +
                    "where MAPHIEU=@m ",new { m = mapb });
                return output.ElementAt<PhieuBanModel>(0);
            }
        }

        public static PhieuMuaModel LoadPhieuBanByMaPhieuMua(string mapb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PhieuMuaModel>("select * " +
                    "from PhieuMua " +
                    "where MAPHIEU=@m ",new { m = mapb });
                return output.ElementAt<PhieuMuaModel>(0);
            }
        }

        //Copy paste tu day 

        public static DonViTinhModel LoadDonViTinhByMADVT(string madvt)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DonViTinhModel>("select * " +
                    "from DonViTinh " +
                    "where MADVT=@m ",new { m = madvt });
                return output.ElementAt<DonViTinhModel>(0);
            }
        }

        public static LoaiDichVuModel LoadLoaiDichVuByMaLDV(string madv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<LoaiDichVuModel>("select * " +
                    "from LoaiDichVu " +
                    "where MALOAIDICHVU=@m ",new { m = madv });
                return output.ElementAt<LoaiDichVuModel>(0);
            }
        }

        public static LoaiSanPhamModel LoadLoaiSanPhamByMaLSP(string malsp)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<LoaiSanPhamModel>("select * " +
                    "from LoaiSanPham " +
                    "where MALOAISANPHAM=@m ",new { m = malsp });
                return output.ElementAt<LoaiSanPhamModel>(0);
            }
        }

        public static PhieuDichVuModel LoadPhieuDichVuByMaPDV(string mapdv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PhieuDichVuModel>("select * " +
                    "from PhieuDichVu " +
                    "where MAPHIEU=@m ", new { m = mapdv });
                return output.ElementAt<PhieuDichVuModel>(0);
            }
        }

        public static TinhTrangModel LoadTinhTrangByMaTT(string matt)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<TinhTrangModel>("select * " +
                    "from TinhTrang " +
                    "where MATINHTRANG=@m ",new { m = matt });
                return output.ElementAt<TinhTrangModel>(0) ;
            }
        }



        //Các bản chi tiết 
        public static ChiTietBanModel LoadChiTietBanByMaCTB(string mactb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ChiTietBanModel>("select * " +
                    "from ChiTietBan " +
                    "where MAPHIEUMUAHANG=@m ", new { m = mactb });
                return output.ElementAt<ChiTietBanModel>(0);
            }
        }


        public static ChiTietMuaModel LoadChiTietMuaByMaCTM(string mactb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output= cnn.Query<ChiTietMuaModel>("select * " +
                    "from ChiTietMua " +
                    "where MAPHIEUMUAHANG=@m ",new { m = mactb });
                return output.ElementAt<ChiTietMuaModel>(0);
            }
        }

        public static ChiTietDichVuModel LoadChiTietDichVuByMaCTDV(string macctdv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<ChiTietDichVuModel>("select * " +
                    "from ChiTietBan " +
                    "where MAPHIEU=@m ",  new { m = macctdv });
                return output.ElementAt<ChiTietDichVuModel>(0);
            }
        }

        
       private  static string LoadConnectionString(string id = "Default")
        {
            return "Data Source=.\\database.db;Version=3;";
        }
    }
}
