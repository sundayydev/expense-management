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

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for WFormExpense.xaml
    /// </summary>
    public partial class WFormExpense : Window
    {
        private readonly CategoryService _categoryService = new();
        private readonly ExpenseService _expenseService = new();
        public WFormExpense()
        {
            InitializeComponent();
        }

        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var addExpense = new ExpenseDto
                {
                    UserId = AppContext.Instance.UserId,
                    CategoryId = (CmbCategory.SelectedItem as CmbCategoryDto).CategoryId,
                    RecipientId = null,
                    Amount = decimal.Parse( txtAmount.Text),
                    ExpenseDate = dtpExpenseDate.SelectedDate.Value,
                    Note = new TextRange(rtbNote.Document.ContentStart, rtbNote.Document.ContentEnd).Text.Trim()
                };

                _expenseService.AddExpense(addExpense);
                DialogCustoms dialogCustoms = new DialogCustoms("Thêm danh mục thành công.", "Thông báo", DialogCustoms.OK);
                dialogCustoms.ShowDialog();
                
            }
            catch (Exception exception)
            {
                MessageBox.Show("Lỗi: " + exception.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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
            CmbCategory.ItemsSource = _categoryService.GetCategoriesByCategoryType(AppContext.Instance.UserId, "Chi tiêu");
            CmbCategory.DisplayMemberPath = "CategoryName";
            CmbCategory.SelectedValuePath = "CategoryId";
        }

        private void WFormExpense_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadCmbCategories();
        }
    }
}