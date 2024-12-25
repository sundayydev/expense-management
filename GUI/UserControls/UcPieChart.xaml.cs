using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using BLL.Services;
using System.Windows.Media.Effects;  

namespace GUI.UserControls
{
    public partial class UcPieChart : UserControl
    {
        public Func<ChartPoint, string> Pointlabel { get; set; }

        private Random _random = new Random();

        private string[] colorArray = new string[]
        {
            "#B3CDE0", // Xanh dương nhạt
            "#F5B7B1", // Hồng nhạt
            "#C8E6C9", // Xanh lá nhạt
            "#FFE893", // Vàng sáng nhẹ
            "#FFCDD2", // Đỏ nhạt
            "#D1C4E9",
            "#85A98F"
        };

        private readonly ExpenseService _expenseService = new ExpenseService();  
        private string userId = BLL.AppContext.Instance.UserId;  
        ExpenseService expense = new ExpenseService();
        public UcPieChart()
        {
            InitializeComponent();
            Pointlabel = chartPoint => string.Format("{0} - {1:N0} VND ({2:P})", chartPoint.SeriesView.Title, chartPoint.Y, chartPoint.Participation);


            UpdateChartData(); 
            DataContext = this;
            decimal totalExpense = expense.GetTotalExpensesByUserId(BLL.AppContext.Instance.UserId);
            //txtTotal.Text = $"{totalExpense:N0} VND";
        }

        private List<string> usedColors = new List<string>();

        private string GetRandomColor()
        {
            string color;
            do
            {
                color = colorArray[_random.Next(colorArray.Length)];
            }
            while (usedColors.Contains(color));

            usedColors.Add(color);
            return color;
        }

        private void UpdateChartData()
        {
            var dailyExpenses = _expenseService.GetDailyExpenses(userId);
            var groupedExpenses = dailyExpenses
                .GroupBy(e => e.CategoryId)  
                .Select(group => new
                {
                    CategoryId = group.Key,
                    TotalAmount = group.Sum(e => e.Amount),
                    CategoryName = group.First().Note 
                })
                .ToList();

            var pieSeriesList = new List<PieSeries>();
            foreach (var expense in groupedExpenses)
            {
                var pieSeries = new PieSeries
                {
                    Title = expense.CategoryName ?? "null",
                    Values = new ChartValues<int> { (int)expense.TotalAmount },
                    DataLabels = true,
                    LabelPoint = chartPoint => string.Format("{0:N0} VND", chartPoint.Y),


            };

                // Gán màu ngẫu nhiên cho từng danh mục
                string color = GetRandomColor();
                pieSeries.Fill = (Brush)new BrushConverter().ConvertFrom(color);
                pieSeriesList.Add(pieSeries);
            }

            PieChart.Series.Clear();
            foreach (var series in pieSeriesList)
            {
                PieChart.Series.Add(series);
            }
        }

        private PieSeries _selectedSeries = null;

        private void PieChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            if (_selectedSeries != null)
            {
                _selectedSeries.PushOut = 0;
                _selectedSeries.Stroke = new SolidColorBrush(Colors.Transparent);
                _selectedSeries.StrokeThickness = 0;
                _selectedSeries.Effect = null;
            }

            if (_selectedSeries == chartPoint.SeriesView)
            {
                _selectedSeries = null;  
                return;  
            }

            _selectedSeries = (PieSeries)chartPoint.SeriesView;
            _selectedSeries.PushOut = 8;  
            _selectedSeries.Stroke = new SolidColorBrush(Colors.Gray);  
            _selectedSeries.StrokeThickness = 1;  
            _selectedSeries.Effect = new DropShadowEffect  
            {
                Color = Colors.Gray,
                Direction = 470,
                ShadowDepth = 30,
                Opacity = 0.8
            };
            var totalAmount = chartPoint.Y;  
            var categoryName = chartPoint.SeriesView.Title;  
            var percentage = chartPoint.Participation * 100;  

            InfoTextBlock.Text = $"{categoryName}: {totalAmount:N0} VND ({percentage:N2}%)";
        }
    }
}
