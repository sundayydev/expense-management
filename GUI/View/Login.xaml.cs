using GUI.UserControls;
using System.Windows;

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
        MainWindow mainWindow { get => Application.Current.MainWindow as MainWindow; }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow f = new MainWindow();
            f.ShowDialog();
            this.Show();
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
