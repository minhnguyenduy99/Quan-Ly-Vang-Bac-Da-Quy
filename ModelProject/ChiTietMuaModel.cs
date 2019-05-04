using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class ChiTietMuaModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private string maPhieuMuaHang;
        private string maSP;
        private int soLuong;
        private long donGia;

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
            set => SetProperty(ref soLuong, value);
        }
        public long DonGia
        {
            get => donGia;
            set => SetProperty(ref donGia, value);
        }
        public override bool Equals(object obj)
        {
            //Two buying details only match if and only if they both have the same maPhieuMuaHang and maSP.
            if (obj is ChiTietMuaModel)
            {
                ChiTietMuaModel secondObj = (ChiTietMuaModel)obj;
                return (maPhieuMuaHang.Equals(secondObj.maPhieuMuaHang)) && (maSP.Equals(secondObj.maSP));
            }
            return false;
        }
    }
}
