﻿using BLL.Services;
using DAL.Models;
using FontAwesome6;
using GUI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL.DTO.Category;
using BLL.DTO.Expenses;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data.Entity;
using Microsoft.Win32;
using DAL.Utils;


namespace GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UcStatistics.xaml
    /// </summary>
    public partial class UcStatistics : UserControl
    {
        private readonly ExpenseService _expenseService = new ExpenseService();
        private readonly IncomeService _incomeService = new IncomeService();
        readonly string _userId = BLL.AppContext.Instance.UserId;
        private List<Expens> selectedExpenses = new List<Expens>();

        private ObservableCollection<Expens> Expenses { get; set; } = new ObservableCollection<Expens>();

        public UcStatistics()
        {
            InitializeComponent();
            ShowInfoCard();
            LoadData();
        }
        private void BtnOpenFormExpense_OnClick(object sender, RoutedEventArgs e)
        {
            WFormExpense wf = new WFormExpense();
            wf.ShowDialog();
            ShowInfoCard();
        }
        void LoadData()
        {
            Expenses.Clear();


            var userExpenses = _expenseService.GetAllExpensesByUserId(BLL.AppContext.Instance.UserId);


            foreach (var expense in userExpenses)
            {
                Expenses.Add(expense);
            }


            dvgExpense.ItemsSource = Expenses;
        }

        private void BtnOpenFormIncome_OnClick(object sender, RoutedEventArgs e)
        {
            WFormIncome wf = new WFormIncome();
            wf.ShowDialog();
            ShowInfoCard();
        }

        void ShowInfoCard()
        {
            try
            {
                int currentMonth = DateTime.Now.Month;
                int currentYear = DateTime.Now.Year;

                LoadCardIncome(currentMonth, currentYear);
                LoadCardExpense(currentMonth, currentYear);
                LoadCardWallet(currentMonth, currentYear);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load card: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        void LoadCardIncome(int currentMonth, int currentYear)
        {
            var brushConverter = new BrushConverter();

            decimal totalIncome = _incomeService.GetTotalIncomeByUserId(_userId);

            int previousMonth, previousYear;
            (previousMonth, previousYear) = FunctionHelper.GetPreviousMonth(currentMonth, currentYear);

            decimal currentMonthAmountIncome = _incomeService.GetTotalAmountByMonthly(_userId, currentMonth, currentYear);
            decimal lastMonthAmountIncome = _incomeService.GetTotalAmountByMonthly(_userId, previousMonth, previousYear);

            decimal incomeLastMonthly = currentMonthAmountIncome - lastMonthAmountIncome;
            decimal percentIncome = 0;
            if(currentMonthAmountIncome != 0)
            {
                percentIncome = (currentMonthAmountIncome - lastMonthAmountIncome) / currentMonthAmountIncome * 100;
            }
            if (percentIncome >= 0)
            {
                FaIconIncome.Icon = EFontAwesomeIcon.Solid_ArrowUp;
                FaIconIncome.PrimaryColor = (Brush)brushConverter.ConvertFromString("#04a08b");
                TxtPercentIncome.Foreground = (Brush)brushConverter.ConvertFromString("#04a08b");
                BorderBgIncome.Background = (Brush)brushConverter.ConvertFromString("#d1f5f0");
                TxtIncomeLastMonth.Text = $"+ {incomeLastMonthly:N0}";
            }
            else
            {
                FaIconIncome.Icon = EFontAwesomeIcon.Solid_ArrowDown;
                FaIconIncome.PrimaryColor = (Brush)brushConverter.ConvertFromString("#ff562f");
                TxtPercentIncome.Foreground = (Brush)brushConverter.ConvertFromString("#ff562f");
                BorderBgIncome.Background = (Brush)brushConverter.ConvertFromString("#fff2e2");
                TxtIncomeLastMonth.Text = $"- {Math.Abs(incomeLastMonthly):N0}";
            }

            TxtPercentIncome.Text = $"{percentIncome:N2}%";
            TxtIncome.Text = $"+ {totalIncome:N0}";
        }

        void LoadCardExpense(int currentMonth, int currentYear)
        {
            var brushConverter = new BrushConverter();
            decimal totalExpense = _expenseService.GetTotalExpensesByUserId(_userId);

            int previousMonth, previousYear;
            (previousMonth, previousYear) = FunctionHelper.GetPreviousMonth(currentMonth, currentYear);

            decimal currentMonthAmountExpense = _expenseService.GetTotalAmountByMonthly(_userId, currentMonth, currentYear);
            decimal lastMonthAmountExpense = _expenseService.GetTotalAmountByMonthly(_userId, previousMonth, previousYear);

            decimal expenseLastMonthly = currentMonthAmountExpense - lastMonthAmountExpense;
            decimal percentExpense = 0;
            if (currentMonthAmountExpense != 0)
            {
                percentExpense = (currentMonthAmountExpense - lastMonthAmountExpense) / currentMonthAmountExpense * 100;
            }
            if (percentExpense >= 0)
            {
                FaIconExpense.Icon = EFontAwesomeIcon.Solid_ArrowUp;
                FaIconExpense.PrimaryColor = (Brush)brushConverter.ConvertFromString("#04a08b");
                TxtPercentExpense.Foreground = (Brush)brushConverter.ConvertFromString("#04a08b");
                BorderBgExpense.Background = (Brush)brushConverter.ConvertFromString("#d1f5f0");
                TxtExpenseLastMonth.Text = $"+ {expenseLastMonthly:N0}";
            }
            else
            {
                FaIconExpense.Icon = EFontAwesomeIcon.Solid_ArrowDown;
                FaIconExpense.PrimaryColor = (Brush)brushConverter.ConvertFromString("#ff562f");
                TxtPercentExpense.Foreground = (Brush)brushConverter.ConvertFromString("#ff562f");
                BorderBgExpense.Background = (Brush)brushConverter.ConvertFromString("#fff2e2");
                TxtExpenseLastMonth.Text = $"- {Math.Abs(expenseLastMonthly):N0}";
            }

            TxtPercentExpense.Text = $"{percentExpense:N2}%";
            TxtExpense.Text = $"- {totalExpense:N0}";
        }

        void LoadCardWallet(int currentMonth, int currentYear)
        {
            var brushConverter = new BrushConverter();

            decimal totalExpense = _expenseService.GetTotalExpensesByUserId(_userId);
            decimal totalIncome = _incomeService.GetTotalIncomeByUserId(_userId);
            decimal totalWallet = totalIncome - totalExpense;

            int previousMonth, previousYear;
            (previousMonth, previousYear) = FunctionHelper.GetPreviousMonth(currentMonth, currentYear);

            decimal currentMonthAmountWallet = _incomeService.GetTotalAmountByMonthly(_userId, currentMonth, currentYear)
                                               - _expenseService.GetTotalAmountByMonthly(_userId, currentMonth, currentYear);
            decimal lastMonthAmountWallet = _incomeService.GetTotalAmountByMonthly(_userId, previousMonth, previousYear)
                                            - _expenseService.GetTotalAmountByMonthly(_userId, previousMonth, previousYear);

            decimal incomeLastMonthly = currentMonthAmountWallet - lastMonthAmountWallet;
            decimal percentWallet = 0;
            if (currentMonthAmountWallet != 0)
            {
                percentWallet = (currentMonthAmountWallet - lastMonthAmountWallet) / currentMonthAmountWallet * 100;
            }
            if (percentWallet >= 0)
            {
                FaIconWallet.Icon = EFontAwesomeIcon.Solid_ArrowUp;
                FaIconWallet.PrimaryColor = (Brush)brushConverter.ConvertFromString("#04a08b");
                TxtPercentWallet.Foreground = (Brush)brushConverter.ConvertFromString("#04a08b");
                BorderBgWallet.Background = (Brush)brushConverter.ConvertFromString("#d1f5f0");
                TxtWalletLastMonth.Text = $"+ {incomeLastMonthly:N0}";
            }
            else
            {
                FaIconWallet.Icon = EFontAwesomeIcon.Solid_ArrowDown;
                FaIconWallet.PrimaryColor = (Brush)brushConverter.ConvertFromString("#ff562f");
                TxtPercentWallet.Foreground = (Brush)brushConverter.ConvertFromString("#ff562f");
                BorderBgWallet.Background = (Brush)brushConverter.ConvertFromString("#fff2e2");
                TxtWalletLastMonth.Text = $"- {Math.Abs(incomeLastMonthly):N0}";
            }

            TxtPercentWallet.Text = $"{percentWallet:N2}%";
            TxtWallet.Text = $"{totalWallet:N0}";
        }

        private void FilterExpensesByDate(DateTime selectedDate)
        {
            var filteredExpenses = Expenses.Where(exp => exp.ExpenseDate.Date == selectedDate.Date).ToList();
            dvgExpense.ItemsSource = filteredExpenses;
        }

        private void cldExpense_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cldExpense.SelectedDate.HasValue)
            {
                DateTime selectedDate = cldExpense.SelectedDate.Value;
                FilterExpensesByDate(selectedDate);
            }
        }

        private void BtnExportExcel_Click(object sender, RoutedEventArgs e)
        {
            WFormStatistic wf = new WFormStatistic();
            wf.ShowDialog();    
        }
    }

}


