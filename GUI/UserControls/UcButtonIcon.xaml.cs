using System.Windows;
using System.Windows.Controls;

namespace GUI.UserControls
{
    public partial class UcButtonIcon : UserControl
    {
        public UcButtonIcon()
        {
            InitializeComponent();
        }

        // DependencyProperty for Icon
        public static readonly DependencyProperty IconProperty =
           DependencyProperty.Register("Icon", typeof(string), typeof(UcButtonIcon), new PropertyMetadata(null));

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        // DependencyProperty for Text
        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register("Text", typeof(string), typeof(UcButtonIcon), new PropertyMetadata(null));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        // Khai báo sự kiện
        public event RoutedEventHandler UcButtonIconClick;

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            UcButtonIconClick?.Invoke(this, e);
        }
    }
}