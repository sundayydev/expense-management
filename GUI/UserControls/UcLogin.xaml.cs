using BLL.Services;
using DAL.Models;
using DAL.Repositories;
using System.Windows;
using System.Windows.Controls;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcLogin.xaml
    /// </summary>
    public partial class UcLogin : UserControl
    {
        public UcLogin()
        {
            InitializeComponent();

            // Khởi tạo UserService
        }
        
        private void btnForgotPass_Click(object sender, RoutedEventArgs e)
        {
            UcForgotPass f = new UcForgotPass();
            this.Content = f;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MainWindow f = new MainWindow();
            f.ShowDialog();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            UcSignUp f = new UcSignUp();
            this.Content = f;
        }
    }
}
