using FontAwesome.WPF;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for TextPlaceHolder.xaml
    /// </summary>
    public partial class TextPlaceHolder : UserControl
    {
        public TextPlaceHolder()
        {
            InitializeComponent();
           
        }
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }
        public static readonly DependencyProperty PlaceHolderProperty =DependencyProperty.Register(
        "PlaceHolder",
        typeof(string),
        typeof(TextPlaceHolder)
      );

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        "Text",
        typeof(string),
        typeof(TextPlaceHolder)
     );
        public bool Pass
        {
            get { return (bool)GetValue(PassProperty); }
            set { SetValue(PassProperty, value); }
        }
        public static readonly DependencyProperty PassProperty = DependencyProperty.Register(
        "Text",
        typeof(string),
        typeof(TextPlaceHolder)
     );

        private void passbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            email.Text = passbox.Password;
        }
    }
}
