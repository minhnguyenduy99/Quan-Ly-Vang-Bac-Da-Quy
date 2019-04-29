using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class ChiTietMuaModel
    {
        private int maPhieuMuaHang;
        private int maSP;
        private int soLuong;
        private long donGia;

        public int MaPhieuMuaHang { get => maPhieuMuaHang; set => maPhieuMuaHang = value; }
        public int MaSP { get => maSP; set => maSP = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public long DonGia { get => donGia; set => donGia = value; }
    }
}
