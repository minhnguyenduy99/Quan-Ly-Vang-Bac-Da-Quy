using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class LoaiDichVuModel : BaseMVVM_Service.BaseMVVM.BaseSubmitableModel
    {
        private long ? maLoaiDV;
        private string tenLoaiDV;
        private long donGiaDV;
        private long chiPhiRieng;

        #region Main properties
        public long ? MaLoaiDV
        {
            get => maLoaiDV;
            set => SetProperty(ref maLoaiDV, value);
        }
        public string TenLoaiDV
        {
            get => tenLoaiDV;
            set => SetProperty(ref tenLoaiDV, value);
        }
        public long DonGiaDV
        {
            get => donGiaDV;
            set => SetProperty(ref donGiaDV, value);
        }

        public long ChiPhiRieng
        {
            get => chiPhiRieng;
            set => SetProperty(ref chiPhiRieng, value);
        }
        #endregion


        public override bool Equals(object obj)
        {
            if (obj is LoaiDichVuModel)
            {
                LoaiDichVuModel secondObj = (LoaiDichVuModel)obj;
                //Two service type only match if and only if they both have the same maLoaiDV.
                return MaLoaiDV == secondObj.MaLoaiDV;
            }
            return false;
        }

        #region ACCESS_DB_METHOD
        protected override void Add()
        {
            maLoaiDV = DataAccess.SaveLoaiDichVu(this);
        }

        protected override void Update()
        {
            DataAccess.UpdateLoaiDichVu(this);
        }

        protected override void Delete()
        {
            DataAccess.RemoveLoaiDichVu(this);
        }
        #endregion
    }
}
