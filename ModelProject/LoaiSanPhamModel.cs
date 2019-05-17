using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class LoaiSanPhamModel
    {
        private int maLoaiSP;
        private int maDVT;
        private int phanTramLoiNhuan;

        public int MaLoaiSP { get => maLoaiSP; set => maLoaiSP = value; }
        public int MaDVT { get => maDVT; set => maDVT = value; }
        public int PhanTramLoiNhuan { get => phanTramLoiNhuan; set => phanTramLoiNhuan = value; }
    }
}
