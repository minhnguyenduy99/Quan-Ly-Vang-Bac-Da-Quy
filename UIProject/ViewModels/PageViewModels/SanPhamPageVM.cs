using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.ServiceProviders;
using UIProject.UIConnector;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class SanPhamPageVM : BasePageViewModel
    {
        /// <summary>
        /// Danh sách sản phẩm
        /// </summary>
        public ObservableCollectionViewModel<SanPhamModel> DanhSachSanPhamVM { get; set; }

        /// <summary>
        /// View model của bộ lọc khu vực
        /// </summary>
        public EnumFilterViewModel<SanPhamModel> LocLoaiSanPhamVM { get; set; }

        public EnumFilterViewModel<SanPhamModel> LocNhaCungCapVM { get; set; }


        /// <summary>
        /// Khách hàng đang được chọn
        /// </summary>
        public ItemViewModel<SanPhamModel> SanPhamDuocChon { get; private set; }


        /// <summary>
        /// View model của việc tìm kiếm khách hàng
        /// </summary>
        public SearchTextBoxViewModel<SanPhamModel> TimKiemSanPhamVM { get; set; }

        /// <summary>
        /// Command cho việc thêm khách hàng
        /// </summary>
        public ICommand ThemSanPhamCommand
        {
            get => new BaseCommand(OnThemSanPhamCommandExecute);
        }

        /// <summary>
        /// Command cho việc chỉnh sửa thông tin
        /// </summary>
        public ICommand ChinhSuaThongTinCommand
        {
            get => new BaseCommand<IWindowExtension>(OnChinhSuaThongTinCommandExecute);
        }

        /// <summary>
        /// Command cho việc xóa khách hàng
        /// </summary>
        public ICommand XoaSanPhamCommand
        {
            get => new BaseCommand<IWindow>(OnXoaSanPhamCommandExecute);
        }


        public SanPhamPageVM() : base() { }
        public SanPhamPageVM(INavigator navigator) : base(navigator) { }


        protected override void LoadPageComponents()
        {
            SetUpDanhSachSanPhamVM();
            SetUpTimKiemSanPhamVM();
            SetUpLocLoaiSanPhamVM();
            SetUpLocNhaCungCapVM();
        }


        private void SetUpDanhSachSanPhamVM()
        {
            var sanPhamSource = DataAccess.LoadSanPham();
            DanhSachSanPhamVM = new ObservableCollectionViewModel<SanPhamModel>(sanPhamSource);
            DanhSachSanPhamVM.SelectedItemChanged += DanhSachSanPhamVM_SelectedItemChanged;

            var temp = DanhSachSanPhamVM.Filters as List<Func<ItemViewModel<SanPhamModel>, bool>>;
            temp.Add(LocSanPhamTheoTen);
        }



        private void SetUpTimKiemSanPhamVM()
        {
            TimKiemSanPhamVM = new SearchTextBoxViewModel<SanPhamModel>(null);
            TimKiemSanPhamVM.TextChanged += TimKiemSanPhamVM_TextChanged;
        }
        private void SetUpLocLoaiSanPhamVM()
        {
            LocLoaiSanPhamVM = new EnumFilterViewModel<SanPhamModel>(
                new List<Func<ItemViewModel<SanPhamModel>, bool>>()
                {
                    LocSanPhamTheoLoaiSanPham,
                },
                DataAccess.LoadLoaiSanPham());

            // Thêm 1 lựa chọn tất cả vào bộ lọc
            LocLoaiSanPhamVM.NonApplyFilterItem.Model = new LoaiSanPhamModel() { MaLoaiSP = "MaLSP-1", TenLoaiSP = "Chọn tất cả" };
            
            (DanhSachSanPhamVM.Filters as List<Func<ItemViewModel<SanPhamModel>, bool>>)
                .Add(LocLoaiSanPhamVM.FilterCallBacks[0]);

            LocLoaiSanPhamVM.SelectedItemChanged += LocSanPhamVM_SelectedItemChanged;
        }
        private void SetUpLocNhaCungCapVM()
        {
            LocNhaCungCapVM = new EnumFilterViewModel<SanPhamModel>(
                new List<Func<ItemViewModel<SanPhamModel>, bool>>()
                {
                     LocSanPhamTheoNhaCungCap
                },
                DataAccess.LoadNhaCungCap());

            // Thêm 1 lựa chọn tất cả vào bộ lọc
            LocNhaCungCapVM.NonApplyFilterItem.Model = new NhaCungCapModel() { MaNCC = "MaLSP-1", TenNCC = "Chọn tất cả" };

            (DanhSachSanPhamVM.Filters as List<Func<ItemViewModel<SanPhamModel>, bool>>)
                .Add(LocNhaCungCapVM.FilterCallBacks[0]);

            LocNhaCungCapVM.SelectedItemChanged += LocSanPhamVM_SelectedItemChanged;
        }




        #region Filter callbacks
        private bool LocSanPhamTheoTen(ItemViewModel<SanPhamModel> sanPham)
        {
            if (string.IsNullOrEmpty(TimKiemSanPhamVM.Text))
                return true;

            return sanPham.Model.TenSP.ToLower().StartsWith(TimKiemSanPhamVM.Text.ToLower());
        }

        private bool LocSanPhamTheoLoaiSanPham(ItemViewModel<SanPhamModel> sanPham)
        {
            var loaiSanPhamDaChon = LocLoaiSanPhamVM.Collection.SelectedItem as ItemViewModel<object>;
            if (loaiSanPhamDaChon == null)
            {
                return true;
            }
            var castLoaiSP = loaiSanPhamDaChon.Model as LoaiSanPhamModel;
            if (castLoaiSP == null)
                return true;

            var chonTatCa = LocLoaiSanPhamVM.NonApplyFilterItem.Model as LoaiSanPhamModel;

            // lựa chọn "Chọn tất cả" đã được chọn
            if (chonTatCa != null && castLoaiSP.MaLoaiSP == chonTatCa.MaLoaiSP)
                return true;

            return sanPham.Model.MaLoaiSP.Equals(castLoaiSP.MaLoaiSP);
        }

        private bool LocSanPhamTheoNhaCungCap(ItemViewModel<SanPhamModel> sanPham)
        {
            var nhaCCDaChon = LocLoaiSanPhamVM.Collection.SelectedItem as ItemViewModel<object>;
            if (nhaCCDaChon == null)
            {
                return true;
            }
            var castNhaCC = nhaCCDaChon.Model as NhaCungCapModel;
            if (castNhaCC == null)
                return true;

            var chonTatCa = LocNhaCungCapVM.NonApplyFilterItem.Model as NhaCungCapModel;

            // lựa chọn "Chọn tất cả" đã được chọn
            if (chonTatCa != null && castNhaCC.MaNCC == chonTatCa.MaNCC)
                return true;

            return sanPham.Model.MaLoaiSP.Equals(castNhaCC.MaNCC);
        }

        #endregion

        #region Event handler
        private void DanhSachSanPhamVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var sanPhamDuocChon = e.SelectedItem as ItemViewModel<SanPhamModel>;
            if (sanPhamDuocChon != null)
            {
                SanPhamDuocChon = sanPhamDuocChon;
            }
        }

        private void TimKiemSanPhamVM_TextChanged(object sender, TextValueChangedEventArgs e)
        {
            if (e.NewValue == null || e.NewValue.Equals(e.OldValue))
                return;

            DanhSachSanPhamVM.Filter();
        }

        private void LocSanPhamVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                DanhSachSanPhamVM.Filter();
            }
        }

        #endregion

        #region Command execution
        private void OnXoaSanPhamCommandExecute(IWindow window)
        {
            if (window.ShowDialog() == true)
            {
                var deleteSanPhamItem = DanhSachSanPhamVM.SelectedItem as ItemViewModel<SanPhamModel>;
                if (deleteSanPhamItem == null)
                    return;
                DataAccess.RemoveSanPham(deleteSanPhamItem.Model);
                DanhSachSanPhamVM.RefreshItemsSource(DataAccess.LoadSanPham());
                DanhSachSanPhamVM.SelectedItem = null;
            }
        }

        private void OnThemSanPhamCommandExecute()
        {
            this.Navigator.Navigate("Nhập hàng");
        }

        private void OnChinhSuaThongTinCommandExecute(IWindowExtension window)
        {
            EditWindowViewModel<SanPhamModel> chinhSuaThongTin
                = new EditWindowViewModel<SanPhamModel>(SanPhamDuocChon.Model);

            var timKiemNCCVM = new SearchTextBoxViewModel<NhaCungCapModel>(DataAccess.LoadNhaCungCap());
            timKiemNCCVM.SelectedValuePath = "Model.TenNCC";
            timKiemNCCVM.SelectedItemChanged += TimKiemNCCVM_SelectedItemChanged;
            chinhSuaThongTin.AdditionData.Add(DataAccess.LoadLoaiSanPham());
            chinhSuaThongTin.Searchers.Add(timKiemNCCVM);

            window.DataContext = chinhSuaThongTin;          

            if (window.ShowDialog(-500, -100) == true)
            {
                DanhSachSanPhamVM.RefreshItemsSource(DataAccess.LoadSanPham());
                DanhSachSanPhamVM.SelectedItem = null;
            }


            void TimKiemNCCVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                var castNhaNCC = e.SelectedItem as ItemViewModel<NhaCungCapModel>;
                if (castNhaNCC == null || castNhaNCC.Model == null)
                    return;

                var castSanPham = chinhSuaThongTin.Data as SanPhamModel;
                castSanPham.MaNCC = castNhaNCC.Model.MaNCC;
            }
        }


        #endregion
    }
}
