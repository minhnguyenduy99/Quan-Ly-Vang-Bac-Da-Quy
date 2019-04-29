using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class LoaiDichVuModel
    {
        private int maLoaiDV;
        private string tenLoaiDV;
        private long donGiaDV;

        public int MaLoaiDV { get => maLoaiDV; set => maLoaiDV = value; }
        public string TenLoaiDV { get => tenLoaiDV; set => tenLoaiDV = value; }
        public long DonGiaDV { get => donGiaDV; set => donGiaDV = value; }
    }
}
