using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class KhachHangModel : BaseSubmitableModel
    {
        private long? maKH;
        private string sdt;
        private string tenKH;
        private string diaChi;
        private long congNo;
        private long? maKhuVuc;
        private string email;

        #region Main properties
        public long ? MaKH
        {
            get => maKH;
            set => SetProperty(ref maKH, value);
        }
        public string SDT
        {
            get => sdt;
            set => SetProperty(ref sdt, value);
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
        public long? MaKhuVuc
        {
            get => maKhuVuc;
            set
            {
                SetProperty(ref maKhuVuc, value);
                TenKhuVuc = DataAccess.LoadKhuVucByMKV(maKhuVuc).TenKhuVuc;   
            }
        }
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        #endregion

        #region Additional properties
        public string TenKhuVuc
        {
            get => GetPropertyValue<string>();
            private set
            {
                SetProperty(value);
            }
        }
        #endregion


        public override bool Equals(object obj)
        {
            if (obj is KhachHangModel)
            {
                KhachHangModel secondObj = (KhachHangModel)obj;
                //Two customers only match if and only if they both have the same maKH.
                return MaKH == secondObj.MaKH;
            }
            return false;
        }

        #region ACCESS_DB_METHOD
        protected override void Add()
        {
            maKH = DataAccess.SaveKhachHang(this);
        }

        protected override void Update()
        {
            DataAccess.UpdateKhachHang(this);
        }

        protected override void Delete()
        {
            DataAccess.RemoveKhachHang(this);
        }
        #endregion
    }
}
