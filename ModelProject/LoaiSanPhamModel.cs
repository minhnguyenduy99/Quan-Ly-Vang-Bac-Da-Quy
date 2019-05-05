using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class LoaiSanPhamModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
<<<<<<< HEAD
        private int maLoaiSP;
        private string tenLoaiSP;
        private int maDVT;
        private int phanTramLoiNhuan;

        public int MaLoaiSP { get => maLoaiSP; set => maLoaiSP = value; }
        public string TenLoaiSP { get => tenLoaiSP; set => tenLoaiSP = value; }
        public int MaDVT { get => maDVT; set => maDVT = value; }
        public int PhanTramLoiNhuan { get => phanTramLoiNhuan; set => phanTramLoiNhuan = value; }


        public override string ToString()
        {
            return tenLoaiSP;
=======
        private string maLoaiSP;
        private string tenLoaiSP;
        private string maDVT;
        private double phanTramLoiNhuan;

        public string MaLoaiSP
        {
            get => maLoaiSP;
            set => SetProperty(ref maLoaiSP, value);
        }

        public string TenLoaiSP
        {
            get => tenLoaiSP;
            set => SetProperty(ref tenLoaiSP, value);
        }
        public string MaDVT
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
>>>>>>> 909146d5e0b0fcfcadbcc2b611576fac86a4f2f9
        }
    }
}
