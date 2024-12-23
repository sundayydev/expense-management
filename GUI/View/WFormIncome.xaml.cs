using BLL.Services;
using DAL.Models;
using GUI.UserControls;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace GUI.View
{
    public partial class WFormIncome : Window
    {
        private readonly IncomeService _incomeService;
        public event Action OnIncomeAdded;
        public WFormIncome()
        {
            InitializeComponent();
            _incomeService = new IncomeService();
        }
        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string recipientName = txtRecipientName.Text;
                decimal amount = Decimal.Parse(txtTotal.Text);
                string note = new TextRange(rtbNote.Document.ContentStart, rtbNote.Document.ContentEnd).Text.Trim();
                var incomeService = new IncomeService();
                incomeService.AddNewIncome(recipientName, amount, note);
                OnIncomeAdded?.Invoke();
                var dialog = new DialogCustoms("Thêm mới thành công!", "Thông báo", DialogCustoms.Show);
                dialog.ShowDialog();
                this.Close();
            }
            catch (Exception ex)
            {
                var dialog = new DialogCustoms($"Lỗi: {ex.Message}", "Thông báo", DialogCustoms.Show);
                dialog.ShowDialog();
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