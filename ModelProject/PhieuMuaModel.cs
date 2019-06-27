using BaseMVVM_Service.BaseMVVM;
using ModelProject.ExtensionFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuMuaModel : BaseSubmitableModel, INgayLap
    {
        private long? maPhieu;
        private string ngayLap;
        private long? maNCC;
        private string ghiChu;
        private long thanhTien;

        #region Main properties
        public string GhiChu
        {
            get => ghiChu;
            set => SetProperty(ref ghiChu, value);
        }
        public long ? MaPhieu
        {
            get => maPhieu;
            set
            {
                SetProperty(ref maPhieu, value);
                SoLuongSanPham = DataAccess.LoadChiTietMuaByMaCTM(value).Count();
            }
        }
        public string NgayLap
        {
            get => ngayLap;
            set
            {
                bool tryParse = DateTime.TryParse(value, out DateTime parseDate);
                if (!tryParse)
                {
                    parseDate = new ToDateConverter().Convert(value);
                }
                SetProperty(ref ngayLap, parseDate.ToString("MM/dd/yyyy hh:mm:ss tt"));
                NgayLapDateTime = parseDate;
            }
        }
        public long? MaNCC
        {
            get => maNCC;
            set
            {
                SetProperty(ref maNCC, value);
                TenNCC = DataAccess.LoadNCCByMaNCC(value).TenNCC;
            }
        }
        public long ThanhTien
        {
            get => thanhTien;
            set => SetProperty(ref thanhTien, value);
        }

        #endregion


        #region Additional properties
        public DateTime NgayLapDateTime
        {
            get => GetPropertyValue<DateTime>();
            set
            {
                SetProperty(value);
            }
        }

        public string NgayLapDate
        {
            get => NgayLapDateTime.ToString("dd/MM/yyyy");
            private set => SetProperty(value);
        }

        public string NgayLapTime
        {
            get => NgayLapDateTime.ToShortTimeString();
        }
        public string TenNCC
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
        }
        public int SoLuongSanPham
        {
            get => GetPropertyValue<int>();
            private set => SetProperty(value);
        }
        #endregion

        public PhieuMuaModel() : base()
        {
            IsDataValid = true;
            NgayLap = DateTime.Now.Date.ToString("MM/dd/yyyy hh:mm:ss tt");
        }

        public override bool Equals(object obj)
        {
            if (obj is PhieuMuaModel)
            {
                PhieuMuaModel secondObj = (PhieuMuaModel)obj;
                //Two import receipt only match if and only if they both have the same maPhieu.
                return MaPhieu == secondObj.MaPhieu;
            }
            return false;
        }

        #region ACCESS_DB_REGION
        protected override void Add()
        {
            MaPhieu = DataAccess.SavePhieuMua(this);
        }

        protected override void Update()
        {
            DataAccess.UpdatePhieuMua(this);
        }

        protected override void Delete()
        {
            DataAccess.RemovePhieuMua(this);
        }
        #endregion
    }
}
