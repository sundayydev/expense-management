using BLL.DTO.Category;
using BLL.DTO.Expenses;
using BLL.DTO.Income;
using BLL.Services;
using DAL.Models;
using DAL.Utils;
using GUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcSpend.xaml
    /// </summary>
    public partial class UcSpend : UserControl
    {
        private SearchManager<Expens> _searchManager;
        private List<Expens> Expenses { get; set; }

        private readonly ExpenseService _expenseService = new ExpenseService();
        public UcSpend()
        {
            InitializeComponent();
            LoadData();
            HandleExpenseSearch();
        }
        void LoadData()
        {
            Expenses = _expenseService.GetAllExpensesByUserId(BLL.AppContext.Instance.UserId);

            var sortedExpenses = Expenses.OrderByDescending(e => e.ExpenseDate).ToList();

            ExpenseDateGrid.ItemsSource = sortedExpenses;
        }

        void HandleExpenseSearch()
        {
            // Khởi tạo SearchManager
            _searchManager = new SearchManager<Expens>(
            Expenses,
            results => ExpenseDateGrid.ItemsSource = results, // Cập nhật DataGrid
            (expense, searchText) => // Logic lọc dữ liệu
                expense.Category.CategoryName.ToLower().Contains(searchText) ||
                expense.Note.ToLower().Contains(searchText) ||
                expense.Amount.ToString(CultureInfo.InvariantCulture).Contains(searchText) ||
                expense.ExpenseDate.ToString(CultureInfo.InvariantCulture).Contains(searchText) ||
                expense.CreatedAt.ToString().Contains(searchText) 
            );

            ExpenseDateGrid.ItemsSource = Expenses;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WFormExpense wf = new WFormExpense();
            wf.ShowDialog();
            wf.Close();

            LoadData();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                if (button == null) return;
                var expenseToDelete = button.DataContext as Expens;

                if (expenseToDelete != null)
                {
                    DialogCustoms dialog = new DialogCustoms("Bạn có muốn xóa không?", "Thông báo", DialogCustoms.YesNo);

                    if (dialog.ShowDialog() == true)
                    {
                        _expenseService.DeleteExpense(expenseToDelete.ExpenseId);
                        Expenses.Remove(expenseToDelete);
                        ExpenseDateGrid.ItemsSource = new ObservableCollection<Expens>(Expenses);

                        DialogCustoms res = new DialogCustoms("Xóa thành công ", "Thông báo", DialogCustoms.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var button = sender as Button;
                var expenseToEdit = button?.DataContext as Expens;

                if (expenseToEdit != null)
                {
                    var editForm = new WFormExpense(expenseToEdit);
                    editForm.ShowDialog();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi chỉnh sửa: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchManager.OnSearchTextChanged(TxtSearch.Text);
        }
    }
}
