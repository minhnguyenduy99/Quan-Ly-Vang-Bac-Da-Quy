using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class NhaCungCapModel : BaseSubmitableModel
    {
        private string maNCC;
        private string tenNCC;
        private string diaChi;
        private string dienThoai;

        public string MaNCC
        {
            get => maNCC;
            set => SetProperty(ref maNCC, value);
        }

        public string TenNCC
        {
            get => tenNCC;
            set => SetProperty(ref tenNCC, value);
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



        protected override void Add()
        {
            
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
