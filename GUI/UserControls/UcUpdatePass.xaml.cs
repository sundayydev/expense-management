using GUI.View;
using System.Windows;
using System.Windows.Controls;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcUpdatePass.xaml
    /// </summary>
    public partial class UcUpdatePass : UserControl
    {
        public UcUpdatePass()
        {
            InitializeComponent();
            /*txtPassOld.Text = "123445566";
            txtPassNew.Text = "123445566";*/
        }


        private void btnUpdatePass_Click(object sender, RoutedEventArgs e)
        {
            /* string passold= txtPassOld.passbox.Password;
             string passnew= txtPassNew.Text.Trim();*/

            DialogCustoms res = new DialogCustoms("Bạn có muốn doi mat khau ko ?", "Thông báo", DialogCustoms.YesNo);
            res.ShowDialog();
            /*if (res.ShowDialog() == true)
            {
                if (string.IsNullOrWhiteSpace(passold) || string.IsNullOrWhiteSpace(passnew))
                {
                    DialogCustoms dia = new DialogCustoms("Oh no", "Thông báo", DialogCustoms.show);
                    dia.ShowDialog();
                }
            }*/
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login f = new Login();
            f.ShowDialog();

        }
    }
}
