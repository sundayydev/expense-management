using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcLiveChart.xaml
    /// </summary>
    public partial class UcLiveChart : UserControl
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; } 
        public Func<double, string> Values { get; set; } 

        public UcLiveChart()
        {
            InitializeComponent();
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

           
            Values = value => value.ToString();

            
            DataContext = this;
        }
    }
}
