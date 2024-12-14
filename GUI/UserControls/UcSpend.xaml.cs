using GUI.View;
using MaterialDesignThemes.Wpf;
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
            dvgExpense.ItemsSource = Expenses;
        }

       
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

      
        private void txtFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtFind.Text.ToLower();

            var res = Expenses.Where(expense =>
                expense.Note.ToLower().Contains(searchText) ||
                expense.CategoryID.ToString().Equals(searchText)
            ).ToList();

            dvgExpense.ItemsSource = new ObservableCollection<Expense>(res);
        }

     
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WFormExpense formExpense = new WFormExpense();
            formExpense.OnExpenseAdded += FormExpense_OnExpenseAdded; 
            formExpense.ShowDialog();

        }
        private void FormExpense_OnExpenseAdded(Expense newExpense)
        {
            Expenses.Add(newExpense);
            dvgExpense.ItemsSource = new ObservableCollection<Expense>(Expenses); 
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var expenseToDelete = button.DataContext as Expense;
           
            if (expenseToDelete != null)
            {
                DialogCustoms dialog = new DialogCustoms("Bạn có muốn xoá ko ?", "Thông báo", DialogCustoms.YesNo);
                if (dialog.ShowDialog() == true)
                {
                    Expenses.Remove(expenseToDelete);
                    dvgExpense.ItemsSource = new ObservableCollection<Expense>(Expenses);
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
