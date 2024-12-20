using System;
using BLL.Services;
using DAL.Models;
using DAL.Repositories;
using System.Windows;
using System.Windows.Controls;
using BLL.DTO.User;
using GUI.View;
using MaterialDesignThemes.Wpf;
using AppContext = BLL.AppContext;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcLogin.xaml
    /// </summary>
    public partial class UcLogin : UserControl
    {
        private readonly UserService _userService = new UserService();
        public UcLogin()
        {
            InitializeComponent();
        }

        private void btnForgotPass_Click(object sender, RoutedEventArgs e)
        {
            UcForgotPass f = new UcForgotPass();
            this.Content = f;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool isLogin = false;
            
            try
            {
                if (IsValidation())
                {
                    LoginDto loginDto = new LoginDto
                    {
                        Email = TxtEmail.Text,
                        Password = TxtPassword.passbox.Password
                    };

                    isLogin = _userService.LoginUser(loginDto);
                }

                if (isLogin)
                {
                    DialogCustoms dialogCustoms =
                        new DialogCustoms("Đăng nhập thành công!", "Thông báo", DialogCustoms.OK);
                    dialogCustoms.ShowDialog();
                    MainWindow f = new MainWindow();
                    f.Show();
                    Window parentWindow = Window.GetWindow(this);
                    if (parentWindow != null) parentWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            UcSignUp f = new UcSignUp();
            this.Content = f;
        }

        bool IsValidation()
        {
            if (string.IsNullOrEmpty(TxtEmail.Text))
            {
                DialogCustoms dialogCustoms = 
                    new DialogCustoms("Email không được để trống", "Lỗi", DialogCustoms.OK);
                dialogCustoms.ShowDialog();
                return false;
            }

            if (string.IsNullOrEmpty(TxtPassword.passbox.Password))
            {
                DialogCustoms dialogCustoms = 
                    new DialogCustoms("Mật khẩu không được để trống", "Lỗi", DialogCustoms.OK);
                dialogCustoms.ShowDialog();
                return false;
            }

            return true;
        }
    }
}
