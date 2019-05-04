using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class SanPhamModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private int maSP;
        private int maLoaiSP;
        private long donGiaMuaVao;
        private string tenSP;

        public int MaSP
        {
            get => maSP;
            set => SetProperty(ref maSP, value);
        }
        public int MaLoaiSP
        {
            get => maLoaiSP;
            set => SetProperty(ref maLoaiSP, value);
        }
        public long DonGiaMuaVao
        {
            get => donGiaMuaVao;
            set => SetProperty(ref donGiaMuaVao, value);
        }
        public string TenSP
        {
            get => tenSP;
            set => SetProperty(ref tenSP, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is SanPhamModel)
            {
                SanPhamModel secondObj = (SanPhamModel)obj;
                //Two products only match if and only if they both have the same maSP.
                return (maSP.Equals(secondObj.maSP));
            }
            return false;
        }
    }

}
