﻿using GUI.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using BLL.Services;
using AppContext = BLL.AppContext;
using BLL.DTO.Category;
using BLL.DTO.Expenses;
using DAL.Models;
using System.Windows.Controls;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for WFormExpense.xaml
    /// </summary>
    public partial class WFormExpense : Window
    {
        private readonly CategoryService _categoryService = new();
        private readonly ExpenseService _expenseService = new();
        private readonly Expens _expense;
        private readonly bool _isEditMode;

        public WFormExpense()
        {
            InitializeComponent();
          
        }
        public WFormExpense(Expens expense = null)
        {
            InitializeComponent();
            _expense = expense;
            _isEditMode = expense != null;
           
            LoadExpenseData();
        }
        private void LoadExpenseData()
        {
            if (_expense != null)
            {
                CmbCategory.SelectedValuePath = _expense.CategoryId;
                txtAmount.Text = _expense.Amount.ToString();
                dtpExpenseDate.DisplayDate = _expense.ExpenseDate;
                rtbNote.Document.Blocks.Clear();
                rtbNote.Document.Blocks.Add(new Paragraph(new Run(_expense.Note)));
            }
        }
            private void btnSaveAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isEditMode)
                {
                    _expense.RecipientId = null;
                    _expense.CategoryId = (CmbCategory.SelectedItem as CmbCategoryDto).CategoryId;
                    _expense.Amount = decimal.Parse(txtAmount.Text);
                    _expense.ExpenseDate = dtpExpenseDate.SelectedDate.HasValue ? dtpExpenseDate.SelectedDate.Value : _expense.ExpenseDate;
                    _expense.Note = new TextRange(rtbNote.Document.ContentStart, rtbNote.Document.ContentEnd).Text.Trim();
                    _expense.CreatedAt = DateTime.Now;

                    var updateExpense = new Expens()
                    {
                        ExpenseId = _expense.ExpenseId,
                        UserId = _expense.UserId,
                        CategoryId = _expense.CategoryId,
                        RecipientId = _expense.RecipientId ?? null,
                        Amount = _expense.Amount,
                        ExpenseDate = _expense.ExpenseDate,
                        Note = _expense.Note
                    };
                    _expenseService.UpdateExpense(updateExpense);
                    DialogCustoms dialogCustomsUpdate = new DialogCustoms("Cập nhật danh mục thành công.", "Thông báo", DialogCustoms.OK);
                    dialogCustomsUpdate.ShowDialog();
                }
                else
                {
                    var addExpense = new ExpenseDto
                    {
                        UserId = BLL.AppContext.Instance.UserId,
                        CategoryId = (CmbCategory.SelectedItem as CmbCategoryDto).CategoryId,
                        RecipientId = null,
                        Amount = decimal.Parse(txtAmount.Text),
                        ExpenseDate = dtpExpenseDate.SelectedDate.Value,
                        Note = new TextRange(rtbNote.Document.ContentStart, rtbNote.Document.ContentEnd).Text.Trim()
                    };

                    _expenseService.AddExpense(addExpense);
                    MessageBox.Show("Thêm chi tiêu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Lỗi: {exception.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogCustoms cancelDialog = new DialogCustoms("Hủy thao tác", "Thông báo", DialogCustoms.Show);
            cancelDialog.ShowDialog();
            this.Close();
        }

        void LoadCmbCategories()
        {
            var categories = _categoryService.GetCategoriesByCategoryType(AppContext.Instance.UserId, "Chi tiêu");
            var filteredCategories = categories.Where(c => c.CategoryType == "Chi tiêu").ToList();
            CmbCategory.ItemsSource = filteredCategories;
            CmbCategory.DisplayMemberPath = "CategoryName";
            CmbCategory.SelectedValuePath = "CategoryId";
        }

        private void WFormExpense_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadCmbCategories();
            if (_isEditMode && _expense != null)
            {
                CmbCategory.SelectedValue = _expense.CategoryId;
                txtAmount.Text = _expense.Amount.ToString();
                dtpExpenseDate.SelectedDate = _expense.ExpenseDate;
                rtbNote.Document.Blocks.Clear();
                rtbNote.Document.Blocks.Add(new Paragraph(new Run(_expense.Note)));

            }
        }

       
    }
}