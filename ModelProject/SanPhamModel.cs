using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class SanPhamModel
    {
        private int maSP;
        private int maLoaiSP;
        private long donGiaMuaVao;
        private string tenSP;

        public int MaSP { get => maSP; set => maSP = value; }
        public int MaLoaiSP { get => maLoaiSP; set => maLoaiSP = value; }
        public long DonGiaMuaVao { get => donGiaMuaVao; set => donGiaMuaVao = value; }
        public string TenSP { get => tenSP; set => tenSP = value; }
        //public int maSP { get; set; }
        //public int maLoaiSP { get; set; }
        //public int donGiaMuaVao { get; set; }
        //public string tenSP { get; set; }

    }

}
