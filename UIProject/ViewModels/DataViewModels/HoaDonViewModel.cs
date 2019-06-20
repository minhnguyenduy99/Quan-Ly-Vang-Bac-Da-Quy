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
    public class HoaDonViewModel : BaseViewModelObject, ISubmitViewModel
    {
        public ISubmitable Data { get; set; } = null;

        public bool IsDataValid
        {
            get => GetPropertyValue<bool>();
            private set => SetProperty(value);
        }
        public ObservableCollectionViewModel<ChiTietBanModel> DanhSachChiTietBan { get; set; }

        public PhieuBanModel PhieuBan
        {
            get => GetPropertyValue<PhieuBanModel>();
            set
            {
                SetProperty(value);
                DanhSachChiTietBan?.Clear();
                if (value == null)
                    return;
                else
                {
                    var dsChiTietBan = DataAccess.LoadChiTietBanByMaCTB(PhieuBan.MaPhieu);
                    dsChiTietBan.ForEach(chiTiet => DanhSachChiTietBan.Add(chiTiet));
                }
            }
        }

        public KhachHangModel KhachHang
        {
            get => GetPropertyValue<KhachHangModel>();
            set => SetProperty(value);
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
            get => (string)new MoneyRuleConverter().Convert(PhieuBan.ThanhTien, null, null, null);
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

        public HoaDonViewModel() : this(new PhieuBanModel()) { }
        public HoaDonViewModel(PhieuBanModel phieuBan) : base()
        {
            PhieuBan = phieuBan;
            PhieuBan.GiaTriThanhTienThayDoi += PhieuBan_GiaTriThanhTienThayDoi;
        }

        // Trong trường hợp tổng tiền hóa đơn thay đổi do chiết khấu và thuế
        private void PhieuBan_GiaTriThanhTienThayDoi(object sender, EventArgs e)
        {    
            SoTienThoiLai = SoTienKhachTra - PhieuBan.ThanhTien;
        }

        public void ThemChiTietBan(ChiTietBanModel chiTiet)
        {
            DanhSachChiTietBan.Add(chiTiet);
        }

        private void ThemSanPhamHandler(object sender, ItemAddedEventArgs<ChiTietBanModel> e)
        {
            e.AddedItem.Model.SoLuongThayDoi += (s, ev) => UpdateHoaDon();
            e.AddedItem.Model.SoLuongSanPhamKhongDu += SoLuongSanPhamKhongDuHandler;
            UpdateHoaDon();
        }

        private void SoLuongSanPhamKhongDuHandler(object sender, EventArgs e)
        {
            SanPhamKhongDu?.Invoke(this, EventArgs.Empty);
        }
        private void XoaSanPhamHandler(object sender, ItemRemovedEventArgs<ChiTietBanModel> e)
        {
            e.RemovedItem.Model.SoLuongThayDoi -= (s, ev) => UpdateHoaDon();
            e.RemovedItem.Model.SoLuongSanPhamKhongDu -= SoLuongSanPhamKhongDuHandler;
            UpdateHoaDon();
        }

        public event EventHandler SanPhamKhongDu;

        private void UpdateHoaDon()
        {
            TongTienChiTietBan = DanhSachChiTietBan.Models.Sum(chiTiet => chiTiet.ThanhTien);
            PhieuBan.ThanhTien = (long)(TongTienChiTietBan * (100 - PhieuBan.ChietKhau + PhieuBan.Thue) / 100);
            SoTienThoiLai = SoTienKhachTra - PhieuBan.ThanhTien;
        }

        public bool Submit()
        {
            try
            {
                PhieuBan.MaKH = KhachHang?.MaKH;
                var submitPhieuBanSuccess = PhieuBan.Submit(SubmitType.Add);
                if (!submitPhieuBanSuccess)
                {
                    OnDataSubmited(new SubmitedDataEventArgs(null, false));
                    return false;
                }

                foreach(var chiTiet in DanhSachChiTietBan.Models)
                {
                    chiTiet.MaPhieuBan = PhieuBan.MaPhieu;
                    chiTiet.Submit(SubmitType.Add);
                }

                OnDataSubmited(new SubmitedDataEventArgs(null, true));
                return true;
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

        protected override void LoadComponentsInternal()
        {
            DanhSachChiTietBan = new ObservableCollectionViewModel<ChiTietBanModel>();
            DanhSachChiTietBan.ItemAdded += ThemSanPhamHandler;
            DanhSachChiTietBan.ItemRemoved += XoaSanPhamHandler;
            PhieuBan = new PhieuBanModel();
            KhachHang = new KhachHangModel();
        }

        protected override void ReloadComponentsInternal()
        {
            DanhSachChiTietBan.Reload();
            SoTienKhachTra = SoTienThoiLai = 0;
            PhieuBan = new PhieuBanModel();
            KhachHang = new KhachHangModel();
        }
    }
}
