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
        private long ? maKhuVuc;
        private string tenKhuVuc;

        #region Main properties
        public long ? MaKhuVuc
        {
            get => maKhuVuc;
            set => SetProperty(ref maKhuVuc, value);
        }
        public string TenKhuVuc
        {
            get => tenKhuVuc;
            set => SetProperty(ref tenKhuVuc, value);
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (obj is KhuVucModel)
            {
                KhuVucModel secondObj = (KhuVucModel)obj;
                //Two location only match if and only if they both have the same maKhuVuc.
                return MaKhuVuc == secondObj.MaKhuVuc;
            }
            return false;
        }

        #region ACCESS_DB_METHOD
        protected override void Add()
        {
            maKhuVuc = DataAccess.SaveKhuVuc(this);
        }

        protected override void Update()
        {
            DataAccess.UpdateKhuVuc(this);
        }

        protected override void Delete()
        {
            DataAccess.RemoveKhuVuc(this);
        }
        #endregion
    }
}
