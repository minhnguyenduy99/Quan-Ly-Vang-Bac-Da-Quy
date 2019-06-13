﻿using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class LoaiSanPhamModel : BaseSubmitableModel
    {

        private long ? maLoaiSP;
        private string tenLoaiSP;
        private long? maDVT;
        private double phanTramLoiNhuan;

        //Biến dùng để xác định xem các dữ liệu ReadOnly đã được load từ CSDL chưa.
        private bool isUpdated = false;

        public long? MaLoaiSP
        {
            get => maLoaiSP;
            set => SetProperty(ref maLoaiSP, value);
        }

        public string TenLoaiSP
        {
            get => tenLoaiSP;
            set => SetProperty(ref tenLoaiSP, value);
        }
        public long? MaDVT
        {
            get => maDVT;
            set => SetProperty(ref maDVT, value);
        }
        public double PhanTramLoiNhuan
        {
            get => phanTramLoiNhuan;
            set => SetProperty(ref phanTramLoiNhuan, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is LoaiSanPhamModel)
            {
                LoaiSanPhamModel secondObj = (LoaiSanPhamModel)obj;
                //Two product type only match if and only if they both have the same maLoaiSP.
                return (maLoaiSP.Equals(secondObj.maLoaiSP));
            }
            return false;
        }

        #region ACCESS_DB_METHOD
        protected override void Add()
        {
            maLoaiSP = DataAccess.SaveLoaiSanPham(this);
        }

        protected override void Update()
        {
            DataAccess.UpdateLoaiSanPham(this);
        }

        protected override void Delete()
        {
            DataAccess.RemoveLoaiSanPham(this);
        }
        #endregion
    }
}
