using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIProject.ViewModels.DataViewModels
{
    public class ThongTinTongQuanViewModel : BaseViewModel
    {
        private IEnumerable<PhieuBanModel> dsPhieuBan;
        private IEnumerable<KhachHangModel> dsKhachHang;
        private IEnumerable<ChiTietBanModel> dsChiTietBan;

        public int SoLuongHoaDon
        {
            get => GetPropertyValue<int>();
            private set => SetProperty(value);
        }

        public long DoanhThu
        {
            get => GetPropertyValue<long>();
            private set => SetProperty(value);
        }

        public int SoLuongKhachHang
        {
            get => GetPropertyValue<int>();
            private set => SetProperty(value);
        }

        public ThongTinTongQuanViewModel() : base()
        {
            Load();
        }

        public void Load()
        {
            LoadResource();
            SoLuongHoaDon = GetDanhSachPhieuBanTrongNgay().Count();
            DoanhThu = GetDoanhThuTrongNgay();
            if (dsKhachHang == null)
                SoLuongKhachHang = 0;
            else
                SoLuongKhachHang = dsKhachHang.Count();
        }

        private void LoadResource()
        {
            dsPhieuBan = DataAccess.LoadPhieuBan();
            dsChiTietBan = DataAccess.LoadChiTietBan();
            dsKhachHang = DataAccess.LoadKhachHang();
        }
        private IEnumerable<PhieuBanModel> GetDanhSachPhieuBanTrongNgay()
        {
            if (dsKhachHang == null || dsKhachHang.Count() == 0)
                return new List<PhieuBanModel>();
            return dsPhieuBan.Where(phieuBan => IsDateToday(phieuBan.NgayLap));
        }
        private IEnumerable<ChiTietBanModel> GetChiTietBanTrongNgay()
        {
            if (dsChiTietBan.Count() == 0)
                return new List<ChiTietBanModel>();
            List<ChiTietBanModel> dsChiTiet = new List<ChiTietBanModel>();
            foreach(var phieuBan in GetDanhSachPhieuBanTrongNgay())
            {
                foreach(var chiTiet in dsChiTietBan)
                {
                    if (phieuBan.MaPhieu == chiTiet.MaPhieuBan)
                        dsChiTiet.Add(chiTiet);
                }
            }
            return dsChiTiet;
        }
        private long GetDoanhThuTrongNgay()
        {
            var dsPhieuBan = GetDanhSachPhieuBanTrongNgay();
            if (dsPhieuBan.Count() == 0)
                return 0;

            long doanhThu = 0;
            foreach(var phieuBan in dsPhieuBan)
            {
                doanhThu += phieuBan.ThanhTien;
            }
            return doanhThu;
        }
        private bool IsDateToday(string date)
        {
            bool parseSuccess = DateTime.TryParse(date, out DateTime castDate);
            if (!parseSuccess)
                return false;
            return DateTime.Compare(DateTime.Now.Date, castDate.Date) == 0;
        }

    }
}
