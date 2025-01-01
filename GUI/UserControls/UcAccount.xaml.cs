using System.Windows;
using System.Windows.Controls;
using BLL;
using BLL.DTO.User;
using BLL.Services;
using DAL.Utils;
using GUI.View;
using System.Windows.Media;
using System.Linq;
using System;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcAccount.xaml
    /// </summary>
    public partial class UcAccount : UserControl
    {
        private readonly UserService _userService = new UserService();
        private readonly ExpenseService _expenseService = new ExpenseService();
        private readonly IncomeService _incomeService = new IncomeService();
        private readonly string _userId = BLL.AppContext.Instance.UserId;

        public UcAccount()
        {
            InitializeComponent();
        }

        private void UcAccount_OnLoaded(object sender, RoutedEventArgs e)
        {
            FaAvatar.PrimaryColor = new RandomColorGenerator().GetRandomColor();
            //Load info user
            LoadData();
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = TxtEmail.Text;
                string oldPassword = PbPassWordOld.Password;
                string newPassword = PbPasswordNew.Password;
                string confirmPassword = PbPasswordNewAgain.Password;

                if (newPassword != confirmPassword)
                {
                    DialogCustoms dialog2 = new DialogCustoms("Mật khẩu mới và xác nhận không khớp.", "Thông báo", DialogCustoms.OK);
                    dialog2.Show();
                    return;
                }
                var userService = new UserService();
                userService.UpdatePassword(email, oldPassword, newPassword);

                DialogCustoms dialog = new DialogCustoms("Cập nhật mật khẩu thành công.", "Thông báo", DialogCustoms.Show);
                dialog.Show();
            }
            catch (Exception ex)
            {
                DialogCustoms dialog = new DialogCustoms($"Lỗi: {ex.Message}", "Thông báo", DialogCustoms.Show);
                dialog.Show();
            }
        }

        private void LoadData()
        {
            var userDto = _userService.GetUserByUserId(_userId);
            if (userDto != null)
            {
                TblFullName.Text = userDto.FullName;
                TblUserId.Text = _userId;

                TblSumExpense.Text = _expenseService.GetQuantityExpenses(_userId).ToString("N0");
                TblSumIncome.Text = _incomeService.GetQuantityIncomes(_userId).ToString("N0");

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
                DialogCustoms dialog = new DialogCustoms("Không tìm thấy thông tin người dùng!", "Thông báo", DialogCustoms.OK);
                dialog.ShowDialog();
            }
        }
    }
}
