using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class ChiTietMuaModel : BaseSubmitableModel
    {
        private long? maPhieuMuaHang;
        private long? maSP;
        private int soLuong;
        private long donGia;
        private long thanhTien;

        #region Main properties
        public long? MaPhieuMuaHang
        {
            get => maPhieuMuaHang;
            set => SetProperty(ref maPhieuMuaHang, value);
        }
        public long? MaSP
        {
            get => maSP;
            set
            {
                SetProperty(ref maSP, value);
                if (maSP == null)
                    return;
                TenSP = DataAccess.LoadSanPham().Where(sp => sp.MaSP == maSP).ElementAt(0).TenSP;
            }
        }
        public int SoLuong
        {
            get => soLuong;
            set
            {
                if (value <= 0)
                    throw new Exception("Số lượng nhập phải lớn hơn 0");
                SetProperty(ref soLuong, value);
                UpdateThanhTien();
            }
        }
        public long DonGia
        {
            get => donGia;
            set
            {
                if (value < 0)
                    IsDataValid = false;
                else
                    IsDataValid = true;
                SetProperty(ref donGia, value);
                UpdateThanhTien();
            }
        }
        public long ThanhTien
        {
            get => thanhTien;
            private set => SetProperty(ref thanhTien, value);
        }
        #endregion

        #region Additional Properties
        public string TenSP
        {
            get
            {
                return GetPropertyValue<string>();
            }
            private set
            {
                SetProperty(value);
            }
        }
        public string TenLoaiSP
        {
            get => GetPropertyValue<string>();
            private set
            {
                SetProperty(value);
            }
        }
        public double LoiNhuan
        {
            get => GetPropertyValue<double>();
            private set => SetProperty(value);
        }
        #endregion

        public void UpdateThanhTien()
        {
            ThanhTien = SoLuong * DonGia;
        }
        public ChiTietMuaModel(long? maSP)
        {
            SanPhamModel sanPham = null;
            LoaiSanPhamModel loaiSP = null;
            // mã sản phẩm có thể sai
            try
            {
                sanPham = DataAccess.LoadSPByMaSP(maSP);
                loaiSP = DataAccess.LoadLoaiSanPhamByMaLSP(sanPham.MaLoaiSP);
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Mã sản phẩm không chính xác");
            }
            this.TenLoaiSP = loaiSP.TenLoaiSP;
            this.LoiNhuan = loaiSP.PhanTramLoiNhuan;
            this.MaSP = sanPham.MaSP;
            this.SoLuong = 1;          
        }
        public override bool Equals(object obj)
        {
            //Two buying details only match if and only if they both have the same maPhieuMuaHang and maSP.
            if (obj is ChiTietMuaModel)
            {
                ChiTietMuaModel secondObj = (ChiTietMuaModel)obj;
                return MaPhieuMuaHang == secondObj.MaPhieuMuaHang && MaSP == secondObj.MaSP;
            }
            return false;
        }


        /// <summary>
        /// Sau khi chi tiết mua được submit thành công, sẽ update lại sản phẩm và phiếu mua tương ứng
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSubmited(SubmitEventArgs e)
        {
            if (e.SubmitResult == true)
            {
                switch (e.SubmitType)
                {
                    case SubmitType.Add:
                        UpdatePhieuMua_SubmitType_Add();
                        UpdateSanPham_SubmitType_Add();
                        break;
                    case SubmitType.Delete:
                        UpdatePhieuMua_SubmitType_Delete();
                        UpdateSanPham_SubmitType_Delete();
                        break;
                }

            }
            base.OnSubmited(e);

            // local function
            void UpdatePhieuMua_SubmitType_Add()
            {
                var phieuMua = DataAccess.LoadPhieuBanByMaPhieuMua(MaPhieuMuaHang);
                phieuMua.ThanhTien += this.ThanhTien;
                phieuMua.Submit(SubmitType.Update);
            }
            void UpdateSanPham_SubmitType_Add()
            {
                var sanPham = DataAccess.LoadSPByMaSP(this.MaSP);
                sanPham.DonGiaMuaVao = this.DonGia;
                sanPham.SoLuong += this.SoLuong;
                sanPham.Submit(SubmitType.Update);
            }

            void UpdatePhieuMua_SubmitType_Delete()
            {
                var phieuMua = DataAccess.LoadPhieuBanByMaPhieuMua(MaPhieuMuaHang);
                phieuMua.ThanhTien -= this.ThanhTien;
                phieuMua.Submit(SubmitType.Update);
            }
            void UpdateSanPham_SubmitType_Delete()
            {
                var sanPham = DataAccess.LoadSPByMaSP(this.MaSP);
                sanPham.SoLuong -= this.SoLuong;
                sanPham.Submit(SubmitType.Update);
            }
        }



        #region ACCESS_DB_METHOD
        protected override void Add()
        {
            DataAccess.SaveChiTietMua(this);
        }

        protected override void Update()
        {
            DataAccess.UpdateChiTietMua(this);
        }

        protected override void Delete()
        {
            DataAccess.RemoveChiTietMua(this);
        }
        #endregion
    }
}
