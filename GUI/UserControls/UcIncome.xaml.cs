using BLL.Services;
using DAL.Models;
using GUI.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcIncome.xaml
    /// </summary>
    public partial class UcIncome : UserControl
    {
        public ObservableCollection<Income> Incomes { get; set; }
        private readonly IncomeService _incomeService;
        public UcIncome()
        {
            InitializeComponent();
            _incomeService = new IncomeService();
            LoadData();
            DataContext = this;
        }
        private void LoadData()
        {
            try
            {
                Incomes = new ObservableCollection<Income>(_incomeService.GetAllIncomes());
                InvoiceDataGrid.ItemsSource = Incomes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var wFormIncome = new WFormIncome();
                wFormIncome.OnIncomeAdded += () =>
                {
                    LoadData();
                };
                wFormIncome.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở cửa sổ thêm thu nhập: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var incomeToDelete = button.DataContext as Income;

            if (incomeToDelete != null)
            {
                try
                {
                    DialogCustoms dialog = new DialogCustoms("Bạn có muốn xóa không?", "Thông báo", DialogCustoms.YesNo);
                    if (dialog.ShowDialog() == true)
                    {
                        _incomeService.DeleteIncome(incomeToDelete); 
                        Incomes.Remove(incomeToDelete); 
                        InvoiceDataGrid.ItemsSource = new ObservableCollection<Income>(Incomes);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa thu nhập: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

