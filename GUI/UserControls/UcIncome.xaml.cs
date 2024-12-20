using GUI.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
                new Income
                {
                    IncomeId = "01", UserId = "1", RecipientId = "10", Source = "Lương", Amount = 1000000f,
                    IncomeDate = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Note = "Lương CV",
                    CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy")
                }
            };
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var income = button.DataContext as Income;

                DialogCustoms dialog = new DialogCustoms("Bạn có muốn xóa không ?", "Thông báo", DialogCustoms.YesNo);
                dialog.ShowDialog();
                var dialog2 = new DialogCustoms("Bạn đã xóa thành công !", "Thông báo", DialogCustoms.OK);
                var dialog3 = new DialogCustoms("Bạn đã hủy thành công !", "Thông báo", DialogCustoms.OK);
                if (DialogCustoms.Show == DialogCustoms.OK)
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

    }

    public class Income
    {
        public string IncomeId { get; set; }
        public string UserId { get; set; }
        public string RecipientId { get; set; }
        public string Source { get; set; }
        public float Amount { get; set; }
        public string IncomeDate { get; set; }
        public string Note { get; set; }
        public string CreatedAt { get; set; }
    }
}
