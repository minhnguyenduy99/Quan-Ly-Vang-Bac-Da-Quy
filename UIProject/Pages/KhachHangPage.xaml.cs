using ModelProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIProject.ServiceProviders;
using UIProject.Test;
using UIProject.ViewModels.LayoutViewModels;
using UIProject.Views;

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for KhachHangPage.xaml
    /// </summary>
    public partial class KhachHangPage : Page
    {
        public KhachHangPage()
        {
            InitializeComponent();

            this.Loaded += KhachHangPage_Loaded;
        }

        private async void KhachHangPage_Loaded(object sender, RoutedEventArgs e)
        {
            await this.SlideFromRightToLeftAndFadeIn(0.7f);
        }

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerAddingDialogWindow customerAddingWnd = new CustomerAddingDialogWindow(this.btnAddCustomer);
            btnAddCustomer.CommandParameter = customerAddingWnd;
        }
        private void DeleteCustomerHandler(object sender, RoutedEventArgs e)
        {
            DialogPopupWindow notifyWnd = new DialogPopupWindow(new DialogWindowViewModel()
            {
                DialogType = DialogWindowType.YesNo,
                NoText = "Không",
                YesText = "Có",
                MessageText = "Bạn muốn xóa người này khỏi danh sách ?"
            });
            btnDelete.CommandParameter = notifyWnd;
        }

        private void BtnEditInfo_Click(object sender, RoutedEventArgs e)
        {
            var khachHangItem = this.gridCustomerInfo.DataContext as ItemViewModel<KhachHangModel>;
            if (khachHangItem == null)
                return;
            
            var editViewModel = new EditWindowViewModel<KhachHangModel>(khachHangItem.Model);
            editViewModel.AdditionData.Add(DataAccess.LoadKhuVuc());

            EditCustomerInfoWindow editInfoWnd = new EditCustomerInfoWindow(editViewModel, btnEditInfo);
            this.btnEditInfo.CommandParameter = editInfoWnd;
        }
    }
}
