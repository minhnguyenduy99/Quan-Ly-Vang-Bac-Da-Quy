using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using UIProject.ViewModels;
using UIProject.ViewModels.LayoutViewModels;
using UIProject.ServiceProviders;
using UIProject.Views;

using MaterialDesignThemes.Wpf;
using UIProject.UIConnector;

namespace UIProject
{
    /// <summary>
    ///  Provide a login window 
    /// </summary>
    public partial class LoginWindow : Window, IWindow
    {
        private LoginWindowViewModel viewModel { get; set; }
        
        public LoginWindow()
        {
            InitializeComponent();

            viewModel = new LoginWindowViewModel
            {
                CanMaximized = false,
                CanMinimized = false,
                WindowState = WindowState.Normal,
                IconSource = (string)Application.Current.FindResource("SoftwareIcon"),
                BackgroundSource = (string)Application.Current.FindResource("LoginBackground"),
                IsPasswordShow = false,
                NavigationBarVisibility = Visibility.Visible,
            };

            DataContext = viewModel;

            this.Loaded += LoginWindow_Loaded;
            this.Closed += LoginWindow_Closed;
        }

        private async void LoginWindow_Closed(object sender, EventArgs e)
        {
            await AnimationHelper.FadeAsync(this, 1f, 0f);
        }

        private async void LoginWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimationHelper.FadeAsync(this, 0f, 1f);
        }

        private void ToggleShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            if (ToggleShowPassword.IsChecked == false)
            {
                PasswordBox.Visibility = Visibility.Visible;
                txbPassword.Visibility = Visibility.Hidden;
                this.PasswordBox.Password = viewModel.TypingPassword;
                return;
            }
            PasswordBox.Visibility = Visibility.Hidden;
            txbPassword.Visibility = Visibility.Visible;
            this.txbPassword.Text = this.PasswordBox.Password;
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            DialogPopupWindow waitingDialog = new DialogPopupWindow(
                new DialogWindowViewModel()
                {
                    CanMaximized = false,
                    CanMinimized = false,
                    MessageText = "Vui lòng đợi",
                    Background = Brushes.White,
                    DialogType = DialogWindowType.WaitingMessage,
                });

            // For the weird behave of PasswordBox, its value cannot be used in binding with 
            // Text property of normal textbox. Therefore, this is a must-have function to update
            // the password in view model last time before login process
            viewModel.UpdateTypingPassword(PasswordBox.Password);

            bool? result = await viewModel.LoginAsync(waitingDialog);
            if (result != null && result == true)
            {
                InitializeHomepageWindow();
                this.Close();
            }
        }


        private void InitializeHomepageWindow()
        {
            MainWindow homepageWnd = new MainWindow();
            homepageWnd.DataContext = new HomePageWindowViewModel();

            homepageWnd.Closed += HomepageWnd_Closed;

            homepageWnd.Show();
        }

        private void HomepageWnd_Closed(object sender, EventArgs e)
        {
            // When the Homepage window is closed, close this one then.
            this.Close();
        }
    }
}
