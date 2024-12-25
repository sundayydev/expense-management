using BLL.Services;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI.UserControls
{
    public partial class UcLiveChart : UserControl
    {
        private readonly ExpenseService _expenseService = new ExpenseService();
        private readonly IncomeService _incomeService = new IncomeService();
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Values { get; set; }

        public UcLiveChart()
        {
            InitializeComponent();
            UpdateChartData(true);
            Values = value => value.ToString("N0") + " VND";
            DataContext = this;
        }

        private void rdbDay_Checked(object sender, RoutedEventArgs e)
        {
            UpdateChartData(false); // Cập nhật khi chuyển sang chế độ ngày
        }

        private void rdbMonth_Checked(object sender, RoutedEventArgs e)
        {
            UpdateChartData(true); // Cập nhật khi chuyển sang chế độ tháng
        }

        private void UpdateChartData(bool isMonthly)
        {
            string userId = BLL.AppContext.Instance.UserId;

            if (isMonthly)
            {
                var months = _expenseService.GetMonthsWithExpenses(userId);

                var monthlyExpenses = _expenseService.GetMonthlyExpenses(userId);
                var monthlyIncome = _incomeService.GetMonthlyIncome(userId);

                var monthlyExpenseValues = monthlyExpenses.Select(e => (int)e.Amount).ToList();
                var monthlyIncomeValues = monthlyIncome.Select(i => (int)i.Amount).ToList();

                SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Thu Nhập",
                        Values = new ChartValues<int>(monthlyIncomeValues),
                        Fill = (Brush)FindResource("IncomeGradient"),
                        LabelPoint = point => point.Y.ToString("N0") + " VND"
                    },
                    new ColumnSeries
                    {
                        Title = "Chi Tiêu",
                        Values = new ChartValues<int>(monthlyExpenseValues),
                        Fill = (Brush)FindResource("ExpenseGradient"),
                        LabelPoint = point => point.Y.ToString("N0") + " VND"
                    }
                };

                
                Labels = months.ToArray();
            }
            else
            {
                // Cập nhật dữ liệu cho chế độ ngày nếu cần
            }

            DataContext = null;
            DataContext = this;
        }
    }
}
    