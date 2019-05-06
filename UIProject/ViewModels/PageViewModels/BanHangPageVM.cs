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
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class BanHangPageVM : BasePageViewModel
    {
        private ICommand thanhToanCommand;
        private PhieuBanModel phieuBan;
        private List<ChiTietBanModel> dsChiTietBan;

        /// <summary>
        /// Thông tin khách hàng 
        /// </summary>
        public KhachHangModel KhachHang
        {
            get
            {
                var khachHangDaChon = TimKiemKhachHangVM.SelectedItem as ItemViewModel<KhachHangModel>;
                if (khachHangDaChon == null)
                    return null;
                return khachHangDaChon.Model;
            }
        }

        
        /// <summary>
        /// View model của việc tìm kiếm sản phẩm
        /// </summary>
        public SearchTextBoxViewModel<SanPhamModel> TimKiemSanPhamVM { get; set; }

        /// <summary>
        /// View model của việc tìm kiếm khách hàng
        /// </summary>
        public SearchTextBoxViewModel<KhachHangModel> TimKiemKhachHangVM { get; set; }

        /// <summary>
        /// Danh sách chi tiết phiếu bán
        /// </summary>
        public ObservableCollectionViewModel<ChiTietBanModel> DanhSachChiTietBan { get; set; }


        /// <summary>
        /// View model của việc thêm khách hàng
        /// </summary>
        public AddingWindowViewModel<KhachHangModel> ThemKhachHangVM { get; set; }


        /// <summary>
        /// Model của phiếu bán hàng
        /// </summary>
        public PhieuBanModel PhieuBan
        {
            get => phieuBan;
            set => SetProperty(ref phieuBan, value);
        }

        public EnumFilterViewModel<SanPhamModel> LoaiSanPhamFilterVM;

        /// <summary>
        /// Danh sách chi tiết bán
        /// </summary>
        public List<ChiTietBanModel> DSChiTietBan
        {
            get => this.dsChiTietBan;
        }

        /// <summary>
        /// Command thực hiện việc tính toán
        /// </summary>
        public ICommand ThanhToanCommand
        {
            get => thanhToanCommand ?? new BaseCommand(OnThanhToanCommandExecute);
            set => thanhToanCommand = value;
        }

        /// <summary>
        /// Command thêm khách hàng
        /// </summary>
        public ICommand ThemKhachHangCommand
        {
            get => new BaseCommand(OnThucThiThemKhachHang);
        }


        public BanHangPageVM() : base() { }


        public event EventHandler<ItemEventArgs<ChiTietBanModel>> SanPhamDaCo
        {
            add { DanhSachChiTietBan.ContainsItemModel += value; }
            remove { DanhSachChiTietBan.ContainsItemModel -= value; }
        }

        public event EventHandler<AddingWindowViewModel<KhachHangModel>> ThucThiThemKhachHang;
        

        private void TimKiemSanPhamVM_SelectionChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var sanPhamDaChon = e.SelectedItem as ItemViewModel<SanPhamModel>;
            if (sanPhamDaChon != null)
            {
                DanhSachChiTietBan.Add(new ChiTietBanModel(PhieuBan, sanPhamDaChon.Model, 1));
            }
        }

      
        private void SetUpBolocTimKiemSanPham()
        {
            TimKiemSanPhamVM = new SearchTextBoxViewModel<SanPhamModel>(new ObservableCollection<SanPhamModel>()
            {
                new SanPhamModel(){MaSP = "SP001", TenSP = "A", DonGiaMuaVao = 50000, MaLoaiSP = "LSP001"},
                new SanPhamModel(){MaSP = "SP002", TenSP = "B", DonGiaMuaVao = 10000, MaLoaiSP = "LSP003"},
                new SanPhamModel(){MaSP = "SP003", TenSP = "C", DonGiaMuaVao = 100000, MaLoaiSP = "LSP004"},
                new SanPhamModel(){MaSP = "SP004", TenSP = "C", DonGiaMuaVao = 100000, MaLoaiSP = "LSP002"},
                new SanPhamModel(){MaSP = "SP005", TenSP = "C", DonGiaMuaVao = 100000, MaLoaiSP = "LSP002"},
                new SanPhamModel(){MaSP = "SP006", TenSP = "B", DonGiaMuaVao = 100000, MaLoaiSP = "LSP001"},
            });

            LoaiSanPhamFilterVM = new EnumFilterViewModel<SanPhamModel>(
                LocLoaiSanPhamCallBack, 
                new ObservableCollection<LoaiSanPhamModel>()
                {
                    new LoaiSanPhamModel(){TenLoaiSP = "Đá quý", MaLoaiSP = "LSP001"},
                    new LoaiSanPhamModel(){TenLoaiSP = "Trang sức", MaLoaiSP = "LSP002"},
                    new LoaiSanPhamModel(){TenLoaiSP = "Vàng bạc", MaLoaiSP = "LSP003"},
                });

            var KhongLoc = new LoaiSanPhamModel() { TenLoaiSP = "Lọc tất cả", MaLoaiSP = "LSP-1" };
            LoaiSanPhamFilterVM.Collection.Add(KhongLoc);

            TimKiemSanPhamVM.DefaultFilter = new Func<ItemViewModel<SanPhamModel>, bool>(LocTenSanPhamCallBack);
            TimKiemSanPhamVM.AdditionFilters.Add
                (
                    new Func<ItemViewModel<SanPhamModel>, bool>(LocLoaiSanPhamCallBack)
                );

            TimKiemSanPhamVM.SelectedItemChanged += TimKiemSanPhamVM_SelectionChanged;
        }

        private void SetUpBoLocTimKiemKhachHang()
        {
            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>(new ObservableCollection<KhachHangModel>()
            {
                new KhachHangModel(){MaKH = "KH001", TenKH = "Nguyễn Duy Minh", DiaChi = "abc xyz ght", CongNo = 100000},
                new KhachHangModel(){MaKH = "KH002", TenKH = "Nguyễn Văn B", DiaChi = "abc gfgfght", CongNo = 0},
                new KhachHangModel(){MaKH = "KH003", TenKH = "Nguyễn Duy Hào", DiaChi = "abc xyfgâz ght", CongNo = 20},
                new KhachHangModel(){MaKH = "KH004", TenKH = "Nguyễn Duy Bảo", DiaChi = "abc xhka ght", CongNo = 1000},
                new KhachHangModel(){MaKH = "KH005", TenKH = "Nguyễn Duy Tâm", DiaChi = "abc faghz ght", CongNo = 10000000},
            });
            TimKiemKhachHangVM.DefaultFilter = new Func<ItemViewModel<KhachHangModel>, bool>(LocTenKhachHangCallBack);
        }

        #region Callback cho bộ lọc tìm kiếm 
        private bool LocLoaiSanPhamCallBack(ItemViewModel<SanPhamModel> sanPham)
        {
            var loaiSanPhamDaChon = LoaiSanPhamFilterVM.Collection.SelectedItem as ItemViewModel<object>;
            if (loaiSanPhamDaChon == null)
            {
                return true;
            }
            var castLoaiSanPhamDaChon = loaiSanPhamDaChon.Model as LoaiSanPhamModel;
            if (castLoaiSanPhamDaChon.MaLoaiSP == "LSP-1")
                return true;
            return sanPham.Model.MaLoaiSP.Equals(castLoaiSanPhamDaChon.MaLoaiSP);
        }
        private bool LocTenSanPhamCallBack(ItemViewModel<SanPhamModel> sanPham)
        {
            return sanPham.Model.TenSP.ToLower().Contains(TimKiemSanPhamVM.Text.ToLower());
        }
        private bool LocTenKhachHangCallBack(ItemViewModel<KhachHangModel> khachHang)
        {
            return khachHang.Model.TenKH.ToLower().Contains(TimKiemKhachHangVM.Text.ToLower());
        }
        #endregion


        protected override void LoadPageComponents()
        {
            PhieuBan = new PhieuBanModel();
            ThemKhachHangVM = new AddingWindowViewModel<KhachHangModel>();


            SetUpBolocTimKiemSanPham();
            SetUpBoLocTimKiemKhachHang();

            DanhSachChiTietBan = new ObservableCollectionViewModel<ChiTietBanModel>();


        }

        private void OnThucThiThemKhachHang()
        {
            ThucThiThemKhachHang?.Invoke(this, ThemKhachHangVM);
        }
        protected virtual void OnThanhToanCommandExecute()
        {

        }


    }
}
