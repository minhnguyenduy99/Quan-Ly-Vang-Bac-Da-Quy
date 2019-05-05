using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class KhachHangModel : BaseMVVM_Service.BaseMVVM.BaseModel
    {
        private string maKH;
        private int SDT;
        private string tenKH;
        private string diaChi;
        private long congNo;
        private string maKhuVuc;
        private string email;

        public string MaKH
        {
            get => maKH;
            set => SetProperty(ref maKH, value);
        }

        public int SDT1
        {
            get => SDT;
            set => SetProperty(ref SDT, value);
        }
        public string TenKH
        {
            get => tenKH;
            set => SetProperty(ref tenKH, value);
        }
        public string DiaChi
        {
            get => diaChi;
            set => SetProperty(ref diaChi, value);
        }
        public long CongNo
        {
            get => congNo;
            set => SetProperty(ref congNo, value);
        }
        public string MaKhuVuc
        {
            get => maKhuVuc;
            set => SetProperty(ref maKhuVuc, value);
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public override bool Equals(object obj)
        {
            if (obj is KhachHangModel)
            {
                KhachHangModel secondObj = (KhachHangModel)obj;
                //Two customers only match if and only if they both have the same maKH.
                return (maKH.Equals(secondObj.maKH));
            }
            return false;
        }
    }
}
