using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class ChiTietDichVuModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private int maPhieu;
        private int maLoaiDV;
        private long chiPhiRieng;
        private int soLuong;
        private long thanhTien;
        private long traTruoc;
        private string ngayGiao;
        private int maTinhTrang;

        public int MaPhieu
        {
            get => maPhieu;
            set => SetProperty(ref maPhieu, value);
        }
        public int MaLoaiDV
        {
            get => maLoaiDV;
            set => SetProperty(ref maLoaiDV, value);
        }
        public long ChiPhiRieng
        {
            get => chiPhiRieng;
            set => SetProperty(ref chiPhiRieng, value);
        }
        public int SoLuong
        {
            get => soLuong;
            set => SetProperty(ref soLuong, value);
        }
        public long ThanhTien
        {
            get => thanhTien;
            set => SetProperty(ref thanhTien, value);
        }
        public long TraTruoc
        {
            get => traTruoc;
            set => SetProperty(ref traTruoc, value);
        }
        public string NgayGiao
        {
            get => ngayGiao;
            set => SetProperty(ref ngayGiao, value);
        }
        public int MaTinhTrang
        {
            get => maTinhTrang;
            set => SetProperty(ref maTinhTrang, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is ChiTietDichVuModel)
            {
                //Two service details only match if and only if they both have the same MaPhieu and MaLoaiDV.
                return ((maPhieu.Equals(((ChiTietDichVuModel)obj).maPhieu)) && (maLoaiDV.Equals(((ChiTietDichVuModel)obj).maLoaiDV)));
            }
            return false;
        }
    }
}
