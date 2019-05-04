﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMVVM_Service;

namespace ModelProject
{
    public class ChiTietBanModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private int maPhieuMuaHang;
        private int maSP;
        private int soLuong;
        private long thanhTien;
        private long donGiaMuaVao;
        private long chietKhau;
        private long thue;
        public int MaPhieuMuaHang
        {
            get => maPhieuMuaHang;
            set => SetProperty(ref maPhieuMuaHang, value);
        }
        public int MaSP
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
        public override bool Equals(object obj)
        {
            if (obj is ChiTietBanModel)
            {
                //Two recept details only match if and only if they both have the same MaPhieuMuaHang and MaSP.
                return ((maPhieuMuaHang.Equals(((ChiTietBanModel)obj).maPhieuMuaHang)) && (maSP.Equals(((ChiTietBanModel)obj).maSP)));
            }
            return false;
        }
    }
}
