using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using ModelProject.AggregationalModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.Converters;
using UIProject.Events;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.DataViewModels
{
    public class HoaDonViewModel : BaseViewModel, ISubmitViewModel
    {
        public ISubmitable Data { get; set; } = null;

        public bool IsDataValid
        {
            get => GetPropertyValue<bool>();
            private set => SetProperty(value);
        }
        public ObservableCollectionViewModel<ChiTietBanModel> DanhSachChiTietBan { get; set; }

        public PhieuBanModel PhieuBan { get; set; }

        public KhachHangModel KhachHang
        {
            get => GetPropertyValue<KhachHangModel>();
            set => SetProperty(value);
        }

        public int Thue
        {
            get => GetPropertyValue<int>();
            set
            {
                SetProperty(value);
                UpdateHoaDon();
            }
        }
        public int ChietKhau
        {
            get => GetPropertyValue<int>();
            set
            {
                SetProperty(value);
                UpdateHoaDon();
            }
        }
        public long SoTienKhachTra
        {
            get => GetPropertyValue<long>();
            set
            {
                SetProperty(value);
                UpdateHoaDon();
            }
        }
        public long SoTienThoiLai
        {
            get => GetPropertyValue<long>();
            set
            {
                if (value < 0)
                    IsDataValid = false;
                else
                    IsDataValid = true;
                SetProperty(value);
            }
        }
        public long TongTienChiTietBan
        {
            get => GetPropertyValue<long>();
            private set => SetProperty(value);
        }
        public long TongTienHoaDon
        {
            get => GetPropertyValue<long>();
            private set => SetProperty(value);
        }

        #region Converts from long type to money type with comma separation
        public string SoTienKhachTraConverter
        {
            get => (string)new MoneyRuleConverter().Convert(SoTienKhachTra, null, null, null);
        }

        public string SoTienThoiLaiConverter
        {
            get => (string)new MoneyRuleConverter().Convert(SoTienThoiLai, null, null, null);
        }

        public string TongTienChiTietBanConverter
        {
            get => (string)new MoneyRuleConverter().Convert(TongTienChiTietBan, null, null, null);
        }

        public string TongTienHoaDonConverter
        {
            get => (string)new MoneyRuleConverter().Convert(TongTienHoaDon, null, null, null);
        }
        #endregion


        public event EventHandler<SubmitedDataEventArgs> SubmitedData;

        /// <summary>
        /// Sự kiện xảy ra khi thêm vào 1 sản phẩm đã tồn tại trong hóa đơn
        /// </summary>
        public event EventHandler<ItemEventArgs<ChiTietBanModel>> SanPhamDaTonTai
        {
            add { DanhSachChiTietBan.ContainsItemModel += value; }
            remove { DanhSachChiTietBan.ContainsItemModel -= value; }
        }

        public HoaDonViewModel(PhieuBanModel phieuBan)
        {
            DanhSachChiTietBan = new ObservableCollectionViewModel<ChiTietBanModel>();
            DanhSachChiTietBan.ItemAdded += ThemSanPhamHandler;
            DanhSachChiTietBan.ItemRemoved += XoaSanPhamHandler;
            PhieuBan = phieuBan;
            KhachHang = new KhachHangModel()
            {
                MaKH = string.Empty,
            };
        }

        public HoaDonViewModel() : this(new PhieuBanModel()) { }



        public void ThemChiTietBan(ChiTietBanModel chiTiet)
        {
            DanhSachChiTietBan.Add(chiTiet);
        }

        private void ThemSanPhamHandler(object sender, ItemAddedEventArgs<ChiTietBanModel> e)
        {
            e.AddedItem.Model.SoLuongThayDoi += (s, ev) => UpdateHoaDon();
            UpdateHoaDon();
        }

        private void XoaSanPhamHandler(object sender, ItemRemovedEventArgs<ChiTietBanModel> e)
        {
            e.RemovedItem.Model.SoLuongThayDoi -= (s, ev) => UpdateHoaDon();
            UpdateHoaDon();
        }

        private void UpdateHoaDon()
        {
            TongTienChiTietBan = DanhSachChiTietBan.Items.ToList().Sum(item => item.Model.ThanhTien);
            TongTienHoaDon = TongTienChiTietBan * (100 + Thue - ChietKhau) / 100;
            SoTienThoiLai = SoTienKhachTra - TongTienHoaDon;
        }

        public bool Submit()
        {
            try
            {
                bool ketQuaSubmit = true;
                if (KhachHang == null)
                {
                    KhachHang = new KhachHangModel();
                }
                PhieuBan.MaKH = KhachHang.MaKH;

                PhieuBan.Submit(SubmitType.Add);
                DanhSachChiTietBan.Models.ForEach(model => model.Submit(SubmitType.Add));

                OnDataSubmited(new SubmitedDataEventArgs(null, true));
                return ketQuaSubmit;
            }
            catch
            {
                OnDataSubmited(new SubmitedDataEventArgs(null, false));
                return false;
            }
        }

        protected virtual void OnDataSubmited(SubmitedDataEventArgs e)
        {
            SubmitedData?.Invoke(this, e);
        }
    }
}
