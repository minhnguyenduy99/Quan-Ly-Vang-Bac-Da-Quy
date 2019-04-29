using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class KhachHangModel
    {
        private int maKH;
        private int SDT;
        private string tenKH;
        private string diaChi;
        private long congNo;
        private int maKhuVuc;
        private string email;

        public int MaKH { get => maKH; set => maKH = value; }
        public int SDT1 { get => SDT; set => SDT = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public long CongNo { get => congNo; set => congNo = value; }
        public int MaKhuVuc { get => maKhuVuc; set => maKhuVuc = value; }
        public string Email { get => email; set => email = value; }
    }
}
