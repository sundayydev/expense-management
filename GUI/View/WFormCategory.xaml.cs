using System;
using System.Windows;
using System.Windows.Documents;
using BLL.DTO.Category;
using BLL.Services;
using AppContext = BLL.AppContext;

namespace GUI.View
{
    public partial class WFormCategory : Window
    {
        private readonly CategoryService _categoryService = new();
        
        public WFormCategory()
        {
            InitializeComponent();
        }

        private void BtnSaveCategory_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var addCategoryDto = new AddCategoryDto
                {
                    UserId = AppContext.Instance.UserId,
                    CategoryType = RdbExpense.IsChecked == true ? "Chi tiêu" : "Thu nhập",
                    CategoryName = TxtCategoryName.Text,
                    Description = new TextRange(RtbDescription.Document.ContentStart, RtbDescription.Document.ContentEnd).Text.Trim()
                };
                
                _categoryService.AddCategory(addCategoryDto);
                DialogCustoms dialogCustoms = new DialogCustoms("Thêm danh mục thành công.", "Thông báo", DialogCustoms.OK);
                dialogCustoms.ShowDialog();
                ResetFormCategory();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Lỗi: " + exception.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        void ResetFormCategory()
        {
            TxtCategoryId.Text = "";
            TxtCategoryName.Text = "";
            RtbDescription.Document.Blocks.Clear();
        }
    }
}