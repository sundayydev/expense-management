using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI.UserControls
{
    public partial class UcLiveChart : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Values { get; set; }

        public UcLiveChart()
        {
            InitializeComponent();

            UpdateChartData(isMonthly: true);

            Values = value => value.ToString();

            DataContext = this;
        }

        private void rdbDay_Checked(object sender, RoutedEventArgs e)
        {
            UpdateChartData(isMonthly: false);
        }

        private void rdbMonth_Checked(object sender, RoutedEventArgs e)
        {
            UpdateChartData(isMonthly: true);
        }

        private void UpdateChartData(bool isMonthly)
        {
            if (isMonthly)
            {
                // Monthly data
                SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Thu Nhập",
                        Values = new ChartValues<int> { 0, 30, 60, 90, 120, 150 },
                        Fill = (Brush)FindResource("IncomeGradient")
                    },
                    new ColumnSeries
                    {
                        Title = "Chi Tiêu",
                        Values = new ChartValues<int> { 100, 30, 60, 48, 5, 15 },
                        Fill = (Brush)FindResource("ExpenseGradient")
                    }
                };

                Labels = new[] { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6" };
            }
            else
            {
                // Daily data
                SeriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Thu Nhập",
                        Values = new ChartValues<int> { 10, 20, 15, 25, 30, 35, 40 },
                        Fill = (Brush)FindResource("IncomeGradient")
                    },
                    new ColumnSeries
                    {
                        Title = "Chi Tiêu",
                        Values = new ChartValues<int> { 5, 15, 10, 20, 25, 30, 35 },
                        Fill = (Brush)FindResource("ExpenseGradient")
                    }
                };

                Labels = new[] { "Ngày 1", "Ngày 2", "Ngày 3", "Ngày 4", "Ngày 5", "Ngày 6", "Ngày 7" };
            }

            // Notify UI about the changes
            DataContext = null;
            DataContext = this;
        }
    }
}
