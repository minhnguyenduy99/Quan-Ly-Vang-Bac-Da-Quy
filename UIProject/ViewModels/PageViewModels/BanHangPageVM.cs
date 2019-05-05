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
            get => ((ItemViewModel<KhachHangModel>)TimKiemKhachHangVM.SelectedItem).Model;
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

        public BanHangPageVM() : base() { }


        public event EventHandler<ItemEventArgs<ChiTietBanModel>> SanPhamDaCo
        {
            add { DanhSachChiTietBan.ContainsItemModel += value; }
            remove { DanhSachChiTietBan.ContainsItemModel -= value; }
        }

        private void TimKiemSanPhamVM_SelectionChanged(object sender, SelectedItemChangedEventArgs e)
        {
            var sanPhamDaChon = e.SelectedItem as ItemViewModel<SanPhamModel>;
            if (sanPhamDaChon != null)
            {
                DanhSachChiTietBan.Add(new ChiTietBanModel()
                {
                    MaPhieuMuaHang = 105,
                    MaSP = sanPhamDaChon.Model.MaSP,
                    SoLuong = 1,
                    ChietKhau = 10000,
                    DonGiaMuaVao = sanPhamDaChon.Model.DonGiaMuaVao,
                    Thue = 10,
                });
            }
        }

        

        private void SetUpBolocTimKiemSanPham()
        {
            LoaiSanPhamFilterVM = new EnumFilterViewModel<SanPhamModel>(
                LocLoaiSanPhamCallBack, 
                new ObservableCollection<LoaiSanPhamModel>()
                {
                    new LoaiSanPhamModel(){TenLoaiSP = "Đá quý", MaLoaiSP = 444},
                    new LoaiSanPhamModel(){TenLoaiSP = "Trang sức", MaLoaiSP = 555},
                    new LoaiSanPhamModel(){TenLoaiSP = "Vàng bạc", MaLoaiSP = 321},
                });

            var KhongLoc = new LoaiSanPhamModel() { TenLoaiSP = "Lọc tất cả", MaLoaiSP = -1 };
            LoaiSanPhamFilterVM.Collection.Add(KhongLoc);

            TimKiemSanPhamVM.DefaultFilter = new Func<ItemViewModel<SanPhamModel>, bool>(LocTenSanPhamCallBack);
            TimKiemSanPhamVM.AdditionFilters = new Func<ItemViewModel<SanPhamModel>, bool>[]
            {
                LoaiSanPhamFilterVM.FilterCallBack             
            };
        }

        private void SetUpBoLocTimKiemKhachHang()
        {
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
            if (castLoaiSanPhamDaChon.MaLoaiSP == -1)
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

            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>(new ObservableCollection<KhachHangModel>()
            {
                new KhachHangModel(){MaKH = 1, TenKH = "Nguyễn Duy Minh", DiaChi = "abc xyz ght", CongNo = 100000},
                new KhachHangModel(){MaKH = 2, TenKH = "Nguyễn Văn B", DiaChi = "abc gfgfght", CongNo = 0},
                new KhachHangModel(){MaKH = 3, TenKH = "Nguyễn Duy Hào", DiaChi = "abc xyfgâz ght", CongNo = 20},
                new KhachHangModel(){MaKH = 4, TenKH = "Nguyễn Duy Bảo", DiaChi = "abc xhka ght", CongNo = 1000},
                new KhachHangModel(){MaKH = 5, TenKH = "Nguyễn Duy Tâm", DiaChi = "abc faghz ght", CongNo = 10000000},
            });
            TimKiemSanPhamVM = new SearchTextBoxViewModel<SanPhamModel>(new ObservableCollection<SanPhamModel>()
            {
                new SanPhamModel(){MaSP = 1, TenSP = "A", DonGiaMuaVao = 50000, MaLoaiSP = 444},
                new SanPhamModel(){MaSP = 2, TenSP = "B", DonGiaMuaVao = 10000, MaLoaiSP = 555},
                new SanPhamModel(){MaSP = 3, TenSP = "C", DonGiaMuaVao = 100000, MaLoaiSP = 321},
                new SanPhamModel(){MaSP = 3, TenSP = "C", DonGiaMuaVao = 100000, MaLoaiSP = 321},
                new SanPhamModel(){MaSP = 3, TenSP = "C", DonGiaMuaVao = 100000, MaLoaiSP = 555},
                new SanPhamModel(){MaSP = 2, TenSP = "B", DonGiaMuaVao = 100000, MaLoaiSP = 3},
            });

            SetUpBolocTimKiemSanPham();
            SetUpBoLocTimKiemKhachHang();

            DanhSachChiTietBan = new ObservableCollectionViewModel<ChiTietBanModel>();

            TimKiemSanPhamVM.SelectedItemChanged += TimKiemSanPhamVM_SelectionChanged;
        }


        protected virtual void OnThanhToanCommandExecute()
        {

        }


    }
}
