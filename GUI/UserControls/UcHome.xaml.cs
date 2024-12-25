using BLL.Services;
using System.Windows.Controls;

namespace GUI.UserControls
{
    public partial class UcHome : UserControl
    {
        ExpenseService expense = new ExpenseService();
        IncomeService income  = new IncomeService();
        public UcHome()
        {
            InitializeComponent();
            decimal totalExpense = expense.GetTotalExpensesByUserId(BLL.AppContext.Instance.UserId);
            decimal totalIncome = income.GetTotalIncomeByUserId(BLL.AppContext.Instance.UserId);
            txtExpense.Text = $"{totalExpense:N0} VND";
            txtIncome.Text = $"{totalIncome:N0} VND";
        }

        private void UcLiveChart_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        void SumExpense()
        {
            
        }


    }
}