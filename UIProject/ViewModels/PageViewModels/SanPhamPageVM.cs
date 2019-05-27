using BaseMVVM_Service.BaseMVVM;
using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.ServiceProviders;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.ViewModels.PageViewModels
{
    public class SanPhamPageVM : BasePageViewModel
    {
        private ICommand themSPCmd;

        public ObservableCollectionViewModel<SanPhamModel> DanhSachSanPham;

        public ICommand ThemSanPhamCommand
        {
            get => themSPCmd ?? new BaseCommand<IWindow>(OnThemSanPhamCommandExecute);
            set => themSPCmd = value;
        }
        protected override void LoadPageComponents()
        {
            
        }

        protected virtual void OnThemSanPhamCommandExecute(IWindow window)
        {
            window.DataContext = new AddingWindowViewModel<SanPhamModel>();
            if (window.ShowDialog() == true)
            {

            }
        }
    }
}
