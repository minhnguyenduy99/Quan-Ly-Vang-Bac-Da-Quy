using BaseMVVM_Service.BaseMVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIProject.UIConnector;

namespace UIProject.ViewModels.LayoutViewModels
{
    /// <summary>
    /// Enumeration represents types of Dialog Window
    /// </summary>
    public enum DialogWindowType
    {
        Waiting,
        WaitingMessage,
        WaitingCancel,
        WaitingMessageCancel,
        YesNo,
        Info,
        None
    }

    /// <summary>
    /// Result of a Dialog Window
    /// </summary>
    public enum DialogResult
    {
        Yes,
        No,
        Cancel,
        OK
    }

    /// <summary>
    /// View model of Dialog Windoww without a specified Data model
    /// </summary>
    public class DialogWindowViewModel : BaseWindowViewModel 
    {
        #region Default content in the buttons of Dialog Window
        public const string DEFAULT_YES_TEXT = "Có";
        public const string DEFAULT_NO_TEXT = "Không";
        public const string DEFAULT_CANCEL_TEXT = "Hủy bỏ";
        public const string DEFAULT_OK_TEXT = "OK";
        public const string DEFAULT_WAITING_TEXT = "Vui lòng chờ";
        #endregion

        #region Private Members
        private string waitingText;
        private string yesText;
        private string noText;
        private string cancelText;
        private string messageText;
        private string okText;
        private DialogResult dialogResult;
        private ICommand yesCmd;
        private ICommand noCmd;
        private ICommand cancelCmd;
        private ICommand okCmd;
        #endregion


        /// <summary>
        /// Type of Dialog Window
        /// </summary>
        public DialogWindowType DialogType { get; set; }


        /// <summary>
        /// Text to display when window type is WaitingType or WaitingCancelType
        /// </summary>
        public string WaitingText
        {
            get => this.waitingText;
            set => SetProperty(ref waitingText, value);
        }


        /// <summary>
        /// Text to display on button Yes of Dialog Window
        /// </summary>
        public string YesText
        {
            get => this.yesText;
            set => SetProperty(ref yesText, value);
        }


        /// <summary>
        /// Text to display on button No of Dialog Window
        /// </summary>
        public string NoText
        {
            get => this.noText;
            set => SetProperty(ref noText, value);
        }


        /// <summary>
        /// Text to display on Cancel Button of Dialog Window
        /// </summary>
        public string CancelText
        {
            get => cancelText;
            set => SetProperty(ref cancelText, value);
        }


        /// <summary>
        /// Text to display on Dialog Window of info type or YesNo type 
        /// </summary>
        public string MessageText
        {
            get => messageText;
            set => SetProperty(ref messageText, value);
        }


        /// <summary>
        /// Dialog Result of dialog window
        /// </summary>
        public DialogResult DialogResult
        {
            get => this.dialogResult;
            set => SetProperty(ref dialogResult, value);
        }


        /// <summary>
        /// Text to display on OK Button on dialog window of info type 
        /// </summary>
        public string OKText
        {
            get => this.okText;
            set => SetProperty(ref okText, value);
        }

        /// <summary>
        /// The window that owns this view model
        /// </summary>
        public IWindow OwnerWindow { get; set; }


        public DialogWindowViewModel()
        {
            // Set up default contents of buttons in dialog window
            yesText = DEFAULT_YES_TEXT;
            noText = DEFAULT_NO_TEXT;
            cancelText = DEFAULT_CANCEL_TEXT;
            okText = DEFAULT_OK_TEXT;
            waitingText = DEFAULT_WAITING_TEXT;
            NavigationBarVisibility = System.Windows.Visibility.Collapsed;
            DialogType = DialogWindowType.YesNo;
        }

        /// <summary>
        /// Command activates when Yes button is pressed
        /// </summary>
        public ICommand YesButtonPressedCommand
        {
            get => yesCmd ?? (yesCmd = new BaseCommand(() => OnButtonPressed(DialogResult.Yes)));
            set => yesCmd = value;
        }

        /// <summary>
        /// Command activates when No button is pressed
        /// </summary>
        public ICommand NoButtonPressedCommand
        {
            get => noCmd ?? (noCmd = new BaseCommand(() => OnButtonPressed(DialogResult.No)));
            set => noCmd = value;
        }

        /// <summary>
        /// Command activates when Cancel button is pressed
        /// </summary>
        public ICommand CancelButtonPressedCommand
        {
            get => cancelCmd ?? (cancelCmd = new BaseCommand(() => OnButtonPressed(DialogResult.Cancel)));
            set => cancelCmd = value;
        }

        /// <summary>
        /// Command activates when OK button is pressed
        /// </summary>
        public ICommand OKButtonPressedCommand
        {
            get => okCmd ?? (okCmd = new BaseCommand(() => OnButtonPressed(DialogResult.OK)));
            set => okCmd = value;
        }

        
        protected virtual void OnButtonPressed(DialogResult result)
        {
            this.DialogResult = result;
            ButtonPressed?.Invoke(this, new DialogButtonPressedEventArgs(result));
        }

        public event EventHandler<DialogButtonPressedEventArgs> ButtonPressed;
    }


    public class DialogButtonPressedEventArgs: EventArgs
    {
        public DialogResult DialogResult { get; private set; }
        
        public DialogButtonPressedEventArgs(DialogResult result)
        {
            DialogResult = result;
        }
    }
}
