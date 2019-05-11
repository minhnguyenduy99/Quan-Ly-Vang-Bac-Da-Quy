using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuBanModel : BaseMVVM_Service.BaseMVVM.BaseModel, IDataErrorInfo
    {
        private string maPhieu;
        private string soPhieu;
        private string ngayLap;
        private string maKH;

        public string this[string columnName]
        {
            get
            {
                if (columnName.Equals("NgayLap"))
                {
                    try
                    {
                        DateTime castDate = Convert.ToDateTime(this.ngayLap);
                        if (castDate.Year <= 2000)
                            return "Ngày không hợp lệ";
                        return null;
                    }
                    catch
                    {
                        return "Ngày không hợp lệ";
                    }
                }
                return null;
            }
        }

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
    }
}
