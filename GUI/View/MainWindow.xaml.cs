﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GUI.UserControls;
using GUI.View;
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

        public MainWindow()
        {
         InitializeComponent();
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
                    break;
                case "BtnExpense":
                    break;
                case "BtnIncome":
                    break;  
                case "BtnStatistical":
                    break;
                case "BtnRemind":
                    break;                
                case "BtnNotification":
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
            BtnRemind.Background = bgTransparent;
            BtnNotification.Background = bgTransparent;
        }

        private void BtnLogout_OnClick(object sender, RoutedEventArgs e)
        {
            DialogCustoms dialog = new DialogCustoms("Bạn có muốn đăng xuất ?", "Thông báo", DialogCustoms.YesNo);
            if(dialog.ShowDialog() == true)
            {
                this.Close();
            }
        }

        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
            _ucAccount = new UcAccount();
            ContentDisplayMain.Content = _ucAccount;
        }
    }
}