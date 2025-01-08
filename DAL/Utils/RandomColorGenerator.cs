using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace DAL.Utils
{
    public class RandomColorGenerator
    {
        private List<Brush> _colorPalette = new List<Brush>();
        

        private static readonly Random Random = new Random();
        private static readonly Queue<Brush> RecentColors = new Queue<Brush >();
        private static readonly int MaxRecentColors = 10; // Số lượng màu tránh trùng lặp

        public RandomColorGenerator()
        { }
        
        public Brush GetRandomColor()
        {
            var brushConverter = new BrushConverter();
            var colorHexes = new[]
            {
                "#CCD6A6", "#FFD1D1", "#FF9494", "#FFC1B6", "#FF4C4C",  
                "#F0D9FF", "#CAB8FF", "#C490E4", "#8174A0", "#949CDF",  
                "#BEDBBB", "#91C788", "#B1C29E", "#89C9B8", "#8DB596",
                "#1D97C1", "#E1F2FB", "#F39E60", "#ABC2E8", "#4F91D2",
                "#F0BB78", "#70AF85", "#F8E1B7", "#E6BA95", "#FFD2A0"
            };


            _colorPalette = colorHexes
                // ReSharper disable once PossibleNullReferenceException
                .Select(hex => (Brush)brushConverter.ConvertFromString(hex)) 
                .ToList();
            
            // Lấy danh sách màu khả dụng (loại trừ các màu gần đây)
            var availableColors = _colorPalette.Except<Brush >(RecentColors).ToList();

            // Nếu không còn màu khả dụng, làm mới danh sách
            if (availableColors.Count == 0)
            {
                ((Queue<Brush>)RecentColors).Clear(); // Xóa danh sách màu gần đây
                availableColors = _colorPalette;
            }

            // Random từ danh sách màu khả dụng
            int index = Random.Next(availableColors.Count);
            Brush selectedColor = availableColors[index];

            // Thêm màu được chọn vào danh sách màu gần đây
            RecentColors.Enqueue(selectedColor);
            if (RecentColors.Count > MaxRecentColors)
            {
                RecentColors.Dequeue();
            }

            return selectedColor;
        }
        
    }
}
