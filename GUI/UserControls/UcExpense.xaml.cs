using GUI.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

        private void LoadData()
        {
            var brushConverter = new BrushConverter();

            Expenses = new ObservableCollection<Expense>
            {
                new Expense{ ExpenseID = 1, UserID = 101, CategoryID = 1, RecipientID = 201, Amount = 150000000, ExpenseDate = DateTime.Now.Date, Note = "Lunch", CreatedAt = DateTime.Now},
                new Expense{ ExpenseID = 2, UserID = 102, CategoryID = 2, RecipientID = 202, Amount = 250000, ExpenseDate = DateTime.Now.Date, Note = "Office Supplies", CreatedAt = DateTime.Now},
                new Expense{ ExpenseID = 3, UserID = 103, CategoryID = 3, RecipientID = 203, Amount = 99.99m, ExpenseDate = DateTime.Now.Date, Note = "Taxi Fare", CreatedAt = DateTime.Now },
                new Expense{ ExpenseID = 4, UserID = 104, CategoryID = 1, RecipientID = 204, Amount = 500.00m, ExpenseDate = DateTime.Now.Date, Note = "Dinner", CreatedAt = DateTime.Now },
                new Expense{ ExpenseID = 5, UserID = 105, CategoryID = 2, RecipientID = 205, Amount = 120.75m, ExpenseDate = DateTime.Now.Date, Note = "Stationery", CreatedAt = DateTime.Now},
                
            };
        }

        private void BtnAddExpense_Click(object sender, RoutedEventArgs e)
        {
            WFormExpense wf = new WFormExpense();
            wf.ShowDialog();
        }
    }

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
