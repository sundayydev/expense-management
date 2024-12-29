using BLL.DTO.Reminder;
using BLL.Services;
using GUI.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AppContext = BLL.AppContext;

namespace GUI.UserControls
{
    public partial class UcRemind : UserControl
    {
        private ObservableCollection<ReminderDto> Reminders { get; set; }
        private readonly ReminderService _reminderService = new ReminderService();
        
        public UcRemind()
        {
            InitializeComponent();
            LoadData();
        }

        private void BtnAddRemind_Click(object sender, RoutedEventArgs e)
        {
            WFormReminder wf = new WFormReminder();
            wf.ShowDialog();
            LoadData();
        }
        
        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DataGridRow row = DataGridRow.GetRowContainingElement(button);

            if (row != null)
            {
                var reminderId = (row.Item as ReminderDto)?.ReminderId;
        
                WFormReminder wf = new WFormReminder(reminderId);
                wf.ShowDialog();
                
                ReminderDto updatedReminder = _reminderService.GetReminderByReminderId(AppContext.Instance.UserId, reminderId);
                var existingReminder = Reminders.FirstOrDefault(r => r.ReminderId == reminderId);
                if (existingReminder != null)
                {
                    int index = Reminders.IndexOf(existingReminder);
                    Reminders[index] = updatedReminder;
                }
                LoadData();
                
            }
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            // Lấy Button được nhấn
            Button button = sender as Button;

            // Lấy DataGridRow
            DataGridRow row = DataGridRow.GetRowContainingElement(button);

            // Lấy dữ liệu dòng
            if (row != null)
            {
                var reminderId = (row.Item as ReminderDto)?.ReminderId;
                
                try
                {
                    DialogCustoms dialog = new DialogCustoms("Bạn có muốn xóa không?", "Thông báo", DialogCustoms.YesNo);

                    if (dialog.ShowDialog() == true)
                    {
                        _reminderService.DeleteReminder(AppContext.Instance.UserId, reminderId);
                        LoadData();
                        DialogCustoms dialogSuccess = new DialogCustoms("Xóa thành công.", "Thông báo", DialogCustoms.OK);
                        dialogSuccess.ShowDialog();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        void LoadData()
        {
            ReminderDataGrid.ItemsSource = null;
            Reminders = new ObservableCollection<ReminderDto>(_reminderService.GetRemindersByUserId(AppContext.Instance.UserId));
            ReminderDataGrid.ItemsSource = Reminders;
        }
    }
}