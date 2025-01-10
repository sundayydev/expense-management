using BLL.DTO.Recipient;
using BLL.Services;
using DAL.Utils;
using GUI.View;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using AppContext = BLL.AppContext;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcRecipient.xaml
    /// </summary>
    public partial class UcRecipient : UserControl
    {
        private ObservableCollection<RecipientDto> Recipients { get; set; }
        private readonly RecipientService _recipientService = new RecipientService();
        private SearchManager<RecipientDto> _searchManager;

        public UcRecipient()
        {
            InitializeComponent();
            LoadData();
            HandleRecipientSearch();
            RecipientDataGrid.ItemsSource = Recipients;
        }
        
        void LoadData()
        {
            RecipientDataGrid.ItemsSource = null;
            Recipients = new ObservableCollection<RecipientDto>(_recipientService.GetRecipientsByUserId(AppContext.Instance.UserId));
            RecipientDataGrid.ItemsSource = Recipients;
        }

        void HandleRecipientSearch()
        {
            // Khởi tạo SearchManager
            _searchManager = new SearchManager<RecipientDto>(
            Recipients,
            results => RecipientDataGrid.ItemsSource = results, // Cập nhật DataGrid
            (recipient, searchText) => // Logic lọc dữ liệu
                recipient.RecipientName.ToLower().Contains(searchText) ||
                recipient.Description.ToLower().Contains(searchText) ||
                recipient.CreatedAt.ToString().Contains(searchText)
            );

            RecipientDataGrid.ItemsSource = Recipients;
        }

        private void BtnAddRecipient_OnClick(object sender, RoutedEventArgs e)
        {
            WFormRecipient wf = new WFormRecipient();
            wf.ShowDialog();
            LoadData();
        }
        
        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DataGridRow row = DataGridRow.GetRowContainingElement(button);

            if (row != null)
            {
                var recipientId = (row.Item as RecipientDto)?.RecipientId;
        
                WFormRecipient wf = new WFormRecipient(recipientId);
                wf.ShowDialog();
                
                RecipientDto updatedCategory = _recipientService.GetRecipientByRecipientId(recipientId);
                var existingRecipient = Recipients.FirstOrDefault(r => r.RecipientId == recipientId);
                if (existingRecipient != null)
                {
                    int index = Recipients.IndexOf(existingRecipient);
                    Recipients[index] = updatedCategory;
                }
                LoadData();
            }
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            // Lấy Button được nhấn
            Button button = sender as Button;

            // Lấy DataGridRow
            DataGridRow row = DataGridRow.GetRowContainingElement(button);

            // Lấy dữ liệu dòng
            if (row != null)
            {
                var recipientId = (row.Item as RecipientDto)?.RecipientId;
                
                try
                {
                    DialogCustoms dialog = new DialogCustoms("Bạn có muốn xóa không?", "Thông báo", DialogCustoms.YesNo);

                    if (dialog.ShowDialog() == true)
                    {
                        _recipientService.DeleteRecipient(recipientId);
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

