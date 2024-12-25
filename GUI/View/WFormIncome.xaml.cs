using BLL.DTO.Category;
using BLL.DTO.Income;
using BLL.Services;
using DAL.Models;
using DAL.Repositories;
using GUI.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.TextFormatting;
using AppContext = BLL.AppContext;

namespace GUI.View
{
    public partial class WFormIncome : Window
    {
        private readonly IncomeService _incomeService = new IncomeService();
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly UcIncome _ucIncome;
        public WFormIncome()
        {
            InitializeComponent();
            _incomeService = new IncomeService();
        }
        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    var addIncomeDto = new AddIncomeDto
                    {
                        UserId = AppContext.Instance.UserId,
                        CategoryId = cmbCategoryType.SelectedValue.ToString(),
                        IncomeDate = ExpenseDatePicker.SelectedDate.Value,
                        Amount = decimal.Parse(txtTotal.Text),
                        Note = new TextRange(rtbNote.Document.ContentStart,
                            rtbNote.Document.ContentEnd).Text.Trim()
                    };

                    _incomeService.AddIncome(addIncomeDto);
                    MessageBox.Show("Thêm thu nhập thành công.", "Thông báo",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
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

            if (ExpenseDatePicker.SelectedDate == null)
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
            private void WFormIncome_OnLoaded(object sender, RoutedEventArgs e)
            {
            LoadCmbCategories();
            }

            public void ResetFormIncome()
            {
            txtTotal.Text = "";
            rtbNote.Document.Blocks.Clear();
            }
    }
} 