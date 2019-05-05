using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class ChiTietBanModel
    {
        private int maPhieuMuaHang;
        private int maSP;
        private int soLuong;
        private long thanhTien;
        private long donGiaMuaVao;
        private long chietKhau;
        private long thue;
        public int MaPhieuMuaHang { get => maPhieuMuaHang; set => maPhieuMuaHang = value; }
        public int MaSP { get => maSP; set => maSP = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public long ThanhTien { get => thanhTien; set => thanhTien = value; }
        public long DonGiaMuaVao { get => donGiaMuaVao; set => donGiaMuaVao = value; }
        public long ChietKhau { get => chietKhau; set => chietKhau = value; }
        public long Thue { get => thue; set => thue = value; }

        public override bool Equals(object obj)
        {
            if (obj is ChiTietBanModel)
            {
                return MaSP.Equals(((ChiTietBanModel)obj).MaSP);
            }
            return false;
        }
    }
}
