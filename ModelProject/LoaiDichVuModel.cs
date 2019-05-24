using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class LoaiDichVuModel : BaseMVVM_Service.BaseMVVM.BaseSubmitableModel
    {
        private string maLoaiDV;
        private string tenLoaiDV;
        private long donGiaDV;

        public string MaLoaiDV
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

        public override bool Equals(object obj)
        {
            if (obj is LoaiDichVuModel)
            {
                LoaiDichVuModel secondObj = (LoaiDichVuModel)obj;
                //Two service type only match if and only if they both have the same maLoaiDV.
                return (maLoaiDV.Equals(secondObj.maLoaiDV));
            }
            return false;
        }

        protected override void Add()
        {
            DataAccess.SaveLoaiDichVu(this);
        }

        protected override void Delete()
        {
            throw new NotImplementedException();
        }

        protected override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
