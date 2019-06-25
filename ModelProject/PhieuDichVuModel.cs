using BaseMVVM_Service.BaseMVVM;
using ModelProject.ExtensionFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class PhieuDichVuModel : BaseSubmitableModel, INgayLap
    {
        private long ? maPhieu;
        private string ngayLap;
        private long? maKH;
        private long tongTien;
        private long tongTienTraTruoc;
        private long tongTienConLai;
        private long? tinhTrang;
        private string ghiChu;

        private IEnumerable<TinhTrangModel> dsTinhTrang;

        #region Main properties

        public string GhiChu
        {
            get => ghiChu;
            set => SetProperty(ref ghiChu, value);
        }
        public long? MaPhieu
        {
            get => maPhieu;
            set => SetProperty(ref maPhieu, value);
        }
        public string NgayLap
        {
            get => ngayLap;
            set
            {
                var parseSuccess = DateTime.TryParse(value, out DateTime ngayLapDateTime);
                if (!parseSuccess)
                {
                    IsDataValid = false;
                    return;
                }
                NgayLapDateTime = ngayLapDateTime;
                SetProperty(ref ngayLap, value);
            }
        }
        public long? MaKH
        {
            get => maKH;
            set
            {
                SetProperty(ref maKH, value);
                TenKH = DataAccess.LoadKHByMaKH(value).TenKH;
            }
        }
        public long TongTien
        {
            get => tongTien;
            set => SetProperty(ref tongTien, value);
        }
        public long TongTienTraTruoc
        {
            get => tongTienTraTruoc;
            set
            {
                SetProperty(ref tongTienTraTruoc, value);
                UpdatePhieuDichVu();
            }
        }
        public long TongTienConLai
        {
            get => tongTienConLai;
            private set => SetProperty(ref tongTienConLai, value);
        }
        public long? TinhTrang
        {
            get => tinhTrang;
            private set
            {
                SetProperty(ref tinhTrang, value);
                TenTinhTrang = dsTinhTrang.Where(tt => tt.MaTinhTrang == value).ElementAt(0).TenTinhTrang;
            }
        }
        #endregion

        #region Additional Properties
        public DateTime NgayLapDateTime
        {
            get => GetPropertyValue<DateTime>();
            set => SetProperty(value);
        }
        public string NgayLapDate
        {
            get => NgayLapDateTime.ToString("dd/MM/yyyy");
        }
        public string NgayLapTime
        {
            get => NgayLapDateTime.ToShortTimeString();
        }
        public string TenTinhTrang
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
        }
        public string TenKH
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
        }
        #endregion

        public PhieuDichVuModel() : base()
        {
            dsTinhTrang = DataAccess.LoadTinhTrang();
            NgayLap = DateTime.Now.ToString();
            IsDataValid = true;
        }


        private void UpdatePhieuDichVu()
        {
            TongTienConLai = TongTien - TongTienTraTruoc;
            if (TongTienConLai == 0)
            {
                TinhTrang = dsTinhTrang.ElementAt(0).MaTinhTrang;
            }
            else
            {
                TinhTrang = dsTinhTrang.ElementAt(1).MaTinhTrang;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is PhieuDichVuModel)
            {
                PhieuDichVuModel secondObj = (PhieuDichVuModel)obj;
                //Two service receipt only match if and only if they both have the same maPhieu.
                return MaPhieu == secondObj.MaPhieu;
            }
            return false;
        }

        #region ACCESS_DB_REGION
        protected override void Add()
        {
            maPhieu = DataAccess.SavePhieuDichVu(this);
        }

        protected override void Update()
        {
            DataAccess.UpdatePhieuDichVu(this);
        }

        protected override void Delete()
        {
            DataAccess.RemovePhieuDichVu(this);
        }
        #endregion
    }
}
