using System;
using BLL.Services;
using GUI.View;
using System.Windows;
using System.Windows.Controls;
using BLL.DTO.User;
using DAL.Utils;

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
            LoadSavedCredentials();
            
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
                    string email = TxtEmail.Text;
                    string password = TxtPassword.passbox.Password;
                    
                    LoginDto loginDto = new LoginDto
                    {
                        Email = email,
                        Password = password
                    };

                    isLogin = _userService.LoginUser(loginDto);
                    
                    if (ChkRememberMe.IsChecked == true)
                    {
                        SecureStorage.SaveCredentials(email, password);
                    }
                    else
                    {
                        SecureStorage.SaveCredentials("", ""); // Xóa thông tin nếu bỏ chọn
                    }
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
            if (string.IsNullOrEmpty(TxtEmail.Text) )
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
        
        private void LoadSavedCredentials()
        {
            var (email, password) = SecureStorage.LoadCredentials();
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                TxtEmail.Text = email;
                TxtPassword.passbox.Password = password;
                ChkRememberMe.IsChecked = true;
            }
        }
        private void ChkRememberMe_Checked(object sender, RoutedEventArgs e)
        {
  
            
        }

        // Event handler for Unchecked event of ChkRememberMe
        private void ChkRememberMe_Unchecked(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
