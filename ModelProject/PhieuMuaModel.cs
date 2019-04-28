using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    class PhieuMuaModel
    {
        private int maPhieu;
        private int soPhieu;
        private string ngayLap;
        private int maNCC;

        public int MaPhieu { get => maPhieu; set => maPhieu = value; }
        public int SoPhieu { get => soPhieu; set => soPhieu = value; }
        public string NgayLap { get => ngayLap; set => ngayLap = value; }
        public int MaNCC { get => maNCC; set => maNCC = value; }
    }
}
