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

namespace UIProject.CustomControls
{
    /// <summary>
    /// Interaction logic for InfoCardLabel.xaml
    /// </summary>
    public partial class InfoCardLabel : UserControl
    {
        public InfoCardLabel()
        {
            InitializeComponent();
        }



        public ImageSource IconInfoSource
        {
            get => (BitmapImage)GetValue(IconInfoSourceProperty);
            set => SetValue(IconInfoSourceProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string ContentInfo
        {
            get => (string)GetValue(ContentInfoProperty);
            set => SetValue(ContentInfoProperty, value);
        }

        public Brush TitleForeground
        {
            get => (Brush)GetValue(TitleForegroundProperty);
            set => SetValue(TitleForegroundProperty, value);
        }

        public Brush ContentInfoForeground
        {
            get => (Brush)GetValue(ContentInfoForegroundProperty);
            set => SetValue(ContentInfoForegroundProperty, value);
        }

        public Brush FocusBackground
        {
            get => (Brush)GetValue(FocusBackgroundProperty);
            set => SetValue(FocusBackgroundProperty, value);
        }

        
        public static readonly DependencyProperty IconInfoSourceProperty =
            DependencyProperty.Register(
                "IconInfoSource", 
                typeof(ImageSource), 
                typeof(InfoCardLabel));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(InfoCardLabel),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ContentInfoProperty =
            DependencyProperty.Register(
                "ContentInfo",
                typeof(string),
                typeof(InfoCardLabel),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register(
                "TitleForeground",
                typeof(Brush),
                typeof(InfoCardLabel),
                new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty ContentInfoForegroundProperty =
            DependencyProperty.Register(
                "ContentInfoForeground",
                typeof(Brush),
                typeof(InfoCardLabel),
                new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty FocusBackgroundProperty =
            DependencyProperty.Register(
                "FocusBackground",
                typeof(Brush),
                typeof(InfoCardLabel),
                new PropertyMetadata(Brushes.White));



    }
}
