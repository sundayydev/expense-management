using GUI.View;
using System;
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
        public ObservableCollection<Expense> Expenses { get; set; }

        public UcSpend()
        {
            InitializeComponent();
            LoadData();
            InvoiceDataGrid.ItemsSource = Expenses;
        }

        // Load initial data
        private void LoadData()
        {
            Expenses = new ObservableCollection<Expense>
            {
                new Expense{ ExpenseID = 1, UserID = 101, CategoryID = 1, RecipientID = 201, Amount = 150000000, ExpenseDate = DateTime.Now.Date, Note = "Lunch", CreatedAt = DateTime.Now},
                new Expense{ ExpenseID = 2, UserID = 102, CategoryID = 2, RecipientID = 202, Amount = 250000, ExpenseDate = DateTime.Now.Date, Note = "Office Supplies", CreatedAt = DateTime.Now},
                new Expense{ ExpenseID = 3, UserID = 103, CategoryID = 3, RecipientID = 203, Amount = 99.99m, ExpenseDate = DateTime.Now.Date, Note = "Taxi Fare", CreatedAt = DateTime.Now },
                new Expense{ ExpenseID = 4, UserID = 104, CategoryID = 1, RecipientID = 204, Amount = 500.00m, ExpenseDate = DateTime.Now.Date, Note = "Dinner", CreatedAt = DateTime.Now },
                new Expense{ ExpenseID = 5, UserID = 105, CategoryID = 2, RecipientID = 205, Amount = 120.75m, ExpenseDate = DateTime.Now.Date, Note = "Stationery", CreatedAt = DateTime.Now}
            };
        }

        // TextChanged event for searching
        private void txtFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtFind.Text.ToLower();

            var res = Expenses.Where(expense =>
                expense.Note.ToLower().Contains(searchText) ||
                expense.CategoryID.ToString().Equals(searchText)
            ).ToList();

            InvoiceDataGrid.ItemsSource = new ObservableCollection<Expense>(res);
        }

        // Add new expense
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var newExpense = new Expense
            {
                ExpenseID = Expenses.Count + 1,
                UserID = 106,
                CategoryID = 2,
                RecipientID = 206,
                Amount = 250m,
                ExpenseDate = DateTime.Now.Date,
                Note = "New Item",
                CreatedAt = DateTime.Now
            };

            Expenses.Add(newExpense);
            InvoiceDataGrid.ItemsSource = new ObservableCollection<Expense>(Expenses);
        }

        // Delete an expense
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var expenseToDelete = button.DataContext as Expense;
           
            if (expenseToDelete != null)
            {
                DialogCustoms dialog = new DialogCustoms("Bạn có muốn xoa ko ?", "Thông báo", DialogCustoms.YesNo);
                if (dialog.ShowDialog() == true)
                {
                    Expenses.Remove(expenseToDelete);
                    InvoiceDataGrid.ItemsSource = new ObservableCollection<Expense>(Expenses);
                }        
            }
        }
    }

    // Expense class definition
    public class Expense
    {
        public int ExpenseID { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public int RecipientID { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
