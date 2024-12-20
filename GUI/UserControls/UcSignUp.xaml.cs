using BLL.DTO.User;
using BLL.Services;
using DAL.Models;
using DAL.Repositories;
using GUI.View;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcSignUp.xaml
    /// </summary>
    public partial class UcSignUp : UserControl
    {
        private readonly UserService _userService;
        public UcSignUp()
        {
            InitializeComponent();

            // Khởi tạo UserService

            _userService = new UserService();
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var registerDTO = new RegisterDto
                {
                    FullName = TxtFullName.Text,
                    Email = TxtEmail.Text,
                    Password = TxtPassword.passbox.Password
                };

                // Gọi UserService để đăng ký
                _userService.RegisterUser(registerDTO);

                DialogCustoms dialog = new DialogCustoms("Đăng ký thành công!", "Thông báo", DialogCustoms.OK);
                dialog.ShowDialog();
                
                UcLogin uc = new UcLogin();
                this.Content = uc;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi:  {ex.Message}!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
