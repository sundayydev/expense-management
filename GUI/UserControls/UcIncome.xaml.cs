using GUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcIncome.xaml
    /// </summary>
    public partial class UcIncome : UserControl
    {
        public ObservableCollection<Income> Incomes { get; set; }
        public UcIncome()
        {
            InitializeComponent();
            LoadData();
            InvoiceDataGrid.ItemsSource = Incomes;
        }
        private void LoadData()
        {
            var brushConverter = new BrushConverter();
            Incomes = new ObservableCollection<Income>
            {
                new Income{IncomeId ="01", UserId = "1",recipientId = "10",Source="Lương",Amount=1000000f,IncomeDate = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),Note="Lương CV",CreatedAt=DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy")}
            };
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        { 
            var button= sender as Button;
            var income = button.DataContext as Income;
            if (income == null)
            {
                return;
            }
            DialogCustoms dialog = new DialogCustoms("Bạn có muốn xóa không ?", "Thông báo", DialogCustoms.YesNo);
            dialog.ShowDialog();
            var dialog2 = new DialogCustoms("Bạn đã xóa thành công !", "Thông báo", DialogCustoms.OK);
            var dialog3 = new DialogCustoms("Bạn đã hủy thành công !", "Thông báo",DialogCustoms.OK);
            if(DialogCustoms.show == DialogCustoms.OK)
            {
                Incomes.Remove(income);
                dialog2.ShowDialog();
            }
            else
            {
               dialog3.ShowDialog();
            }    
        }

        }
    public class Income
    {
        public string IncomeId { get; set; }
        public string UserId { get; set; }
        public string recipientId { get; set; }

        public string Source { get; set; }
        public float Amount { get; set; }
        public string IncomeDate { get; set; }
        public string Note { get; set; }
        public string CreatedAt { get; set; }
    }
}
