using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using UIProject.Converters;
using UIProject.ServiceProviders;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.DataViewModels
{
    /// <summary>
    /// View model của báo cáo tồn kho
    /// </summary>
    public class BaoCaoTonKhoViewModel : BaseViewModelObject, ITableConvertable
    {
        public class ChiTietTonKho : BaseModel
        {
            #region Resources
            private static IEnumerable<PhieuMuaModel> dsPhieuMua;
            private static IEnumerable<ChiTietMuaModel> dsChiTietMua;
            private static IEnumerable<PhieuBanModel> dsPhieuBan;
            private static IEnumerable<ChiTietBanModel> dsChiTietBan;
            private static IEnumerable<SanPhamModel> dsSanPham;
            #endregion
            public static bool ResourceLoaded = false;


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
                if (!ResourceLoaded)
                {
                    LoadResource();
                    ResourceLoaded = true;
                }
                TonDau = QuerySoLuongTonDau(SanPham.MaSP, thang, nam);
                SoLuongMuaVao = QuerySoLuongMua(SanPham.MaSP, thang, nam);
                SoLuongBanRa = QuerySoLuongBan(SanPham.MaSP, thang, nam);
                TonCuoi = QuerySoLuongTonCuoi(SanPham.MaSP, thang, nam);
            }

            private void LoadResource()
            {
                dsPhieuMua = DataAccess.LoadPhieuMua();
                dsPhieuBan = DataAccess.LoadPhieuBan();
                dsChiTietMua = DataAccess.LoadChiTietMua();
                dsChiTietBan = DataAccess.LoadChiTietBan();
                dsSanPham = DataAccess.LoadSanPham();
            }
            private List<PhieuBanModel> QueryPhieuBan(int thang, int nam)
            {
                List<PhieuBanModel> queryPhieuBan = new List<PhieuBanModel>();
                foreach (var phieuBan in dsPhieuBan)
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
                List<PhieuMuaModel> queryPhieuMua = new List<PhieuMuaModel>();
                foreach (var phieuMua in dsPhieuMua)
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
                    var dsChiTietBanTrongThang = dsChiTietBan.Where(chiTiet => chiTiet.MaSP == maSP && chiTiet.MaPhieuBan == phieuban.MaPhieu);
                    if (dsChiTietBanTrongThang.Count() != 0)
                        soLuongSanPham += dsChiTietBanTrongThang.Select(chiTiet => chiTiet.SoLuong).Aggregate((sl1, sl2) => sl1 + sl2);
                }
                return soLuongSanPham;
            }
            private int QuerySoLuongMua(long? maSP, int thang, int nam)
            {
                int soLuongSanPham = 0;
                var dsPhieuMua = QueryPhieuMua(thang, nam);
                foreach (var phieuMua in dsPhieuMua)
                {
                    var dsChiTietMuaTrongThang = dsChiTietMua.Where(chiTiet => chiTiet.MaSP == maSP && chiTiet.MaPhieuMua == phieuMua.MaPhieu);
                    if (dsChiTietMuaTrongThang.Count() != 0)
                        soLuongSanPham += dsChiTietMuaTrongThang.Select(chiTiet => chiTiet.SoLuong).Aggregate((sl1, sl2) => sl1 + sl2);
                }
                return soLuongSanPham;
            }
            private int QuerySoLuongTonDau(long? maSP, int thang, int nam)
            {
                var dsSanPhamTuongUng = dsSanPham.Where(sanPham => sanPham.MaSP == maSP);
                int soLuongHienTai = 0;
                if (dsSanPhamTuongUng.Count() != 0)
                    soLuongHienTai = dsSanPhamTuongUng.Select(sanPham => sanPham.SoLuong).Aggregate((sl1, sl2) => sl1 + sl2);
                int soLuongBan = QuerySoLuongSanPhamBan();
                int soLuongMua = QuerySoLuongSanPhamMua();

                return soLuongHienTai + soLuongBan - soLuongMua;

                // local function
                int QuerySoLuongSanPhamBan()
                {
                    int soLuong = 0;
                    foreach (var chiTiet in dsChiTietBan)
                    {
                        foreach (var phieuBan in dsPhieuBan)
                        { 
                            int ngayBanTreHon = DateTime.Compare(
                                phieuBan.NgayLapDateTime,
                                new DateTime(nam, thang, 1));
                            if (chiTiet.MaPhieuBan == phieuBan.MaPhieu && chiTiet.MaSP == maSP &&
                                ngayBanTreHon >= 0)
                            {
                                soLuong += chiTiet.SoLuong;
                            }
                        }
                    }
                    return soLuong;
                }
                int QuerySoLuongSanPhamMua()
                {
                    int soLuong = 0;
                    foreach (var chiTiet in dsChiTietMua)
                    {
                        foreach (var phieuMua in dsPhieuMua)
                        {
                            int ngayMuaTreHon = DateTime.Compare(
                                                    phieuMua.NgayLapDateTime,
                                                    new DateTime(nam, thang, 1));
                            if (chiTiet.MaPhieuMua == phieuMua.MaPhieu && chiTiet.MaSP == maSP &&
                                ngayMuaTreHon >= 0)
                            {
                                soLuong += chiTiet.SoLuong;
                            }
                        }
                    }
                    return soLuong;
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
            set
            {
                SetProperty(value);
                ThoiGianBaoCao = $"{Thang} / {Nam}";
            }
        }

        public int Nam
        {
            get => GetPropertyValue<int>();
            set
            {
                SetProperty(value);
                ThoiGianBaoCao = $"{Thang} / {Nam}";
            }
        }

        public string ThoiGianBaoCao
        {
            get => GetPropertyValue<string>();
            private set => SetProperty(value);
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
            DanhSachSanPhamBaoCao.Clear();
            ChiTietTonKho.ResourceLoaded = false;
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

        public bool ConvertDataToTable(Table table)
        {
            return CollectionToTableConverter.ConvertToTable(
                DanhSachSanPhamBaoCao.Models,
                new string[]
                {
                    "MaSP",
                    "TenLoaiSP",
                    "TenNhaCC",
                    "TonDau",
                    "SoLuongMuaVao",
                    "SoLuongBanRa",
                    "TonCuoi",
                    "GiaTriTonKho"
                }, new TryMoneyConverter(), table);
        }
    }
}