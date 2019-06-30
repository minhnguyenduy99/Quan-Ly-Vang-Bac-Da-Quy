using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.UIConnector;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class DanhSachPhieuMuaPageVM : BasePageViewModel
    {
        private IEnumerable<PhieuMuaModel> phieuMuaSource;
        private IEnumerable<ChiTietMuaModel> dsChiTietMua;

        private DateTime? thoiGianLapPhieu;

        public SearchTextBoxViewModel<PhieuMuaModel> TimKiemPhieuMuaVM { get; set; }

        public ObservableCollectionViewModel<PhieuMuaModel> DanhSachPhieuMuaVM { get; set; }

        public ObservableCollectionViewModel<ChiTietMuaModel> DanhSachChiTietMuaVM { get; private set; }

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
                    if (DanhSachPhieuMuaVM != null)
                        DanhSachPhieuMuaVM.Filter();
                }
            }

        }

        public ICommand NavigateNhapHangPageCommand
        {
            get => new BaseCommand(OnNavigateNhapHangPageCommandExecute);
        }
        public ICommand XoaPhieuMuaCommand
        {
            get => new BaseCommand<IWindow>(
                OnXoaPhieuMuaCommandExecute,
                window => DanhSachPhieuMuaVM?.SelectedItem != null);
        }
        public ICommand HienThiTatCaCommand
        {
            get => new BaseCommand(() => ThoiGianLapPhieu = null);
        }

        public DanhSachPhieuMuaPageVM() : base() { }
        public DanhSachPhieuMuaPageVM(INavigator navigator) : base(navigator)
        {
        }

        private void SetUpDanhSachChiTietMuaVM()
        {
            DanhSachChiTietMuaVM = new ObservableCollectionViewModel<ChiTietMuaModel>();
        }
        private void SetUpDanhSachPhieuMuaVM()
        {
            DanhSachPhieuMuaVM = new ObservableCollectionViewModel<PhieuMuaModel>(phieuMuaSource);
            DanhSachPhieuMuaVM.Filters.Add(LocTheoMaDonHangCallBack);
            DanhSachPhieuMuaVM.Filters.Add(LocTheoThoiGianCallBack);
            DanhSachPhieuMuaVM.SelectedItemChanged += DanhSachPhieuMuaVM_SelectedItemChanged;


            // local function
            void DanhSachPhieuMuaVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var phieuMuaDaChonItem = e.SelectedItem as ItemViewModel<PhieuMuaModel>;
                if (phieuMuaDaChonItem == null)
                {
                    DanhSachChiTietMuaVM.Clear();
                    return;
                }

                var danhSachChiTietMua = dsChiTietMua.Where(chiTiet => chiTiet.MaPhieuMua == phieuMuaDaChonItem.Model.MaPhieu);
                DanhSachChiTietMuaVM.RefreshItemsSource(danhSachChiTietMua);
            }
            bool LocTheoThoiGianCallBack(ItemViewModel<PhieuMuaModel> phieuMua)
            {
                if (phieuMua == null)
                    return true;
                if (phieuMua.Model == null)
                    return true;
                if (ThoiGianLapPhieu == null)
                    return true;

                DateTime ngayLap = DateTime.Parse(phieuMua.Model.NgayLap);
                return ngayLap.Date == ThoiGianLapPhieu?.Date;
            }
            bool LocTheoMaDonHangCallBack(ItemViewModel<PhieuMuaModel> phieuMua)
            {
                if (phieuMua == null || phieuMua.Model == null)
                    return true;
                return phieuMua.Model.MaPhieu.ToString().ToLower()
                    .StartsWith(TimKiemPhieuMuaVM.Text.ToLower());
            }
        }



        private void SetUpTimKiemPhieuMuaVM()
        {
            TimKiemPhieuMuaVM = new SearchTextBoxViewModel<PhieuMuaModel>(phieuMuaSource);
            TimKiemPhieuMuaVM.TextChanged += TimKiemPhieuMuaVM_TextChanged;


            // local function
            void TimKiemPhieuMuaVM_TextChanged(object sender, TextValueChangedEventArgs e)
            {
                if (e.NewValue != e.OldValue)
                {
                    DanhSachPhieuMuaVM.Filter();
                }
            }
        }

        #region Filter callbacks




        #endregion


        #region Command Execution
        private void OnXoaPhieuMuaCommandExecute(IWindow window)
        {
            DialogWindowViewModel notifyDeleteWnd = new DialogWindowViewModel()
            {
                DialogType = DialogWindowType.YesNo,
                MessageText = "Bạn muốn xóa phiếu nhập hàng này?",
                YesText = "Có",
                NoText = "Không"
            };
            window.DataContext = notifyDeleteWnd;
            notifyDeleteWnd.ButtonPressed += NotifyDeleteWnd_ButtonPressed;

            if (window.ShowDialog() == true)
            {
                // not implemented yet
            }
            
            void NotifyDeleteWnd_ButtonPressed(object sender, DialogButtonPressedEventArgs e)
            {
                if (e.DialogResult == DialogResult.Yes)
                    window.DialogResult =  true;
                else
                    window.DialogResult = false;
                window.Close();
            }
        }



        private void OnNavigateNhapHangPageCommandExecute()
        {
            this.Navigator.Navigate("Nhập hàng");
        }
        private void OnHienThiTatCaCommandExecute()
        {
            ThoiGianLapPhieu = null;
        }

        private void RefreshResource()
        {
            phieuMuaSource = DataAccess.LoadPhieuMua();
            dsChiTietMua = DataAccess.LoadChiTietMua();
        }

        protected override void LoadComponentsInternal()
        {
            RefreshResource();

            SetUpDanhSachChiTietMuaVM();
            SetUpDanhSachPhieuMuaVM();
            SetUpTimKiemPhieuMuaVM();
            ThoiGianLapPhieu = null;
        }

        protected override void ReloadComponentsInternal()
        {
            RefreshResource();

            TimKiemPhieuMuaVM.Reload();
            TimKiemPhieuMuaVM.RefreshItemSource(phieuMuaSource);
            DanhSachPhieuMuaVM.RefreshItemsSource(phieuMuaSource);
            DanhSachPhieuMuaVM.Reload();
            ThoiGianLapPhieu = null;
            DanhSachChiTietMuaVM.Clear();
        }
        #endregion
    }
}
