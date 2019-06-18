using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseMVVM_Service;
using BaseMVVM_Service.BaseMVVM;
using ModelProject.ExtensionFunctions;

namespace ModelProject
{
    public class ChiTietBanModel : BaseSubmitableModel
    {
        private long? maPhieu;
        private long? maSP;    
        private int soLuong;
        private long thanhTien;
        

        #region Main properties
        public long? MaPhieu
        {
            get => maPhieu;
            private set => SetProperty(ref maPhieu, value);
        }
        public long? MaSP
        {
            get => maSP;
            private set
            {
                SetProperty(ref maSP, value);
            }
        }
        public int SoLuong
        {
            get => soLuong;
            set
            {
                SetProperty(ref soLuong, value);
                OnSoLuongThayDoi();
            }
        }
        public long ThanhTien
        {
            get => thanhTien;
            set => SetProperty(ref thanhTien, value);
        }
        #endregion


        #region Additional properties
        public long DonGiaMuaVao
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
        public string DonGiaMuaVaoMoneyFormat
        {
            get => MoneyConverter.ConvertToMoneyFormat(DonGiaMuaVao);
        }
        #endregion

        // Tạo 1 chi tiết hóa đơn mới
        public ChiTietBanModel(PhieuBanModel hoaDon, SanPhamModel sanPham)
        {
            int soLuongSanPham = DataAccess.LoadSPByMaSP(sanPham.MaSP).SoLuong;

            MaPhieu = hoaDon.MaPhieu;
            MaSP = sanPham.MaSP;
            SoLuong = 1;

            // load additional data
            var loaiSanPham = DataAccess.LoadLoaiSanPhamByMaLSP(sanPham.MaLoaiSP);
            PhanTramLoiNhuan = loaiSanPham.PhanTramLoiNhuan;
            TenSP = sanPham.TenSP;
            TenLoaiSP = loaiSanPham.TenLoaiSP;
            TenDVT = DataAccess.LoadDonViTinhByMADVT(loaiSanPham.MaDVT).TenDVT;
            DonGiaMuaVao = sanPham.DonGiaMuaVao;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is ChiTietBanModel)
            {
                var castChiTietBan = obj as ChiTietBanModel;
                //Two recept details only match if and only if they both have the same MaPhieuMuaHang and MaSP.
                return MaPhieu == castChiTietBan.MaPhieu && MaSP == castChiTietBan.MaSP;
            }
            return false;
        }

        public event EventHandler SoLuongThayDoi;
        
        protected virtual void OnSoLuongThayDoi()
        {
            int soLuongSanPham = DataAccess.LoadSPByMaSP(MaSP).SoLuong;

            // số lượng sản phẩm đã chọn trong chi tiết lớn hơn số lượng sản phẩm hiện có
            if (SoLuong > soLuongSanPham)
            {
                OnSoLuongSanPhamKhongDu();
                return;
            }

            UpdateTongTienChiTiet();

            SoLuongThayDoi?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void UpdateTongTienChiTiet()
        {
            ThanhTien = (long)(100 + PhanTramLoiNhuan) / 100 * SoLuong * DonGiaMuaVao;
        }

        public event EventHandler SoLuongSanPhamKhongDu;
        protected virtual void OnSoLuongSanPhamKhongDu()
        {
            SoLuongSanPhamKhongDu?.Invoke(this, EventArgs.Empty);
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
