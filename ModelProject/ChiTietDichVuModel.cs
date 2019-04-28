using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    class ChiTietDichVuModel
    {
        private int maPhieu;
        private int maLoaiDV;
        private long chiPhiRieng;
        private int soLuong;
        private long thanhTien;
        private long traTruoc;
        private string ngayGiao;
        private int maTinhTrang;

        public int MaPhieu { get => maPhieu; set => maPhieu = value; }
        public int MaLoaiDV { get => maLoaiDV; set => maLoaiDV = value; }
        public long ChiPhiRieng { get => chiPhiRieng; set => chiPhiRieng = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public long ThanhTien { get => thanhTien; set => thanhTien = value; }
        public long TraTruoc { get => traTruoc; set => traTruoc = value; }
        public string NgayGiao { get => ngayGiao; set => ngayGiao = value; }
        public int MaTinhTrang { get => maTinhTrang; set => maTinhTrang = value; }
    }
}
