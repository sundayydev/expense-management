using BLL.DTO.Income;
using BLL.Services;
using DAL.Utils;
using GUI.View;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AppContext = BLL.AppContext;

namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcIncome.xaml
    /// </summary>
    public partial class UcIncome : UserControl
    {
        private ObservableCollection<IncomeDto> Incomes { get; set; }
        private readonly IncomeService _incomeService = new IncomeService();
        private SearchManager<IncomeDto> _searchManager;
        
        public UcIncome()
        {
            InitializeComponent();
            LoadData();
            IncomeDataGrid.ItemsSource = Incomes;
            HandleIncomeSearch();
        }
        
        private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            WFormIncome wf = new WFormIncome();
            wf.ShowDialog();
            LoadData();
        }
        
        void LoadData()
        {
            IncomeDataGrid.ItemsSource = null;
            Incomes = new ObservableCollection<IncomeDto>(_incomeService.GetIncomeByUserId(AppContext.Instance.UserId));
            IncomeDataGrid.ItemsSource = Incomes;
        }

        void HandleIncomeSearch()
        {
            // Khởi tạo SearchManager
            _searchManager = new SearchManager<IncomeDto>(
            Incomes,
            results => IncomeDataGrid.ItemsSource = results, // Cập nhật DataGrid
            (recipient, searchText) => // Logic lọc dữ liệu
                recipient.CategoryName.ToLower().Contains(searchText) ||
                recipient.Note.ToLower().Contains(searchText) ||
                recipient.Amount.ToString(CultureInfo.InvariantCulture).Contains(searchText) ||
                recipient.CreatedAt.ToString(CultureInfo.InvariantCulture).Contains(searchText)
            );

            IncomeDataGrid.ItemsSource = Incomes;
        }
        
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var income = (IncomeDto)IncomeDataGrid.SelectedItem;
            if (income == null)
            {
                DialogCustoms chose = new DialogCustoms("Vui lòng chọn một dòng để xóa.", "Thông báo", DialogCustoms.Show);
                return;
            }
            DialogCustoms remove = new DialogCustoms("Bạn có chắc chắn muốn xóa dòng này không?", "Xác nhận", DialogCustoms.YesNo);
            if (remove.ShowDialog() == true)
            {
                try
                {
                    _incomeService.DeleteIncome(income.IncomeId);
                    DialogCustoms success = new DialogCustoms("Xóa thu nhập thành công.", "Thông báo", DialogCustoms.Show);
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            DataGridRow row = DataGridRow.GetRowContainingElement(button);
            if (row != null)
            {
                var incomeDto = row.Item as IncomeDto;
                if (incomeDto != null)
                {
                    var income = _incomeService.GetIncomeById(incomeDto.IncomeId);

                    if (income != null)
                    {
                        WFormIncome wf = new WFormIncome(income);
                        var result = wf.ShowDialog();
                        if (result == true)
                        {
                            LoadData();
                        }
                    }
                    else
                    {
                        DialogCustoms error = new DialogCustoms("Không tìm thấy thông tin thu nhập.", "Lỗi", DialogCustoms.Show);
                        error.ShowDialog();
                    }
                }
                LoadData();
            }
        }

        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchManager.OnSearchTextChanged(TxtSearch.Text);
        }
    } 
}

