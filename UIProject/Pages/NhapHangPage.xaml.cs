using System.Windows.Controls;
using UIProject.ServiceProviders;

namespace UIProject.Pages
{
    /// <summary>
    /// Interaction logic for NhapHangPage.xaml
    /// </summary>
    public partial class NhapHangPage : Page
    {
        public NhapHangPage()
        {
            InitializeComponent();

            this.Loaded += NhapHangPage_Loaded;
        }

        private async void NhapHangPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await this.FadeIn(0.5f, 0.5f);
        }
    }
}
