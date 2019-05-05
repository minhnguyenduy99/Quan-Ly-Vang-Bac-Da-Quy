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
        private int soLuong;
        private long thanhTien;
        private long donGiaMuaVao;
        private long chietKhau;
        private long thue;
<<<<<<< HEAD
        public int MaPhieuMuaHang { get => maPhieuMuaHang; set => maPhieuMuaHang = value; }
        public int MaSP { get => maSP; set => maSP = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public long ThanhTien { get => thanhTien; set => thanhTien = value; }
        public long DonGiaMuaVao { get => donGiaMuaVao; set => donGiaMuaVao = value; }
        public long ChietKhau { get => chietKhau; set => chietKhau = value; }
        public long Thue { get => thue; set => thue = value; }

=======
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
        public long DonGiaMuaVao
        {
            get => donGiaMuaVao;
            set => SetProperty(ref donGiaMuaVao, value);
        }
        public long ChietKhau
        {
            get => chietKhau;
            set => SetProperty(ref chietKhau, value);
        }
        public long Thue
        {
            get => thue;
            set => SetProperty(ref thue, value);
        }
>>>>>>> 909146d5e0b0fcfcadbcc2b611576fac86a4f2f9
        public override bool Equals(object obj)
        {
            if (obj is ChiTietBanModel)
            {
<<<<<<< HEAD
                return MaSP.Equals(((ChiTietBanModel)obj).MaSP);
=======
                //Two recept details only match if and only if they both have the same MaPhieuMuaHang and MaSP.
                return ((maPhieuMuaHang.Equals(((ChiTietBanModel)obj).maPhieuMuaHang)) && (maSP.Equals(((ChiTietBanModel)obj).maSP)));
>>>>>>> 909146d5e0b0fcfcadbcc2b611576fac86a4f2f9
            }
            return false;
        }
    }
}
