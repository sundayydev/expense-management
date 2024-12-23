namespace BLL.DTO.Category;

public class AddCategoryDto
{
   public string CategoryId { get; set; }
   public string UserId { get; set; }
   public string CategoryType { get; set; }
   public string CategoryName { get; set; }
   public string Description { get; set; }
}