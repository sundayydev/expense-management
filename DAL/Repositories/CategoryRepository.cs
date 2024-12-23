using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Repositories
{
   public class CategoryRepository
   {
      private readonly MyDBContext _context = new MyDBContext();
      
      public CategoryRepository()
      { }
      
      public bool ExistsByCategoryName(string categoryName)
      {
         return _context.Categories.Any(c => c.CategoryName == categoryName);
      }
      
      public void AddCategory(Category category)
      {
         _context.Categories.Add(category);
         _context.SaveChanges();
      }
      
      public List<Category> GetCategoriesByUserId(string userId)
      {
         return _context.Categories.Where(c => c.UserId.ToString() == userId).ToList();
      }
      
      public List<Category> GetCategoriesByCategoryType(string userId, string categoryType)
      {
         return _context.Categories.Where(c => c.UserId.ToString() == userId && c.CategoryType == categoryType).ToList();
      }
   }
}