using System;
using System.Collections.Generic;
using System.Windows.Media;
using BLL.DTO.Category;
using DAL.Models;
using DAL.Repositories;
using DAL.Utils;

namespace BLL.Services;

public class CategoryService
{
   private readonly CategoryRepository _categoryRepository = new();

   public void AddCategory(AddCategoryDto addCategoryDto)
   {
      if (_categoryRepository.ExistsByCategoryName(addCategoryDto.CategoryName))
      {
         throw new InvalidOperationException("Tên danh mục đã tồn tại.");
      }
      
      var category = new Category
      {
         CategoryId = new CodeGenerator().GenerateCodeCategory(),
         UserId = Guid.Parse(addCategoryDto.UserId),
         CategoryType = addCategoryDto.CategoryType,
         CategoryName = addCategoryDto.CategoryName,
         Description = addCategoryDto.Description,
         CreatedAt = DateTime.Now
      };
      
      _categoryRepository.AddCategory(category);
   }
   
   public List<CategoryDto> GetCategoryByUserId(string userId)
   {
      List<Category> list =_categoryRepository.GetCategoriesByUserId(userId);

      List<CategoryDto> listDto = new();
      
      var brushConverter = new BrushConverter();
      
      foreach (var item in list)
      {
         CategoryDto categoryDto = new()
         {
            CategoryId = item.CategoryId.ToString(),
            CategoryName = item.CategoryName,
            CategoryType = item.CategoryType,
            Description = item.Description,
            CreatedAt = item.CreatedAt.Value.ToString("ss:mm:hh dd/MM/yyyy"),
            ColorCategoryType = item.CategoryType == "Chi tiêu" ? (Brush)brushConverter.ConvertFromString("#DC8686") : (Brush)brushConverter.ConvertFromString("#AAD9BB")
         };
         
         listDto.Add(categoryDto);
      }
      
      return listDto;
   }
   
   public List<CmbCategoryDto> GetCategoriesByCategoryType(string userId, string categoryType)
   {
      List<Category> list = _categoryRepository.GetCategoriesByCategoryType(userId, categoryType);

      List<CmbCategoryDto> listDto = new();
      
      foreach (var item in list)
      {
         CmbCategoryDto cmbCategoryDto = new()
         {
            CategoryId = item.CategoryId.ToString(),
            CategoryName = item.CategoryName,
            CategoryType = item.CategoryType
         };
         
         listDto.Add(cmbCategoryDto);
      }
      
      return listDto;
   }
}