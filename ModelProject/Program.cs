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

            PhieuMuaModel phieuMuaModel = new PhieuMuaModel()
            {
                MaPhieu = "MA123",
                SoPhieu = "WTF",
                MaNCC = "MACC",
                NgayLap = "123456"
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


            //Test khách hàng.
            //TestKH(khachHangModel);

            //Test sản phẩm.
            //TestSP(spModel);

            //Test đơn vị tính.
            //TestDVT(donViTinhModel);

            //Test khu vực.
            //TestKV(khuVucModel);

            //Test tình trạng.
            //TestTT(tinhTrangModel);

            //Test loại dịch vụ.
            //TestLDV(loaiDichVuModel);

            //Test chi tiết bán.
            //TestCTB(chiTietBanModel);

            //Test chi tiết mua.
            //TestCTM(chiTietMuaModel);

            //Test chi tiết dịch vụ.
            //TestCTDV(chiTietDichVuModel);

            //Test loại sản phẩm.
            //TestLSP(loaiSanPhamModel);

            //Test phiếu mua.
            TestPM(phieuMuaModel);

            Console.ReadKey();
        }

        private static void TestPM(PhieuMuaModel model)
        {
            List<PhieuMuaModel> list = new List<PhieuMuaModel>();

            //Test thêm.
            DataAccess.SavePhieuMua(model);
            list = DataAccess.LoadPhieuMua();
            Console.WriteLine("Load : ");
            foreach (PhieuMuaModel ldv in list)
            {
                Console.WriteLine(ldv.NgayLap);
            }

            //Test update.
            model.NgayLap = "UPDATED";
            DataAccess.UpdatePhieuMua(model);
            list = DataAccess.LoadPhieuMua();
            Console.WriteLine("Update : ");
            foreach (PhieuMuaModel ldv in list)
            {
                Console.WriteLine(ldv.NgayLap);
            }

            //Test xoá.
            DataAccess.RemovePhieuMua(model);

            list = DataAccess.LoadPhieuMua();
            Console.WriteLine("Remove : ");
            foreach (PhieuMuaModel ldv in list)
            {
                Console.WriteLine(ldv.NgayLap);
            }
        }

        private static void TestLSP(LoaiSanPhamModel model)
        {
            List<LoaiSanPhamModel> list = new List<LoaiSanPhamModel>();

            //Test thêm.
            DataAccess.SaveLoaiSanPham(model);
            list = DataAccess.LoadLoaiSanPham();
            Console.WriteLine("Load : ");
            foreach (LoaiSanPhamModel ldv in list)
            {
                Console.WriteLine(ldv.TenLoaiSP);
            }

            //Test update.
            model.TenLoaiSP = "UPDATED";
            DataAccess.UpdateLoaiSanPham(model);
            list = DataAccess.LoadLoaiSanPham();
            Console.WriteLine("Update : ");
            foreach (LoaiSanPhamModel ldv in list)
            {
                Console.WriteLine(ldv.TenLoaiSP);
            }

            //Test xoá.
            DataAccess.RemoveLoaiSanPham(model);

            list = DataAccess.LoadLoaiSanPham();
            Console.WriteLine("Remove : ");
            foreach (LoaiSanPhamModel ldv in list)
            {
                Console.WriteLine(ldv.TenLoaiSP);
            }
        }

        private static void TestCTDV(ChiTietDichVuModel model)
        {
            List<ChiTietDichVuModel> list = new List<ChiTietDichVuModel>();

            //Test thêm.
            DataAccess.SaveChiTietDichVu(model);
            list = DataAccess.LoadChiTietDichVu();
            Console.WriteLine("Load : ");
            foreach (ChiTietDichVuModel ldv in list)
            {
                Console.WriteLine(ldv.SoLuong);
            }

            //Test update.
            model.SoLuong = -1;
            DataAccess.UpdateChiTietDichVu(model);
            list = DataAccess.LoadChiTietDichVu();
            Console.WriteLine("Update : ");
            foreach (ChiTietDichVuModel ldv in list)
            {
                Console.WriteLine(ldv.SoLuong);
            }

            //Test xoá.
            DataAccess.RemoveChiTietDichVu(model);

            list = DataAccess.LoadChiTietDichVu();
            Console.WriteLine("Remove : ");
            foreach (ChiTietDichVuModel ldv in list)
            {
                Console.WriteLine(ldv.SoLuong);
            }
        }

        private static void TestCTM(ChiTietMuaModel model)
        {
            List<ChiTietMuaModel> list = new List<ChiTietMuaModel>();

            //Test thêm.
            DataAccess.SaveChiTietMua(model);
            list = DataAccess.LoadChiTietMua();
            Console.WriteLine("Load : ");
            foreach (ChiTietMuaModel ldv in list)
            {
                Console.WriteLine(ldv.SoLuong);
            }

            //Test update.
            model.SoLuong = -1;
            DataAccess.UpdateChiTietMua(model);
            list = DataAccess.LoadChiTietMua();
            Console.WriteLine("Update : ");
            foreach (ChiTietMuaModel ldv in list)
            {
                Console.WriteLine(ldv.SoLuong);
            }

            //Test xoá.
            DataAccess.RemoveChiTietMua(model);

            list = DataAccess.LoadChiTietMua();
            Console.WriteLine("Remove : ");
            foreach (ChiTietMuaModel ldv in list)
            {
                Console.WriteLine(ldv.SoLuong);
            }
        }

        private static void TestCTB(ChiTietBanModel model)
        {
            List<ChiTietBanModel> list = new List<ChiTietBanModel>();

            //Test thêm.
            DataAccess.SaveChiTietBan(model);
            list = DataAccess.LoadChiTietBan();
            Console.WriteLine("Load : ");
            foreach (ChiTietBanModel ldv in list)
            {
                Console.WriteLine(ldv.ChietKhau);
            }

            //Test update.
            model.ChietKhau = -1;
            DataAccess.UpdateChiTietBan(model);
            list = DataAccess.LoadChiTietBan();
            Console.WriteLine("Update : ");
            foreach (ChiTietBanModel ldv in list)
            {
                Console.WriteLine(ldv.ChietKhau);
            }

            //Test xoá.
            DataAccess.RemoveChiTietBan(model);

            list = DataAccess.LoadChiTietBan();
            Console.WriteLine("Remove : ");
            foreach (ChiTietBanModel ldv in list)
            {
                Console.WriteLine(ldv.ChietKhau);
            }
        }

        private static void TestLDV(LoaiDichVuModel model)
        {
            List<LoaiDichVuModel> list = new List<LoaiDichVuModel>();

            //Test thêm.
            DataAccess.SaveLoaiDichVu(model);
            list = DataAccess.LoadLoaiDichVu();
            Console.WriteLine("Load : ");
            foreach (LoaiDichVuModel ldv in list)
            {
                Console.WriteLine(ldv.TenLoaiDV);
            }

            //Test update.
            model.TenLoaiDV = "*EDITED*";
            DataAccess.UpdateLoaiDichVu(model);
            list = DataAccess.LoadLoaiDichVu();
            Console.WriteLine("Update : ");
            foreach (LoaiDichVuModel ldv in list)
            {
                Console.WriteLine(ldv.TenLoaiDV);
            }

            //Test xoá.
            DataAccess.RemoveLoaiDichVu(model);

            list = DataAccess.LoadLoaiDichVu();
            Console.WriteLine("Remove : ");
            foreach (LoaiDichVuModel ldv in list)
            {
                Console.WriteLine(ldv.TenLoaiDV);
            }
        }

        private static void TestTT(TinhTrangModel model)
        {
            List<TinhTrangModel> list = new List<TinhTrangModel>();

            //Test thêm.
            DataAccess.SaveTinhTrang(model);
            list = DataAccess.LoadTinhTrang();
            Console.WriteLine("Load : ");
            foreach (TinhTrangModel tt in list)
            {
                Console.WriteLine(tt.TenTinhTrang);
            }

            //Test update.
            model.TenTinhTrang = "*EDITED*";
            DataAccess.UpdateTinhTrang(model);
            list = DataAccess.LoadTinhTrang();
            Console.WriteLine("Update : ");
            foreach (TinhTrangModel tt in list)
            {
                Console.WriteLine(tt.TenTinhTrang);
            }

            //Test xoá.
            DataAccess.RemoveTinhTrang(model);

            list = DataAccess.LoadTinhTrang();
            Console.WriteLine("Remove : ");
            foreach (TinhTrangModel tt in list)
            {
                Console.WriteLine(tt.TenTinhTrang);
            }
        }

        private static void TestKV(KhuVucModel model)
        {
            List<KhuVucModel> list = new List<KhuVucModel>();

            //Test thêm.
            DataAccess.SaveKhuVuc(model);
            list = DataAccess.LoadKhuVuc();
            Console.WriteLine("Load : ");
            foreach (KhuVucModel kv in list)
            {
                Console.WriteLine(kv.TenKhuVuc);
            }

            //Test update.
            model.TenKhuVuc = "TENKV1 - EDITED";
            DataAccess.UpdateKhuVuc(model);
            list = DataAccess.LoadKhuVuc();
            Console.WriteLine("Update : ");
            foreach (KhuVucModel kv in list)
            {
                Console.WriteLine(kv.TenKhuVuc);
            }

            //Test xoá.
            DataAccess.RemoveKhuVuc(model);

            list = DataAccess.LoadKhuVuc();
            Console.WriteLine("Remove : ");
            foreach (KhuVucModel kv in list)
            {
                Console.WriteLine(kv.TenKhuVuc);
            }
        }

        private static void TestDVT(DonViTinhModel donViTinhModel)
        {
            List<DonViTinhModel> list = new List<DonViTinhModel>();

            //Test thêm.
            DataAccess.SaveDonViTinh(donViTinhModel);
            list = DataAccess.LoadDonViTinh();
            Console.WriteLine("Load : ");
            foreach (DonViTinhModel dvt in list)
            {
                Console.WriteLine(dvt.TenDVT);
            }

            //Test update.
            donViTinhModel.TenDVT = "TENDVT1 - EDITED";
            DataAccess.UpdateDonViTinh(donViTinhModel);
            list = DataAccess.LoadDonViTinh();
            Console.WriteLine("Update : ");
            foreach (DonViTinhModel dvt in list)
            {
                Console.WriteLine(dvt.TenDVT);
            }

            //Test xoá.
            DataAccess.RemoveDonViTinh(donViTinhModel);

            list = DataAccess.LoadDonViTinh();
            Console.WriteLine("Remove : ");
            foreach (DonViTinhModel dvt in list)
            {
                Console.WriteLine(dvt.TenDVT);
            }
        }

        private static void TestSP(SanPhamModel spModel)
        {
            List<SanPhamModel> list = new List<SanPhamModel>();

            //Test thêm.
            DataAccess.SaveSanPham(spModel);
            list = DataAccess.LoadSanPham();
            Console.WriteLine("Load : ");
            foreach(SanPhamModel sp in list)
            {
                Console.WriteLine(sp.TenSP);
            }

            //Test update.
            spModel.TenSP = "TENSP1 - EDITED";
            DataAccess.UpdateSanPham(spModel);
            list = DataAccess.LoadSanPham();
            Console.WriteLine("Update : ");
            foreach (SanPhamModel sp in list)
            {
                Console.WriteLine(sp.TenSP);
            }

            //Test xoá.
            DataAccess.RemoveSanPham(spModel);

            list = DataAccess.LoadSanPham();
            Console.WriteLine("Remove : ");
            foreach (SanPhamModel sp in list)
            {
                Console.WriteLine(sp.TenSP);
            }
        }

        private static void TestKH(KhachHangModel khachHangModel)
        {
            //Test thêm.
            DataAccess.SaveKhachHang(khachHangModel);

            List<KhachHangModel> list = new List<KhachHangModel>();
            list = DataAccess.LoadKhachHang();
            Console.WriteLine("Load success.");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i].TenKH);
            }

            //Test cập nhập.
            Console.WriteLine("Update : ");

            khachHangModel.TenKH = "TENKH1 - EDITED";

            DataAccess.UpdateKhachHang(khachHangModel);
            list = DataAccess.LoadKhachHang();

            foreach (KhachHangModel k in list)
            {
                Console.WriteLine(k.TenKH);
            }

            //Test xoá.
            Console.WriteLine("Delete : ");

            DataAccess.RemoveKhachHang(khachHangModel);
            list = DataAccess.LoadKhachHang();

            foreach (KhachHangModel k in list)
            {
                Console.WriteLine(k.TenKH);
            }
        }
    }
}

