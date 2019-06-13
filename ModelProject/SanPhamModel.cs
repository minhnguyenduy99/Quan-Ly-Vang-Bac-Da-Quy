using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelProject
{
    public class SanPhamModel : BaseSubmitableModel
    {
        private long? maSP;
        private long maLoaiSP;
        private long donGiaMuaVao;
        private string tenSP;
        private long? maNCC;
        private int soLuong;

        //public SanPhamModel(string MASP, string TENSP, string MALOAISP, long DONGIA)
        //{
        //    maSP = MASP;
        //    maLoaiSP = MALOAISP;
        //    donGiaMuaVao = DONGIA;
        //    tenSP = TENSP;
        //}
        public long ? MaSP
        {
            get => maSP;
            set => SetProperty(ref maSP, value);
        }
        public long MaLoaiSP
        {
            get => maLoaiSP;
            set
            {
                SetProperty(ref maLoaiSP, value);
                TenLoaiSP = DataAccess.LoadLoaiSanPhamByMaLSP(maLoaiSP).TenLoaiSP;
            }
        }

        public long? MaNCC
        {
            get => maNCC;
            set
            {
                SetProperty(ref maNCC, value);
                TenNhaCC = DataAccess.LoadNCCByMaNCC(maNCC).TenNCC;
            }
        }
        public long DonGiaMuaVao
        {
            get => donGiaMuaVao;
            set => SetProperty(ref donGiaMuaVao, value);
        }
        public string TenSP
        {
            get => tenSP;
            set => SetProperty(ref tenSP, value);
        }

        public int SoLuong
        {
            get => soLuong;
            set => SetProperty(ref soLuong, value);
        }


        #region Additional Properties
        public string TenLoaiSP
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
        }

        public string TenNhaCC
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
        }
        #endregion


        public override bool Equals(object obj)
        {
            if (obj is SanPhamModel)
            {
                SanPhamModel secondObj = (SanPhamModel)obj;
                //Two products only match if and only if they both have the same maSP.
                return (maSP.Equals(secondObj.maSP));
            }
            return false;
        }

        #region ACCESS_DB_REGION
        protected override void Add()
        {
            maSP = DataAccess.SaveSanPham(this);
        }

        protected override void Update()
        {
            DataAccess.UpdateSanPham(this);
        }

        protected override void Delete()
        {
            DataAccess.RemoveSanPham(this);
        }
        #endregion
    }

}
