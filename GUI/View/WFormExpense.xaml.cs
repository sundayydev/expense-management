using GUI.UserControls;
using System;
using System.Windows;
using System.Windows.Documents;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for WFormExpense.xaml
    /// </summary>
    public partial class WFormExpense : Window
    {
        public Expense ExpenseData { get; set; }
        public event Action<Expense> OnExpenseAdded;

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

            DialogCustoms dialog = new DialogCustoms("Bạn có muốn cập nhật dữ liệu không?", "Thông báo", DialogCustoms.YesNo);
            if (dialog.ShowDialog() == true)
            {
                DialogCustoms successDialog = new DialogCustoms("Cập nhật thành công", "Thông báo", DialogCustoms.Show);
                successDialog.ShowDialog();
                this.Close();
            }
            else
            {
                DialogCustoms cancelDialog = new DialogCustoms("Hủy thao tác", "Thông báo", DialogCustoms.OK);
                cancelDialog.ShowDialog();
                this.Close();
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogCustoms cancelDialog = new DialogCustoms("Hủy thao tác", "Thông báo", DialogCustoms.Show);
            cancelDialog.ShowDialog();
            this.Close(); 
        }
    }
}
