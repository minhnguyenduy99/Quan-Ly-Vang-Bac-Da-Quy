using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject.Model_rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class ChiTietDichVuModel : BaseSubmitableModel
    {
        private long? maPhieu;
        private long? maLoaiDV;
        private int soLuong;
        private long traTruoc;
        private long thanhTien;
        private string ngayGiao;
        private long conLai;
        private long? maTinhTrang;
        private long chiPhiRieng;

        private IEnumerable<TinhTrangModel> dsTinhTrang;

        public static bool IsUpdatedFromDatabase = false;

        private bool requiredInnerUpdate = false;

        #region Main properties
        public long? MaPhieu
        {
            get => maPhieu;
            set => SetProperty(ref maPhieu, value);
        }
        public long? MaLoaiDV
        {
            get => maLoaiDV;
            set
            {
                SetProperty(ref maLoaiDV, value);
                TenLoaiDV = DataAccess.LoadLoaiDichVuByMaLDV(value).TenLoaiDV;
            }
        }
        public int SoLuong
        {
            get => soLuong;
            set
            {
                if (IsUpdatedFromDatabase)
                {
                    SetProperty(ref soLuong, value);
                    return;
                } 
                if (value <= 0)
                {
                    IsDataValid = false;
                    return;
                }
                IsDataValid = true;
                SetProperty(ref soLuong, value);
                OnThongTinChiTietThayDoi();
            }
        }
        public long ThanhTien
        {
            get => thanhTien;
            private set => SetProperty(ref thanhTien, value);
        }
        public long TraTruoc
        {
            get => traTruoc;
            set
            {
                if (IsUpdatedFromDatabase)
                {
                    SetProperty(ref traTruoc, value);
                    return;
                }
                if (requiredInnerUpdate)
                {
                    SetProperty(ref traTruoc, value);
                    requiredInnerUpdate = false;
                    return;
                }
                SoTienTraTruocRule = new ValueRangeRule<long>(ThanhTien / 2, ThanhTien, Comparer<long>.Default);
                SoTienTraTruocRule.DefaultValue = ThanhTien / 2;
                var valueTemp = value;
                if (!SoTienTraTruocRule.ApplyRuleValid(value))
                {
                    valueTemp = (long)SoTienTraTruocRule.DefaultValue;
                }
                IsDataValid = true;
                SetProperty(ref traTruoc, valueTemp);
                OnThongTinChiTietThayDoi();
            }
        }
        public long ConLai
        {
            get => conLai;
            private set => SetProperty(ref conLai, value);
        }
        public string NgayGiao
        {
            get => ngayGiao;
            private set
            {
                SetProperty(ref ngayGiao, value);
            }
        }
        public long? MaTinhTrang
        {
            get => maTinhTrang;
            set
            {
                SetProperty(ref maTinhTrang, value);
                TenTinhTrang = DataAccess.LoadTinhTrangByMaTT(value).TenTinhTrang;
            }
        }
        public long ChiPhiRieng
        {
            get => chiPhiRieng;
            set
            {
                if (IsUpdatedFromDatabase)
                {
                    SetProperty(ref chiPhiRieng, value);
                    return;
                }
                if (value < 0)
                {
                    IsDataValid = false;
                    return;
                }
                IsDataValid = true;
                SetProperty(ref chiPhiRieng, value);
                OnThongTinChiTietThayDoi();
            }
        }

        #endregion 

        #region Additional properties
        public DateTime NgayGiaoDateTime
        {
            get => GetPropertyValue<DateTime>();
            set
            {
                SetProperty(value);
                NgayGiao = value.ToString("dd/MM/yyyy");
            }
        }

        public string TenLoaiDV
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
        }

        public long DonGiaDuocTinh
        {
            get => GetPropertyValue<long>();
            private set => SetProperty(value);
        }
        public long DonGiaDV
        {
            get => GetPropertyValue<long>();
            private set => SetProperty(value);
        }
        public string TenTinhTrang
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
        }
        public IRule SoTienTraTruocRule
        {
            get => GetPropertyValue<IRule>();
            set => SetProperty(value);
        }
        #endregion

        public ChiTietDichVuModel(PhieuDichVuModel phieuDV, LoaiDichVuModel dichVu) : base()
        {
            // load danh sách tình trạng
            dsTinhTrang = DataAccess.LoadTinhTrang();
            ChiPhiRieng = 0;

            DonGiaDV = dichVu.DonGiaDV;
            DonGiaDuocTinh = DonGiaDV + ChiPhiRieng;

            // Main properties
            MaPhieu = phieuDV.MaPhieu;
            MaLoaiDV = dichVu.MaLoaiDV;
            SoLuong = 1;
            TraTruoc = ThanhTien / 2;
         
            // additional properties
            TenTinhTrang = dsTinhTrang.ElementAt(1).TenTinhTrang;
            TenLoaiDV = dichVu.TenLoaiDV;
            NgayGiaoDateTime = DateTime.Now;         
        }

        // Default constructor for automatical loading from database, should not be used 
        // in any case else
        public ChiTietDichVuModel() : base()
        {
            dsTinhTrang = DataAccess.LoadTinhTrang();
        }

        public event EventHandler ThongTinChiTietThayDoi;

        protected virtual void OnThongTinChiTietThayDoi()
        {
            // cập nhật lại các thông tin của chi tiết dịch vụ khi số lượng thay đổi
            UpdateChiTietDichVu();
            ThongTinChiTietThayDoi?.Invoke(this, EventArgs.Empty);
        }
        private void UpdateChiTietDichVu()
        {
            DonGiaDuocTinh = DonGiaDV + ChiPhiRieng;
            ThanhTien = DonGiaDuocTinh * SoLuong;
            UpdateTinhTrangTraTien();
        }
        private void UpdateTinhTrangTraTien()
        {
            if (TraTruoc < ThanhTien / 2)
            {
                requiredInnerUpdate = true;
                TraTruoc = ThanhTien / 2;
            }
            ConLai = ThanhTien - TraTruoc;
            if (ConLai == 0)
            {
                MaTinhTrang = dsTinhTrang.ElementAt(0).MaTinhTrang;
            }
            else
            {
                MaTinhTrang = dsTinhTrang.ElementAt(1).MaTinhTrang;
            }
        }




        public override bool Equals(object obj)
        {
            if (obj is ChiTietDichVuModel)
            {
                var castObj = obj as ChiTietDichVuModel;
                //Two service details only match if and only if they both have the same MaPhieu and MaLoaiDV.
                return MaPhieu == castObj.MaPhieu && MaLoaiDV == castObj.MaLoaiDV;
            }
            return false;
        }

        #region ACCESS_DB_METHOD
        protected override void Add()
        {
            DataAccess.SaveChiTietDichVu(this);
        }

        protected override void Update()
        {
            DataAccess.UpdateChiTietDichVu(this);
        }

        protected override void Delete()
        {
            DataAccess.RemoveChiTietDichVu(this);
        }
        #endregion ACCESS_DB_METHOD
    }
}
