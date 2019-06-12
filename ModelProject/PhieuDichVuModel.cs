using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuDichVuModel : BaseSubmitableModel
    {
        private long ? maPhieu;
        private string soPhieu;
        private string ngayLap;
        private long maKH;
        private long tongTien;
        private long tongTienTraTruoc;
        private int tinhTrang;
        private long maNV;
        private string ghiChu;

        public long MaNV
        {
            get => maNV;
            set => SetProperty(ref maNV, value);
        }

        public string GhiChu
        {
            get => ghiChu;
            set => SetProperty(ref ghiChu, value);
        }

        public long ? MaPhieu
        {
            get => maPhieu;
            set => SetProperty(ref maPhieu, value);
        }
        public string SoPhieu
        {
            get => soPhieu;
            set => SetProperty(ref soPhieu, value);
        }
        public string NgayLap
        {
            get => ngayLap;
            set => SetProperty(ref ngayLap, value);
        }
        public long MaKH
        {
            get => maKH;
            set => SetProperty(ref maKH, value);
        }
        public long TongTien
        {
            get => tongTien;
            set => SetProperty(ref tongTien, value);
        }
        public long TongTienTraTruoc
        {
            get => tongTienTraTruoc;
            set => SetProperty(ref tongTienTraTruoc, value);
        }
        public int TinhTrang
        {
            get => tinhTrang;
            set => SetProperty(ref tinhTrang, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is PhieuDichVuModel)
            {
                PhieuDichVuModel secondObj = (PhieuDichVuModel)obj;
                //Two service receipt only match if and only if they both have the same maPhieu.
                return (maPhieu.Equals(secondObj.maPhieu));
            }
            return false;
        }

        #region ACCESS_DB_REGION
        protected override void Add()
        {
            maPhieu = DataAccess.SavePhieuDichVu(this);
        }

        protected override void Update()
        {
            DataAccess.UpdatePhieuDichVu(this);
        }

        protected override void Delete()
        {
            DataAccess.RemovePhieuDichVu(this);
        }
        #endregion
    }
}
