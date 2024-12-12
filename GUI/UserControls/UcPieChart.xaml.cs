using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.Generic;
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
            "#FFEB3B", // Vàng sáng nhẹ
            "#FFCDD2", // Đỏ nhạt
            "#D1C4E9"
        };

        public UcPieChart()
        {
            InitializeComponent();

            Pointlabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            var pieChart = this.PieChart; 

            var seriesList = new[]
            {
                new PieSeries { Title = "Game", Values = new ChartValues<int> { 3 }, DataLabels = true, LabelPoint = Pointlabel },
                new PieSeries { Title = "Sim", Values = new ChartValues<int> { 4 }, DataLabels = true, LabelPoint = Pointlabel },
                new PieSeries { Title = "Cafe", Values = new ChartValues<int> { 6 }, DataLabels = true, LabelPoint = Pointlabel },
                new PieSeries { Title = "Beer", Values = new ChartValues<int> { 2 }, DataLabels = true, LabelPoint = Pointlabel }
            };

            for (int i = 0; i < seriesList.Length; i++)
            {
                string color = GetRandomColor(); 
                seriesList[i].Fill = (Brush)new BrushConverter().ConvertFrom(color);
            }

            pieChart.Series.Clear();
            foreach (var series in seriesList)
            {
                pieChart.Series.Add(series);
            }
            DataContext = this;
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

        }
    }
}
