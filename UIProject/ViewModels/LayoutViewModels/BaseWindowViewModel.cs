using UIProject.ServiceProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BaseMVVM_Service.BaseMVVM;
using UIProject.Events;
using UIProject.UIConnector;

namespace UIProject.ViewModels.LayoutViewModels
{

    /// <summary>
    /// View model of Base window without a specified model
    /// </summary>
    public class BaseWindowViewModel : BaseViewModel
    {
        private string _title;
        private WindowState _windowState;
        private bool _canMinimized;
        private bool _canMaximized;
        private string _name;
        private bool? dialogResult;

        protected ICommand _minimizedCmd;
        protected ICommand _maximizedCmd;
        protected ICommand _exitCmd;
        protected ICommand _showCmd;

        /// <summary>
        /// Title of the window
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Indicating weither window should appear in a specific mode 
        /// </summary>
        public WindowState WindowState
        {
            get => _windowState;
            set => SetProperty(ref _windowState, value);
        }

        /// <summary>
        /// Source of window icon 
        /// </summary>
        public string IconSource { get; set; } = string.Empty;

        /// <summary>
        /// Source of main background of window
        /// </summary>
        public string BackgroundSource { get; set; } = string.Empty;

        /// <summary>
        /// Indicating weither window can be minimized
        /// </summary>
        public bool CanMinimized
        {
            get => this._canMinimized;
            set => SetProperty(ref _canMinimized, value);
        }

        /// <summary>
        /// Indicating weither window can be maximized
        /// </summary>
        public bool CanMaximized
        {
            get => _canMaximized;
            set => SetProperty(ref _canMaximized, value);
        }

        /// <summary>
        /// Indicating the visibility mode of Navigation bar
        /// </summary>
        public Visibility NavigationBarVisibility { get; set; } = Visibility.Visible;

        /// <summary>
        /// The x-coordinate startup location of the window
        /// </summary>
        public double Left { get; set; } = 0f;

        /// <summary>
        /// The y-coordinate startup location of the window
        /// </summary>
        public double Top { get; set; } = 0f;

        /// <summary>
        /// The dialog result of the window after closing
        /// </summary>
        public bool? DialogResult
        {
            get => dialogResult;
            set => SetProperty(ref dialogResult, value);
        }
        public ICommand MinimizedCommand
        {
            get => _minimizedCmd ?? (_minimizedCmd = new BaseCommand(OnMinimizedWindow, OnCanMinimized));
        }
        public ICommand MaximizedCommand
        {
            get => _maximizedCmd ?? (_maximizedCmd = new BaseCommand(OnMaximizedWindow, OnCanMaximized));
        }
        public ICommand ExitCommand
        {
            get => _exitCmd ?? (_exitCmd = new BaseCommand<IWindow>(OnExitWindow));
        }

        public ICommand ShowCommand
        {
            get => _showCmd ?? (_showCmd = new BaseCommand<IWindow>(OnShowWindow));
        }
        public BaseWindowViewModel() { }

        public event EventHandler<EventArgs> Closed;

        protected virtual void OnClosed(EventArgs e)
        {
            Closed?.Invoke(this, e);
        }

        protected virtual void OnMinimizedWindow() => WindowState = WindowState.Minimized;
        protected virtual void OnMaximizedWindow()
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        protected virtual bool OnCanMaximized() => CanMaximized;
        protected virtual bool OnCanMinimized() => CanMinimized;
        protected virtual void OnExitWindow(IWindow window)
        {
            if (window != null)
            {
                OnClosed(EventArgs.Empty);
                window.Close();
            }
        }
        protected virtual void OnShowWindow(IWindow window)
        {
            window.Show();
        }

    }
}
