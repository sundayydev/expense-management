using GUI.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL.DTO.Category;
using BLL.Services;
using DAL.Models;
using DAL.Utils;
using System.Windows.Threading;
using AppContext = BLL.AppContext;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcCategory.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class UcCategory : UserControl
    {
        private ObservableCollection<CategoryDto> Categories { get; set; }
        
        private readonly CategoryService _categoryService = new CategoryService();
        
        private SearchManager<CategoryDto> _searchManager;
        
        public UcCategory()
        {
            InitializeComponent();
            LoadData();
            HandleCategorySearch();
        }

        void LoadData()
        {
            CategoryDataGrid.ItemsSource = null;
            Categories = new ObservableCollection<CategoryDto>(_categoryService.GetCategoryByUserId(AppContext.Instance.UserId));
            CategoryDataGrid.ItemsSource = Categories;
        }

        void HandleCategorySearch()
        {
            // Khởi tạo SearchManager
            _searchManager = new SearchManager<CategoryDto>(
            Categories,
            results => CategoryDataGrid.ItemsSource = results, // Cập nhật DataGrid
            (category, searchText) => // Logic lọc dữ liệu
                category.CategoryName.ToLower().Contains(searchText) ||
                category.CategoryId.ToString().Contains(searchText) ||
                category.CategoryType.ToLower().Contains(searchText) ||
                category.Description.ToLower().Contains(searchText) ||
                category.CreatedAt.ToString().Contains(searchText)
            );

            CategoryDataGrid.ItemsSource = Categories;
        }

        private void BtnAddCategory_OnClick(object sender, RoutedEventArgs e)
        {
            WFormCategory wf = new WFormCategory();
            wf.ShowDialog();
            wf.Close();
            
            LoadData();
        }

        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DataGridRow row = DataGridRow.GetRowContainingElement(button);

            if (row != null)
            {
                var categoryId = (row.Item as CategoryDto)?.CategoryId;
        
                WFormCategory wf = new WFormCategory(categoryId);
                wf.ShowDialog();
                
                CategoryDto updatedCategory = _categoryService.GetCategoryByCategoryId(AppContext.Instance.UserId, categoryId);
                var existingCategory = Categories.FirstOrDefault(c => c.CategoryId == categoryId);
                if (existingCategory != null)
                {
                    int index = Categories.IndexOf(existingCategory);
                    Categories[index] = updatedCategory;
                }
                LoadData();
                
            }
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            // Lấy Button được nhấn
            Button button = sender as Button;

            // Lấy DataGridRow
            DataGridRow row = DataGridRow.GetRowContainingElement(button);

            // Lấy dữ liệu dòng
            if (row != null)
            {
                var categoryId = (row.Item as CategoryDto)?.CategoryId;
                
                try
                {
                    DialogCustoms dialog = new DialogCustoms("Bạn có muốn xóa không?", "Thông báo", DialogCustoms.YesNo);

                    if (dialog.ShowDialog() == true)
                    {
                        _categoryService.DeleteCategory(AppContext.Instance.UserId, categoryId);
                        LoadData();
                        DialogCustoms dialogSuccess = new DialogCustoms("Xóa thành công.", "Thông báo", DialogCustoms.OK);
                        dialogSuccess.ShowDialog();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchManager.OnSearchTextChanged(TxtSearch.Text);
        }
    }
}
