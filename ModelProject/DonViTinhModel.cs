using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ModelProject
{
    public class DonViTinhModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private int maDVT;
        private string tenDVT;

        public int MaDVT
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
    }
}
