using GUI.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static System.Windows.Forms.LinkLabel;

namespace GUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

           
        }
        MainWindow  mainWindow { get => Application.Current.MainWindow as MainWindow; }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow f = new MainWindow();
            this.Hide();
            f.ShowDialog();
            this.Close();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));

            e.Handled = true;

        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            UcSignUp f = new UcSignUp();
            this.Content = f;

        }

        private void btnForgotPass_Click(object sender, RoutedEventArgs e)
        {
            UcForgotPass f = new UcForgotPass();
            this.Content = f;

        }
    }
}
