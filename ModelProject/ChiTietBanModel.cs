using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMVVM_Service;

namespace ModelProject
{
    public class ChiTietBanModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private string maPhieuMuaHang;
        private string maSP;

        //Đây là những thuộc tính ReadOnly. Sẽ tự load giá trị từ database với mã phiếu cho trước.
        private string tenSP;
        private string loaiSP;
        private string donViTinh;
        private double donGiaBanRa;
        private double phanTramLoiNhuan;

        private int soLuong;
        private long thanhTien;
        private double donGiaMuaVao;
        
        private long thue;

        //Biến dùng để xác định xem các dữ liệu ReadOnly đã được load từ CSDL chưa.
        private bool isUpdated = false;

        //Constructor tự động tạo 1 chi tiết bán từ object sản phẩm và object hoá đơn.
        public ChiTietBanModel(PhieuBanModel _hoaDon, SanPhamModel _sanPham, int _soLuong)
        {
            maPhieuMuaHang = _hoaDon.MaPhieu;
            maSP = _sanPham.MaSP;
            soLuong = _soLuong;

            LoaiSanPhamModel _loaiSP = DataAccess.LoadLoaiSanPhamByMaLSP(_sanPham.MaLoaiSP);
            tenSP = _sanPham.TenSP;
            loaiSP = _loaiSP.TenLoaiSP;
            donGiaBanRa = DonGiaBanRa;
            phanTramLoiNhuan = _loaiSP.PhanTramLoiNhuan;

            DonViTinhModel dvtModel = DataAccess.LoadDonViTinhByMADVT(_loaiSP.MaDVT);
            donViTinh = dvtModel.TenDVT;
        }



        public string MaPhieuMuaHang
        {
            get => maPhieuMuaHang;
            set => SetProperty(ref maPhieuMuaHang, value);
        }
        public string MaSP
        {
            get => maSP;
            set => SetProperty(ref maSP, value);
        }

        public double PhanTramLoiNhuan
        {
            get
            {
                if (phanTramLoiNhuan != null && phanTramLoiNhuan >= 0)
                    return phanTramLoiNhuan;
                else if (maSP != null && maSP.Length > 0)
                {
                    SanPhamModel product = DataAccess.LoadSPByMaSP(maSP);
                    LoaiSanPhamModel productType = DataAccess.LoadLoaiSanPhamByMaLSP(product.MaLoaiSP);
                    phanTramLoiNhuan = productType.PhanTramLoiNhuan;
                    return phanTramLoiNhuan;
                }
                else
                {
                    Console.WriteLine("Please set masp before retrieve the phanTramLoiNhuan value !");
                    return -1;
                }
            }
        }

        public string TenSP
        {
            get
            {
                if (tenSP != null && tenSP.Length > 0)
                    return tenSP;
                else if (maSP != null && maSP.Length > 0)
                {
                    SanPhamModel product = DataAccess.LoadSPByMaSP(maSP);
                    tenSP = product.TenSP;
                    return tenSP;
                }
                else
                {
                    Console.WriteLine("Please set maSP before get name. Otherwise, will always return null");
                    return "";
                }
            }
        }

        public string LoaiSP
        {
            get
            {
                if (loaiSP != null && loaiSP.Length > 0)
                    return loaiSP;
                else if (maSP != null && maSP.Length > 0)
                {
                    SanPhamModel product = DataAccess.LoadSPByMaSP(maSP);
                    LoaiSanPhamModel productType = DataAccess.LoadLoaiSanPhamByMaLSP(product.MaLoaiSP);
                    loaiSP = productType.TenLoaiSP;
                    return loaiSP;
                }
                else
                {
                    Console.WriteLine("Please set maSP before get LoaiSanPham. Otherwise, will always return null");
                    return "";
                }
            }
        }
        public string DonViTinh
        {
            get
            {
                if (donViTinh != null && donViTinh.Length > 0)
                    return donViTinh;
                else if (maSP != null && maSP.Length > 0)
                {
                    SanPhamModel product = DataAccess.LoadSPByMaSP(maSP);
                    LoaiSanPhamModel productType = DataAccess.LoadLoaiSanPhamByMaLSP(product.MaLoaiSP);
                    donViTinh = productType.TenLoaiSP;
                    return donViTinh;
                }
                else
                {
                    Console.WriteLine("Please set maSP before get DonViTinh. Otherwise, will always return null");
                    return "";
                }
            }
        }

        public double DonGiaBanRa
        {
            get
            {
                if (donGiaBanRa >= 0)
                donGiaBanRa = donGiaMuaVao + (donGiaMuaVao * PhanTramLoiNhuan);
                return donGiaBanRa;
            }
        }

        public int SoLuong
        {
            get => soLuong;
            set => SetProperty(ref soLuong,value);
        }
        public long ThanhTien
        {
            get => thanhTien;
            set => SetProperty(ref thanhTien, value);
        }
        public double DonGiaMuaVao
        {
            get => donGiaMuaVao;
            set => SetProperty(ref donGiaMuaVao, value);
        }
        public long Thue
        {
            get => thue;
            set => SetProperty(ref thue, value);
        }
        public override bool Equals(object obj)
        {
            if (obj is ChiTietBanModel)
            {
                //Two recept details only match if and only if they both have the same MaPhieuMuaHang and MaSP.
                return ((maPhieuMuaHang.Equals(((ChiTietBanModel)obj).maPhieuMuaHang)) && (maSP.Equals(((ChiTietBanModel)obj).maSP)));
            }
            return false;
        }

        public override bool Submit()
        {
            return false;
        }
    }
}
