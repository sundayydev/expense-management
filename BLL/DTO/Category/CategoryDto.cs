using System;
using System.Windows.Media;

namespace BLL.DTO.Category;

public class CategoryDto
{
   private readonly BrushConverter _brushConverter = new BrushConverter();
   public string CategoryId { get; set; }
   public string CategoryName { get; set; }
   public string CategoryType { get; set; }
   public string Description { get; set; }
   public string CreatedAt { get; set; }

   public Brush ColorCategoryType
   {
      get
      {
         return CategoryType switch
         {
            "Chi tiêu" => (Brush)_brushConverter.ConvertFromString("#FFCF9D"),
            "Thu nhập" => (Brush)_brushConverter.ConvertFromString("#7AB2D3"),
            _ => throw new ArgumentOutOfRangeException()
         };
      }
   }
   public Brush BgCategoryType
   {
      get
      {
         return CategoryType switch
         {
            "Chi tiêu" => (Brush)_brushConverter.ConvertFromString("#DE8F5F"),
            "Thu nhập" => (Brush)_brushConverter.ConvertFromString("#4A628A"),
            _ => throw new ArgumentOutOfRangeException()
         };
      }
   }
}