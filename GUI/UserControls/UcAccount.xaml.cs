using System.Windows;
using System.Windows.Controls;
using BLL;
using BLL.DTO.User;
using BLL.Services;
using DAL.Utils;
using GUI.View;
using System.Windows.Media;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcAccount.xaml
    /// </summary>
    public partial class UcAccount : UserControl
    {
        private Brush ColorAvatar { get; set; }
        private readonly UserService _userService = new UserService();
        public UcAccount()
        {
            InitializeComponent();
        }

        private void UcAccount_OnLoaded(object sender, RoutedEventArgs e)
        {
            ColorAvatar = new RandomColorGenerator().GetRandomColor();
            //Load info user
            LoadData();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
        
        private void LoadData()
        {
            var userDto = _userService.GetUserByUserId(AppContext.Instance.UserId);
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
