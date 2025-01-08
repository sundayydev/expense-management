using System.ComponentModel;
using GUI.UserControls;
using GUI.View;
using System.Windows;
using System.Windows.Media;
using Control = System.Windows.Controls.Control;
using System.Collections.Generic;
using System.Windows.Controls;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private bool _isMenuCollapsed = false;

        private Queue<UserControl> navigationQueue = new Queue<UserControl>();

        private UcHome _ucHome;
        private UcAccount _ucAccount;
        private UcRemind _ucRemind;
        private UcCategory _ucCategory;
        private UcRecipient _ucRecipient;
        private UcIncome _ucIncome;
        private UcSpend _ucSpend;
        private UcStatistics _ucStatistics;

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
                    NavigateToNewScreen(new UcHome());
                    break;
                case "BtnCategory":
                    NavigateToNewScreen(new UcCategory());
                    break;
                case "BtnExpense":
                    NavigateToNewScreen(new UcSpend());
                    break;
                case "BtnIncome":
                    NavigateToNewScreen(new UcIncome());
                    break;
                case "BtnRecipient":
                    NavigateToNewScreen(new UcRecipient());
                    break;
                case "BtnStatistical":
                    NavigateToNewScreen(new UcStatistics());
                    break;
                case "BtnRemind":
                    NavigateToNewScreen(new UcRemind());
                    break;
            }

        }
        private void NavigateToNewScreen(UserControl newControl)
        {
            // Lưu UserControl hiện tại vào Queue trước khi điều hướng đến màn hình mới
            if (ContentDisplayMain.Content != null)
            {
                navigationQueue.Enqueue(ContentDisplayMain.Content as UserControl);
            }

            DisplayUserControl(newControl);
        }

        // Phương thức để hiển thị UserControl
        private void DisplayUserControl(UserControl control)
        {
            ContentDisplayMain.Content = control;
        }

        // Phương thức xử lý quay lại
        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            if (navigationQueue.Count > 0)
            {
                ResetBackgroundButtonIcon();
                var previousControl = navigationQueue.Dequeue();
                DisplayUserControl(previousControl);
                if (previousControl is UcHome)
                {
                    BtnHome.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (previousControl is UcCategory)
                {
                    BtnCategory.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (previousControl is UcSpend)
                {
                    BtnExpense.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (previousControl is UcIncome)
                {
                    BtnIncome.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (previousControl is UcStatistics)
                {
                    BtnStatistical.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (previousControl is UcRecipient)
                {
                    BtnRecipient.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (previousControl is UcRemind)
                {
                    BtnRemind.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
            }
            else
            {
                MessageBox.Show("Không có màn hình trước đó để quay lại.");
            }
        }
        private void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            if (navigationQueue.Count > 0)
            {
                ResetBackgroundButtonIcon();
                var nextControl = navigationQueue.Peek(); 
                DisplayUserControl(nextControl);

                if (nextControl is UcHome)
                {
                    BtnHome.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (nextControl is UcCategory)
                {
                    BtnCategory.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (nextControl is UcSpend)
                {
                    BtnExpense.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (nextControl is UcIncome)
                {
                    BtnIncome.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (nextControl is UcStatistics)
                {
                    BtnStatistical.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (nextControl is UcRecipient)
                {
                    BtnRecipient.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
                else if (nextControl is UcRemind)
                {
                    BtnRemind.Background = (SolidColorBrush)Application.Current.Resources["BackgroundSecondaryLight"];
                }
            }
            else
            {
                MessageBox.Show("Không có màn hình tiếp theo để chuyển.");
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