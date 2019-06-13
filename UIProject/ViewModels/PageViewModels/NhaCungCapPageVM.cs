using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.Events;
using UIProject.UIConnector;
using UIProject.ViewModels.FunctionInterfaces;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class NhaCungCapPageVM : BasePageViewModel
    {
        private List<NhaCungCapModel> dsNhaCungCap;
        private List<KhuVucModel> dsKhuVuc;


        public ObservableCollectionViewModel<NhaCungCapModel> DanhSachNhaCungCapVM { get; set; }
        public SearchTextBoxViewModel<NhaCungCapModel> TimKiemNhaCungCapVM { get; set; }
        public EnumFilterViewModel<NhaCungCapModel> LocTheoKhuVucVM { get; set; }

        public ICommand ThemNhaCungCapCommand
        {
            get => GetPropertyValue<ICommand>() ?? new BaseCommand<IWindowExtension>(OnThemNhaCungCapCommandExecute);
            set => SetProperty(value);
        }

        public ICommand ChinhSuaNhaCungCapCommand
        {
            get => GetPropertyValue<ICommand>() ?? new BaseCommand<IWindowExtension>(OnChinhSuaNhaCungCapCommandExecute);
            set => SetProperty(value);
        }
        public ICommand XoaNhaCungCapCommand
        {
            get => GetPropertyValue<ICommand>() ?? new BaseCommand<IWindow>(OnXoaNhaCungCapCommandExecute);
            set => SetProperty(value);
        }



        public NhaCungCapPageVM() : base() { }
        public NhaCungCapPageVM(INavigator navigator) : base(navigator) { }

        
        private void SetUpDanhSachNhaCungCapVM()
        {
            DanhSachNhaCungCapVM = new ObservableCollectionViewModel<NhaCungCapModel>(dsNhaCungCap);
            DanhSachNhaCungCapVM.Filters.Add(LocTheoTenCallBack);
            DanhSachNhaCungCapVM.Filters.Add(LocTheoKhuVucVM.FilterCallBack);


            bool LocTheoTenCallBack(ItemViewModel<NhaCungCapModel> NCCItem)
            {
                return NCCItem.Model.TenNCC.ToLower().StartsWith(TimKiemNhaCungCapVM.Text.ToLower());
            }
        }



        private void SetUpTimKiemNhaCungCapVM()
        {
            TimKiemNhaCungCapVM = new SearchTextBoxViewModel<NhaCungCapModel>(null);
            TimKiemNhaCungCapVM.TextChanged += TimKiemNhaCungCapVM_TextChanged;

            void TimKiemNhaCungCapVM_TextChanged(object sender, Events.TextValueChangedEventArgs e)
            {
                DanhSachNhaCungCapVM.Filter();
            }
        }

        private void SetUpLocTheoKhuVucVM()
        {
            LocTheoKhuVucVM = new EnumFilterViewModel<NhaCungCapModel>(
                LocTheoKhuVucCallBack, dsKhuVuc);

            LocTheoKhuVucVM.IsApplyNonFilterItem = true;
            LocTheoKhuVucVM.NonApplyFilterItem.Model = new KhuVucModel() { MaKhuVuc = null, TenKhuVuc = "Chọn tất cả" };
            LocTheoKhuVucVM.SelectedItemChanged += LocTheoKhuVucVM_SelectedItemChanged;
            
            // local function
            bool LocTheoKhuVucCallBack(ItemViewModel<NhaCungCapModel> khuVucItem)
            {
                if (khuVucItem == null)
                    return true;

                var khuVucDaChonItem = LocTheoKhuVucVM.Collection.SelectedItem as ItemViewModel<object>;
                if (khuVucDaChonItem == null)
                    return true;

                long? maKhuVucDaChon = (khuVucDaChonItem.Model as KhuVucModel)?.MaKhuVuc;
                long? maKhuVuc = khuVucItem.Model.MaKhuVuc;

                if (maKhuVucDaChon == null)
                    return true;

                return maKhuVuc == maKhuVucDaChon;
            }
            void LocTheoKhuVucVM_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
            {
                DanhSachNhaCungCapVM.Filter();
            }
        }







        #region Command execution
        private void OnXoaNhaCungCapCommandExecute(IWindow window)
        {
            var notifyDeleteWnd = new DialogWindowViewModel()
            {
                DialogType = DialogWindowType.YesNo,
                MessageText = "Bạn muốn xóa nhà cung cấp này?",
                YesText = "Có",
                NoText = "Không"
            };

            notifyDeleteWnd.ButtonPressed += NotifyDeleteWnd_ButtonPressed;
            window.DataContext = notifyDeleteWnd;

            if (window.ShowDialog() == true)
            {
                // Nếu người dùng chọn có thì xóa nhà cung cấp đã chọn và reload page
                var nhaCungCap = DanhSachNhaCungCapVM.SelectedItem as ItemViewModel<NhaCungCapModel>;
                if (nhaCungCap != null && nhaCungCap.Model != null)
                {
                    DataAccess.RemoveNhaCungCap(nhaCungCap.Model);
                    Reload();
                }
            }

            // local functon
            void NotifyDeleteWnd_ButtonPressed(object sender, DialogButtonPressedEventArgs e)
            {
                if (e.DialogResult == DialogResult.Yes)
                    window.DialogResult = true;
                if (e.DialogResult == DialogResult.No)
                    window.DialogResult = false;

                window.Close();
            }
        }



        private void OnChinhSuaNhaCungCapCommandExecute(IWindowExtension window)
        {
            var nhaCungCapDuocChon = DanhSachNhaCungCapVM.SelectedItem as ItemViewModel<NhaCungCapModel>;
            var chinhSuaNCCVM = new EditWindowViewModel<NhaCungCapModel>(nhaCungCapDuocChon?.Model);
            chinhSuaNCCVM.AdditionData.Add(DataAccess.LoadKhuVuc());

            window.DataContext = chinhSuaNCCVM;
            window.Closing += (sender, e) => e.Cancel = true;

            if (window.ShowDialog(-500,-400) == true)
            {
                DataAccess.UpdateNhaCungCap(chinhSuaNCCVM.Data as NhaCungCapModel);
                Reload();
            }
        }
        private void OnThemNhaCungCapCommandExecute(IWindowExtension window)
        {
            AddingWindowViewModel<NhaCungCapModel> themNhaCungCapVM
                = new AddingWindowViewModel<NhaCungCapModel>();
            themNhaCungCapVM.AdditionData.Add(dsKhuVuc);

            window.Closing += (sender, e) => e.Cancel = true;
            window.DataContext = themNhaCungCapVM;

            if (window.ShowDialog(-500, -400) == true)
            {
                Reload();
            } 
        }

        private void RefreshSource()
        {
            dsKhuVuc = DataAccess.LoadKhuVuc();
            dsNhaCungCap = DataAccess.LoadNhaCungCap();
        }
        #endregion


        protected override void LoadComponentsInternal()
        {
            RefreshSource();
            SetUpLocTheoKhuVucVM();
            SetUpTimKiemNhaCungCapVM();
            SetUpDanhSachNhaCungCapVM();
        }

        protected override void ReloadComponentsInternal()
        {
            RefreshSource();

            DanhSachNhaCungCapVM.RefreshItemsSource(dsNhaCungCap);

            TimKiemNhaCungCapVM.Reload();
            DanhSachNhaCungCapVM.Reload();
            LocTheoKhuVucVM.Collection.Reload();
        }
    }
}
