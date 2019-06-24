using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Dapper;

/// <summary>
/// Tất cả hàm Load đều phải đăt try - catch vì trong trường hợp 1 table rỗng thì sẽ gây ra <see cref="DataException"/>
/// </summary>


namespace ModelProject
{
    public class DataAccess
    {
        //Đã test
        //Đã duyệt và fix bởi N.
        //Đã fix return last row.
        #region SANPHAM_DataAcess
        public static List<SanPhamModel> LoadSanPham()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<SanPhamModel>("select * from SANPHAM", new DynamicParameters());
                    return output?.ToList();
                }
                catch (DataException)
                {
                    return new List<SanPhamModel>();
                }
            }
        }

        public static long SaveSanPham(SanPhamModel sanPham)        //Đã check
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into SANPHAM (TENSP,MALOAISP,DONGIAMUAVAO, MANCC, SOLUONG, DONGIABANRA) values (@TenSP,@MaLoaiSP,@DonGiaMuaVao, @MaNCC, @SoLuong, @DonGiaBanRa); SELECT last_insert_rowid()", sanPham);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdateSanPham(SanPhamModel sanPham)      //Đã check
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update SANPHAM set TENSP = @tenSP, MALOAISP = @MaLoaiSP, DONGIAMUAVAO = @DonGiaMuaVao, SOLUONG = @SoLuong, DONGIABANRA = @DonGiaBanRa WHERE MASP = @MaSP", sanPham);
            }
        }


        public static void RemoveSanPham(SanPhamModel sanPham)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM SANPHAM WHERE MASP = @MaSP", sanPham);
            }
        }

        #endregion SANPHAM_DataAcess 

        //Đã test
        //Đã duyệt và fix bởi N.
        //Đã fix return last row.
        #region DONVITINH_DataAcess

        public static List<DonViTinhModel> LoadDonViTinh()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<DonViTinhModel>("select * from DONVITINH", new DynamicParameters());
                    return output.ToList();
                }
                catch(DataException)
                {
                    return new List<DonViTinhModel>();
                }
            }
        }
        public static long SaveDonViTinh(DonViTinhModel dvt)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into DonViTinh(TENDVT) values (@TenDVT); SELECT last_insert_rowid() ", dvt);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }
        public static void UpdateDonViTinh(DonViTinhModel dvt)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update DonViTinh set TENDVT = @TenDVT WHERE MADVT = @maDVT", dvt);
            }
        }

        public static void RemoveDonViTinh(DonViTinhModel dvt)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM DonViTinh WHERE MADVT = @maDVT", dvt);
            }
        }

        #endregion DONVITINH_DataAcess

        //Đã duyệt và fix bởi N.
        //Đã fix return last row.
        #region KHACHHANG_DataAcess

        public static List<KhachHangModel> LoadKhachHang()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<KhachHangModel>("select * from KhachHang", new DynamicParameters());
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<KhachHangModel>();
                }
            }
        }

        public static long SaveKhachHang(KhachHangModel khachHang)
        {
            Console.WriteLine(khachHang.MaKH);
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into KhachHang(TENKH,DIACHI,SDT,MAKHUVUC,EMAIL) values (@TenKH,@DiaChi,@SDT,@MaKhuVuc,@Email); SELECT last_insert_rowid()", khachHang);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdateKhachHang(KhachHangModel KhachHang)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update KhachHang set TENKH = @TenKH,DIACHI = @DiaChi ,SDT = @SDT,CONGNO = @CongNo ,MAKHUVUC = @MaKhuVuc ,EMAIL = @Email WHERE MAKH = @MaKH", KhachHang);
            }
        }

        public static void RemoveKhachHang(KhachHangModel KhachHang)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM KhachHang WHERE MAKH=@MaKH",KhachHang);
            }
        }

        #endregion KHACHHANG_DataAcess

        //Đã test
        //Đã duyệt và fix bởi N.
        //Đã fix last row id.
        #region KHUVUC_DataAcess
        public static List<KhuVucModel> LoadKhuVuc()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<KhuVucModel>("select * from KhuVuc", new DynamicParameters());
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<KhuVucModel>();
                }
            }
        }
        public static long SaveKhuVuc(KhuVucModel khuVuc)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into KhuVuc(TENKHUVUC) values (@TenKhuVuc); SELECT last_insert_rowid()", khuVuc);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }
        public static void UpdateKhuVuc(KhuVucModel khuVuc)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update KhuVuc set TENKHUVUC= @TenKhuVuc WHERE MAKHUVUC = @maKhuVuc", khuVuc);
            }
        }

        public static void RemoveKhuVuc(KhuVucModel khuVuc)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM KHUVUC WHERE MAKHUVUC = @maKhuVuc", khuVuc);
            }
        }

        #endregion KHUVUC_DataAcess

        //Đã test
        //Đã duyệt và fix bởi N.
        //Đã fix last row id.
        #region TINHTRANG_DataAcess
        public static List<TinhTrangModel> LoadTinhTrang()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<TinhTrangModel>("select * from TinhTrang", new DynamicParameters());
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<TinhTrangModel>();
                }
            }
        }
        public static long SaveTinhTrang(TinhTrangModel TinhTrang)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into TinhTrang(TENTINHTRANG) values (@TenTinhTrang); SELECT last_insert_rowid()", TinhTrang);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdateTinhTrang(TinhTrangModel TinhTrang)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update TinhTrang set TENTINHTRANG = @TenTinhTrang WHERE MATINHTRANG = @maTinhTrang", TinhTrang);
            }
        }

        public static void RemoveTinhTrang(TinhTrangModel TinhTrang)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM TINHTRANG WHERE MATINHTRANG = @maTinhTrang", TinhTrang);
            }
        }

        #endregion TINHTRANG_DataAcess

        //Đã test
        //Đã duyệt và fix bởi N.
        //Đã fix last row id.
        #region LOAIDICHVU_DataAcess
        public static List<LoaiDichVuModel> LoadLoaiDichVu()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<LoaiDichVuModel>("select * from LoaiDichVu", new DynamicParameters());
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<LoaiDichVuModel>();
                }
            }
        }
        public static long SaveLoaiDichVu(LoaiDichVuModel loaiDV)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into LoaiDichVu(TENLOAIDV,DONGIADV) values (@TenLoaiDV,@DonGiaDV); SELECT last_insert_rowid()", loaiDV);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdateLoaiDichVu(LoaiDichVuModel loaiDV)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update LoaiDichVu set TENLOAIDV = @TenLoaiDV , DONGIADV = @DonGiaDV WHERE MALOAIDV = @maLoaiDV", loaiDV);
            }
        }

        public static void RemoveLoaiDichVu(LoaiDichVuModel loaiDV)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM LOAIDICHVU WHERE MALOAIDV = @maLoaiDV", loaiDV);
            }
        }

        #endregion LOAIDICHVU_DataAcess

        //Đã test
        //Đã duyệt và fix bởi N.
        //Đã fix last row id.
        #region CHITIETBAN_DataAcess
        public static List<ChiTietBanModel> LoadChiTietBan()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    ChiTietBanModel.IsUpdateFromDatabase = true;
                    var output = cnn.Query<ChiTietBanModel>("select * from chitietban", new DynamicParameters());
                    ChiTietBanModel.IsUpdateFromDatabase = false;
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<ChiTietBanModel>();
                }
            }
        }
        public static long SaveChiTietBan(ChiTietBanModel ctb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into chitietban(MAPHIEUBAN,MASP,SOLUONG,THANHTIEN) values (@MaPhieuBan,@MaSP,@SoLuong,@ThanhTien); SELECT last_insert_rowid()", ctb);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdateChiTietBan(ChiTietBanModel ctb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update chitietban set SOLUONG=@SoLuong,THANHTIEN=@ThanhTien WHERE MAPHIEUMUAHANG = @maPhieuMuaHang and MASP = @maSP", ctb);
            }
        }

        public static void RemoveChiTietBan(ChiTietBanModel ctb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM CHITIETBAN WHERE MAPHIEUMUAHANG = @maPhieuMuaHang and MASP = @maSP", ctb);
            }
        }

        #endregion CHITIETBAN_DataAcess

        //Đã test
        //Đã duyệt và fix bởi N.
        //Đã fix last row id.
        #region CHITIETMUA_DataAcess
        public static List<ChiTietMuaModel> LoadChiTietMua()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    ChiTietMuaModel.IsUpdateFromDatabase = true;
                    var output = cnn.Query<ChiTietMuaModel>("select * from ChiTietMua", new DynamicParameters());
                    ChiTietMuaModel.IsUpdateFromDatabase = false;
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<ChiTietMuaModel>();
                }
            }
        }
        public static long SaveChiTietMua(ChiTietMuaModel ctm)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into ChiTietMua(MAPHIEUMUA,MASP,SOLUONG,DONGIA,THANHTIEN) values (@MaPhieuMua,@MaSP,@SoLuong,@DonGia,@ThanhTien); SELECT last_insert_rowid()", ctm);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdateChiTietMua(ChiTietMuaModel ctm)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update ChiTietMua set SOLUONG = @SoLuong ,DONGIA = @DonGia WHERE MAPHIEUMUAHANG = @maPhieuMuaHang AND MASP = @maSP", ctm);
            }
        }

        public static void RemoveChiTietMua(ChiTietMuaModel ctm)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM CHITIETMUA WHERE MAPHIEUMUAHANG = @maPhieuMuaHang AND MASP = @maSP", ctm);
            }
        }

        #endregion CHITIETMUA_DataAcess

        //Đã test
        //Đã duyệt và fix bởi N.
        //Đã fix last row id.
        #region CHITIETDICHVU_DataAcess
        public static List<ChiTietDichVuModel> LoadChiTietDichVu()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    ChiTietDichVuModel.IsUpdatedFromDatabase = true;
                    var output = cnn.Query<ChiTietDichVuModel>("select * from ChiTietDichVu", new DynamicParameters());
                    ChiTietDichVuModel.IsUpdatedFromDatabase = false;
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<ChiTietDichVuModel>();
                }
            }
        }
        public static long SaveChiTietDichVu(ChiTietDichVuModel ctdv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into ChiTietDichVu(MAPHIEU,MALOAIDV,CHIPHIRIENG,SOLUONG,THANHTIEN,TRATRUOC,CONLAI,NGAYGIAO,MATINHTRANG) values (@MaPhieu,@MaLoaiDV,@ChiPhiRieng,@SoLuong,@ThanhTien,@TraTruoc,@ConLai, @NgayGiao,@MaTinhTrang); SELECT last_insert_rowid()", ctdv);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdateChiTietDichVu(ChiTietDichVuModel ctdv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update ChiTietDichVu set CHIPHIRIENG=@ChiPhiRieng,SOLUONG=@SoLuong,THANHTIEN=@ThanhTien,TRATRUOC=@TraTruoc, CONLAI=@ConLai, NGAYGIAO=@NgayGiao,MATINHTRANG=@MaTinhTrang" , ctdv);
            }
        }

        public static void RemoveChiTietDichVu(ChiTietDichVuModel ctdv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM ChiTietDichVu WHERE MAPHIEU = @maPhieu and MALOAIDV = @maLoaiDV", ctdv);
            }
        }

        #endregion CHITIETDICHVU_DataAcess

        //Đã test
        //Đã duyệt và fix bởi N.
        //Đã fix last row id.
        #region LOAISANPHAM_DataAcess

        public static List<LoaiSanPhamModel> LoadLoaiSanPham()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<LoaiSanPhamModel>("select * from LoaiSanPham", new DynamicParameters());
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<LoaiSanPhamModel>();
                }
            }
        }
        public static long SaveLoaiSanPham(LoaiSanPhamModel loaiSP)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into LoaiSanPham(MADVT,PHANTRAMLOINHUAN, TENLOAISP) values (@MaDVT,@PhanTramLoiNhuan,@TenLoaiSP); SELECT last_insert_rowid()", loaiSP);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdateLoaiSanPham(LoaiSanPhamModel loaiSP)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update LoaiSanPham set MADVT=@MaDVT,PHANTRAMLOINHUAN=@PhanTramLoiNhuan, TENLOAISP = @TenLoaiSP WHERE MALOAISP = @maLoaiSP", loaiSP);
            }
        }

        public static void RemoveLoaiSanPham(LoaiSanPhamModel loaiSP)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM LoaiSanPham WHERE MALOAISP = @maLoaiSP", loaiSP);
            }
        }

        #endregion LOAISANPHAM_DataAcess

        //Viết bởi N.
        //Đã fix last row id.
        #region NHACC_DataAcess

        public static List<NhaCungCapModel> LoadNhaCungCap()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<NhaCungCapModel>("select * from NHACUNGCAP", new DynamicParameters());
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<NhaCungCapModel>();
                }
            }
        }
        public static long SaveNhaCungCap(NhaCungCapModel nhaCC)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into NHACUNGCAP(TENNCC,DIACHI,DIENTHOAI, MAKHUVUC, EMAIL) values (@tenNCC,@diaChi,@dienThoai, @maKhuVuc, @email); SELECT last_insert_rowid()", nhaCC);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdateNhaCungCap(NhaCungCapModel nhaCC)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update NHACUNGCAP set TENNCC=@tenNCC, DIACHI = @diaChi, DIENTHOAI = @dienThoai, MAKHUVUC = @maKhuVuc, EMAIL = @email  WHERE MANCC = @maNCC", nhaCC);
            }
        }

        public static void RemoveNhaCungCap(NhaCungCapModel nhaCC)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM NHACUNGCAP WHERE MANCC = @maNCC", nhaCC);
            }
        }

        #endregion NHACC_DataAcess

        //Đã test
        //Cần check lại số lượng thuộc tính. //Đã check.
        //Cần check lại thuộc tính maPhieu. //Done.
        //Đã fix last row id.
        #region PHIEUBAN_DataAcess

        public static List<PhieuBanModel> LoadPhieuBan()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<PhieuBanModel>("select * from PhieuBan", new DynamicParameters());
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<PhieuBanModel>();
                }
            }
        }
        public static long SavePhieuBan(PhieuBanModel phieuBan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into PhieuBan(MAPHIEU,NGAYLAP,MAKH,CHIETKHAU, THUE, THANHTIEN, GHICHU) values (@MaPhieu,@NgayLap,@MaKH,@ChietKhau, @Thue, @ThanhTien, @GhiChu); SELECT last_insert_rowid()", phieuBan);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdatePhieuBan(PhieuBanModel phieuBan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update PhieuBan set NGAYLAP=@NgayLap,MAKH=@MaKH,CHIETKHAU = @ChietKhau, THUE = @Thue, THANHTIEN = @ThanhTien, GHICHU = @GhiChu WHERE MAPHIEU = @maPhieu", phieuBan);
            }
        }

        public static void RemovePhieuBan(PhieuBanModel phieuBan)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM PhieuBan WHERE MAPHIEU = @maPhieu", phieuBan);
            }
        }

        #endregion PHIEUBAN_DataAcess

        //Đã test
        //Cần check lại số lượng thuộc tính. //Done.
        //Cần check lại thuộc tính maPhieu. //Done.
        //Đã duyệt và fix bởi N.
        //Đã fix last row id.
        #region PHIEUMUA_DataAcess
        public static List<PhieuMuaModel> LoadPhieuMua()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    
                    var output = cnn.Query<PhieuMuaModel>("select * from PhieuMua", new DynamicParameters());
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<PhieuMuaModel>();
                }
            }
        }

        public static long SavePhieuMua(PhieuMuaModel PhieuMua)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into PhieuMua(MAPHIEU,NGAYLAP,MANCC,GHICHU) values (@MaPhieu,@NgayLap,@MaNCC, @GhiChu); SELECT last_insert_rowid()", PhieuMua);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }
        public static void UpdatePhieuMua(PhieuMuaModel PhieuMua)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update PhieuMua set NGAYLAP=@NgayLap,MANCC=@MaNCC, GHICHU = @GhiChu, THANHTIEN = @ThanhTien WHERE MAPHIEU = @maPhieu", PhieuMua);
            }
        }

        public static void RemovePhieuMua(PhieuMuaModel PhieuMua)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM PhieuMua WHERE MAPHIEU = @maPhieu", PhieuMua);
            }
        }

        #endregion PHIEUMUA_DataAcess

        //Đã test
        //Cần check lại số lượng thuộc tính. //Done.
        //Đã duyệt và fix bởi N.
        //Đã fix last row id.
        #region PHIEUDICHVU_DataAcess
        public static List<PhieuDichVuModel> LoadPhieuDichVu()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<PhieuDichVuModel>("select * from PhieuDichVu", new DynamicParameters());
                    return output.ToList();
                }
                catch (DataException)
                {
                    return new List<PhieuDichVuModel>();
                }
            }
        }
        public static long SavePhieuDichVu(PhieuDichVuModel PhieuDichVu)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                long lastRowID = (long)cnn.ExecuteScalar("insert into PhieuDichVu(MAPHIEU,NGAYLAP,MAKH,TONGTIEN,TONGTIENTRATRUOC, TONGTIENCONLAI, TINHTRANG, GHICHU) values (@MaPhieu,@NgayLap,@MaKH,@TongTien,@TongTienTraTruoc,@TongTienConLai, @TinhTrang, @GhiChu); SELECT last_insert_rowid()", PhieuDichVu);
                //lastRowID dùng để xác định ID của một hàng vừa được thêm vào.
                return lastRowID;
            }
        }

        public static void UpdatePhieuDichVu(PhieuDichVuModel PhieuDichVu)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update PhieuDichVu set NGAYLAP=@NgayLap,MAKH=@MaKH,TONGTIEN=@TongTien,TONGTIENTRATRUOC=@TongTienTraTruoc, TONGTIENCONLAI=@TongTienConLai, TINHTRANG = @TinhTrang, GHICHU = @GhiChu WHERE MAPHIEU = @maPhieu", PhieuDichVu);
            }
        }

        public static void RemovePhieuDichVu(PhieuDichVuModel PhieuDichVu)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM PhieuDichVu WHERE MAPHIEU = @maPhieu", PhieuDichVu);
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
        public static SanPhamModel LoadSPByMaSP(long? masp)
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
        public static KhachHangModel LoadKHByMaKH(long? makh)
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
        public static NhaCungCapModel LoadNCCByMaNCC(long? mancc)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<NhaCungCapModel>("select * " +
                    "from NhaCungCap " +
                    "where MANCC=@m ",new { m = mancc });
                return output.ElementAt<NhaCungCapModel>(0);
            }
        }


        public static KhuVucModel LoadKhuVucByMKV(long? makv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<KhuVucModel>("select * " +
                    "from KhuVuc " +
                    "where MAKHUVUC=@m ", new { m = makv });
                return output.ElementAt<KhuVucModel>(0);
            }
        }

        public static PhieuBanModel LoadPhieuBanByMaPhieuBan(long? mapb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PhieuBanModel>("select * " +
                    "from PhieuBan " +
                    "where MAPHIEU=@m ",new { m = mapb });
                return output.ElementAt<PhieuBanModel>(0);
            }
        }

        public static PhieuMuaModel LoadPhieuBanByMaPhieuMua(long? mapb)
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

        public static DonViTinhModel LoadDonViTinhByMADVT(long? madvt)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<DonViTinhModel>("select * " +
                    "from DonViTinh " +
                    "where MADVT=@m ",new { m = madvt });
                return output.ElementAt<DonViTinhModel>(0);
            }
        }

        public static LoaiDichVuModel LoadLoaiDichVuByMaLDV(long? madv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<LoaiDichVuModel>("select * " +
                        "from LoaiDichVu " +
                        "where MALOAIDV=@m ", new { m = madv });
                    return output.ElementAt<LoaiDichVuModel>(0);
                }
                catch { return null; }
            }
        }

        public static LoaiSanPhamModel LoadLoaiSanPhamByMaLSP(long? malsp)
        {
            if (malsp == null)
                return null;
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<LoaiSanPhamModel>("select * " +
                    "from LoaiSanPham " +
                    "where MALOAISP=@m ",new { m = malsp });
                return output.ElementAt<LoaiSanPhamModel>(0);
            }
        }

        public static PhieuDichVuModel LoadPhieuDichVuByMaPDV(long? mapdv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<PhieuDichVuModel>("select * " +
                    "from PhieuDichVu " +
                    "where MAPHIEU=@m ", new { m = mapdv });
                return output.ElementAt<PhieuDichVuModel>(0);
            }
        }

        public static TinhTrangModel LoadTinhTrangByMaTT(long? matt)
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
        public static List<ChiTietBanModel> LoadChiTietBanByMaCTB(long? mactb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<ChiTietBanModel>("select * " +
                        "from ChiTietBan " +
                        "where MAPHIEUBAN=@m ", new { m = mactb });
                    return output.ToList();
                }
                catch { return new List<ChiTietBanModel>(); }
            }
        }


        public static ChiTietMuaModel LoadChiTietMuaByMaCTM(long? mactb)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output= cnn.Query<ChiTietMuaModel>("select * " +
                    "from ChiTietMua " +
                    "where MAPHIEUMUAHANG=@m ",new { m = mactb });
                return output.ElementAt<ChiTietMuaModel>(0);
            }
        }

        public static List<ChiTietDichVuModel> LoadChiTietDichVuByMaCTDV(long? macctdv)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                try
                {
                    var output = cnn.Query<ChiTietDichVuModel>("select * " +
                        "from ChiTietDichVu " +
                        "where MAPHIEU=@m ", new { m = macctdv });
                    return output.ToList();
                }
                catch (DataException) { return new List<ChiTietDichVuModel>(); }
            }
        }

        
       private  static string LoadConnectionString(string id = "Default")
       {
            //return "Data Source=.\\..\\..\\..\\ModelProject\\database.db;Version=3;";

            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
