using BLL.DTO.Category;
using BLL.DTO.Income;
using BLL.DTO.Recipient;
using BLL.Services;
using DAL.Models;
using GUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using AppContext = BLL.AppContext;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcIncome.xaml
    /// </summary>
    public partial class UcIncome : UserControl
    {
        private ObservableCollection<IncomeDto> Incomes { get; set; }
        private readonly IncomeService _incomeService = new();
        public UcIncome()
        {
            InitializeComponent();
            LoadData();
            InvoiceDataGrid.ItemsSource = Incomes;
        }
        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            WFormIncome wf = new WFormIncome();
            wf.ShowDialog();
            LoadData();
        }
        public void LoadData()
        {
            InvoiceDataGrid.ItemsSource = null;
            Incomes = new ObservableCollection<IncomeDto>(_incomeService.GetIncomeByUserId(AppContext.Instance.UserId));
            InvoiceDataGrid.ItemsSource = Incomes;
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var income = (IncomeDto)InvoiceDataGrid.SelectedItem;
            if (income == null)
            {
                DialogCustoms chose = new DialogCustoms("Vui lòng chọn một dòng để xóa.", "Thông báo", DialogCustoms.Show);
                return;
            }
            DialogCustoms remove = new DialogCustoms("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận", DialogCustoms.YesNo);
            if (remove.ShowDialog() == true)
            {
                try
                {
                    _incomeService.DeleteIncome(income.IncomeId);
                    DialogCustoms success = new DialogCustoms("Xóa thu nhập thành công.", "Thông báo", DialogCustoms.Show);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DataGridRow row = DataGridRow.GetRowContainingElement(button);
            if (row != null)
            {
                var incomeDto = row.Item as IncomeDto;
                if (incomeDto != null)
                {
                    var income = _incomeService.GetIncomeById(incomeDto.IncomeId);

                    if (income != null)
                    {
                        WFormIncome wf = new WFormIncome(income);
                        var result = wf.ShowDialog();
                        if (result == true)
                        {
                            LoadData();
                        }
                    }
                    else
                    {
                        DialogCustoms error = new DialogCustoms("Không tìm thấy thông tin thu nhập.", "Lỗi", DialogCustoms.Show);
                        error.ShowDialog();
                    }
                }
            }
        }
    } 
}

