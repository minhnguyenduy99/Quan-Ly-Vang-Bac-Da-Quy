using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using UIProject.Converters;
using UIProject.Events;
using UIProject.ServiceProviders;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.DataViewModels
{
    public class NhapHangViewModel : BaseViewModelObject, ISubmitViewModel, ITableConvertable
    {
        /// <summary>
        /// This property is not implemented in this class
        /// </summary>
        public ISubmitable Data { get; set; } = null;

        public bool IsDataValid
        {
            get => GetPropertyValue<bool>();
            private set
            {
                SetProperty(value);
            }
        }

        public PhieuMuaModel PhieuMua { get; private set; }
        public ObservableCollectionViewModel<ChiTietMuaModel> DSChiTietMua { get; private set; }

        public int TongSoLuong
        {
            get => GetPropertyValue<int>();
            private set => SetProperty(value);
        }

        public long TongTien
        {
            get => GetPropertyValue<long>();
            private set => SetProperty(value);
        }

        public NhapHangViewModel(): base()
        {
            DSChiTietMua = new ObservableCollectionViewModel<ChiTietMuaModel>();
            PhieuMua = new PhieuMuaModel();
            PhieuMua.DataValidChanged += DataValidChangedHandler;
            PhieuMua.NgayLap = DateTime.Now.ToString();
            DSChiTietMua.ItemAdded += DSChiTietMua_ItemAdded;
            DSChiTietMua.ItemRemoved += DSChiTietMua_ItemRemoved;        
        }

        public void Add(ItemViewModel<SanPhamModel> sanPham)
        {
            var chiTietMua = new ChiTietMuaModel(sanPham.Model.MaSP);
            chiTietMua.PropertyChanged += ChiTietMua_DonGiaThayDoi;
            DSChiTietMua.Add(chiTietMua);

            // local function
            void ChiTietMua_DonGiaThayDoi(object sender, EventArgs e)
            {
                UpdatePhieuMua();
            }
        }

        private void DataValidChangedHandler(object sender, BaseSubmitableModel.DataValidChangedEventArgs e)
        {
            IsDataValid = e.DataValid;
        }

        public void Clear()
        {
            DSChiTietMua.Clear();
        }
        private void DSChiTietMua_ItemAdded(object sender, ItemAddedEventArgs<ChiTietMuaModel> e)
        {
            UpdatePhieuMua();

            // Subcribe the DataValidChanged event
            e.AddedItem.Model.DataValidChanged += DataValidChangedHandler;
        }
        private void DSChiTietMua_ItemRemoved(object sender, ItemRemovedEventArgs<ChiTietMuaModel> e)
        {
            UpdatePhieuMua();

            // Unsubcribe the DataValidChanged event
            e.RemovedItem.Model.DataValidChanged -= DataValidChangedHandler;
        }

        public void UpdatePhieuMua()
        {
            TongSoLuong = 0;
            TongTien = 0;
            foreach(var chiTiet in DSChiTietMua.Items)
            {
                TongSoLuong++;
                TongTien += chiTiet.Model.DonGia * chiTiet.Model.SoLuong;
            }        
        }

        public event EventHandler<SubmitedDataEventArgs> SubmitedData;

        public bool Submit()
        {
            var dsChiTiet = DSChiTietMua.Models;
            var dsSanPham = DataAccess.LoadSanPham();
            try
            {
                PhieuMua.MaPhieu = DataAccess.SavePhieuMua(PhieuMua);
                foreach (var chiTiet in dsChiTiet)
                {
                    // Gán mã phiếu mua cho chi tiết và lưu xuống database
                    chiTiet.MaPhieuMua = PhieuMua.MaPhieu;
                    chiTiet.Submit(SubmitType.Add);                  
                }

                return true;
            }
            catch { return false; }
        }

        protected override void LoadComponentsInternal()
        {
            PhieuMua = new PhieuMuaModel();
            DSChiTietMua?.Load();
        }

        protected override void ReloadComponentsInternal()
        {
            DSChiTietMua?.Reload();
        }

        public bool ConvertDataToTable(Table table)
        {
            return CollectionToTableConverter.ConvertToTable(
                DSChiTietMua.Models,
                new string[]
                {
                    "MaSP",
                    "TenSP",
                    "TenLoaiSP",
                    "SoLuong",
                    "DonGia",
                    "ThanhTien"
                }, new TryMoneyConverter(), table);
        }
    }
}
