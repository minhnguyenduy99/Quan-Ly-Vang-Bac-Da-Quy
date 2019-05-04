using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class LoaiSanPhamModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private int maLoaiSP;
        private int maDVT;
        private int phanTramLoiNhuan;

        public int MaLoaiSP
        {
            get => maLoaiSP;
            set => SetProperty(ref maLoaiSP, value);
        }
        public int MaDVT
        {
            get => maDVT;
            set => SetProperty(ref maDVT, value);
        }
        public int PhanTramLoiNhuan
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
    }
}
