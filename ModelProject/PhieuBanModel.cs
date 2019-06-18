using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuBanModel : BaseSubmitableModel
    {
        private long ? maPhieu;
        private string ngayLap;
        private long? maKH;
        private double thue;
        private double chietKhau;
        private string ghichu;
        private double thanhTien;

        #region Main properties
        public double ThanhTien
        {
            get => thanhTien;
            set => SetProperty(ref thanhTien, value);
        }

        public long? MaPhieu
        {
            get => maPhieu;
            set => SetProperty(ref maPhieu, value);
        }

        public string NgayLap
        {
            get => ngayLap;
            set => SetProperty(ref ngayLap, value);
        }
        public long? MaKH
        {
            get => maKH;
            set => SetProperty(ref maKH, value);
        }

        public double Thue
        {
            get => thue;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new Exception("Thuế (đơn vị %) có giá trị từ 0 đến 100");
                }
                SetProperty(ref thue, value);
            }
        }

        public double ChietKhau
        {
            get => chietKhau;
            set
            {
                if (value < 0 || value > 100)
                    throw new Exception("Chiết khấu (đơn vị %) có giá trị từ 0 đến 100");
                SetProperty(ref chietKhau, value);
            }
        }

        public string GhiChu
        {
            get => ghichu;
            set => SetProperty(ref ghichu, value);
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is PhieuBanModel)
            {
                PhieuBanModel secondObj = (PhieuBanModel)obj;
                //Two recepts only match if and only if they both have the same maPhieu.
                return MaPhieu == secondObj.MaPhieu;
            }
            return false;
        }
       

        #region ACCESS_DB_REGION
        protected override void Add()
        {
            maPhieu = DataAccess.SavePhieuBan(this);
        }

        protected override void Update()
        {
            DataAccess.UpdatePhieuBan(this);
        }

        protected override void Delete()
        {
            DataAccess.RemovePhieuBan(this);
        }
        #endregion
    }
}
