using GUI.View;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows;

namespace GUI.UserControls
{
    public partial class UcForgotPass : System.Windows.Controls.UserControl
    {
        public UcForgotPass()
        {
            InitializeComponent();
        }

        string emailtest = "duyhoanggl98@gmail.com";

        private void btnForgotPass_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;

            if (string.IsNullOrEmpty(email))
            {
                DialogCustoms dialog = new DialogCustoms("Lấy cc mật khẩu cũng không nhớ ? m thích để trống email ko ?", "Thông báo", DialogCustoms.Show);
                dialog.Show();
                return;
            }

            if (!IsValidEmail(email))
            {
                DialogCustoms dialog = new DialogCustoms("Địa chỉ email không hợp lệ.", "Thông báo", DialogCustoms.Show);
                dialog.Show();
                return;
            }

            string newPassword = GenerateRandomPassword();
            txtPass.Text = newPassword;
            try
            {
                SendPasswordResetEmail(email, newPassword);
                DialogCustoms dialog = new DialogCustoms("Một email đã được gửi đến bạn để khôi phục mật khẩu.", "Thông báo", DialogCustoms.Show);
                dialog.Show();
            }
            catch (Exception ex)
            {
                DialogCustoms dialog = new DialogCustoms("Đã xảy ra lỗi khi gửi email: " + ex.Message, "Lỗi", DialogCustoms.Show);
                dialog.Show();
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private string GenerateRandomPassword()
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < 8; i++)
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }

            return password.ToString();
        }

        private void SendPasswordResetEmail(string recipientEmail, string newPassword)
        {
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;
            string senderEmail = "duyhoanggl98@gmail.com";
            string senderPassword = "ekik wvlq bxnf rnqe";

            using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
            {
                smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtp.EnableSsl = true;

                MailMessage mail = new MailMessage(senderEmail, recipientEmail)
                {
                    Subject = "Hướng dẫn khôi phục mật khẩu",
                    IsBodyHtml = true,
                };
                mail.Body = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: Arial, sans-serif; color: #333333; margin: 0; padding: 5; }}
                        .container {{
                            width: 90%; 
                            padding: 20px; 
                            border: 2px solid #4CAF50; 
                            border-radius: 10px;
                            background-color: #f4f4f4;
                            text-align: center;
                            
                        }}
                        .content {{
                            font-size: 16px; 
                            color: #0000FF;
                            margin-top: 20px;
                        }}
                        .new-password {{
                            font-size: 18px; 
                            font-weight: bold; 
                            color: #FF5733;
                        }}
                        .footer {{
                            font-size: 14px; 
                            color: #888888;
                            margin-top: 30px;
                            text-align: center;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='content'>
                            <p>Chúng tôi đã tạo một mật khẩu mới cho bạn.</p>
                            <p>Mật khẩu mới của bạn là: <span class='new-password'>{newPassword}</span></p>
                        </div>
                        <div class='footer'>
                            <p>Nếu bạn không yêu cầu khôi phục mật khẩu, vui lòng bỏ qua email này.</p>
                        </div>
                    </div>
                </body>
                </html>
                ";


                smtp.Send(mail);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            UcLogin f = new UcLogin();
            this.Content = f;
        }
    }
}
