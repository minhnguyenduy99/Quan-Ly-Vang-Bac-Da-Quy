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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projects.CustomControls
{
    /// <summary>
    /// Interaction logic for InfoCard.xaml
    /// </summary>
    [ContentProperty("Text")]
    public partial class InfoCard : UserControl
    {
        /// <summary>
        /// The source of icon represents this info card
        /// </summary>
        public ImageSource IconSource
        {
            get => (BitmapImage)GetValue(IconSourceProperty);
            set
            {
                SetValue(IconSourceProperty, value);
            }
        }

        /// <summary>
        /// The title of card
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set
            {
                SetValue(TitleProperty, value);

            }
        }

        /// <summary>
        /// The text represents the information of card
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);
            }
        }

        /// <summary>
        /// The foreground of the title
        /// </summary>
        public Brush TitleForeground
        {
            get => (Brush)GetValue(TitleForegroundProperty);
            set => SetValue(TitleForegroundProperty, value);
        }

        /// <summary>
        /// The foreground of the information 
        /// </summary>
        public Brush TextForeground
        {
            get => (Brush)GetValue(TextForegroundProperty);
            set => SetValue(TextForegroundProperty, value);
        }

        public InfoCard()
        {
            InitializeComponent();
        }


        #region Dependency Property Declaration
        public static readonly DependencyProperty IconSourceProperty =
            DependencyProperty.Register(
                "IconSource",
                typeof(ImageSource),
                typeof(InfoCard));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(InfoCard));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(string),
                typeof(InfoCard));

        public static readonly DependencyProperty TextForegroundProperty =
            DependencyProperty.Register(
                "TextForeground",
                typeof(Brush),
                typeof(InfoCard),
                new PropertyMetadata(Brushes.Black));

        public static readonly DependencyProperty TitleForegroundProperty =
            DependencyProperty.Register(
                "TitleForeground",
                typeof(Brush),
                typeof(InfoCard),
                new PropertyMetadata(Brushes.Black));
        #endregion
    }
}
