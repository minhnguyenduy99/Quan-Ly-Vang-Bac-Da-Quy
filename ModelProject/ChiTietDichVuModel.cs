using BaseMVVM_Service.BaseMVVM;
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
        private long conLai;
        private string ngayGiao;
        private long? maTinhTrang;


        #region Main properties
        public long? MaPhieu
        {
            get => maPhieu;
            set => SetProperty(ref maPhieu, value);
        }
        public long? MaLoaiDV
        {
            get => maLoaiDV;
            set => SetProperty(ref maLoaiDV, value);
        }
        public int SoLuong
        {
            get => soLuong;
            set
            {
                SetProperty(ref soLuong, value);
                UpdateTongTienChiTiet();
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
                if (value > ThanhTien)
                {
                    throw new Exception("Gía trị trả phải nhỏ hơn hoặc bằng tổng tiền chi tiết");
                }
                SetProperty(ref traTruoc, value);
                UpdateTongTienChiTiet();
            }
        }
        public long ConLai
        {
            get => GetPropertyValue<long>();
            private set => SetProperty(value);
        }
        public string NgayGiao
        {
            get => ngayGiao;
            private set
            {
                SetProperty(ref ngayGiao, value);
            }
        }

        #endregion 

        #region Additional properties
        public DateTime NgayGiaoDateTime
        {
            get => GetPropertyValue<DateTime>();
            set => SetProperty(value);
        }

        public long? MaTinhTrang
        {
            get => maTinhTrang;
            set => SetProperty(ref maTinhTrang, value);
        }

        public string TenLoaiDV
        {
            get => GetPropertyValue<string>();
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

        #endregion


        public ChiTietDichVuModel(PhieuDichVuModel phieuDV, LoaiDichVuModel dichVu)
        {
            MaPhieu = phieuDV.MaPhieu;
            SoLuong = 1;
            MaLoaiDV = dichVu.MaLoaiDV;
            TenLoaiDV = dichVu.TenLoaiDV;
        }

        public void UpdateTongTienChiTiet()
        {
            LoaiDichVuModel dichVu = DataAccess.LoadLoaiDichVuByMaLDV(MaLoaiDV);
            if (dichVu == null)
                return;
            ThanhTien = (DonGiaDV + dichVu.ChiPhiRieng) * SoLuong;
            ConLai = ThanhTien - TraTruoc;
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
