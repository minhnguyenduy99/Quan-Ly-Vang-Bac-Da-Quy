using ModelProject.Models;
using ModelProject.DataViewModels;
using UIProject.ViewModels.LayoutViewModels;
using UIProject.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UIProject.ViewModels
{
    /// <summary>
    /// Provide a view model for login window to interact with login process
    /// </summary>
    public class LoginWindowViewModel: BaseWindowViewModel
    {
        private const string DEFAULT_ERROR_TEXT = "Tên đăng nhập hoặc mật khẩu không đúng";

        #region Private Fields 
        private string typingUsername = string.Empty;
        private string typingPwd = string.Empty;
        private bool isPasswordShow = false;
        private string errorText = string.Empty;
        private int loginCount = 0;

        #endregion


        /// <summary>
        /// The username that is typing in the textbox in the UI
        /// </summary>
        public string TypingUsername
        {
            get => this.typingUsername;
            set
            {
                this.typingUsername = value;
                OnPropertyChanged("TypingUsername");
            }
        }

        /// <summary>
        /// The password that is typed in the textbox in the UI
        /// </summary>       
        public string TypingPassword
        {
            get => this.typingPwd;
            set
            {
                this.typingPwd = value;
                OnPropertyChanged("TypingPassword");
            }
        }

        public bool IsAccountValid { get; private set; } = true;

        /// <summary>
        /// Error text to display when username or password is incorrect
        /// </summary>
        public string ErrorText
        {
            get
            {
                return errorText;
            }
            private set
            {
                SetProperty(ref errorText, value);
            }
        }

        /// <summary>
        /// The number of times the user has tried to login
        /// </summary>
        public int LoginCount
        {
            get => this.loginCount;
            set
            {
                SetProperty(ref loginCount, value);
            }
        }

        /// <summary>
        /// Indicating weither the password should be visibly shown in the UI
        /// </summary>
        public bool IsPasswordShow
        {
            get => this.isPasswordShow;
            set
            {
                SetProperty(ref isPasswordShow, value);
            }
        }

        public LoginWindowViewModel() : base()
        {

        }

        public bool Login()
        {
            IsAccountValid = true;
            Thread.Sleep(3000);
            OnPropertyChanged("IsAccountValid");
            return IsAccountValid;
        }

        public void UpdateTypingPassword(string password)
        {
            if (!IsPasswordShow)
                this.TypingPassword = password;
        }
    }
}
