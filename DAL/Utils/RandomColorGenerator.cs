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
                "#E16A54", "#7E5CAD", "#605678", "#898121", "#3E5879",
                "#D0B8A8", "#405D72", "#DA7297", "#E88D67", "#597445",
                "#3AA6B9", "#74512D", "#FFC470", "#FA7070", "#8E7AB5",
                "#88AB8E", "#EDB7ED", "#82A0D8", "#867070", "#EA8FEA",
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
