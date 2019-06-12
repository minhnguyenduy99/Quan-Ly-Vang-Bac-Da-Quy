using BaseMVVM_Service.BaseMVVM;
using BaseMVVM_Service.BaseMVVM.Interfaces;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.UIConnector;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class NhapHangPageVM : BasePageViewModel 
    {
        private ICommand submitPhieuNhapHangCmd;
        private ICommand huyPhieuNhapHangCmd;
        private ICommand themSanPhamCmd;

        public ICommand SubmitPhieuNhapHangCommand
        {
            get => submitPhieuNhapHangCmd ?? new BaseCommand<IWindowExtension>(OnSubmitPhieuNhapHangCommandExecute);
            private set => submitPhieuNhapHangCmd = value;
        }

        public ICommand HuyPhieuNhapHangCommand
        {
            get => huyPhieuNhapHangCmd ?? new BaseCommand<IWindowExtension>(OnHuyPhieuNhapHangComamndExecute);
            private set => huyPhieuNhapHangCmd = value;
        }

        public ICommand ThemSanPhamCommand
        {
            get => themSanPhamCmd ?? new BaseCommand<IWindowExtension>(OnThemSanPhamCommandExecute);
            set => themSanPhamCmd = value;
        }



        public SearchTextBoxViewModel<SanPhamModel> TimKiemSanPhamVM { get; set; }
        public ObservableCollectionViewModel<ChiTietMuaModel> DanhSachChiTietMuaVM { get; set; }
        public PhieuMuaModel PhieuMua { get; set; }







        public NhapHangPageVM() : base() { }
        public NhapHangPageVM(INavigator navigator) : base(navigator) { }

     

        private void SetUpTimKiemSanPhamVM()
        {
            var dsSanPham = DataAccess.LoadSanPham();
            TimKiemSanPhamVM = new SearchTextBoxViewModel<SanPhamModel>(dsSanPham);
        }

        private void OnSubmitPhieuNhapHangCommandExecute(IWindowExtension window)
        {
            if (window.ShowDialog() == true)
            {
                bool submitSuccess = PhieuMua.Submit(SubmitType.Add);
                if (submitSuccess)
                {
                    foreach(var chiTiet in DanhSachChiTietMuaVM.Models)
                    {
                        try
                        {
                            chiTiet.Submit(SubmitType.Add);
                        }
                        catch { }
                    }
                }
            }
        }

        private void OnHuyPhieuNhapHangComamndExecute(IWindowExtension window)
        {
            throw new NotImplementedException();
        }

        private void OnThemSanPhamCommandExecute(IWindowExtension window)
        {
            AddingWindowViewModel<SanPhamModel> themSanPhamVM = new AddingWindowViewModel<SanPhamModel>();
            SearchTextBoxViewModel<NhaCungCapModel> timKiemNCC
                = new SearchTextBoxViewModel<NhaCungCapModel>(DataAccess.LoadNhaCungCap());

            themSanPhamVM.AdditionData.Add(DataAccess.LoadLoaiSanPham());
            themSanPhamVM.Searchers.Add(timKiemNCC);

            window.DataContext = themSanPhamVM;

            // Thêm sản phẩm vào database thành công
            if (window.ShowDialog(-500, 0) == true)
            {
                // Cập nhật lại danh sách sản phẩm
                TimKiemSanPhamVM.RefreshItemSource(DataAccess.LoadSanPham());
            }
            
        }

        protected override void LoadComponentsInternal()
        {
            SetUpTimKiemSanPhamVM();
        }

        protected override void ReloadComponentsInternal()
        {
            TimKiemSanPhamVM.Reload();
        }
    }
}
