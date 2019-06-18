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
    public class PhieuDichVuViewModel : BaseViewModel, ISubmitViewModel
    {
        /// <summary>
        /// This property is not implemented to use in this class
        /// </summary>
        public ISubmitable Data { get; set; } = null;

        public bool IsDataValid { get; private set; }

        public PhieuDichVuModel PhieuBan { get; set; }
        public ObservableCollectionViewModel<ChiTietDichVuModel> DSChiTietDichVuVM { get ; private set; }

        public PhieuDichVuViewModel()
        {
            PhieuBan = new PhieuDichVuModel();

            DSChiTietDichVuVM.ItemAdded += DSChiTietDichVuVM_ItemAdded;
            DSChiTietDichVuVM.ItemRemoved += DSChiTietDichVuVM_ItemRemoved;
        }

        private void DSChiTietDichVuVM_ItemRemoved(object sender, ItemRemovedEventArgs<ChiTietDichVuModel> e)
        {
            UpdatePhieuDichVu();
        }

        private void DSChiTietDichVuVM_ItemAdded(object sender, ItemAddedEventArgs<ChiTietDichVuModel> e)
        {
            UpdatePhieuDichVu();
        }

        public event EventHandler<SubmitedDataEventArgs> SubmitedData;

        public void UpdatePhieuDichVu()
        {
            PhieuBan.ThanhTien = 0;
            foreach(var chiTiet in DSChiTietDichVuVM.Items)
            {
                PhieuBan.ThanhTien += chiTiet.Model.ThanhTien;
                PhieuBan.TraTruoc += chiTiet.Model.TraTruoc;
            }
        }
        public void AddChiTietDichVu(ChiTietDichVuModel chiTietDV)
        {
            DSChiTietDichVuVM.Add(chiTietDV);
        }

        public void Remove(ItemViewModel<ChiTietDichVuModel> chiTietDV)
        {
            DSChiTietDichVuVM.Remove(chiTietDV);
        }
        public void Clear()
        {
            DSChiTietDichVuVM.Clear();
        }
        public bool Submit()
        {
            bool submitPhieuBanSuccess = PhieuBan.Submit(SubmitType.Add);
            if (submitPhieuBanSuccess)
            {
                try
                {
                    long? maPhieuBan = PhieuBan.MaPhieu;
                    foreach(var chiTiet in DSChiTietDichVuVM.Models)
                    {
                        chiTiet.Submit(SubmitType.Add);
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
    }
}
