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

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcSignUp.xaml
    /// </summary>
    public partial class UcSignUp : UserControl
    {
        public UcSignUp()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            UcUpdatePass f = new UcUpdatePass();
            this.Content = f;
            

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            /*Login f = new Login();

            f.ShowDialog();*/
            UcForgotPass f = new UcForgotPass();
            this.Content = f;
        }

        private void TextBoxInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(c => char.IsLetter(c)))
            {
                e.Handled = true; 
            }
        }
    }
}
