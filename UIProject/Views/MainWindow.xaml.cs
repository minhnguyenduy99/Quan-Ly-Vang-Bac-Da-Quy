using UIProject.ViewModels;
using UIProject.ServiceProviders;
using UIProject.Pages;

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

using static UIProject.ViewModels.TabViewModel;
using UIProject.ViewModels.LayoutViewModels;
using UIProject.UIConnector;

namespace UIProject.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IWindow
    {
        public HomePageWindowViewModel HomePageWindowVM { get; private set; }
        public Window LoginWindowInstance { get; private set; }
        public MainWindow(Window loginWindow)
        {

            InitializeComponent();

            this.LoginWindowInstance = loginWindow;

            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;        
        }

        private async void MainWindow_Closed(object sender, EventArgs e)
        {
            await AnimationHelper.FadeAsync(this, 1f, 0f);
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetupBindingTabControl();
            await AnimationHelper.FadeAsync(this, 0f, 1f);
        }

        private void SetupBindingTabControl()
        {
            var tabs = GetAllRadioButtons();
            for(int i=0;i<tabs.Count();i++)
            {
                try
                {
                    tabs.ElementAt(i).DataContext = (DataContext as HomePageWindowViewModel).ListTabs[i];
                }
                catch { break; }
            }

            // If a tab (radio button) is checked, its parent (expander) will be expanded
            tabs.ForEach(tab => tab.Checked += Tab_Checked);
        }

        private void Tab_Checked(object sender, RoutedEventArgs e)
        {
            var tabCast = sender as RadioButton;
            if (tabCast != null)
            {
                var tabParent = tabCast.Parent as StackPanel;
                if (tabParent != null && tabParent != TabContainerStackPanel)
                {
                    var expanderCast = tabParent.Parent as Expander; 
                    if (expanderCast != null)
                    {
                        expanderCast.IsExpanded = true;
                    }
                }
            }
        }

        private List<RadioButton> GetAllRadioButtons()
        {
            List<RadioButton> listRadioBtns = new List<RadioButton>();
            StackPanel container = TabContainerStackPanel;
            foreach(var child in container.Children)
            {
                if (child is Expander)
                {
                    List<RadioButton> subChildren = GetRadioButtonsFromExpander((Expander)child);
                    listRadioBtns.AddRange(subChildren);
                }
                else
                {
                    if (child is RadioButton)
                    {
                        listRadioBtns.Add((RadioButton)child);
                    }
                }
            }
            return listRadioBtns;
        }

        private List<RadioButton> GetRadioButtonsFromExpander(Expander expander)
        {
            if (expander == null)
                return null;
            List<RadioButton> radioButtons = new List<RadioButton>();
            var containerCast = expander.Content as StackPanel;
            if (containerCast != null)
            {
                foreach(var child in containerCast.Children)
                {
                    if (child is RadioButton)
                        radioButtons.Add((RadioButton)child);
                }
            }
            return radioButtons;
        }
    }
}
