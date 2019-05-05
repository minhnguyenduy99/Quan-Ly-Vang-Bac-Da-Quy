using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class ChiTietDichVuModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private string maPhieu;
        private string maLoaiDV;

        //Đây là những thuộc tính ReadOnly. Sẽ tự load giá trị từ database với mã phiếu cho trước.
        private string tenDV;
        private long donGiaDV;
        private string tinhTrangDV;
        private string ngayBatDauCungCapDV;

        private long chiPhiRieng;
        private int soLuong;
        private long thanhTien;
        private long traTruoc;
        private string ngayGiao;
        private string maTinhTrang;

        public string MaPhieu
        {
            get => maPhieu;
            set => SetProperty(ref maPhieu, value);
        }
        public string MaLoaiDV
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
        public string MaTinhTrang
        {
            get => maTinhTrang;
            set => SetProperty(ref maTinhTrang, value);
        }

        public string TenDV
        {
            get
            {
                LoaiDichVuModel serviceType = DataAccess.LoadLoaiDichVuByMaLDV(maLoaiDV);
                tenDV = serviceType.TenLoaiDV;
                return tenDV;
            }
        }

        public long DonGiaDV
        {
            get
            {
                LoaiDichVuModel serviceType = DataAccess.LoadLoaiDichVuByMaLDV(maLoaiDV);
                donGiaDV = serviceType.DonGiaDV;
                return donGiaDV;
            }
        }

        public string TinhTrangDV
        {
            get
            {
                TinhTrangModel status = DataAccess.LoadTinhTrangByMaTT(maTinhTrang);
                tinhTrangDV = status.TenTinhTrang;
                return tinhTrangDV;
            }
        }

        public string NgayBatDauCungCapDV
        {
            //Chưa implement. Hình như trong dataaccess chưa có get ngày bắt đầu cung cấp DV ?
            get
            {
                return "";
            }
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
