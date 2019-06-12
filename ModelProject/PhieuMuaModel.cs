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
        private long ? maPhieu;
        private string soPhieu;
        private string ngayLap;
        private string maNCC;
        private string ghiChu;

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
        public string MaNCC
        {
            get => maNCC;
            set => SetProperty(ref maNCC, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is PhieuMuaModel)
            {
                PhieuMuaModel secondObj = (PhieuMuaModel)obj;
                //Two import receipt only match if and only if they both have the same maPhieu.
                return (maPhieu.Equals(secondObj.maPhieu));
            }
            return false;
        }

        #region ACCESS_DB_REGION
        protected override void Add()
        {
            maPhieu = DataAccess.SavePhieuMua(this);
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
