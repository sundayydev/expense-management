using System.ComponentModel;
using GUI.UserControls;
using GUI.View;
using System.Windows;
using System.Windows.Media;
using Control = System.Windows.Controls.Control;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _isMenuCollapsed = false;

        private UcHome _ucHome;
        private UcAccount _ucAccount;
        private UcRemind _ucRemind;
        private UcCategory _ucCategory;
        private UcRecipient _ucRecipient;
        private UcIncome _ucIncome;
        private UcSpend _ucSpend;

        public MainWindow()
        {
            InitializeComponent();

            var bgSecondaryLight = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
            ResetBackgroundButtonIcon();
            BtnHome.Background = bgSecondaryLight;
            _ucHome = new UcHome();
            ContentDisplayMain.Content = _ucHome;
        }

        private void ToggleMenu_Click(object sender, RoutedEventArgs e)
        {
            // Đổi trạng thái menu
            _isMenuCollapsed = !_isMenuCollapsed;

            if (_isMenuCollapsed)
            {
                MenuColumn.Width = new GridLength(50); // Chỉ hiện icon
            }
            else
            {
                MenuColumn.Width = new GridLength(260); // Hiện cả icon và chữ
            }
        }

        private void Btn_OnClick(object sender, RoutedEventArgs e)
        {
            var bgSecondaryLight = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
            ResetBackgroundButtonIcon();
            Control btn = sender as Control;
            if (btn != null) btn.Background = bgSecondaryLight;

            switch (btn.Name)
            {
                case "BtnHome":
                    _ucHome = new UcHome();
                    ContentDisplayMain.Content = _ucHome;
                    break;
                case "BtnCategory":
                    _ucCategory = new UcCategory();
                    ContentDisplayMain.Content = _ucCategory;
                    break;
                case "BtnExpense":
                    _ucSpend = new UcSpend();
                    ContentDisplayMain.Content = _ucSpend;
                    break;
                case "BtnIncome":
                    _ucIncome = new UcIncome();
                    ContentDisplayMain.Content = _ucIncome;
                    break;
                case "BtnRecipient":
                    _ucRecipient = new UcRecipient();
                    ContentDisplayMain.Content = _ucRecipient;
                    break;
                case "BtnLoans":
                    break;
                case "BtnStatistical":
                    break;
                case "BtnRemind":
                    _ucRemind = new UcRemind();
                    ContentDisplayMain.Content = _ucRemind;
                    break;
            }

        }

        void ResetBackgroundButtonIcon()
        {
            var bgTransparent = (SolidColorBrush)Application.Current.Resources["BackgroundTransparent"];
            BtnHome.Background = bgTransparent;
            BtnCategory.Background = bgTransparent;
            BtnExpense.Background = bgTransparent;
            BtnIncome.Background = bgTransparent;
            BtnStatistical.Background = bgTransparent;
            BtnRecipient.Background = bgTransparent;
            BtnLoans.Background = bgTransparent;
            BtnRemind.Background = bgTransparent;
        }

        private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
        {
            DialogCustoms dialog = new DialogCustoms("Bạn có muốn đăng xuất ?", "Thông báo", DialogCustoms.YesNo);
            if (dialog.ShowDialog() == true)
            {
                Login login = new Login();
                login.Show();
                this.Close();
            }
        }

        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
            _ucAccount = new UcAccount();
            ContentDisplayMain.Content = _ucAccount;
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
    }
}