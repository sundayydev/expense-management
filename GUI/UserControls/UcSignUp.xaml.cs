using BLL.DTO.User;
using BLL.Services;
using DAL.Models;
using DAL.Repositories;
using GUI.View;
using System;
using System.Linq;
using System.Text.RegularExpressions;
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
            _userService = new UserService();
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!IsValidInput())
                {
                    return; 
                }

                if (!cbAgree.IsChecked.GetValueOrDefault())
                {
                    DialogCustoms res = new DialogCustoms("Bạn phải đồng ý với các điều khoản để tiếp tục.", "Thông báo", DialogCustoms.OK);
                    res.ShowDialog();
                    return;
                }
                var registerDTO = new RegisterDTO
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
        private void TextBoxEmailInput(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;  
            }
        }
        private bool IsValidInput()
        {
            var password = TxtPassword.passbox.Password;
            var email = TxtEmail.Text;


            if (string.IsNullOrWhiteSpace(password)|| string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(TxtFullName.Text))
            {
                DialogCustoms dialog = new DialogCustoms("Vui lòng nhập đầy đủ thông tin", "Thông báo", DialogCustoms.OK);
                dialog.ShowDialog();
                return false;
            }

            if (password.Length < 10)
            {
                DialogCustoms dialog = new DialogCustoms("Mật khẩu phải có ít nhất 10 ký tự.", "Thông báo", DialogCustoms.OK);
                dialog.ShowDialog();
                return false;
            }
            var emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
            if (!emailRegex.IsMatch(email))
            {
                DialogCustoms dialog = new DialogCustoms("Email không hợp lệ. Vui lòng kiểm tra lại email.!", "Thông báo", DialogCustoms.OK);
                dialog.ShowDialog();
                return false;
            }

            return true; 
        }
    }
}
