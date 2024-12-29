using GUI.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using BLL.Services;
using AppContext = BLL.AppContext;
using BLL.DTO.Category;
using BLL.DTO.Expenses;
using DAL.Models;
using System.Windows.Controls;
using BLL.DTO.Recipient;
using System.Globalization;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for WFormExpense.xaml
    /// </summary>
    public partial class WFormExpense : Window
    {
        private readonly CategoryService _categoryService = new();
        private readonly ExpenseService _expenseService = new();
        private readonly RecipientService _recipientService = new();
        private readonly Expens _expense;
        private readonly bool _isEditMode;
        private readonly string _expenseId;
        public WFormExpense()
        {
            InitializeComponent();
          
        }
        public WFormExpense(Expens expense = null)
        {
            InitializeComponent();
            _expense = expense;
            _isEditMode = expense != null;
            
            LoadExpenseData();
        }
        private void LoadExpenseData()
        {
            if (_expense != null)
            {
                txtExpenseId.Text = _expense.ExpenseId.ToString();
                CmbCategory.SelectedValuePath = _expense.CategoryId;
                CmbRecipient.SelectedValuePath = _expense.RecipientId;
                txtAmount.Text = _expense.Amount.ToString();
                DtpExpenseDate.DisplayDate = _expense.ExpenseDate;
                DtpExpenseDate.SelectedDate = _expense.ExpenseDate;
                rtbNote.Document.Blocks.Clear();
                rtbNote.Document.Blocks.Add(new Paragraph(new Run(_expense.Note)));
            }
        }
        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isEditMode)
                {
                    _expense.RecipientId = CmbRecipient.SelectedValue?.ToString();
                    _expense.CategoryId = (CmbCategory.SelectedItem as CmbCategoryDto).CategoryId;
                    _expense.Amount = decimal.Parse(txtAmount.Text);
                    _expense.ExpenseDate = DtpExpenseDate.SelectedDate ?? _expense.ExpenseDate;
                    _expense.Note = new TextRange(rtbNote.Document.ContentStart, rtbNote.Document.ContentEnd).Text.Trim();
                    _expense.CreatedAt = DateTime.Now;

                    var updateExpense = new Expens()
                    {
                        ExpenseId = _expense.ExpenseId,
                        UserId = _expense.UserId,
                        CategoryId = _expense.CategoryId,
                        RecipientId = _expense.RecipientId,
                        Amount = _expense.Amount,
                        ExpenseDate = _expense.ExpenseDate,
                        Note = _expense.Note
                    };
                    _expenseService.UpdateExpense(updateExpense);
                    DialogCustoms dialogCustomsUpdate = new DialogCustoms("Cập nhật danh mục thành công.", "Thông báo", DialogCustoms.OK);
                    dialogCustomsUpdate.ShowDialog();
                }
                else
                {
                    var addExpense = new ExpenseDto
                    {
                        UserId = BLL.AppContext.Instance.UserId,
                        CategoryId = (CmbCategory.SelectedItem as CmbCategoryDto).CategoryId,
                        RecipientId = CmbRecipient.SelectedValue?.ToString(),
                        Amount = decimal.Parse(txtAmount.Text),
                        ExpenseDate = DtpExpenseDate.SelectedDate ?? DateTime.Now,
                        Note = new TextRange(rtbNote.Document.ContentStart, rtbNote.Document.ContentEnd).Text.Trim()
                    };

                    _expenseService.AddExpense(addExpense);
                    DialogCustoms dialogCustomsUpdate = new DialogCustoms("Thêm chi tiêu thành công!", "Thông báo", DialogCustoms.OK);
                    dialogCustomsUpdate.ShowDialog();
                }

                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Lỗi: {exception.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogCustoms cancelDialog = new DialogCustoms("Hủy thao tác", "Thông báo", DialogCustoms.Show);
            cancelDialog.ShowDialog();
            this.Close();
        }

        void LoadCmbCategories()
        {
            var categories = _categoryService.GetCategoriesByCategoryType(AppContext.Instance.UserId, "Chi tiêu");
            var filteredCategories = categories.Where(c => c.CategoryType == "Chi tiêu").ToList();
            CmbCategory.ItemsSource = filteredCategories;
            CmbCategory.DisplayMemberPath = "CategoryName";
            CmbCategory.SelectedValuePath = "CategoryId";
        }
        void LoadCmbRecipient()
        {
            var recipients =_recipientService.GetRecipientsByUserId(AppContext.Instance.UserId);
            CmbRecipient.ItemsSource = recipients;
            CmbRecipient.DisplayMemberPath = "RecipientName";
            CmbRecipient.SelectedValuePath = "RecipientId";
        }
        private void WFormExpense_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadCmbCategories();
            LoadCmbRecipient();
            if (_isEditMode && _expense != null)
            {
                CmbRecipient.SelectedValue = _expense.RecipientId;
                CmbCategory.SelectedValue = _expense.CategoryId;
                txtAmount.Text = ((int)_expense.Amount).ToString();
                DtpExpenseDate.SelectedDate = _expense.ExpenseDate;
                rtbNote.Document.Blocks.Clear();
                rtbNote.Document.Blocks.Add(new Paragraph(new Run(_expense.Note)));

            }
        }

        private void txtAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Lấy giá trị hiện tại của TextBox
                string text = textBox.Text;

                // Loại bỏ dấu chấm, dấu phẩy hoặc ký tự không phải số
                string numericText = new string(text.Where(char.IsDigit).ToArray());

                if (!string.IsNullOrEmpty(numericText))
                {
                    // Chuyển đổi sang định dạng số có phân tách bằng dấu phẩy
                    string formattedText = $"{long.Parse(numericText):N0}";

                    // Cập nhật TextBox mà không tạo vòng lặp sự kiện
                    textBox.TextChanged -= txtAmount_TextChanged;
                    textBox.Text = formattedText;
                    textBox.CaretIndex = textBox.Text.Length; // Đưa con trỏ về cuối
                    textBox.TextChanged += txtAmount_TextChanged;
                }
            }
        }
    }
}