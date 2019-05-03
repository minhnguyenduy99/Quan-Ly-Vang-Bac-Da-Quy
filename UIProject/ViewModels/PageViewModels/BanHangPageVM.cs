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
        /// Tên sản phẩm đã nhập
        /// </summary>
        public string TenSanPhamDaNhap
        {
            get => TimKiemSanPhamVM.Text;
        }

        /// <summary>
        /// Tên khách hàng đã nhập
        /// </summary>
        public string TenKhachHangDaNhap
        {
            get => TimKiemKhachHangVM.Text;
        }

        /// <summary>
        /// Thông tin khách hàng 
        /// </summary>
        public KhachHangModel KhachHang
        {
            get => GetPropertyValue<KhachHangModel>();
            set => SetProperty(value);
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
        public DataGridViewModel<ChiTietBanModel> DanhSachChiTietBan { get; set; }


        /// <summary>
        /// Model của phiếu bán hàng
        /// </summary>
        public PhieuBanModel PhieuBan
        {
            get => phieuBan;
            set => SetProperty(ref phieuBan, value);
        }

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

        private void TimKiemSanPhamVM_SelectionChanged(object sender, ItemSelectedEventArgs<SanPhamModel> e)
        {
            var sanPhamDaChon = e.Item;
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
        protected override void LoadPageComponents()
        {
            PhieuBan = new PhieuBanModel();

            TimKiemSanPhamVM = new SearchTextBoxViewModel<SanPhamModel>(new ObservableCollection<SanPhamModel>()
            {
                new SanPhamModel(){MaSP = 1, TenSP = "A", DonGiaMuaVao = 50000},
                new SanPhamModel(){MaSP = 2, TenSP = "B", DonGiaMuaVao = 10000},
                new SanPhamModel(){MaSP = 3, TenSP = "C", DonGiaMuaVao = 100000},
                new SanPhamModel(){MaSP = 3, TenSP = "C", DonGiaMuaVao = 100000},
                new SanPhamModel(){MaSP = 3, TenSP = "C", DonGiaMuaVao = 100000},
                new SanPhamModel(){MaSP = 2, TenSP = "B", DonGiaMuaVao = 100000},
            });

            DanhSachChiTietBan = new DataGridViewModel<ChiTietBanModel>();
            TimKiemKhachHangVM = new SearchTextBoxViewModel<KhachHangModel>();

            TimKiemSanPhamVM.Filters = new Func<ItemViewModel<SanPhamModel>, bool>[]
            {
                sanPham => sanPham.Model.TenSP.ToLower().Contains(TimKiemSanPhamVM.Text.ToLower())
            };
            TimKiemSanPhamVM.SelectItem += TimKiemSanPhamVM_SelectionChanged;
        }
        protected virtual void OnThanhToanCommandExecute()
        {

        }
    }
}
