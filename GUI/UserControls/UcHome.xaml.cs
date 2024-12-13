using System.Windows.Controls;
using System.Windows.Media;

namespace GUI.UserControls
{
   public partial class UcHome : UserControl
   {
      public UcHome()
      {
            InitializeComponent();
            AddMonthsToComboBox();
        }
        private void AddMonthsToComboBox()
        {
            string[] months = new string[]
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            };

            // Lặp qua mảng tháng và thêm vào ComboBox
            foreach (var month in months)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem
                {
                    Content = month
                };
                cmbMonth.Items.Add(comboBoxItem);
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMonth.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)cmbMonth.SelectedItem;
                cmbMonth.Foreground = new SolidColorBrush(Colors.White);  
            }
        }
    }
}