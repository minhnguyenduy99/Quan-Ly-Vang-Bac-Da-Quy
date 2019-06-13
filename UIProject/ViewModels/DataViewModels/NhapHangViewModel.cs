using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIProject.Events;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.DataViewModels
{
    public class NhapHangViewModel : BaseViewModelObject, ISubmitViewModel
    {
        /// <summary>
        /// This property is not implemented in this class
        /// </summary>
        public ISubmitable Data { get; set; } = null;

        public bool IsDataValid { get; private set; }

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
            DSChiTietMua.ItemAdded += DSChiTietMua_ItemAdded;
            DSChiTietMua.ItemRemoved += DSChiTietMua_ItemRemoved;
        }

        public void Add(ItemViewModel<SanPhamModel> sanPham)
        {
            DSChiTietMua.Add(new ChiTietMuaModel(sanPham.Model.MaSP));
        }

        private void DSChiTietMua_ItemAdded(object sender, ItemAddedEventArgs<ChiTietMuaModel> e)
        {
            UpdatePhieuMua();
        }
        private void DSChiTietMua_ItemRemoved(object sender, ItemRemovedEventArgs<ChiTietMuaModel> e)
        {
            UpdatePhieuMua();
        }

        public void UpdatePhieuMua()
        {
            TongSoLuong = 0;
            TongTien = 0;
            foreach(var chiTiet in DSChiTietMua.Items)
            {
                TongSoLuong += chiTiet.Model.SoLuong;
                TongTien += chiTiet.Model.DonGia * chiTiet.Model.SoLuong;
            }        
        }

        public event EventHandler<SubmitedDataEventArgs> SubmitedData;

        public bool Submit()
        {
            var dsChiTiet = DSChiTietMua.Models;
            try
            {
                foreach (var chiTiet in dsChiTiet)
                {
                    chiTiet.Submit(SubmitType.Add);
                }
                PhieuMua.Submit(SubmitType.Add);
                return true;
            }
            catch { return false; }
        }

        protected override void LoadComponentsInternal()
        {
            DSChiTietMua?.Load();
        }

        protected override void ReloadComponentsInternal()
        {
            DSChiTietMua?.Reload();
        }
    }
}
