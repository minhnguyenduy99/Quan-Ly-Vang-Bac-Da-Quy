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
        private string ngayLap;
        private long maKH;
        private long thanhTien;
        private long traTruoc;
        private long conLai;
        private long? maTinhTrang;
        private string ghiChu;

        #region Main properties

        public string GhiChu
        {
            get => ghiChu;
            set => SetProperty(ref ghiChu, value);
        }
        public long? MaPhieu
        {
            get => maPhieu;
            set => SetProperty(ref maPhieu, value);
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
        public long ThanhTien
        {
            get => thanhTien;
            set => SetProperty(ref thanhTien, value);
        }
        public long TraTruoc
        {
            get => traTruoc;
            set
            {
                if (value < 0 || value > ThanhTien)
                    throw new Exception("Số tiền trả trước không hợp lệ");
                SetProperty(ref traTruoc, value);
                ConLai = ThanhTien - TraTruoc;
            }
        }
        public long ConLai
        {
            get => conLai;
            private set => SetProperty(ref conLai, value);
        }
        public long? MaTinhTrang
        {
            get => maTinhTrang;
            private set => SetProperty(ref maTinhTrang, value);
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is PhieuDichVuModel)
            {
                PhieuDichVuModel secondObj = (PhieuDichVuModel)obj;
                //Two service receipt only match if and only if they both have the same maPhieu.
                return MaPhieu == secondObj.MaPhieu;
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
