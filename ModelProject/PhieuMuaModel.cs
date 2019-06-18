using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuMuaModel : BaseSubmitableModel
    {
        private long? maPhieu;
        private string ngayLap;
        private long? maNCC;
        private string ghiChu;
        private long thanhTien;

        #region Main properties
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
        public string NgayLap
        {
            get => ngayLap;
            private set
            {
                SetProperty(ref ngayLap, value);
            }
        }
        public long? MaNCC
        {
            get => maNCC;
            set => SetProperty(ref maNCC, value);
        }
        public long ThanhTien
        {
            get => thanhTien;
            set => SetProperty(ref thanhTien, value);
        }

        #endregion


        #region Additional properties
        public DateTime NgayLapDateTime
        {
            get => GetPropertyValue<DateTime>();
            set
            {
                SetProperty(value);
                NgayLap = value.ToShortDateString();
            }
        }
        #endregion


        public override bool Equals(object obj)
        {
            if (obj is PhieuMuaModel)
            {
                PhieuMuaModel secondObj = (PhieuMuaModel)obj;
                //Two import receipt only match if and only if they both have the same maPhieu.
                return MaPhieu == secondObj.MaPhieu;
            }
            return false;
        }

        #region ACCESS_DB_REGION
        protected override void Add()
        {
            MaPhieu = DataAccess.SavePhieuMua(this);
        }

        protected override void Update()
        {
            DataAccess.UpdatePhieuMua(this);
        }

        protected override void Delete()
        {
            DataAccess.RemovePhieuMua(this);
        }
        #endregion
    }
}
