using GUI.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using BLL.Services;
using AppContext = BLL.AppContext;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for WFormExpense.xaml
    /// </summary>
    public partial class WFormExpense : Window
    {
        private readonly CategoryService _categoryService = new();
        public WFormExpense()
        {
            InitializeComponent();
        }

        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogCustoms cancelDialog = new DialogCustoms("Hủy thao tác", "Thông báo", DialogCustoms.Show);
            cancelDialog.ShowDialog();
            this.Close();
        }

        void LoadCmbCategories()
        {
            CmbCategory.ItemsSource = _categoryService.GetCategoriesByCategoryType(AppContext.Instance.UserId, "Chi tiêu");
            CmbCategory.DisplayMemberPath = "CategoryName";
            CmbCategory.SelectedValuePath = "CategoryId";
        }

        private void WFormExpense_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadCmbCategories();
        }
    }
}