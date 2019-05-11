using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuMuaModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private string maPhieu;
        private string soPhieu;
        private string ngayLap;
        private string maNCC;

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
        public string MaNCC
        {
            get => maNCC;
            set => SetProperty(ref maNCC, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is PhieuMuaModel)
            {
                PhieuMuaModel secondObj = (PhieuMuaModel)obj;
                //Two import receipt only match if and only if they both have the same maPhieu.
                return (maPhieu.Equals(secondObj.maPhieu));
            }
            return false;
        }

        public override bool Submit()
        {
            return false;
        }
    }
}
