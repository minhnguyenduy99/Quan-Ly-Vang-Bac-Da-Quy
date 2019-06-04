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
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class DanhSachDonHangPageVM : BasePageViewModel
    {
        private IEnumerable<PhieuBanModel> phieuBanSource;

        public LocDonHangWindowVM LocDonHangVM { get; set; }

        public SearchTextBoxViewModel<PhieuBanModel> TimKiemPhieuBanVM { get; set; }

        public ObservableCollectionViewModel<PhieuBanModel> DanhSachPhieuBanVM { get; set; }

        public DanhSachDonHangPageVM() : base() { }
        public DanhSachDonHangPageVM(INavigator navigator) : base(navigator)
        {
        }


        protected override void LoadPageComponents()
        {
            phieuBanSource = new ObservableCollection<PhieuBanModel>()
            {
                 new PhieuBanModel(){MaPhieu = "PH001", MaKH = "KH01", NgayLap = "12/2/2018"},
                 new PhieuBanModel(){MaPhieu = "PH002", MaKH = "KH02", NgayLap = "11/1/2017"},
                 new PhieuBanModel(){MaPhieu = "PH003", MaKH = "KH03", NgayLap = "13/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH004", MaKH = "KH02", NgayLap = "25/4/2016"},
                 new PhieuBanModel(){MaPhieu = "PH005", MaKH = "KH01", NgayLap = "11/8/2015"},
                 new PhieuBanModel(){MaPhieu = "PH006", MaKH = "KH01", NgayLap = "29/9/2014"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/02/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "4/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
                 new PhieuBanModel(){MaPhieu = "PH007", MaKH = "KH03", NgayLap = "04/2/2019"},
            };
            SetUpDanhSachPhieuBanVM();
            SetUpBoLocTimKiemPhieuBan();
        }

        private void SetUpDanhSachPhieuBanVM()
        {
            DanhSachPhieuBanVM = new ObservableCollectionViewModel<PhieuBanModel>(phieuBanSource);
        }

        private void SetUpBoLocTimKiemPhieuBan()
        {
            TimKiemPhieuBanVM = new SearchTextBoxViewModel<PhieuBanModel>(phieuBanSource);
            TimKiemPhieuBanVM.DefaultFilter = new Func<ItemViewModel<PhieuBanModel>, bool>(LocTheoMaDonHangCallBack);
            
        }
       

        private void TimKiemPhieuBanVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var castPhieuBan = e.SelectedItem as ItemViewModel<PhieuBanModel>;
            if (castPhieuBan != null)
            {
                DanhSachPhieuBanVM.Items.Clear();
                DanhSachPhieuBanVM.Add(castPhieuBan);
            }
        }

        private void LocDonHangVM_BoLocHoanThanh(object sender, FilterEventArgs<PhieuBanModel> e)
        {
            Func<ItemViewModel<PhieuBanModel>, bool>[] arrayFilters = null;
            e.FilterCallbacks.CopyTo(arrayFilters);

            DanhSachPhieuBanVM.Filters = arrayFilters;

        }

        protected virtual bool LocTheoMaDonHangCallBack(ItemViewModel<PhieuBanModel> phieuBan)
        {
            var castPhieuBan = TimKiemPhieuBanVM.SelectedItem as ItemViewModel<PhieuBanModel>;
            if (castPhieuBan == null)
                return true;
            return castPhieuBan.Model.MaPhieu.ToLower().Equals(phieuBan.Model.MaPhieu.ToLower());
        }
    }
}
