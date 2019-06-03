using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class KhuVucModel : BaseSubmitableModel
    {
        private string maKhuVuc;
        private string tenKhuVuc;

        public string MaKhuVuc
        {
            get => maKhuVuc;
            set => SetProperty(ref maKhuVuc, value);
        }
        public string TenKhuVuc
        {
            get => tenKhuVuc;
            set => SetProperty(ref tenKhuVuc, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is KhuVucModel)
            {
                KhuVucModel secondObj = (KhuVucModel)obj;
                //Two location only match if and only if they both have the same maKhuVuc.
                return (maKhuVuc.Equals(secondObj.maKhuVuc));
            }
            return false;
        }


        protected override void Add()
        {
            DataAccess.SaveKhuVuc(this);
        }

        protected override void Update()
        {
            DataAccess.UpdateKhuVuc(this);
        }

        protected override void Delete()
        {
            DataAccess.RemoveKhuVuc(this);
        }
    }
}
