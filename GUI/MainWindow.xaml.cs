using System.Windows;

namespace GUI
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
    public partial class MainWindow
    {
        private bool _isMenuCollapsed = false;

        public MainWindow()
        {
         InitializeComponent();
        }

        private void ToggleMenu_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Đổi trạng thái menu
            _isMenuCollapsed = !_isMenuCollapsed;

            if (_isMenuCollapsed)
            {
                MenuColumn.Width = new GridLength(50); // Chỉ hiện icon
            }
            else
            {
                MenuColumn.Width = new GridLength(260); // Hiện cả icon và chữ
            }
        }
    }
}