using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.Converters;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.DataViewModels
{
    /// <summary>
    /// View model của báo cáo tồn kho
    /// </summary>
    public class BaoCaoTonKhoViewModel : BaseViewModelObject
    {
        public class ChiTietTonKho : BaseModel
        {
            public SanPhamModel SanPham
            {
                get => GetPropertyValue<SanPhamModel>();
                set => SetProperty(value);
            }
            public string TenLoaiSP
            {
                get => SanPham.TenLoaiSP;
            }
            public long? MaSP
            {
                get => SanPham.MaSP;
            }
            public int TonDau
            {
                get => GetPropertyValue<int>();
                private set => SetProperty(value);
            }
            public int SoLuongMuaVao
            {
                get => GetPropertyValue<int>();
                private set => SetProperty(value);
            }
            public int SoLuongBanRa
            {
                get => GetPropertyValue<int>();
                private set => SetProperty(value);
            }
            public int TonCuoi
            {
                get => GetPropertyValue<int>();
                private set => SetProperty(value);
            }
            public string TenNhaCC
            {
                get => SanPham.TenNhaCC;
            }

            public long GiaTriTonKho
            {
                get => SanPham.DonGiaMuaVao * TonCuoi;
            }

            public ChiTietTonKho(SanPhamModel sanPham, int thang, int nam)
            {
                SanPham = sanPham;

                TonDau = QuerySoLuongTonDau(SanPham.MaSP, thang, nam);
                SoLuongMuaVao = QuerySoLuongMua(SanPham.MaSP, thang, nam);
                SoLuongBanRa = QuerySoLuongBan(SanPham.MaSP, thang, nam);
                TonCuoi = QuerySoLuongTonCuoi(SanPham.MaSP, thang, nam);
            }
            private List<PhieuBanModel> QueryPhieuBan(int thang, int nam)
            {
                var danhSachPhieuBan = DataAccess.LoadPhieuBan();
                List<PhieuBanModel> queryPhieuBan = new List<PhieuBanModel>();
                foreach (var phieuBan in danhSachPhieuBan)
                {
                    var ngayLap = (DateTime)new ToShortDateConverter().ConvertBack(phieuBan.NgayLapDate, null, null, null);
                    if (ngayLap.Month == thang && ngayLap.Year == nam)
                    {
                        queryPhieuBan.Add(phieuBan);
                    }
                }
                return queryPhieuBan;
            }
            private List<PhieuMuaModel> QueryPhieuMua(int thang, int nam)
            {
                var danhSachPhieuMua = DataAccess.LoadPhieuMua();
                List<PhieuMuaModel> queryPhieuMua = new List<PhieuMuaModel>();
                foreach (var phieuMua in danhSachPhieuMua)
                {
                    bool parseSuccess = DateTime.TryParse(phieuMua.NgayLap, out DateTime ngayLap);
                    if (parseSuccess)
                    {
                        if (ngayLap.Month == thang && ngayLap.Year == nam)
                        {
                            queryPhieuMua.Add(phieuMua);
                        }
                    }
                }
                return queryPhieuMua;
            }

            private int QuerySoLuongBan(long? maSP, int thang, int nam)
            {
                int soLuongSanPham = 0;
                var dsPhieuBan = QueryPhieuBan(thang, nam);
                foreach (var phieuban in dsPhieuBan)
                {
                    var dsChiTietBan = DataAccess.LoadChiTietBan().Where(chiTiet => chiTiet.MaSP == maSP && chiTiet.MaPhieuBan == phieuban.MaPhieu);
                    if (dsChiTietBan.Count() != 0)
                        soLuongSanPham += dsChiTietBan.Select(chiTiet => chiTiet.SoLuong).Aggregate((sl1, sl2) => sl1 + sl2);
                }
                return soLuongSanPham;
            }
            private int QuerySoLuongMua(long? maSP, int thang, int nam)
            {
                int soLuongSanPham = 0;
                var dsPhieuMua = QueryPhieuMua(thang, nam);
                foreach (var phieuMua in dsPhieuMua)
                {
                    var dsChiTietMua = DataAccess.LoadChiTietMua().Where(chiTiet => chiTiet.MaSP == maSP && chiTiet.MaPhieuMua == phieuMua.MaPhieu);
                    if (dsChiTietMua.Count() != 0)
                        soLuongSanPham += dsChiTietMua.Select(chiTiet => chiTiet.SoLuong).Aggregate((sl1, sl2) => sl1 + sl2);
                }
                return soLuongSanPham;
            }
            private int QuerySoLuongTonDau(long? maSP, int thang, int nam)
            {
                int soLuongHienTai = DataAccess.LoadSPByMaSP(maSP).SoLuong;

                IEnumerable<ChiTietBanModel> dsChiTietBan =
                    from chiTiet in DataAccess.LoadChiTietBan()
                    join phieuBan in DataAccess.LoadPhieuBan()
                    on chiTiet.MaPhieuBan equals phieuBan.MaPhieu
                    where chiTiet.MaSP == maSP &&
                    GetDateTime(phieuBan.NgayLapDate) > new DateTime(nam, thang, 1)
                    select chiTiet;

                IEnumerable<ChiTietMuaModel> dsChiTietMua =
                    from chiTiet in DataAccess.LoadChiTietMua()
                    join phieuMua in DataAccess.LoadPhieuMua()
                    on chiTiet.MaPhieuMua equals phieuMua.MaPhieu
                    where chiTiet.MaSP == maSP &&
                    phieuMua.NgayLapDateTime > new DateTime(nam, thang, 1)
                    select chiTiet;

                int soLuongBan = dsChiTietBan.Select(chiTiet => chiTiet.SoLuong).Aggregate((sl1, sl2) => sl1 + sl2);
                int soLuongMua = dsChiTietMua.Select(chiTiet => chiTiet.SoLuong).Aggregate((sl1, sl2) => sl1 + sl2);
                return soLuongHienTai + soLuongBan - soLuongMua;

                DateTime GetDateTime(string date)
                {
                    return (DateTime)new ToShortDateConverter().ConvertBack(date, null, null, null);
                }
            }
            private int QuerySoLuongTonCuoi(long? maSP, int thang, int nam)
            {
                return TonDau + SoLuongMuaVao - SoLuongBanRa;
            }
        }

        #region Resources
        private IEnumerable<SanPhamModel> danhSachSanPham;
        #endregion

        public ObservableCollectionViewModel<ChiTietTonKho> DanhSachSanPhamBaoCao { get; private set; }

        public int Thang
        {
            get => GetPropertyValue<int>();
            set => SetProperty(value);
        }

        public int Nam
        {
            get => GetPropertyValue<int>();
            set => SetProperty(value);
        }

        public int TongSoLuongTonKho
        {
            get => GetPropertyValue<int>();
            set => SetProperty(value);
        }

        public long TongGiaTriTonKho
        {
            get => GetPropertyValue<long>();
            set => SetProperty(value);
        }

        /// <summary>
        /// Tạo báo cáo tồn kho của sản phẩm
        /// </summary>
        public bool LoadBaoCaoTonKho()
        {
            try
            {
                foreach (var sanPham in danhSachSanPham)
                {
                    DanhSachSanPhamBaoCao.Add(new ChiTietTonKho(sanPham, Thang, Nam));
                }
                TongSoLuongTonKho = GetTongSoLuongTonKho();
                TongGiaTriTonKho = GetTongGiaTriTonKho();
                return true;
            }
            catch { return false; }
        }


        private long GetTongGiaTriTonKho()
        {
            long tongGiaTri = 0;
            foreach (var chiTietTonKho in DanhSachSanPhamBaoCao.Models)
            {
                tongGiaTri += chiTietTonKho.GiaTriTonKho;
            }
            return tongGiaTri;
        }

        private int GetTongSoLuongTonKho()
        {
            int soLuong = 0;
            foreach (var chiTietTonKho in DanhSachSanPhamBaoCao.Models)
            {
                soLuong += chiTietTonKho.TonCuoi;
            }
            return soLuong;
        }

        protected override void LoadComponentsInternal()
        {
            danhSachSanPham = DataAccess.LoadSanPham();
            DanhSachSanPhamBaoCao = new ObservableCollectionViewModel<ChiTietTonKho>();
        }

        protected override void ReloadComponentsInternal()
        {
            
        }
    }
}
