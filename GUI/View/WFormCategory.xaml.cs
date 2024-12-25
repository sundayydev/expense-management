using System;
using System.Windows;
using System.Windows.Documents;
using BLL.DTO.Category;
using BLL.Services;
using DAL.Models;
using AppContext = BLL.AppContext;

namespace GUI.View
{
    public partial class WFormCategory : Window
    {
        private readonly CategoryService _categoryService = new();
        private readonly string _categoryId;
        
        public WFormCategory()
        {
            InitializeComponent();
        }
        
        public WFormCategory(string categoryId)
        {
            InitializeComponent();
            this._categoryId = categoryId;
            LoadData();
        }

        private void BtnSaveCategory_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtCategoryId.Text))
                {
                    
                    var updateCategory = new Category()
                    {
                        CategoryId = TxtCategoryId.Text,
                        CategoryType = RdbExpense.IsChecked == true ? "Chi tiêu" : "Thu nhập",
                        CategoryName = TxtCategoryName.Text,
                        Description = new TextRange(RtbDescription.Document.ContentStart, RtbDescription.Document.ContentEnd).Text.Trim()
                    };
                    _categoryService.UpdateCategory(updateCategory);
                    DialogCustoms dialogCustomsUpdate = new DialogCustoms("Cập nhật danh mục thành công.", "Thông báo", DialogCustoms.OK);
                    dialogCustomsUpdate.ShowDialog();
                }
                else
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
                }
                this.Close();
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

        void LoadData()
        {
            CategoryDto category = _categoryService.GetCategoryByCategoryId(AppContext.Instance.UserId, this._categoryId);
            TxtCategoryId.Text = category.CategoryId;
            TxtCategoryName.Text = category.CategoryName;
            RtbDescription.Document.Blocks.Clear();
            RtbDescription.Document.Blocks.Add(new Paragraph(new Run(category.Description)));
            if (category.CategoryType == "Chi tiêu")
            {
                RdbExpense.IsChecked = true;
            }
            else
            {
                RdbIncome.IsChecked = true;
            }
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogCustoms diaglog = new DialogCustoms("Bạn có chắc chắn muốn hủy không?", "Xác nhận", DialogCustoms.YesNo);
            diaglog.ShowDialog();
            if (diaglog.DialogResult == true)
            {
                ResetFormCategory();
                this.Close();
            }
        }
    }
}