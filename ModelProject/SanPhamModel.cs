using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class SanPhamModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private string maSP;
        private string maLoaiSP;
        private long donGiaMuaVao;
        private string tenSP;

<<<<<<< HEAD

        public int MaSP { get => maSP; set => maSP = value; }
        public int MaLoaiSP { get => maLoaiSP; set => maLoaiSP = value; }
        public long DonGiaMuaVao { get => donGiaMuaVao; set => donGiaMuaVao = value; }
        public string TenSP { get => tenSP; set => tenSP = value; }
        //public int maSP { get; set; }
        //public int maLoaiSP { get; set; }
        //public int donGiaMuaVao { get; set; }
        //public string tenSP { get; set; }



=======
        public string MaSP
        {
            get => maSP;
            set => SetProperty(ref maSP, value);
        }
        public string MaLoaiSP
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
>>>>>>> 909146d5e0b0fcfcadbcc2b611576fac86a4f2f9
    }

}
