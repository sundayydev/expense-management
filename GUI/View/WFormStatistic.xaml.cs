using DAL.Models;
using DAL.Repositories;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL.Services;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for WFormStatistic.xaml
    /// </summary>
    public partial class WFormStatistic : Window
    {
        private readonly ExpenseRepository _expenseRepository = new();
        private readonly IncomeRepository _incomeRepository = new();
        private readonly ExpenseService _expenseService = new();
        private readonly IncomeService _incomeService = new();
        public WFormStatistic()
        {
            InitializeComponent();
            LoadMonths(); 
        }
        private void rdbDay_Checked(object sender, RoutedEventArgs e)
        {
            txtDay.Visibility = Visibility.Visible;
            CmbMonth.Visibility = Visibility.Collapsed;
            CmbMonth.IsEnabled = true;
            CmbYear.IsEnabled = false;
            CmbYear.Items.Clear();
        }
        private void rdbMonth_Checked(object sender, RoutedEventArgs e)
        {
            CmbMonth.Visibility = Visibility.Visible;
            bdMonth.Visibility = Visibility.Visible;
            CmbMonth.IsEnabled = true;
            CmbYear.IsEnabled = true;
            CmbYear.Items.Clear();
            LoadMonths();
        }
        private void rdbFull_Checked(object sender, RoutedEventArgs e)
        {
            txtDay.Visibility = Visibility.Hidden;
            CmbMonth.IsEnabled=false;
            CmbYear.IsEnabled = false;
            CmbYear.Items.Clear();
            CmbMonth.Items.Clear();
        }

        public void LoadMonths()
        {
            var userId = BLL.AppContext.Instance.UserId;
            var months = _expenseRepository.GetMonths(userId).Union(_incomeRepository.GetMonths(userId)).Distinct().OrderBy(m => m).ToList();
            CmbMonth.Items.Clear();

            foreach (var month in months)
            {
                CmbMonth.Items.Add(month);
            }
        }

        public void LoadYear()
        {
            var userId = BLL.AppContext.Instance.UserId;
            var months = _expenseRepository.GetYear(userId).Union(_incomeRepository.GetYear(userId)).Distinct().OrderBy(m => m).ToList();
            CmbYear.Items.Clear();

            foreach (var month in months)
            {
                CmbYear.Items.Add(month);
            }
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            var userId = BLL.AppContext.Instance.UserId;
            try
            {
               // bool isMonthSelected = CmbMonth.SelectedItem != null;
                
                // Kiểm tra xem đã chọn bất kỳ radio button nào
                bool isAllSelected = rdbFull.IsChecked == true;
                bool isDaySelected = rdbDay.IsChecked == true;
                bool isMonthSelected = rdbMonth.IsChecked == true;

                if (!isAllSelected && !isDaySelected && !isMonthSelected)
                {
                    MessageBox.Show("Vui lòng chọn ngày, tháng hoặc 'Tất cả'.");
                    return;
                }

                using (var context = new MyDBContext())
                {
                    List<Expens> expenses = new List<Expens>();
                    List<Income> incomes = new List<Income>();
                    // Nếu chọn Tháng
                    if (isMonthSelected)
                    {
                        if (CmbMonth.Items.Count == 0)
                        {
                            MessageBox.Show("Không có tháng nào.");
                            return;
                        }
                        if (CmbMonth.SelectedItem == null)
                        {
                            MessageBox.Show("Vui lòng chọn tháng.");
                            return;
                        }

                        int selectedMonth = Convert.ToInt32(CmbMonth.SelectedItem);
                        int selectedYear = DateTime.Now.Year;

                        if (CmbYear.SelectedItem != null)
                        {
                            selectedYear = Convert.ToInt32(CmbYear.SelectedItem);
                        }

                        expenses = _expenseService.GetMonthlyExpenses(userId, selectedMonth, selectedYear);
                        incomes = _incomeService.GetMonthlyIncome(userId, selectedMonth, selectedYear);

                        if (!expenses.Any() && !incomes.Any())
                        {
                            MessageBox.Show("Không có dữ liệu chi tiêu hoặc thu nhập cho người dùng này.");
                            return;
                        }

                    }
                    // Nếu chọn Ngày
                    else if (isDaySelected)
                    {
                        if (string.IsNullOrEmpty(txtDay.Text))
                        {
                            MessageBox.Show("Vui lòng ghi ngày. (dd/MM/yyyy) ");
                            return;
                        }
                        if (!DateTime.TryParseExact(txtDay.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime selectedDate))
                        {
                            MessageBox.Show("Định dạng ngày không đúng. Vui lòng nhập ngày theo định dạng dd/MM/yyyy.");
                            return;
                        }
                        string formattedDate = selectedDate.ToString("dd/MM/yyyy");
                        expenses = _expenseService.GetExpenseByDate(userId, selectedDate);
                        incomes = _incomeService.GetIncomeByDate(userId, selectedDate);

                        if (!expenses.Any() && !incomes.Any())
                        {
                            MessageBox.Show("Không có dữ liệu chi tiêu hoặc thu nhập cho người dùng này.");
                            return;
                        }
                    }
                
                    else if (isAllSelected)
                    {
                        expenses = _expenseService.GetAllExpensesByUserId(userId);

                        incomes = _incomeService.GetAll(userId);

                        if (!expenses.Any() && !incomes.Any())
                        {
                            MessageBox.Show("Không có dữ liệu chi tiêu hoặc thu nhập cho người dùng này.");
                            return;
                        }
                    }

                    string fileName;

                    if (isMonthSelected)
                    {
                        int selectedMonth = Convert.ToInt32(CmbMonth.SelectedItem);
                        int selectedYear = DateTime.Now.Year;

                        if (CmbYear.SelectedItem != null)
                        {
                            selectedYear = Convert.ToInt32(CmbYear.SelectedItem);
                        }

                        fileName = $"Data_Export_{selectedMonth}_{selectedYear}";

                    }
                    else if (isDaySelected)
                    {
                        DateTime.TryParse(txtDay.Text, out DateTime selectedDateName);
                        fileName = $"Data_Export_{selectedDateName.ToString("dd-MM-yyyy")}";
                    }
                    else
                    {
                        fileName = "Data_Export_Full";
                    }


                    var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                    {
                        Filter = "Excel Files|*.xlsx",
                        FileName = fileName
                    };
                    ResetControls();
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        IWorkbook workbook = new XSSFWorkbook();
                        ISheet sheet = workbook.CreateSheet("Expenses");

                        ICellStyle boldStyle = workbook.CreateCellStyle();
                        IFont boldFont = workbook.CreateFont();
                        boldFont.IsBold = true;
                        boldStyle.SetFont(boldFont);

                        IRow expenseTitleRow = sheet.CreateRow(0);
                        expenseTitleRow.CreateCell(0).SetCellValue("Chi Tiêu");
                        expenseTitleRow.GetCell(0).CellStyle = boldStyle;

                        IRow headerRow = sheet.CreateRow(1);
                        string[] headers = { "Mã Chi Tiêu", "Danh Mục", "Người Nhận", "Số Tiền", "Ngày Chi Tiêu", "Ghi Chú", "Ngày Tạo Chi Tiêu" };

                        for (int i = 0; i < headers.Length; i++)
                        {
                            headerRow.CreateCell(i).SetCellValue(headers[i]);
                            sheet.SetColumnWidth(i, 20 * 256);
                        }

                        int rowIndex = 2;
                        foreach (var expense in expenses)
                        {
                            IRow row = sheet.CreateRow(rowIndex);
                            row.CreateCell(0).SetCellValue(expense.ExpenseId);
                            row.CreateCell(1).SetCellValue(expense.Category?.CategoryName ?? "");
                            row.CreateCell(2).SetCellValue(expense.Recipient?.RecipientName ?? "");
                            row.CreateCell(3).SetCellValue(expense.Amount.ToString("N0") + " VND");
                            row.CreateCell(4).SetCellValue(expense.ExpenseDate.ToString("dd-MM-yyyy"));
                            row.CreateCell(5).SetCellValue(expense.Note ?? "");
                            row.CreateCell(6).SetCellValue(expense.CreatedAt.ToString());
                            rowIndex++;
                        }

                        rowIndex++;
                        IRow incomeTitleRow = sheet.CreateRow(rowIndex++);
                        incomeTitleRow.CreateCell(0).SetCellValue("Thu Nhập");
                        incomeTitleRow.GetCell(0).CellStyle = boldStyle;

                        // --- Bảng Thu Nhập ---
                        rowIndex++;
                        IRow headerRowIncomes = sheet.CreateRow(rowIndex);
                        string[] incomeHeaders = { "Mã Thu Nhập", "Danh Mục", "Người Nhận", "Số Tiền", "Ngày Thu Nhập", "Ghi Chú", "Ngày Tạo Thu Nhập" };
                        for (int i = 0; i < incomeHeaders.Length; i++)
                        {
                            headerRowIncomes.CreateCell(i).SetCellValue(incomeHeaders[i]);
                            sheet.SetColumnWidth(i, 20 * 256);
                        }

                        rowIndex++;
                        foreach (var income in incomes)
                        {
                            IRow row = sheet.CreateRow(rowIndex);
                            row.CreateCell(0).SetCellValue(income.IncomeId);
                            row.CreateCell(1).SetCellValue(income.Category?.CategoryName ?? "");
                            row.CreateCell(2).SetCellValue(income.Recipient?.RecipientName ?? "");
                            row.CreateCell(3).SetCellValue(income.Amount.ToString("N0") + " VND");
                            row.CreateCell(4).SetCellValue(income.IncomeDate.ToString("dd-MM-yyyy"));
                            row.CreateCell(5).SetCellValue(income.Note ?? "");
                            row.CreateCell(6).SetCellValue(income.CreatedAt.ToString());
                            rowIndex++;
                        }

                        using (var fs = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            workbook.Write(fs);
                        }

                        DialogCustoms dialog = new DialogCustoms("Xuất Excel thành công!", "Thông báo", DialogCustoms.Show);
                        dialog.ShowDialog();
                        ResetControls();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }
        private void ResetControls()
        {
            rdbDay.IsChecked = false;
            rdbMonth.IsChecked = false;
            rdbFull.IsChecked = false;
            CmbMonth.SelectedIndex = -1;
            txtDay.Clear();
        }

        private void CmbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbMonth.SelectedItem != null)
            {
                int selectedMonth = Convert.ToInt32(CmbMonth.SelectedItem);

                var userId = BLL.AppContext.Instance.UserId;

                var years = _expenseRepository.GetYearsByMonth(userId, selectedMonth);

                CmbYear.Items.Clear();
                foreach (var year in years)
                {
                    CmbYear.Items.Add(year);
                }

                if (CmbYear.Items.Count > 0)
                {
                    CmbYear.SelectedIndex = 0;
                }
            }
        }
    }
}
