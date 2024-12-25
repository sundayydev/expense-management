using BLL.Services;
using System.Windows.Controls;

namespace GUI.UserControls
{
    public partial class UcHome : UserControl
    {   
        public UcHome()
        {
            InitializeComponent();

            ExpenseService service = new ExpenseService();
            decimal totalExpense = service.GetTotalExpensesByUserId(BLL.AppContext.Instance.UserId);
            txtExpense.Text = $"{totalExpense:N0} VND";
        }

        private void UcLiveChart_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }
        void SumExpense()
        {
            
        }


    }
}