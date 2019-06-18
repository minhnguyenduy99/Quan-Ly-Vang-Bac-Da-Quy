using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.UIConnector;
using UIProject.ViewModels.DataViewModels;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class NhapHangPageVM : BasePageViewModel 
    {
        #region Resources
        private IEnumerable<SanPhamModel> dsSanPham;
        private IEnumerable<NhaCungCapModel> dsNhaCungCap;
        #endregion

        private ICommand submitPhieuNhapHangCmd;
        private ICommand huyPhieuNhapHangCmd;
        private ICommand themSanPhamCmd;

        public ICommand SubmitPhieuNhapHangCommand
        {
            get => submitPhieuNhapHangCmd ?? new BaseCommand<IWindow>(OnSubmitPhieuNhapHangCommandExecute);
            private set => submitPhieuNhapHangCmd = value;
        }

        public ICommand HuyPhieuNhapHangCommand
        {
            get => huyPhieuNhapHangCmd ?? new BaseCommand<IWindow>(OnHuyPhieuNhapHangCommandExecute);
            private set => huyPhieuNhapHangCmd = value;
        }

        public ICommand ThemSanPhamCommand
        {
            get => themSanPhamCmd ?? new BaseCommand<IWindowExtension>(OnThemSanPhamCommandExecute);
            set => themSanPhamCmd = value;
        }



        public SearchTextBoxViewModel<SanPhamModel> TimKiemSanPhamVM { get; private set; }
        public SearchTextBoxViewModel<NhaCungCapModel> TimKiemNhaCungCapVM { get; private set; }
        public NhapHangViewModel NhapHangVM { get; set; }


        public NhapHangPageVM() : base() { }
        public NhapHangPageVM(INavigator navigator) : base(navigator) { }

     
        private void SetUpTimKiemNhaCungCapVM()
        {
            TimKiemNhaCungCapVM = new SearchTextBoxViewModel<NhaCungCapModel>(dsNhaCungCap);
            TimKiemNhaCungCapVM.Filters.Add(LocTheoTenNCCCallback);
            TimKiemNhaCungCapVM.SelectedValuePath = "TenNCC";
            TimKiemNhaCungCapVM.SelectedItemChanged += TimKiemNhaCungCapVM_SelectedItemChanged;

            // local function
            bool LocTheoTenNCCCallback(ItemViewModel<NhaCungCapModel> nhaCCItem)
            {
                if (nhaCCItem == null)
                    return true;

                return nhaCCItem.Model.TenNCC.ToLower().StartsWith(TimKiemNhaCungCapVM.Text.ToLower());
            }

            void TimKiemNhaCungCapVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var nhaCCDaChon = e.SelectedItem as ItemViewModel<NhaCungCapModel>;
                if (nhaCCDaChon == null)
                    return;
                NhapHangVM.Clear();
                NhapHangVM.PhieuMua.MaNCC = nhaCCDaChon.Model.MaNCC;
            }
        }

        private void SetUpTimKiemSanPhamVM()
        {
            TimKiemSanPhamVM = new SearchTextBoxViewModel<SanPhamModel>(dsSanPham);
            TimKiemSanPhamVM.Filters.Add(LocSanPhamTheoTen);
            TimKiemSanPhamVM.Filters.Add(LocSanPhamTheoNhaCungCap);
            TimKiemSanPhamVM.SelectedItemChanged += TimKiemSanPhamVM_SelectedItemChanged;

            bool LocSanPhamTheoTen(ItemViewModel<SanPhamModel> sanPhamItem)
            {
                if (sanPhamItem == null)
                    return true;

                return sanPhamItem.Model.TenSP.ToLower().StartsWith(TimKiemSanPhamVM.Text.ToLower());
            }
            bool LocSanPhamTheoNhaCungCap(ItemViewModel<SanPhamModel> sanPhamItem)
            {
                var nhaCungCap = TimKiemNhaCungCapVM.SelectedItem as ItemViewModel<NhaCungCapModel>;

                //  không cho phép người dùng tìm sản phẩm nếu chưa chọn nhà cung câp
                if (nhaCungCap == null)
                    return false;

                return sanPhamItem?.Model?.MaNCC == nhaCungCap?.Model?.MaNCC;
            }

            void TimKiemSanPhamVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var sanPhamDaChon = e.SelectedItem as ItemViewModel<SanPhamModel>;
                if (sanPhamDaChon == null)
                    return;

                NhapHangVM.Add(sanPhamDaChon);
            }
        }



        private void SetUpNhapHangVM()
        {
            NhapHangVM = new NhapHangViewModel();
        }

        private void OnSubmitPhieuNhapHangCommandExecute(IWindow window)
        {
            DialogWindowViewModel dialogVM = new DialogWindowViewModel()
            {
                DialogType = DialogWindowType.YesNo,
                YesText = "Có",
                NoText = "Không",
                MessageText = "Bạn muốn lưu phiếu mua hàng?",
            };

            dialogVM.ButtonPressed += NotifySubmitWnd_ButtonPressed;
            window.DataContext = dialogVM;
            if (window.ShowDialog() == true)
            {
                bool submitSuccess = NhapHangVM.Submit();
                if (submitSuccess)
                    Reload();
            }

            // local functon
            void NotifySubmitWnd_ButtonPressed(object sender, DialogButtonPressedEventArgs e)
            {
                if (e.DialogResult == DialogResult.Yes)
                    window.DialogResult = true;
                if (e.DialogResult == DialogResult.No)
                    window.DialogResult = false;

                window.Close();
            }
        }


        private void OnThemSanPhamCommandExecute(IWindowExtension window)
        {
            AddingWindowViewModel<SanPhamModel> themSanPhamVM = new AddingWindowViewModel<SanPhamModel>();
            SearchTextBoxViewModel<NhaCungCapModel> timKiemNCC
                = new SearchTextBoxViewModel<NhaCungCapModel>(dsNhaCungCap);
            timKiemNCC.SelectedValuePath = "TenNCC";
            timKiemNCC.SelectedItemChanged += TimKiemNCC_SelectedItemChanged;
            themSanPhamVM.AdditionData.Add(DataAccess.LoadLoaiSanPham());
            themSanPhamVM.Searchers.Add(timKiemNCC);

            window.DataContext = themSanPhamVM;

            // trigger the closing handler to allow a manual one
            window.Closing += (sender, e) => e.Cancel = true;

            // Thêm sản phẩm vào database thành công
            if (window.ShowDialog(-500, 0) == true)
            {
                // Cập nhật lại danh sách sản phẩm
                RefreshResource();
                TimKiemSanPhamVM.RefreshItemSource(dsSanPham);
            }

            // local function
            void TimKiemNCC_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var nhaCungCapDaChon = e.SelectedItem as ItemViewModel<NhaCungCapModel>;
                if (nhaCungCapDaChon == null)
                    return;

                (themSanPhamVM.Data as SanPhamModel).MaNCC = nhaCungCapDaChon.Model?.MaNCC;
            }
        }



        private void OnHuyPhieuNhapHangCommandExecute(IWindow window)
        {
            DialogWindowViewModel popUpWndVM = new DialogWindowViewModel()
            {
                DialogType = DialogWindowType.YesNo,
                MessageText = "Bạn muốn hủy phiếu mua đang làm ?",
                YesText = "Có",
                NoText = "Không"
            };

            window.DataContext = popUpWndVM;
            popUpWndVM.ButtonPressed += PopUpWndVM_ButtonPressed;

            // Chấp nhận hủy phiếu ban đang làm hiện tại
            if (window.ShowDialog() == true)
            {
                Reload();
            }


            // local function
            void PopUpWndVM_ButtonPressed(object sender, DialogButtonPressedEventArgs e)
            {
                if (e.DialogResult == DialogResult.Yes)
                    window.DialogResult = true;
                if (e.DialogResult == DialogResult.No)
                    window.DialogResult = false;

                window.Close();
            }
        }

        private void RefreshResource()
        {
            dsSanPham = DataAccess.LoadSanPham();
            dsNhaCungCap = DataAccess.LoadNhaCungCap();
        }

        protected override void LoadComponentsInternal()
        {
            RefreshResource();

            SetUpNhapHangVM();
            SetUpTimKiemSanPhamVM();
            SetUpTimKiemNhaCungCapVM();
        }

        protected override void ReloadComponentsInternal()
        {
            RefreshResource();

            TimKiemSanPhamVM.Reload();
            TimKiemSanPhamVM.RefreshItemSource(dsSanPham);
            TimKiemNhaCungCapVM.Reload();
            TimKiemNhaCungCapVM.RefreshItemSource(dsNhaCungCap);

            NhapHangVM.Reload();
        }
    }
}
