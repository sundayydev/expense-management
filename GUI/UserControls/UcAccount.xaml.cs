using System.Windows;
using System.Windows.Controls;
using BLL;
using BLL.DTO.User;
using BLL.Services;
using GUI.View;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcAccount.xaml
    /// </summary>
    public partial class UcAccount : UserControl
    {
        private UserService userService = new UserService();
        public UcAccount()
        {
            InitializeComponent();
        }

        private void UcAccount_OnLoaded(object sender, RoutedEventArgs e)
        {
            //Load info user
            LoadData();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
        
        private void LoadData()
        {
            var userDto = userService.GetUserByUserId(AppContext.Instance.UserId);
            if (userDto != null)
            {
                TblFullName.Text = userDto.FullName;
                TblUserId.Text = AppContext.Instance.UserId;
            
                TxtFullName.Text = userDto.FullName;
                TxtEmail.Text = userDto.Email;

                if (userDto.Gender == null)
                {
                    RdbMale.IsChecked = false;
                    RdbFamale.IsChecked = false;
                }
                else
                {
                    RdbMale.IsChecked = userDto.Gender;
                    RdbFamale.IsChecked = !userDto.Gender;
                }
            }
            else
            {
                DialogCustoms dialog = new  DialogCustoms("Không tìm thấy thông tin người dùng!", "Thông báo", DialogCustoms.OK);
                dialog.ShowDialog();
            }
        }
    }
}
