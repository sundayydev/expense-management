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
        private static readonly int MaxRecentColors = 5; // Số lượng màu tránh trùng lặp

        public RandomColorGenerator()
        { }
        
        public Brush GetRandomColor()
        {
            var brushConverter = new BrushConverter();
            var colorHexes = new[]
            {
                "#FFF5E4", "#FFD1D1", "#FF9494", "#FF7878", "#FF4C4C",  
                "#F0D9FF", "#CAB8FF", "#C490E4", "#BFA2DB", "#949CDF",  
                "#BEDBBB", "#CCF6C8", "#CFF6CF", "#89C9B8", "#8DB596",  
                "#DDF3F5", "#E1F2FB", "#BADFDB", "#ABC2E8", "#4F91D2",
                "#FFE6A9", "#FDFFAE", "#F6EFBD", "#FFF9BF", "#FFD2A0"
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
