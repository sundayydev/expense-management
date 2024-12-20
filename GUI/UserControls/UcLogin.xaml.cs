using BLL.DTO.User;
using BLL.Services;
using DAL.Models;
using DAL.Repositories;
using GUI.View;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcLogin.xaml
    /// </summary>
    public partial class UcLogin : UserControl
    {
        private readonly UserService _userService;
        public UcLogin()
        {
            InitializeComponent();

            _userService = new UserService();
        }
        
        private void btnForgotPass_Click(object sender, RoutedEventArgs e)
        {
            UcForgotPass f = new UcForgotPass();
            this.Content = f;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var loginDTO = new LoginDTO
                {
                    Email = txtEmail.Text,
                    Password = txtPassword.passbox.Password,
                };

                // Gọi service để đăng nhập
                var user = _userService.LoginUser(loginDTO);

                if (user == null)
                {
                    DialogCustoms res = new DialogCustoms("Đăng nhập thất bại! Email hoặc mật khẩu không đúng.", "Thông báo", DialogCustoms.OK);
                    res.ShowDialog();
                }
                else
                {
                    DialogCustoms res = new DialogCustoms($"Đăng nhập thành công! Chào {user.FullName}", "Thông báo", DialogCustoms.OK);
                    res.ShowDialog();

                    MainWindow f = new MainWindow();
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } 

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            UcSignUp f = new UcSignUp();
            this.Content = f;
        }
    }
}
