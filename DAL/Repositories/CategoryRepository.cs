using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
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
         return _context.Categories
            .AsNoTracking() // Không theo dõi các thực thể
            .Where(c => c.UserId.ToString() == userId || c.UserId == null)
            .ToList();
      }
      
      public List<Category> GetCategoriesByCategoryType(string userId, string categoryType)
      {
         return _context.Categories.Where(c => c.UserId.ToString() == userId || c.UserId == null && c.CategoryType == categoryType).ToList();
      }
      
      public Category GetCategoryByCategoryId(string userId, string categoryId)
      {
         return _context.Categories.FirstOrDefault(c => c.UserId.ToString() == userId && c.CategoryId == categoryId);
      }

      public void UpdateCategory(Category category)
      {
         var categoryUpdate = _context.Categories.FirstOrDefault(c => c.CategoryId == category.CategoryId);

         if (categoryUpdate != null)
         {
            categoryUpdate.CategoryName = category.CategoryName;
            categoryUpdate.CategoryType = category.CategoryType;
            categoryUpdate.Description = category.Description;
         }
         
         _context.Categories.AddOrUpdate(categoryUpdate);
         _context.SaveChanges();
      }
      
      public void DeleteCategory(Category category)
      {
         _context.Categories.Remove(category);
         _context.SaveChanges();
      }
   }
}