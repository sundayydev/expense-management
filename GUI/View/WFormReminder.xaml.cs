using BLL.DTO.Category;
using BLL.DTO.Reminder;
using BLL.Services;
using DAL.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using AppContext = BLL.AppContext;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for WFormReminder.xaml
    /// </summary>
    public partial class WFormReminder : Window
    {
        private readonly string _userId = AppContext.Instance.UserId;
        private readonly string _reminderId;
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly ReminderService _reminderService = new ReminderService();
        
        public WFormReminder()
        {
            InitializeComponent();
        }
        
        public WFormReminder(string reminderId)
        {
            InitializeComponent();
            this._reminderId = reminderId;
        }
        
        private void WFormReminder_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadCmbCategories();

            if (_reminderId != null)
            {
                LoadData();
            }
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogCustoms diaglog = new DialogCustoms("Bạn có chắc chắn muốn hủy không?", "Xác nhận", DialogCustoms.YesNo);
            diaglog.ShowDialog();
            if (diaglog.DialogResult == true)
            {
                ResetFormReminder();
                this.Close();
            }
        }
        
        private void ChkRecurrence_OnClick(object sender, RoutedEventArgs e)
        {
            if (ChkRecurrence.IsChecked == true)
            {
                RdbMonthly.IsEnabled = true;
                RdbWeekly.IsEnabled = true;
            }
            else
            {
                RdbMonthly.IsEnabled = false;
                RdbWeekly.IsEnabled = false;
            }
        }
        
        void LoadCmbCategories()
        {
            var categories = _categoryService.GetCategoryByUserId(_userId);
            CmbCategory.ItemsSource = categories.ToList();
            CmbCategory.DisplayMemberPath = "CategoryName";
            CmbCategory.SelectedValuePath = "CategoryId";
        }

        void ResetFormReminder()
        {
            TxtReminderId.Text = "";
            TxtTitle.Text = "";
            RtbDescription.Document.Blocks.Clear();
            ChkRecurrence.IsChecked = false;
            DtpReminderDate.SelectedDate = DateTime.Now;
            DtpReminderDate.DisplayDate = DateTime.Now;
            CmbCategory.SelectedIndex = -1;
        }
        
        void LoadData()
        {
            ReminderDto reminder = _reminderService.GetReminderByReminderId(_userId, _reminderId);
            TxtReminderId.Text = reminder.ReminderId;
            TxtTitle.Text = reminder.Title;
            DtpReminderDate.SelectedDate = reminder.ReminderDate;
            DtpReminderDate.DisplayDate = reminder.ReminderDate;
            CmbCategory.SelectedValue = reminder.CategoryId;
            RtbDescription.Document.Blocks.Clear();
            RtbDescription.Document.Blocks.Add(new Paragraph(new Run(reminder.Description)));
            ChkRecurrence.IsChecked = reminder.IsRecurring;
            if (reminder.IsRecurring == true)
            {
                RdbMonthly.IsEnabled = true;
                RdbWeekly.IsEnabled = true;
               _ = reminder.RecurrenceType == "Hàng Tuần" ? RdbWeekly.IsChecked = true : RdbMonthly.IsChecked = true;
            }
            
            ChkCompleted.IsChecked = reminder.IsCompleted;
        }


        private void BtnSaveReminder_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtReminderId.Text))
                {
                    
                    var updateReminder = new Reminder()
                    {
                        ReminderId = TxtReminderId.Text,
                        Title = TxtTitle.Text,
                        CategoryId = (CmbCategory.SelectedItem as CategoryDto).CategoryId,
                        ReminderDate = DtpReminderDate.SelectedDate.Value,
                        IsRecurring = ChkRecurrence.IsChecked,
                        RecurrenceType = RdbWeekly.IsChecked == true ? "Hàng Tuần" : "Hàng Tháng",
                        IsCompleted = ChkCompleted.IsChecked,
                        Description = new TextRange(RtbDescription.Document.ContentStart, RtbDescription.Document.ContentEnd).Text.Trim()
                    };
                    _reminderService.UpdateReminder(updateReminder);
                    DialogCustoms dialogCustomsUpdate = new DialogCustoms("Cập nhật nhắc nhở thành công.", "Thông báo", DialogCustoms.OK);
                    dialogCustomsUpdate.ShowDialog();
                }
                else
                {
                    var addReminderDto = new AddReminderDto()
                    {
                        UserId = AppContext.Instance.UserId,
                        Title = TxtTitle.Text,
                        CategoryId = (CmbCategory.SelectedItem as CategoryDto).CategoryId,
                        ReminderDate = DtpReminderDate.SelectedDate.Value,
                        IsRecurring = ChkRecurrence.IsChecked,
                        RecurrenceType = RdbWeekly.IsChecked == true ? "Hàng Tuần" : "Hàng Tháng",
                        IsCompleted = ChkCompleted.IsChecked,
                        Description = new TextRange(RtbDescription.Document.ContentStart, RtbDescription.Document.ContentEnd).Text.Trim()
                    };
                
                    _reminderService.AddReminder(addReminderDto);
                    DialogCustoms dialogCustoms = new DialogCustoms("Thêm nhắc nhở thành công.", "Thông báo", DialogCustoms.OK);
                    dialogCustoms.ShowDialog();
                }
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Lỗi: " + exception.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

