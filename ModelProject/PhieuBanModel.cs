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
        private string soPhieu;
        private string ngayLap;
        private string maNV;
        private string maKH;
        private int thue;
        private int chietKhau;
        private string ghichu;
        private double thanhTien;


        public double ThanhTien
        {
            get => thanhTien;
            set => SetProperty(ref thanhTien, value);
        }

        public long ? MaPhieu
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

        public int Thue
        {
            get => thue;
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new Exception("Thuế (đơn vị %) có giá trị từ 0 đến 100");
                }
                SetProperty(ref thue, value);
                OnThueThayDoi();
            }
        }

        public int ChietKhau
        {
            get => chietKhau;
            set
            {
                if (value < 0 || value > 100)
                    throw new Exception("Chiết khấu (đơn vị %) có giá trị từ 0 đến 100");
                SetProperty(ref chietKhau, value);
                OnChietKhauThayDoi();
            }
        }
        public string MaNV
        {
            get => maNV;
            set => SetProperty(ref maNV, value);
        }

        public string GhiChu
        {
            get => ghichu;
            set => SetProperty(ref ghichu, value);
        }


        public long TongTien { get; set; }

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


        public event EventHandler ThueThayDoi;
        public event EventHandler ChietKhauThayDoi;
        

        protected virtual void OnThueThayDoi()
        {
            ThueThayDoi?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnChietKhauThayDoi()
        {
            ChietKhauThayDoi?.Invoke(this, EventArgs.Empty);
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
