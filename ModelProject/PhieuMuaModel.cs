using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuMuaModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private int maPhieu;
        private int soPhieu;
        private string ngayLap;
        private int maNCC;

        public int MaPhieu
        {
            get => maPhieu;
            set => SetProperty(ref maPhieu, value);
        }
        public int SoPhieu
        {
            get => soPhieu;
            set => SetProperty(ref soPhieu, value);
        }
        public string NgayLap
        {
            get => ngayLap;
            set => SetProperty(ref ngayLap, value);
        }
        public int MaNCC
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
    }
}
