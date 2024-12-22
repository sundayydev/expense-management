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
                    IncomeDate =DateTime.Now.Date, Note = "Lương CV",
                    CreatedAt = DateTime.Now.Date
                }
            };

        }
        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            WFormIcome wFormIcome = new WFormIcome();
            wFormIcome.ShowDialog();
            wFormIcome.OnExpenseAdded += FormExpense_OnExpenseAdded;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var incomeToDelete = button.DataContext as Income;

            if (incomeToDelete != null)
            {
<<<<<<< HEAD
                DialogCustoms dialog = new DialogCustoms("Bạn có muốn xóa ko ?", "Thông báo", DialogCustoms.YesNo);
                if (dialog.ShowDialog() == true)
=======
                var income = button.DataContext as Income;

                DialogCustoms dialog = new DialogCustoms("Bạn có muốn xóa không ?", "Thông báo", DialogCustoms.YesNo);
                dialog.ShowDialog();
                var dialog2 = new DialogCustoms("Bạn đã xóa thành công !", "Thông báo", DialogCustoms.OK);
                var dialog3 = new DialogCustoms("Bạn đã hủy thành công !", "Thông báo", DialogCustoms.OK);
                if (DialogCustoms.Show == DialogCustoms.OK)
>>>>>>> 243ea2ce251d03d8b0510887c18d3b81eb45194a
                {
                    Incomes.Remove(incomeToDelete);
                    InvoiceDataGrid.ItemsSource = new ObservableCollection<Income>(Incomes);

                }
            }
        }
        public void RefreshDataGrid()
        {
            InvoiceDataGrid.ItemsSource = Incomes;
        }
        private void FormExpense_OnExpenseAdded(Income newIcomes)
        {
            Incomes.Add(newIcomes);
            InvoiceDataGrid.ItemsSource = new ObservableCollection<Income>(Incomes);
        }
        public class Income
        {
            public string IncomeId { get; set; }
            public string UserId { get; set; }
            public string RecipientId { get; set; }

<<<<<<< HEAD
            public string Source { get; set; }
            public float Amount { get; set; }
            public DateTime IncomeDate { get; set; }
            public string Note { get; set; }
            public DateTime CreatedAt { get; set; }
        }
=======
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
>>>>>>> 243ea2ce251d03d8b0510887c18d3b81eb45194a
    }
}

