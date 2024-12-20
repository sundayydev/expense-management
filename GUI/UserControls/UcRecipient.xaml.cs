using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcRecipient.xaml
    /// </summary>
    public partial class UcRecipient : UserControl
    {
        private ObservableCollection<Recipient> Recipients { get; set; }

        public UcRecipient()
        {
            InitializeComponent();
            LoadData();
            InvoiceDataGrid.ItemsSource = Recipients;
        }
        private void LoadData()
        {
            var brushConverter = new BrushConverter();
            Recipients = new ObservableCollection<Recipient>
            {
                new Recipient{RecipientID = "R001", Character = "N", RecipientName = "Ngô Mạnh Hùng", CharacterColor = (Brush)brushConverter.ConvertFromString("#FCC737"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Không có gì cả"},
                new Recipient{RecipientID = "R002", Character = "L", RecipientName = "Lê Hữu Duy Hoàng", CharacterColor = (Brush)brushConverter.ConvertFromString("#89A8B2"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Không là gì cả"},
                new Recipient{RecipientID = "R003", Character = "L", RecipientName = "Lê Hoài Huân", CharacterColor = (Brush)brushConverter.ConvertFromString("#BC7C7C"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Không quan tâm"},
                new Recipient{RecipientID = "R004", Character = "P", RecipientName = "Phan Thành Tài", CharacterColor = (Brush)brushConverter.ConvertFromString("#4B88A2"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Chỉ là bánh bèo"},
                new Recipient{RecipientID = "R005", Character = "T", RecipientName = "Trần Bá Trí", CharacterColor = (Brush)brushConverter.ConvertFromString("#F99F1C"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Không gì đặc biệt"},
                new Recipient{RecipientID = "R006", Character = "H", RecipientName = "Huỳnh Nguyễn Khang", CharacterColor = (Brush)brushConverter.ConvertFromString("#D45656"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Cần thêm chi tiết"},
                new Recipient{RecipientID = "R007", Character = "D", RecipientName = "Dương Minh Hải", CharacterColor = (Brush)brushConverter.ConvertFromString("#23CBA0"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Rất bình thường"},
                new Recipient{RecipientID = "R008", Character = "C", RecipientName = "Cao Quốc Thành", CharacterColor = (Brush)brushConverter.ConvertFromString("#FAA61A"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Ít giao tiếp"},
                new Recipient{RecipientID = "R009", Character = "H", RecipientName = "Hoàng Thị Kim Liên", CharacterColor = (Brush)brushConverter.ConvertFromString("#94B49F"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Không gây chú ý"},
                new Recipient{RecipientID = "R010", Character = "V", RecipientName = "Võ Hoàng Nhật", CharacterColor = (Brush)brushConverter.ConvertFromString("#7B61FF"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Đang học lập trình"},
                new Recipient{RecipientID = "R011", Character = "B", RecipientName = "Bùi Thành Nam", CharacterColor = (Brush)brushConverter.ConvertFromString("#FF6F61"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Thích màu đỏ"},
                new Recipient{RecipientID = "R012", Character = "N", RecipientName = "Nguyễn Thị Thu Hồng", CharacterColor = (Brush)brushConverter.ConvertFromString("#59C173"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Luôn vui vẻ"},
                new Recipient{RecipientID = "R013", Character = "M", RecipientName = "Mai Thị Quỳnh Như", CharacterColor = (Brush)brushConverter.ConvertFromString("#F97316"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Thấy hay thì làm"},
                new Recipient{RecipientID = "R014", Character = "T", RecipientName = "Trần Quang Minh", CharacterColor = (Brush)brushConverter.ConvertFromString("#A9D6E5"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Thập kỷ hoạch"},
                new Recipient{RecipientID = "R015", Character = "L", RecipientName = "Lại Minh Vũ", CharacterColor = (Brush)brushConverter.ConvertFromString("#EE6C4D"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Sống hết mình"},
                new Recipient{RecipientID = "R016", Character = "H", RecipientName = "Hà Thị Tuyết", CharacterColor = (Brush)brushConverter.ConvertFromString("#5B9279"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Thắng thắn"},
                new Recipient{RecipientID = "R017", Character = "P", RecipientName = "Phạm Đức Anh", CharacterColor = (Brush)brushConverter.ConvertFromString("#F4A261"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Học sinh gương mẫu"},
                new Recipient{RecipientID = "R018", Character = "V", RecipientName = "Vũ Thành Long", CharacterColor = (Brush)brushConverter.ConvertFromString("#8E9A7B"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Thầm lặng"},
                new Recipient{RecipientID = "R019", Character = "D", RecipientName = "Dõ Hoàng Bảo", CharacterColor = (Brush)brushConverter.ConvertFromString("#DEB887"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Yêu thích bóng đá"},
                new Recipient{RecipientID = "R020", Character = "K", RecipientName = "Kiều Phúc Hảo", CharacterColor = (Brush)brushConverter.ConvertFromString("#ADB36E"), CreatedAt = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"), Description = "Luôn đầy năng lượng"}
            };

        }

        private void BtnAddRecipient_Click(object sender, RoutedEventArgs e)
        {
            /* WFormRecipient wf = new WFormRecipient();
             wf.ShowDialog();*/
        }
    }

    public class Recipient
    {
        public string RecipientID { get; set; }
        public string RecipientName { get; set; }
        public string Character { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public Brush CharacterColor { get; set; }

    }
}

