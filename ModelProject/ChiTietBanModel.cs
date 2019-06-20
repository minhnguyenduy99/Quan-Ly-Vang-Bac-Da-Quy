using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMVVM_Service;
using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject.ExtensionFunctions;

namespace ModelProject
{
    public class ChiTietBanModel : BaseSubmitableModel
    {
        private long? maPhieuBan;
        private long? maSP;    
        private int soLuong;
        private long thanhTien;

        public static bool IsUpdateFromDatabase { get; set; }

        #region Main properties
        public long? MaPhieuBan
        {
            get => maPhieuBan;
            set => SetProperty(ref maPhieuBan, value);
        }
        public long? MaSP
        {
            get => maSP;
            private set
            {
                SetProperty(ref maSP, value);
                var sanPham = DataAccess.LoadSPByMaSP(value);
                if (sanPham == null)
                    return;
                TenSP = sanPham.TenSP;
                DonGiaBanRa = sanPham.DonGiaBanRa;
            }
        }
        public int SoLuong
        {
            get => soLuong;
            set
            {
                if (IsUpdateFromDatabase)
                {
                    SetProperty(ref soLuong, value);
                    return;
                }
                var soLuongSanPham = DataAccess.LoadSPByMaSP(MaSP).SoLuong;
                if (value > soLuongSanPham)
                {
                    OnSoLuongSanPhamKhongDu();
                    return;
                }
                SetProperty(ref soLuong, value);
                OnSoLuongThayDoi();
            }
        }
        public long ThanhTien
        {
            get => thanhTien;
            private set => SetProperty(ref thanhTien, value);
        }
        #endregion


        #region Additional properties
        public long DonGiaBanRa
        {
            get => GetPropertyValue<long>();
            private set
            {
                SetProperty(value);
            }
        }
        public string TenSP
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
        }

        public string TenLoaiSP
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
        }

        public string TenDVT
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
        }
        public double PhanTramLoiNhuan
        {
            get => GetPropertyValue<double>();
            private set => SetProperty(value);
        }
        #endregion


        #region Converts from normal type to money format with comma separation
        public string ThanhTienMoneyFormat
        {
            get => MoneyConverter.ConvertToMoneyFormat(ThanhTien);
        }
        public string DonGiaBanRaMoneyFormat
        {
            get => MoneyConverter.ConvertToMoneyFormat(DonGiaBanRa);
        }
        #endregion

        // Tạo 1 chi tiết hóa đơn mới
        public ChiTietBanModel(PhieuBanModel hoaDon, SanPhamModel sanPham)
        {
            int soLuongSanPham = DataAccess.LoadSPByMaSP(sanPham.MaSP).SoLuong;

            MaPhieuBan = hoaDon.MaPhieu;
            MaSP = sanPham.MaSP;
            SoLuong = 1;


            // load additional data
            var loaiSanPham = DataAccess.LoadLoaiSanPhamByMaLSP(sanPham.MaLoaiSP);
            PhanTramLoiNhuan = loaiSanPham.PhanTramLoiNhuan;
            TenSP = sanPham.TenSP;
            TenLoaiSP = loaiSanPham.TenLoaiSP;
            TenDVT = DataAccess.LoadDonViTinhByMADVT(loaiSanPham.MaDVT).TenDVT;
            DonGiaBanRa = sanPham.DonGiaBanRa;

            ThanhTien = DonGiaBanRa;
        }

        /// <summary>
        /// This constructor is only used for data access from database
        /// </summary>
        public ChiTietBanModel() : base() { }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is ChiTietBanModel)
            {
                var castChiTietBan = obj as ChiTietBanModel;
                //Two recept details only match if and only if they both have the same MaPhieuMuaHang and MaSP.
                return MaPhieuBan == castChiTietBan.MaPhieuBan && MaSP == castChiTietBan.MaSP;
            }
            return false;
        }

        public event EventHandler SoLuongThayDoi;
        
        protected virtual void OnSoLuongThayDoi()
        {
            UpdateTongTienChiTiet();
            SoLuongThayDoi?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void UpdateTongTienChiTiet()
        {
            ThanhTien = DonGiaBanRa * SoLuong;
        }

        public event EventHandler SoLuongSanPhamKhongDu;
        protected virtual void OnSoLuongSanPhamKhongDu()
        {
            SoLuongSanPhamKhongDu?.Invoke(this, EventArgs.Empty);
        }
        protected override void OnSubmited(SubmitEventArgs e)
        {
            if (e.SubmitResult == true)
            {
                switch (e.SubmitType)
                {
                    case SubmitType.Add:
                        UpdateSanPham_SubmitType_Add();
                        break;
                    case SubmitType.Delete:
                        UpdatePhieuBan_SubmitType_Delete();
                        UpdateSanPham_SubmitType_Delete();
                        break;
                }
            }
            base.OnSubmited(e);

            // local function
            void UpdatePhieuBan_SubmitType_Add()
            {
                var phieuBan = DataAccess.LoadPhieuBanByMaPhieuBan(this.MaPhieuBan);
                phieuBan.ThanhTien += this.ThanhTien;
                phieuBan.Submit(SubmitType.Update);
            }
            void UpdateSanPham_SubmitType_Add()
            {
                var sanPham = DataAccess.LoadSPByMaSP(this.MaSP);
                sanPham.SoLuong -= this.SoLuong;
                sanPham.Submit(SubmitType.Update);
            }

            void UpdateSanPham_SubmitType_Delete()
            {
                var phieuBan = DataAccess.LoadPhieuBanByMaPhieuBan(this.MaPhieuBan);
                phieuBan.ThanhTien -= this.ThanhTien;
                phieuBan.Submit(SubmitType.Update);
            }
            void UpdatePhieuBan_SubmitType_Delete()
            {
                var sanPham = DataAccess.LoadSPByMaSP(this.MaSP);
                sanPham.SoLuong += this.SoLuong;
                sanPham.Submit(SubmitType.Update); 
            }
        }



        #region Access Database methods
        protected override void Add()
        {
            DataAccess.SaveChiTietBan(this);
        }

        protected override void Update()
        {
            DataAccess.UpdateChiTietBan(this);
        }

        protected override void Delete()
        {
            DataAccess.RemoveChiTietBan(this);
        }
        #endregion
    }
}
