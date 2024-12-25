using BLL.DTO.Category;
using BLL.DTO.Expenses;
using BLL.Services;
using DAL.Models;
using GUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private List<Expens> Expenses { get; set; }

        private readonly ExpenseService _expenseService = new();
        public UcSpend()
        {
            InitializeComponent();
            LoadData();
        }
        void LoadData()
        {
            Expenses = new List<Expens>();
            Expenses = _expenseService.GetAllExpensesByUserId(BLL.AppContext.Instance.UserId);
            dvgExpense.ItemsSource = Expenses;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WFormExpense wf = new WFormExpense();
            wf.ShowDialog();
            wf.Close();

            LoadData();
        }

        private void txtFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtFind.Text.ToLower();
            DateTime? searchDate = null;
            if (DateTime.TryParse(searchText, out DateTime parsedDate))
            {
                searchDate = parsedDate.Date; 
            }
            var res = Expenses.Where(expense =>
                expense.Note.ToLower().Contains(searchText) ||
                expense.ExpenseId.ToString().Equals(searchText)||
                (searchDate.HasValue && expense.ExpenseDate.Date == searchDate.Value)
            ).ToList();

            dvgExpense.ItemsSource = res.ToList();
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
                        dvgExpense.ItemsSource = new ObservableCollection<Expens>(Expenses);
                        
                        DialogCustoms res = new DialogCustoms("Xóa thành công ","Thông báo",DialogCustoms.OK);
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
    }
    
}
