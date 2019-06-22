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
using static BaseMVVM_Service.BaseMVVM.BaseSubmitableModel;

namespace UIProject.ViewModels.DataViewModels
{
    public class PhieuDichVuViewModel : BaseViewModel, ISubmitViewModel
    {
        /// <summary>
        /// This property is not implemented to use in this class
        /// </summary>
        public ISubmitable Data { get; set; } = null;

        public bool IsDataValid { get; private set; }

        public PhieuDichVuModel PhieuDichVu { get; set; }

        #region Thông tin liên hệ
        public string DiaChi
        {
            get => GetPropertyValue<string>();
            set => SetProperty(value);
        }

        public string SoDienThoai
        {
            get => GetPropertyValue<string>();
            set => SetProperty(value);
        }
        #endregion


        public ObservableCollectionViewModel<ChiTietDichVuModel> DSChiTietDichVu { get ; private set; }

        public PhieuDichVuViewModel()
        {
            PhieuDichVu = new PhieuDichVuModel();

            DSChiTietDichVu = new ObservableCollectionViewModel<ChiTietDichVuModel>();
            DSChiTietDichVu.ItemAdded += DSChiTietDichVuVM_ItemAdded;
            DSChiTietDichVu.ItemRemoved += DSChiTietDichVuVM_ItemRemoved;
        }

        private void DSChiTietDichVuVM_ItemAdded(object sender, ItemAddedEventArgs<ChiTietDichVuModel> e)
        {
            if (e.AddedItem == null || e.AddedItem.Model == null)
                return;

            var chiTietThem = e.AddedItem.Model;
            chiTietThem.ThongTinChiTietThayDoi += ChiTietDichVu_ThongTinThayDoiHandler;
            chiTietThem.DataValidChanged += DuLieuHopLeHandler;

            UpdatePhieuDichVu();
        }

        private void DSChiTietDichVuVM_ItemRemoved(object sender, ItemRemovedEventArgs<ChiTietDichVuModel> e)
        {
            if (e.RemovedItem == null || e.RemovedItem.Model == null)
                return;

            var chiTietXoa = e.RemovedItem.Model;
            chiTietXoa.ThongTinChiTietThayDoi -= ChiTietDichVu_ThongTinThayDoiHandler;
            chiTietXoa.DataValidChanged -= DuLieuHopLeHandler;

            UpdatePhieuDichVu();
        }


        private void DuLieuHopLeHandler(object sender, DataValidChangedEventArgs e)
        { 
            this.IsDataValid = e.DataValid;
        }


        /// <summary>
        /// Mỗi khi có một chi tiết dịch vụ thay đổi thì cập nhật lại thông tin phiếu dịch vụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChiTietDichVu_ThongTinThayDoiHandler(object sender, EventArgs e)
        {
            UpdatePhieuDichVu();
        }

        public event EventHandler<SubmitedDataEventArgs> SubmitedData;

        /// <summary>
        /// Tính lại các khoản tiền của phiếu dịch vụ
        /// </summary>
        public void UpdatePhieuDichVu()
        {
            PhieuDichVu.TongTien = 0;
            PhieuDichVu.TongTienTraTruoc = 0;
            foreach(var chiTiet in DSChiTietDichVu.Items)
            {
                PhieuDichVu.TongTien += chiTiet.Model.ThanhTien;
                PhieuDichVu.TongTienTraTruoc += chiTiet.Model.TraTruoc;
            }
        }
        public void AddChiTietDichVu(ChiTietDichVuModel chiTietDV)
        {
            DSChiTietDichVu.Add(chiTietDV);
        }

        public void Remove(ItemViewModel<ChiTietDichVuModel> chiTietDV)
        {
            DSChiTietDichVu.Remove(chiTietDV);
        }
        public void Clear()
        {
            DSChiTietDichVu.Clear();
        }
        public bool Submit()
        {
            bool submitPhieuDichVuSuccess = PhieuDichVu.Submit(SubmitType.Add);
            if (submitPhieuDichVuSuccess)
            {
                try
                {
                    long? maPhieuDichVu = PhieuDichVu.MaPhieu;
                    foreach(var chiTiet in DSChiTietDichVu.Models)
                    {
                        chiTiet.MaPhieu = maPhieuDichVu;
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
