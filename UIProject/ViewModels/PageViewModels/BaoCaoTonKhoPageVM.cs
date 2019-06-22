using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UIProject.Events;
using UIProject.UIConnector;
using UIProject.ViewModels.DataViewModels;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class BaoCaoTonKhoPageVM : BasePageViewModel
    {
        private IEnumerable<NhaCungCapModel> dsNhaCungCap;
        private IEnumerable<LoaiSanPhamModel> dsLoaiSanPham;

        private ICommand loadBaoCaoCmd;

        public BaoCaoTonKhoViewModel BaoCaoTonKhoVM { get; private set; }
        public ObservableCollectionViewModel<LoaiSanPhamModel> DanhSachLoaiSanPhamVM { get; private set; }
        public SearchTextBoxViewModel<NhaCungCapModel> TimKiemNhaCungCapVM { get; private set; }

        public ICommand LoadBaoCaoCommand
        {
            get => loadBaoCaoCmd ?? new BaseCommand<IWindow>(OnLoadBaoCaoCommandExcute);
            set => loadBaoCaoCmd = value;
        }


        public BaoCaoTonKhoPageVM(): base() { }
        public BaoCaoTonKhoPageVM(INavigator navigator) : base(navigator) { }

        private void SetUpBaoCaoTonKhoVM()
        {
            BaoCaoTonKhoVM = new BaoCaoTonKhoViewModel();
            BaoCaoTonKhoVM.DanhSachSanPhamBaoCao.Filters.Add(LocTheoLoaiSanPhamCallback);
            BaoCaoTonKhoVM.DanhSachSanPhamBaoCao.Filters.Add(LocTheoNhaCungCapCallback);

            // local function
            bool LocTheoLoaiSanPhamCallback(ItemViewModel<BaoCaoTonKhoViewModel.ChiTietTonKho> sanPhamItem)
            {
                if (sanPhamItem == null || sanPhamItem.Model == null)
                    return true;
                var loaiSanPhamDaChon = DanhSachLoaiSanPhamVM.SelectedItem as ItemViewModel<LoaiSanPhamModel>;
                if (loaiSanPhamDaChon == null || loaiSanPhamDaChon.Model == null)
                    return true;
                return sanPhamItem.Model.SanPham.MaLoaiSP == loaiSanPhamDaChon.Model.MaLoaiSP;
            }
            bool LocTheoNhaCungCapCallback(ItemViewModel<BaoCaoTonKhoViewModel.ChiTietTonKho> sanPhamItem)
            {
                if (sanPhamItem == null || sanPhamItem.Model == null)
                    return true;
                var nhaCungCapDaChon = TimKiemNhaCungCapVM.SelectedItem as ItemViewModel<NhaCungCapModel>;
                if (nhaCungCapDaChon == null || nhaCungCapDaChon.Model == null)
                    return true;
                return sanPhamItem.Model.SanPham.MaNCC == nhaCungCapDaChon.Model.MaNCC;
            }
        }



        private void SetUpBoLocLoaiSanPhamVM()
        {
            DanhSachLoaiSanPhamVM = new ObservableCollectionViewModel<LoaiSanPhamModel>(dsLoaiSanPham);
            DanhSachLoaiSanPhamVM.SelectedItemChanged += DanhSachLoaiSanPhamVM_SelectedItemChanged;
            
            // local function
            void DanhSachLoaiSanPhamVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var loaiSanPhamDaChon = e.SelectedItem as ItemViewModel<LoaiSanPhamModel>;
                if (loaiSanPhamDaChon == null)
                    return;

                BaoCaoTonKhoVM.DanhSachSanPhamBaoCao.Filter();
            }
        }

        private void SetUpTimKiemNhaCungCapVM()
        {
            TimKiemNhaCungCapVM = new SearchTextBoxViewModel<NhaCungCapModel>(dsNhaCungCap);
            TimKiemNhaCungCapVM.SelectedValuePath = "TenNCC";
            TimKiemNhaCungCapVM.SelectedItemChanged += TimKiemNhaCungCapVM_SelectedItemChanged;


            // local function
            void TimKiemNhaCungCapVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var nhaCungCapDaChon = e.SelectedItem as ItemViewModel<NhaCungCapModel>;
                if (nhaCungCapDaChon == null)
                    return;

                BaoCaoTonKhoVM.DanhSachSanPhamBaoCao.Filter();
            }
        }


        private async void OnLoadBaoCaoCommandExcute(IWindow loadingWindow)
        {
            DialogWindowViewModel loadingWindowVM = new DialogWindowViewModel()
            {
                DialogType = DialogWindowType.WaitingMessage,
                MessageText = "Đang tải dữ liệu",
            };
            loadingWindow.DataContext = loadingWindowVM;

            Task<bool> loadBaoCaoTask = new Task<bool>(() => BaoCaoTonKhoVM.LoadBaoCaoTonKho());
            loadBaoCaoTask.Start();
            loadingWindow.Show();
            bool loadResult = await loadBaoCaoTask;
            loadingWindow.Close();            
            if (loadResult == false)
            {
                MessageBox.Show("Có lỗi xảy ra trong quá trình tạo báo cáo");   
            }

        }

        private void RefreshResource()
        {
            dsNhaCungCap = DataAccess.LoadNhaCungCap();
            dsLoaiSanPham = DataAccess.LoadLoaiSanPham();
        }

        protected override void LoadComponentsInternal()
        {
            RefreshResource();

            SetUpBaoCaoTonKhoVM();
            SetUpBoLocLoaiSanPhamVM();
            SetUpTimKiemNhaCungCapVM();
        }

        protected override void ReloadComponentsInternal()
        {
            RefreshResource();

            BaoCaoTonKhoVM.Reload();
            TimKiemNhaCungCapVM.RefreshItemSource(dsNhaCungCap);
            TimKiemNhaCungCapVM.Reload();
            DanhSachLoaiSanPhamVM.RefreshItemsSource(dsLoaiSanPham);
            DanhSachLoaiSanPhamVM.Reload();
        }
    }
}
