using GUI.View;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcCategory.xaml
    /// </summary>
    public partial class UcCategory : UserControl
    {
        private ObservableCollection<Category> Categories { get; set; }
        public UcCategory()
        {
            InitializeComponent();
            LoadData();
            CategoryDataGrid.ItemsSource = Categories;
        }

        void LoadData()
        {
            var brushConverter = new BrushConverter();
            Categories = new ObservableCollection<Category>
            {
                new Category
                {
                    CategoryId = "1", CategoryName = "Thức ăn", CategoryType = "Chi phí",
                    Description = "Chi tiêu cho đồ ăn và thức uống hàng ngày",
                    CreatedAt = DateTime.Now.ToString("ss:mm:hh dd/MM/yyyy"),
                    ColorCategoryType = (Brush)brushConverter.ConvertFromString("#DC8686")
                },
                new Category
                {
                    CategoryId = "2", CategoryName = "Giải trí", CategoryType = "Chi phí",
                    Description = "Phim ảnh, chơi game, và các hoạt động giải trí khác",
                    CreatedAt = DateTime.Now.ToString("ss:mm:hh dd/MM/yyyy"),
                    ColorCategoryType = (Brush)brushConverter.ConvertFromString("#DC8686")
                },
                new Category
                {
                    CategoryId = "3", CategoryName = "Lương", CategoryType = "Thu nhập",
                    Description = "Thu nhập từ công việc chính",
                    CreatedAt = DateTime.Now.ToString("ss:mm:hh dd/MM/yyyy"),
                    ColorCategoryType = (Brush)brushConverter.ConvertFromString("#AAD9BB")
                },
                new Category
                {
                    CategoryId = "4", CategoryName = "Tiền thuê nhà", CategoryType = "Chi phí",
                    Description = "Chi phí thuê nhà hàng tháng",
                    CreatedAt = DateTime.Now.ToString("ss:mm:hh dd/MM/yyyy"),
                    ColorCategoryType = (Brush)brushConverter.ConvertFromString("#DC8686")
                },
                new Category
                {
                    CategoryId = "5", CategoryName = "Quà tặng", CategoryType = "Chi phí",
                    Description = "Chi phí mua quà tặng bạn bè, gia đình",
                    CreatedAt = DateTime.Now.ToString("ss:mm:hh dd/MM/yyyy"),
                    ColorCategoryType = (Brush)brushConverter.ConvertFromString("#DC8686")
                },
                new Category
                {
                    CategoryId = "6", CategoryName = "Tiền thưởng", CategoryType = "Thu nhập",
                    Description = "Khoản tiền thưởng từ công việc",
                    CreatedAt = DateTime.Now.ToString("ss:mm:hh dd/MM/yyyy"),
                    ColorCategoryType = (Brush)brushConverter.ConvertFromString("#AAD9BB")
                },
                new Category
                {
                    CategoryId = "7", CategoryName = "Hóa đơn điện nước", CategoryType = "Chi phí",
                    Description = "Hóa đơn thanh toán điện nước hàng tháng",
                    CreatedAt = DateTime.Now.ToString("ss:mm:hh dd/MM/yyyy"),
                    ColorCategoryType = (Brush)brushConverter.ConvertFromString("#DC8686")
                },
                new Category
                {
                    CategoryId = "8", CategoryName = "Đầu tư", CategoryType = "Thu nhập",
                    Description = "Thu nhập từ các khoản đầu tư",
                    CreatedAt = DateTime.Now.ToString("ss:mm:hh dd/MM/yyyy"),
                    ColorCategoryType = (Brush)brushConverter.ConvertFromString("#AAD9BB")
                },
                new Category
                {
                    CategoryId = "9", CategoryName = "Di chuyển", CategoryType = "Chi phí",
                    Description = "Chi phí di chuyển như xăng xe, vé xe buýt",
                    CreatedAt = DateTime.Now.ToString("ss:mm:hh dd/MM/yyyy"),
                    ColorCategoryType = (Brush)brushConverter.ConvertFromString("#DC8686")
                },
                new Category
                {
                    CategoryId = "10", CategoryName = "Học phí", CategoryType = "Chi phí",
                    Description = "Chi phí học hành, giáo dục",
                    CreatedAt = DateTime.Now.ToString("ss:mm:hh dd/MM/yyyy"),
                    ColorCategoryType = (Brush)brushConverter.ConvertFromString("#DC8686")
                }
            };
        }

        private void BtnAddCategory_OnClick(object sender, RoutedEventArgs e)
        {
            WFormCategory wf = new WFormCategory();
            wf.ShowDialog();
            wf.Close();
        }
    }

    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryType { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public Brush ColorCategoryType { get; set; }
    }
}
