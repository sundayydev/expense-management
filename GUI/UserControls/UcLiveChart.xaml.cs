using BLL.Services;
using DAL.Utils;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
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
        private readonly string _userId = BLL.AppContext.Instance.UserId;

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Values { get; set; }

        public UcLiveChart()
        {
            InitializeComponent();

            rdbMonth.IsChecked = true;
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
            try
            {
                string userId = BLL.AppContext.Instance.UserId;

                if (isMonthly)
                {
                    int currentMonth = DateTime.Now.Month;
                    int currentYear = DateTime.Now.Year;

                    var monthlyIncomeValues = new int[6];
                    var monthlyExpenseValues = new int[6];
                    string[] months = new string[6];

                    for (int i = 0; i < 6; i++)
                    {
                        monthlyExpenseValues[i] = (int)_expenseService.GetTotalAmountByMonthly(userId, currentMonth, currentYear);
                        monthlyIncomeValues[i] = (int)_incomeService.GetTotalAmountByMonthly(userId, currentMonth, currentYear);

                        months[i] = $"{currentMonth}/{currentYear}";
                        (currentMonth, currentYear) = FunctionHelper.GetPreviousMonth(currentMonth, currentYear);
                    }

                    // Lật ngược thứ tự dữ liệu
                    Array.Reverse(months);
                    Array.Reverse(monthlyIncomeValues);
                    Array.Reverse(monthlyExpenseValues);

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
                    DateTime date = DateTime.Now;
                    var dailyIncomeValues = new int[7];
                    var dailyExpenseValues = new int[7];

                    string[] days = new string[7];

                    List<DateTime> datesInWeek = FunctionHelper.GetDatesInWeek(date);

                    foreach (var day in datesInWeek)
                    {
                        dailyIncomeValues[datesInWeek.IndexOf(day)] = (int)_incomeService.GetTotalAmountByDate(userId, day);
                        dailyExpenseValues[datesInWeek.IndexOf(day)] = (int)_expenseService.GetTotalAmountByDate(userId, day);
                        days[datesInWeek.IndexOf(day)] = FunctionHelper.GetDayOfWeekInVietnamese(day);
                    }

                    SeriesCollection = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Thu Nhập",
                            Values = new ChartValues<int>(dailyIncomeValues),
                            Fill = (Brush)FindResource("IncomeGradient"),
                            LabelPoint = point => point.Y.ToString("N0") + " VND"
                        },
                        new ColumnSeries
                        {
                            Title = "Chi Tiêu",
                            Values = new ChartValues<int>(dailyExpenseValues),
                            Fill = (Brush)FindResource("ExpenseGradient"),
                            LabelPoint = point => point.Y.ToString("N0") + " VND"
                        }
                    };

                    Labels = days.ToArray();

                }

                DataContext = null;
                DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi cập nhật dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

    }
}
    