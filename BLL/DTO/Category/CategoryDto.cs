using System.Windows.Media;

namespace BLL.DTO.Category;

public class CategoryDto
{
   public string CategoryId { get; set; }
   public string CategoryName { get; set; }
   public string CategoryType { get; set; }
   public string Description { get; set; }
   public string CreatedAt { get; set; }
   public Brush ColorCategoryType { get; set; }
}