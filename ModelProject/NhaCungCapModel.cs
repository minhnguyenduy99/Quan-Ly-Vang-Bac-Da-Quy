using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class NhaCungCapModel
    {
        private int maNCC;
        private string diaChi;
        private int dienThoai;

        public int MaNCC { get => maNCC; set => maNCC = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public int DienThoai { get => dienThoai; set => dienThoai = value; }
    }
}
