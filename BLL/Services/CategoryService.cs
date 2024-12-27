using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
   
   public ObservableCollection<CategoryDto> GetCategoryByUserId(string userId)
   {
      ObservableCollection<Category> list = new ObservableCollection<Category>(_categoryRepository.GetCategoriesByUserId(userId));

      ObservableCollection<CategoryDto> listDto = new();
      
      var brushConverter = new BrushConverter();
      
      foreach (var item in list)
      {
         CategoryDto categoryDto = new()
         {
            CategoryId = item.CategoryId.ToString(),
            CategoryName = item.CategoryName,
            CategoryType = item.CategoryType,
            Description = item.Description,
            CreatedAt = item.CreatedAt.Value.ToString("HH:mm:ss dd/MM/yyyy"),
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

   public CategoryDto GetCategoryByCategoryId(string userId, string categoryId)
   {
      Category item = _categoryRepository.GetCategoryByCategoryId(userId, categoryId);
      return new CategoryDto()
      {
         CategoryId = item.CategoryId.ToString(),
         CategoryName = item.CategoryName,
         CategoryType = item.CategoryType,
         Description = item.Description,
         CreatedAt = item.CreatedAt.Value.ToString("ss:mm:hh dd/MM/yyyy"),
      };
   }
   
   public void UpdateCategory(Category category)
   {
      try
      {
         _categoryRepository.UpdateCategory(category);
      }
      catch (Exception ex)
      {
         throw new InvalidOperationException("Cập nhật danh mục thất bại.");
      }
   }
   
   public void DeleteCategory(string userId, string categoryId)
   {
      var category = _categoryRepository.GetCategoryByCategoryId(userId, categoryId);
      if (category == null)
      {
         throw new Exception("Danh mục này không xóa được.");
      }
      _categoryRepository.DeleteCategory(category);
   }
}