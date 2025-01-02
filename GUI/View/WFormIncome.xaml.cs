using BLL.DTO.Category;
using BLL.DTO.Income;
using BLL.Services;
using DAL.Models;
using DAL.Repositories;
using GUI.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.TextFormatting;
using AppContext = BLL.AppContext;

namespace GUI.View
{
    public partial class WFormIncome : Window
    {
        private readonly IncomeService _incomeService = new IncomeService();
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly RecipientService _recipientService = new RecipientService();
        private readonly Income _income;
        private readonly bool _isEditMode;
        private readonly string _incomeId;

        public WFormIncome(Income income = null)
        {
            InitializeComponent();
            _income = income;
            _isEditMode = income != null;
            filldata();
        }
        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    if (_isEditMode)
                    {
                        _income.RecipientId = CmbRecipentName.SelectedValue?.ToString();
                        _income.CategoryId = (cmbCategoryType.SelectedItem as CmbCategoryDto).CategoryId;
                        _income.Amount = decimal.Parse(txtTotal.Text);
                        _income.IncomeDate = dtpIncomeDate.SelectedDate.HasValue ? dtpIncomeDate.SelectedDate.Value : _income.IncomeDate;
                        _income.Note = new TextRange(rtbNote.Document.ContentStart, rtbNote.Document.ContentEnd).Text.Trim();
                        _income.CreatedAt = DateTime.Now;

                        var updateIncome = new Income()
                        {
                            IncomeId = _income.IncomeId,
                            UserId = _income.UserId,
                            CategoryId = _income.CategoryId,
                            RecipientId = _income.RecipientId,
                            Amount = _income.Amount,
                            IncomeDate = _income.IncomeDate,
                            Note = _income.Note
                        };
                        _incomeService.UpdateIncome(updateIncome);
                        DialogCustoms dialogCustomsUpdate = new DialogCustoms("Cập nhật danh mục thành công.", "Thông báo", DialogCustoms.OK);
                        dialogCustomsUpdate.ShowDialog();
                        LoadData();
                        Close(); 
                       
                    }
                    
                    else
                    {
                        var addIncomeDto = new AddIncomeDto
                        {
                            UserId = AppContext.Instance.UserId,
                            CategoryId = cmbCategoryType.SelectedValue.ToString(),
                            RecipientId = CmbRecipentName.SelectedValue?.ToString(),
                            IncomeDate = dtpIncomeDate.SelectedDate.Value,
                            Amount = decimal.Parse(txtTotal.Text),
                            Note = new TextRange(rtbNote.Document.ContentStart,
                                rtbNote.Document.ContentEnd).Text.Trim()
                        };

                        _incomeService.AddIncome(addIncomeDto);
                        DialogCustoms dialogCustomsUpdate = new DialogCustoms("Thêm Thu Nhập thành công!", "Thông báo", DialogCustoms.OK);
                        dialogCustomsUpdate.ShowDialog();
                        DialogResult = true;
                        LoadData();
                        Close();

                    } 
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ResetFormIncome();
        }

        private bool ValidateForm()
        {
            if (cmbCategoryType.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục.", "Thông báo");
                return false;
            }

            if (dtpIncomeDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày.", "Thông báo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTotal.Text) ||
                !decimal.TryParse(txtTotal.Text, out decimal amount) ||
                amount <= 0)
            {
                MessageBox.Show("Vui lòng nhập số tiền hợp lệ.", "Thông báo");
                return false;
            }

            return true;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
                DialogCustoms cancelDialog = new DialogCustoms("Hủy thao tác", "Thông báo", DialogCustoms.Show);
                cancelDialog.ShowDialog();
                this.Close();
        }
        public void LoadCmbCategories()
        {
            var categories = _categoryService.GetCategoriesByCategoryType(AppContext.Instance.UserId, "Thu nhập");
            var filteredCategories = categories.Where(c => c.CategoryType == "Thu nhập").ToList();
            cmbCategoryType.ItemsSource = filteredCategories;
            cmbCategoryType.DisplayMemberPath = "CategoryName";
            cmbCategoryType.SelectedValuePath = "CategoryId";
        }
        public void LoadCmbRecipients()
        {
            var recipients = _recipientService.GetRecipientsByUserId(AppContext.Instance.UserId);
            CmbRecipentName.ItemsSource = recipients;
            CmbRecipentName.DisplayMemberPath = "RecipientName";
            CmbRecipentName.SelectedValuePath = "RecipientId";
        }
        private void WFormIncome_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadCmbCategories();
            LoadCmbRecipients();
            if (_isEditMode && _income != null)
            {
                CmbRecipentName.SelectedValue = _income.RecipientId;
                cmbCategoryType.SelectedValue = _income.CategoryId;
                txtTotal.Text = ((int)_income.Amount).ToString();
                dtpIncomeDate.SelectedDate = _income.IncomeDate;
                rtbNote.Document.Blocks.Clear();
                rtbNote.Document.Blocks.Add(new Paragraph(new Run(_income.Note)));

            }
        }

        public void ResetFormIncome()
        {
            txtTotal.Text = "";
            rtbNote.Document.Blocks.Clear();
        }

        private void txtTotal_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            if (textBox != null)
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
                    textBox.TextChanged -= txtTotal_TextChanged;
                    textBox.Text = formattedText;
                    textBox.CaretIndex = textBox.Text.Length; // Đưa con trỏ về cuối
                    textBox.TextChanged += txtTotal_TextChanged;
                }
            }
        }
        private void filldata()
        {
            if (_income != null)
            {
                cmbCategoryType.Text = _income.Category.CategoryName;
                CmbRecipentName.SelectedValuePath = _income.RecipientId;
                txtTotal.Text = _income.Amount.ToString();
                dtpIncomeDate.DisplayDate = _income.IncomeDate;
                rtbNote.Document.Blocks.Clear();
                rtbNote.Document.Blocks.Add(new Paragraph(new Run(_income.Note)));
            }
        }
        private void LoadData()
        {
            LoadCmbCategories();
            LoadCmbRecipients();

            if (_isEditMode && _income != null)
            {
                CmbRecipentName.SelectedValue = _income.RecipientId;
                cmbCategoryType.SelectedValue = _income.CategoryId;
                txtTotal.Text = ((int)_income.Amount).ToString();
                dtpIncomeDate.SelectedDate = _income.IncomeDate;
                rtbNote.Document.Blocks.Clear();
                rtbNote.Document.Blocks.Add(new Paragraph(new Run(_income.Note)));
            }
        }
    }
} 