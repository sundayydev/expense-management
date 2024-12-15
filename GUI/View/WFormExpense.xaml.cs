using GUI.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for WFormExpense.xaml
    /// </summary>
    public partial class WFormExpense : Window
    {
        public Expense ExpenseData { get; set; }
        public event Action<Expense> OnExpenseAdded;
        private List<Expense> Expenses = new List<Expense>();
        public WFormExpense()
        {
            InitializeComponent();
            ExpenseData = new Expense();
            this.DataContext = this;
        }

        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            Expense newExpense = new Expense
            {
                /*ExpenseID= 11,
                UserID = 1,
                CategoryID = 1,
                RecipientID = 1,
                Amount = 120000  ,
                ExpenseDate = DateTime.Now,
                Note = "co cao con c",
                CreatedAt = DateTime.Now*/
                ExpenseID = 11,
                UserID = int.Parse(txtUser.Text.ToString()),
                CategoryID = int.Parse(txtCatagory.Text.ToString()),
                RecipientID = int.Parse(txtRecipient.Text.ToString()),
                Amount = decimal.Parse(txtAmount.Text.ToString()),
                ExpenseDate = DateTime.Now,
                Note = new TextRange(rtbNote.Document.ContentStart, rtbNote.Document.ContentEnd).Text.Trim(),
                CreatedAt = DateTime.Now
            };

            OnExpenseAdded?.Invoke(newExpense);

            var findExpense = Expenses.FirstOrDefault(f => f.ExpenseID == newExpense.ExpenseID);

            if (findExpense != null)
            {
                findExpense.UserID = newExpense.UserID;
                findExpense.CategoryID = newExpense.CategoryID;
                findExpense.RecipientID = newExpense.RecipientID;
                findExpense.Amount = newExpense.Amount;
                findExpense.ExpenseDate = newExpense.ExpenseDate;
                findExpense.Note = newExpense.Note;

                DialogCustoms dialog = new DialogCustoms("Cập nhật thành công!", "Thông báo", DialogCustoms.Show);
                dialog.ShowDialog();
            }
            else
            {

                newExpense.ExpenseID = Expenses.Count + 1; 
                Expenses.Add(newExpense); 
                DialogCustoms dialog = new DialogCustoms("Thêm mới thành công!", "Thông báo", DialogCustoms.Show);
                dialog.ShowDialog();
            }

            this.Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogCustoms cancelDialog = new DialogCustoms("Hủy thao tác", "Thông báo", DialogCustoms.Show);
            cancelDialog.ShowDialog();
            this.Close();
        }
    }
}