using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Converters;
using UIProject.Events;
using UIProject.UIConnector;
using UIProject.ViewModels.DataViewModels;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class DanhSachDonHangPageVM : BasePageViewModel
    {
        private IEnumerable<PhieuBanModel> phieuBanSource;
        private DateTime? thoiGianLapPhieu;

        public SearchTextBoxViewModel<PhieuBanModel> TimKiemPhieuBanVM { get; set; }

        public ObservableCollectionViewModel<PhieuBanModel> DanhSachPhieuBanVM { get; set; }

        public ObservableCollectionViewModel<ChiTietBanModel> DanhSachChiTietBanVM { get; private set; }

        public DateTime? ThoiGianLapPhieu
        {
            get => thoiGianLapPhieu;
            set
            {
                SetProperty(ref thoiGianLapPhieu, value);
                OnThoiGianLapPhieuThayDoi();

                // local function
                void OnThoiGianLapPhieuThayDoi()
                {
                    if (DanhSachPhieuBanVM != null)
                        DanhSachPhieuBanVM.Filter();
                }
            }

        }



        public ICommand BanHangCommand
        {
            get => new BaseCommand(OnBanHangCommandExecute);
        }


        public ICommand ChinhSuaThongTinCommand
        {
            get => new BaseCommand<IWindowExtension>(
                OnChinhSuaThongTinCommandExecute,
                window => DanhSachPhieuBanVM?.SelectedItem != null);
        }

        public ICommand XoaPhieuBanCommand
        {
            get => new BaseCommand<IWindowExtension>(
                OnXoaPhieuBanCommandExecute,
                window => DanhSachPhieuBanVM?.SelectedItem != null);
        }


        public DanhSachDonHangPageVM() : base() { }
        public DanhSachDonHangPageVM(INavigator navigator) : base(navigator)
        {
        }

        private void SetUpDanhSachChiTietBanVM()
        {
            DanhSachChiTietBanVM = new ObservableCollectionViewModel<ChiTietBanModel>();
        }
        private void SetUpDanhSachPhieuBanVM()
        {
            DanhSachPhieuBanVM = new ObservableCollectionViewModel<PhieuBanModel>(phieuBanSource);
            DanhSachPhieuBanVM.Filters.Add(LocTheoMaDonHangCallBack);
            DanhSachPhieuBanVM.Filters.Add(LocTheoThoiGianCallBack);
            DanhSachPhieuBanVM.SelectedItemChanged += DanhSachPhieuBanVM_SelectedItemChanged;


            // local function
            void DanhSachPhieuBanVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var phieuBanDaChonItem = e.SelectedItem as ItemViewModel<PhieuBanModel>;
                if (phieuBanDaChonItem == null)
                {
                    DanhSachChiTietBanVM.Clear();
                    return;
                }

                var danhSachChiTietBan = DataAccess.LoadChiTietBan().Where(chiTiet => chiTiet.MaPhieuBan == phieuBanDaChonItem.Model.MaPhieu);
                DanhSachChiTietBanVM.RefreshItemsSource(danhSachChiTietBan);
            }
        }



        private void SetUpTimKiemPhieuBanVM()
        {
            TimKiemPhieuBanVM = new SearchTextBoxViewModel<PhieuBanModel>(phieuBanSource);
            TimKiemPhieuBanVM.TextChanged += TimKiemPhieuBanVM_TextChanged;


            // local function
            void TimKiemPhieuBanVM_TextChanged(object sender, TextValueChangedEventArgs e)
            {
                if (e.NewValue != e.OldValue)
                {
                    DanhSachPhieuBanVM.Filter();
                }
            }
        }

        #region Filter callbacks

        private bool LocTheoMaDonHangCallBack(ItemViewModel<PhieuBanModel> phieuBan)
        {
            if (phieuBan == null || phieuBan.Model == null)
                return true;
            return phieuBan.Model.MaPhieu.ToString().ToLower()
                .StartsWith(TimKiemPhieuBanVM.Text.ToLower());
        }

        private bool LocTheoThoiGianCallBack(ItemViewModel<PhieuBanModel> phieuBan)
        {
            if (phieuBan == null)
                return true;
            if (phieuBan.Model == null)
                return true;
            if (ThoiGianLapPhieu == null)
                return true;

            DateTime ngayLap = DateTime.Parse(phieuBan.Model.NgayLap);
            return ngayLap.Date == ThoiGianLapPhieu?.Date;
        }
        #endregion


        #region Command Execution
        private void OnChinhSuaThongTinCommandExecute(IWindowExtension obj)
        {
            throw new NotImplementedException();
        }
        private void OnXoaPhieuBanCommandExecute(IWindowExtension obj)
        {
            throw new NotImplementedException();
        }

        private void OnBanHangCommandExecute()
        {
            this.Navigator.Navigate("Bán hàng");
        }

        protected override void LoadComponentsInternal()
        {
            phieuBanSource = DataAccess.LoadPhieuBan();

            SetUpDanhSachChiTietBanVM();
            SetUpDanhSachPhieuBanVM();
            SetUpTimKiemPhieuBanVM();
            ThoiGianLapPhieu = null;
        }

        protected override void ReloadComponentsInternal()
        {
            phieuBanSource = DataAccess.LoadPhieuBan();

            TimKiemPhieuBanVM.Reload();
            TimKiemPhieuBanVM.RefreshItemSource(phieuBanSource);
            DanhSachPhieuBanVM.Reload();
            DanhSachPhieuBanVM.RefreshItemsSource(phieuBanSource);
            ThoiGianLapPhieu = null;
            DanhSachChiTietBanVM.Clear();
        }
        #endregion

    }
}
