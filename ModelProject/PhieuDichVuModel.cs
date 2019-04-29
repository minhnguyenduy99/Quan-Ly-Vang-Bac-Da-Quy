using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuDichVuModel
    {
        private int maPhieu;
        private int soPhieu;
        private string ngayLap;
        private int maKH;
        private long tongTien;
        private long tongTienTraTruoc;

        public int MaPhieu { get => maPhieu; set => maPhieu = value; }
        public int SoPhieu { get => soPhieu; set => soPhieu = value; }
        public string NgayLap { get => ngayLap; set => ngayLap = value; }
        public int MaKH { get => maKH; set => maKH = value; }
        public long TongTien { get => tongTien; set => tongTien = value; }
        public long TongTienTraTruoc { get => tongTienTraTruoc; set => tongTienTraTruoc = value; }
    }
}
