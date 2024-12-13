using GUI.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for ForgotPass.xaml
    /// </summary>
    public partial class UcForgotPass : System.Windows.Controls.UserControl
    {
        public UcForgotPass()
        {
            InitializeComponent();
            
        }

       

        private void btnForgotPass_Click(object sender, RoutedEventArgs e)
        {
          
            string email = txtEmail.Text;

            if (string.IsNullOrEmpty(email))
            {
                DialogCustoms dialog = new DialogCustoms("Lay cc mat khau cx ko nho ?", "Thông báo", DialogCustoms.show);
                dialog.Show();
            }
            /*if (string.IsNullOrEmpty(email))
            {
                MessageCus dialog = new MessageCus("Nội dung thông báo", "Tiêu đề");
                dialog.ShowDialog();

            }
            else
            {
                MessageCus dialog = new MessageCus("1 pass da gui toi email ban ?", "Thông báo");
            }*/
            /* Login f = new Login();
             f.ShowDialog();*/
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login f = new Login();
            f.ShowDialog();
            
        }

    }
}
