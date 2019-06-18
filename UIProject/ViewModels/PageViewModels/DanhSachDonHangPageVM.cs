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

        public SearchTextBoxViewModel<PhieuBanModel> TimKiemPhieuBanVM { get; set; }

        public ObservableCollectionViewModel<PhieuBanModel> DanhSachPhieuBanVM { get; set; }

        public HoaDonViewModel HoaDonHienTai { get; private set; }

        public DateTime ThoiGianLapPhieu { get; set; }

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

        private void SetUpHoaDonVM()
        {
            HoaDonHienTai = new HoaDonViewModel();  
        }
        private void SetUpDanhSachPhieuBanVM()
        {
            DanhSachPhieuBanVM = new ObservableCollectionViewModel<PhieuBanModel>(phieuBanSource);
            DanhSachPhieuBanVM.Filters.Add(LocTheoMaDonHangCallBack);
            DanhSachPhieuBanVM.Filters.Add(LocTheoThoiGianCallBack);
            DanhSachPhieuBanVM.SelectedItemChanged += DanhSachPhieuBanVM_SelectedItemChanged;

            void DanhSachPhieuBanVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var phieuBanDaChonItem = e.SelectedItem as ItemViewModel<PhieuBanModel>;
                if (phieuBanDaChonItem == null)
                    return;

                HoaDonHienTai.PhieuBan = phieuBanDaChonItem.Model;

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
            var castPhieuBan = TimKiemPhieuBanVM.SelectedItem as ItemViewModel<PhieuBanModel>;
            if (castPhieuBan == null)
                return true;
            return castPhieuBan.Model.MaPhieu.ToString().ToLower()
                .StartsWith(phieuBan.Model.MaPhieu.ToString().ToLower());
        }

        private bool LocTheoThoiGianCallBack(ItemViewModel<PhieuBanModel> phieuBan)
        {
            if (phieuBan == null)
                return true;
            if (phieuBan.Model == null)
                return true;

            DateTime ngayLap = (DateTime)new ToShortDateConverter().ConvertBack(phieuBan.Model.NgayLap, null, null, null);
            return ngayLap.Date == ThoiGianLapPhieu.Date;
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

            SetUpDanhSachPhieuBanVM();
            SetUpTimKiemPhieuBanVM();
        }

        protected override void ReloadComponentsInternal()
        {
            phieuBanSource = DataAccess.LoadPhieuBan();

            TimKiemPhieuBanVM.Reload();
            TimKiemPhieuBanVM.RefreshItemSource(phieuBanSource);
            DanhSachPhieuBanVM.Reload();
        }
        #endregion

    }
}
