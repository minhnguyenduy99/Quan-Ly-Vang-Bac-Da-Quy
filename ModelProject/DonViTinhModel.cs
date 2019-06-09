using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelProject
{
    public class DonViTinhModel : BaseMVVM_Service.BaseMVVM.BaseSubmitableModel
    {
        private string maDVT;
        private string tenDVT;

        public string MaDVT
        {
            get => maDVT;
            set => SetProperty(ref maDVT, value);
        }
        public string TenDVT
        {
            get => tenDVT;
            set => SetProperty(ref tenDVT, value);
        }
        public override bool Equals(object obj)
        {
            if (obj is DonViTinhModel)
            {
                //Two counting meter only match if and only if they both have the same maDVT.
                return (maDVT.Equals(((DonViTinhModel)obj).maDVT));
            }
            return false;
        }

        #region ACCESS_DB_METHOD
        protected override void Add()
        {
            DataAccess.SaveDonViTinh(this);
        }

        protected override void Update()
        {
            DataAccess.UpdateDonViTinh(this);
        }

        protected override void Delete()
        {
            DataAccess.RemoveDonViTinh(this);
        }
        #endregion
    }
}
