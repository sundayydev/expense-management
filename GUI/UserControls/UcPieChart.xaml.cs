using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BLL.Services;
using DAL.Utils;

namespace GUI.UserControls
{
    public partial class UcPieChart : UserControl
    {
        private readonly CategoryService _categoryService = new CategoryService();
        private readonly ExpenseService _expenseService = new ExpenseService();
        private string userId = BLL.AppContext.Instance.UserId;

        public UcPieChart()
        {
            InitializeComponent();
            UpdateChartData();
            DataContext = this;
        }

        private void UpdateChartData()
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return; 
                }
                var dailyExpenses = _expenseService.GetExpenseByDate(userId, DateTime.Now);
                if (dailyExpenses == null || !dailyExpenses.Any())
                {
                    PieChart.Series.Clear();
                    return;
                }

                var totalAmount = dailyExpenses.Sum(e => e.Amount);
                var categories = _categoryService.GetCategoryByUserId(userId);
                var groupedExpenses = dailyExpenses
                    .GroupBy(e => e.CategoryId)
                    .Select(group => new
                    {
                        CategoryId = group.Key,
                        TotalAmount = group.Sum(e => e.Amount),
                        CategoryName = categories.FirstOrDefault(c => c.CategoryId == group.Key)?.CategoryName ?? null
                    })
                    .ToList();

                var pieSeriesList = new List<PieSeries>();
                foreach (var expense in groupedExpenses)
                {
                    var percentage = (expense.TotalAmount / totalAmount) * 100;
                    var pieSeries = new PieSeries
                    {
                        Title = expense.CategoryName,
                        Values = new ChartValues<decimal> { expense.TotalAmount },
                        DataLabels = true,
                       // LabelPoint = chartPoint => $"{percentage:0.##}%" 
                    };

                    // Sự kiện MouseEnter để hiển thị tooltip
                    pieSeries.MouseEnter += (sender, e) =>
                    {
                        var series = sender as PieSeries;
                        if (series != null)
                        {
                            // Hiển thị tooltip với thông tin chi tiết
                            var tooltipData = new
                            {
                                CategoryName = expense.CategoryName,
                                Amount = expense.TotalAmount.ToString("C"), 
                                Percentage = $"{percentage:0.##}%" 
                            };

                            ToolTip tooltip = new ToolTip
                            {
                                ContentTemplate = (DataTemplate)FindResource("PieChartToolTipTemplate"),
                                DataContext = tooltipData
                            };

                            // Cập nhật vị trí tooltip tại phần của pie chart
                            tooltip.PlacementTarget = series;
                            tooltip.IsOpen = true; // Mở tooltip khi hover vào phần
                        }
                    };
                    pieSeries.Fill = new RandomColorGenerator().GetRandomColor();
                    pieSeriesList.Add(pieSeries);
                }

                PieChart.Series.Clear();
                foreach (var series in pieSeriesList)
                {
                    PieChart.Series.Add(series);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating chart: {ex.Message}");
            }
        }
    }
}
