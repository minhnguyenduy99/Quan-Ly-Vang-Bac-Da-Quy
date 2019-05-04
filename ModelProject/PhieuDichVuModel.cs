using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuDichVuModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private int maPhieu;
        private int soPhieu;
        private string ngayLap;
        private int maKH;
        private long tongTien;
        private long tongTienTraTruoc;

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
        public int MaKH
        {
            get => maKH;
            set => SetProperty(ref maKH, value);
        }
        public long TongTien
        {
            get => tongTien;
            set => SetProperty(ref tongTien, value);
        }
        public long TongTienTraTruoc
        {
            get => tongTienTraTruoc;
            set => SetProperty(ref tongTienTraTruoc, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is PhieuDichVuModel)
            {
                PhieuDichVuModel secondObj = (PhieuDichVuModel)obj;
                //Two service receipt only match if and only if they both have the same maPhieu.
                return (maPhieu.Equals(secondObj.maPhieu));
            }
            return false;
        }
    }
}
