using BaseMVVM_Service.BaseMVVM;
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
using UIProject.Pages;
using UIProject.ServiceProviders;
using UIProject.ViewModels;
using UIProject.ViewModels.LayoutViewModels;

namespace UIProject.Test
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window, IClosable
    {
        public TestWindow()
        {
            InitializeComponent();

            DataContext = new BaseWindowViewModel()
            {
                CanMaximized = true,
                CanMinimized = true,
                BackgroundSource = (string)Application.Current.FindResource("LoginBackground"),
                IconSource = (string)Application.Current.FindResource("SoftwareIcon"),
                WindowState = WindowState.Maximized
            };

            this.DataContext = new MainWindowViewModel();

        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(object.ReferenceEquals(btn1.Content, btn2.Content).ToString());
        }

        
    }

    public class MainWindowViewModel
    {
        private ICommand openMessageBoxCmd;
        public ICommand OpenMessageBoxCommand
        {
            get => openMessageBoxCmd ?? new BaseCommand(OnOpenMessageBox);
            set => openMessageBoxCmd = value;
        }

        protected void OnOpenMessageBox()
        {
            MessageBox.Show("Message box");
        }
    }
}
