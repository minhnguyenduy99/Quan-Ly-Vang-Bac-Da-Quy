using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.ServiceProviders;
using UIProject.UIConnector;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels
{
    public class LocDonHangWindowVM : BaseWindowViewModel
    {
        private ICommand locPhieuBanCmd;
        private ICommand huyCmd;

        public SearchTextBoxViewModel<KhachHangModel> TimKiemKhachHangVM { get; set; }
   
        public IEnumerable<KhachHangModel> DanhSachKhachHangSource { get; set; }
        
        public EnumFilterViewModel<PhieuBanModel> LocPhieuBanVM { get; set; }


        public DateTime NgayBatDau
        {
            get => GetPropertyValue<DateTime>();
            set => SetProperty(value);
        }

        public DateTime NgayKetThuc
        {
            get => GetPropertyValue<DateTime>();
            set => SetProperty(value);
        }


        public ICommand LocPhieuBanCommand
        {
            get => locPhieuBanCmd ?? new BaseCommand<IWindowExtension>(OnLocPhieuBanCommandExecute);
        }

        public ICommand HuyCommand
        {
            get => huyCmd ?? new BaseCommand<IWindowExtension>(OnHuyCommandExecute);
        }


        public event EventHandler<FilterEventArgs<PhieuBanModel>> BoLocHoanThanh;

        public LocDonHangWindowVM() : base()
        {
            LoadResources();
        }


        private void LoadResources()
        {
            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>(DanhSachKhachHangSource);
            SetUpTimKiemKhachHangVM();
            SetUpBoLoc();
        }

        private void SetUpTimKiemKhachHangVM()
        {
            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>(new ObservableCollection<KhachHangModel>()
            {
                new KhachHangModel(){MaKH = "KH001", TenKH = "Nguyễn Duy Minh", DiaChi = "abc xyz ght", CongNo = 100000},
                new KhachHangModel(){MaKH = "KH002", TenKH = "Nguyễn Văn B", DiaChi = "abc gfgfght", CongNo = 0},
                new KhachHangModel(){MaKH = "KH003", TenKH = "Nguyễn Duy Hào", DiaChi = "abc xyfgâz ght", CongNo = 20},
                new KhachHangModel(){MaKH = "KH004", TenKH = "Nguyễn Duy Bảo", DiaChi = "abc xhka ght", CongNo = 1000},
                new KhachHangModel(){MaKH = "KH005", TenKH = "Nguyễn Duy Tâm", DiaChi = "abc faghz ght", CongNo = 10000000},
            });
            TimKiemKhachHangVM.SelectedValuePath = "TenKH";
            TimKiemKhachHangVM.DefaultFilter = LocTheoTenKhachHangCallBack;
        }

        private void SetUpBoLoc()
        {
            LocPhieuBanVM = new EnumFilterViewModel<PhieuBanModel>();
            LocPhieuBanVM.FilterCallBacks = new List<Func<ItemViewModel<PhieuBanModel>, bool>>()
            {
                new Func<ItemViewModel<PhieuBanModel>, bool>(LocTheoKhachHangCallBack),
                new Func<ItemViewModel<PhieuBanModel>, bool>(LocTheoThoiGianPhieuBanCallBack),
                new Func<ItemViewModel<PhieuBanModel>, bool>(LocTheoNhanVienCallBack)
            };
        }


        #region Các callbacks của bộ lọc
        protected virtual bool LocTheoKhachHangCallBack(ItemViewModel<PhieuBanModel> phieuBan)
        {
            var khachHangDaChon = TimKiemKhachHangVM.SelectedItem as ItemViewModel<KhachHangModel>;
            if (khachHangDaChon == null)
                return true;
            return khachHangDaChon.Model.MaKH.ToLower().Equals(phieuBan.Model.MaKH);
        }
        protected virtual bool LocTheoTenKhachHangCallBack(ItemViewModel<KhachHangModel> khachHang)
        {
            if (khachHang == null || khachHang.Model == null)
                return true;
            return khachHang.Model.TenKH.ToLower().Contains(TimKiemKhachHangVM.Text.ToLower());
            
        }
        protected virtual bool LocTheoThoiGianPhieuBanCallBack(ItemViewModel<PhieuBanModel> phieuBan)
        {
            if (NgayBatDau == null || NgayKetThuc == null)
                return true;
            try
            {
                DateTime castNgayLap = Convert.ToDateTime(phieuBan.Model.NgayLap);
                return castNgayLap >= NgayBatDau && castNgayLap <= NgayKetThuc;
            }
            catch { return true; }
        }      
        protected virtual bool LocTheoNhanVienCallBack(ItemViewModel<PhieuBanModel> phieuBan)
        {
            return true;
        }
        #endregion

        protected virtual void OnLocPhieuBanCommandExecute(IWindowExtension window)
        {
            if (LocPhieuBanVM.FilterCallBacks == null)
                OnBoLocHoanThanh(new FilterEventArgs<PhieuBanModel>(new List<Func<ItemViewModel<PhieuBanModel>, bool>>()));
            else
            {
                var e = new FilterEventArgs<PhieuBanModel>(LocPhieuBanVM.FilterCallBacks);
                OnBoLocHoanThanh(e);
            }
        }

        protected virtual void OnHuyCommandExecute(IWindowExtension window)
        {
            OnClosed(EventArgs.Empty);
        }

        protected virtual void OnBoLocHoanThanh(FilterEventArgs<PhieuBanModel> e)
        {
            BoLocHoanThanh?.Invoke(this, e);
        }
    }
}
