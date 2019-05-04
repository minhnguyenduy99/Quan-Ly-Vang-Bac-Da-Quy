using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class TinhTrangModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private string maTinhTrang;
        private string tenTinhTrang;

        public string MaTinhTrang
        {
            get => maTinhTrang;
            set => SetProperty(ref maTinhTrang, value);
        }
        public string TenTinhTrang
        {
            get => tenTinhTrang;
            set => SetProperty(ref tenTinhTrang, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is TinhTrangModel)
            {
                TinhTrangModel secondObj = (TinhTrangModel)obj;
                //Two status only match if and only if they both have the same maTinhTrang.
                return (maTinhTrang.Equals(secondObj.maTinhTrang));
            }
            return false;
        }
    }
}
