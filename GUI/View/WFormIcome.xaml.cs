    using GUI.UserControls;
    using MaterialDesignThemes.Wpf;
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
    using System.Windows.Shapes;
    using static GUI.UserControls.UcIncome;

    namespace GUI.View
    {
        /// <summary>
        /// Interaction logic for WFormIcome.xaml
        /// </summary>
        public partial class WFormIcome : Window

        {
            public Income IncomeData { get; set; }
            public event Action<Income> OnExpenseAdded;
            private ObservableCollection<Income> Incomes = new ObservableCollection<Income>();
            public WFormIcome()
            {
                InitializeComponent();
                IncomeData = new Income();
                this.DataContext = this;
            }
            private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
            {
                Income newIncome = new Income
                {
                    IncomeId ="11",
                    UserId = txtUserID.Text,
                    RecipientId = txtRecipientID.Text,
                    Amount =float.Parse(txtTotal.Text.ToString()),
                    IncomeDate = DateTime.Now,
                    Note = new TextRange(rtbNote.Document.ContentStart, rtbNote.Document.ContentEnd).Text.Trim(),
                    CreatedAt = DateTime.Now,

                };
                OnExpenseAdded?.Invoke(newIncome);
                var findIcomes = Incomes.FirstOrDefault(f => f.IncomeId == newIncome.IncomeId);
                if(findIcomes != null)
                {
                    findIcomes.UserId=newIncome.UserId;
                    findIcomes.RecipientId=newIncome.RecipientId;
                    findIcomes.Amount= newIncome.Amount;
                    findIcomes.IncomeDate= newIncome.IncomeDate;
                    findIcomes.Note = newIncome.Note;
                    findIcomes.CreatedAt = newIncome.CreatedAt;
                    DialogCustoms dialog = new DialogCustoms("Cập nhật thành công!", "Thông báo", DialogCustoms.Show);
                    dialog.ShowDialog();
                }
                else
                {
                    newIncome.UserId = (Incomes.Count+1).ToString();
                    Incomes.Add(newIncome);
                    DialogCustoms dialog = new DialogCustoms("Thêm mới thành công!", "Thông báo", DialogCustoms.Show);
                    dialog.ShowDialog();
                }

                UcIncome ucIncome = new UcIncome();
                ucIncome.Incomes.Add(newIncome);
                ucIncome.RefreshDataGrid();
                this.Close();
            }
            private void btnCancel_Click(object sender, RoutedEventArgs e)
            {
                DialogCustoms cancelDialog = new DialogCustoms("Hủy thao tác", "Thông báo", DialogCustoms.Show);
                cancelDialog.ShowDialog();
                this.Close();
            }

        }
    }
