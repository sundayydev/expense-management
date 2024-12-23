using GUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using BLL.DTO.Category;
using BLL.Services;
using AppContext = BLL.AppContext;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcCategory.xaml
    /// </summary>
    public partial class UcCategory : UserControl
    {
        private List<CategoryDto> Categories { get; set; }
        
        private readonly CategoryService _categoryService = new();
        
        public UcCategory()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData()
        {
            Categories = new List<CategoryDto>();
            Categories = _categoryService.GetCategoryByUserId(AppContext.Instance.UserId);
            CategoryDataGrid.ItemsSource = Categories;
        }

        private void BtnAddCategory_OnClick(object sender, RoutedEventArgs e)
        {
            WFormCategory wf = new WFormCategory();
            wf.ShowDialog();
            wf.Close();
            
            LoadData();
        }
    }
}
