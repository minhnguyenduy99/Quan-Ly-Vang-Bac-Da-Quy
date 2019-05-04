using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class NhaCungCapModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private string maNCC;
        private string diaChi;
        private string dienThoai;

        public string MaNCC
        {
            get => maNCC;
            set => SetProperty(ref maNCC, value);
        }
        public string DiaChi
        {
            get => diaChi;
            set => SetProperty(ref diaChi, value);
        }
        public string DienThoai
        {
            get => dienThoai;
            set => SetProperty(ref dienThoai, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is NhaCungCapModel)
            {
                NhaCungCapModel secondObj = (NhaCungCapModel)obj;
                //Two suppliers only match if and only if they both have the same maNCC.
                return (maNCC.Equals(secondObj.maNCC));
            }
            return false;
        }
    }
}
