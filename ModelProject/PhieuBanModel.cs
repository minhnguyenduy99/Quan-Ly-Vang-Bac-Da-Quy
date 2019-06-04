using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuBanModel : BaseSubmitableModel
    {
        private string maPhieu;
        private string soPhieu;
        private string ngayLap;
        private string maKH;

        public string MaPhieu
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
        public string MaKH
        {
            get => maKH;
            set => SetProperty(ref maKH, value);
        }

        public long TongTien
        {
            get => 10000;
        }

        public string Error => throw new NotImplementedException();

        public override bool Equals(object obj)
        {
            if (obj is PhieuBanModel)
            {
                PhieuBanModel secondObj = (PhieuBanModel)obj;
                //Two recepts only match if and only if they both have the same maPhieu.
                return (maPhieu.Equals(secondObj.maPhieu));
            }
            return false;
        }

        protected override void Add()
        {
            DataAccess.SavePhieuBan(this);
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
