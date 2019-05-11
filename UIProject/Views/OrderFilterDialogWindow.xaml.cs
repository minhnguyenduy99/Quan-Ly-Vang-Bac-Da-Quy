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
using System.Windows.Shapes;
using UIProject.ServiceProviders;
using UIProject.ViewModels;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for OrderFilterDialogWindow.xaml
    /// </summary>
    public partial class OrderFilterDialogWindow : Window, IWindow
    {
        public LocDonHangWindowVM ViewModel { get; set; }
        public OrderFilterDialogWindow(LocDonHangWindowVM viewModel)
        {
            InitializeComponent();
            DataContext = ViewModel = viewModel;
            PART_SearchTextBox.DataContext = viewModel.TimKiemKhachHangVM;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void SearchTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
