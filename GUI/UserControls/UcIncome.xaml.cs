using BLL.DTO.Category;
using BLL.DTO.Income;
using BLL.Services;
using DAL.Models;
using GUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private List<IncomeDto> Incomes {get;set;}
        private readonly IncomeService _incomeService = new ();
        public UcIncome()
        {
            InitializeComponent();
            LoadData();
        }
        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
           WFormIncome wf = new WFormIncome();
           wf.ShowDialog();
           wf.Close();
           LoadData();
        }
        public void LoadData()
        {
            Incomes = new List<IncomeDto>();
            Incomes = _incomeService.GetIncomeByUserId(AppContext.Instance.UserId);
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
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _incomeService.DeleteIncome(income.IncomeId);
                    MessageBox.Show("Xóa thu nhập thành công.", "Thông báo",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

