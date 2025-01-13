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
            "Chi tiêu" => (Brush)_brushConverter.ConvertFromString("#ff562f"),
            "Thu nhập" => (Brush)_brushConverter.ConvertFromString("#04a08b"),
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
            "Chi tiêu" => (Brush)_brushConverter.ConvertFromString("#ffeae5"),
            "Thu nhập" => (Brush)_brushConverter.ConvertFromString("#d1f5f0"),
            _ => throw new ArgumentOutOfRangeException()
         };
      }
   }
}