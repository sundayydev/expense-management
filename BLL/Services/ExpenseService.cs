using BLL.DTO.Category;
using BLL.DTO.Expenses;
using DAL.Models;
using DAL.Repositories;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ExpenseService
    {
        private readonly ExpenseRepository _expenseRepository = new();
       
        public void AddExpense(ExpenseDto addExpense)
        {

            var expense = new Expens
            {
                ExpenseId = new CodeGenerator().GenerateCodeExpense(),
                UserId = Guid.Parse(addExpense.UserId),
                CategoryId = addExpense.CategoryId,
                RecipientId = addExpense.RecipientId,
                Amount = addExpense.Amount,
                ExpenseDate = addExpense.ExpenseDate,
                Note = addExpense.Note,
                CreatedAt = DateTime.Now
            };
            _expenseRepository.AddExpense(expense);
        }

        public List<Expens> GetAllExpensesByUserId(string UserId)
        {
            return _expenseRepository.GetExpenseByUserId(UserId);
        }
        
        public void DeleteExpense(string expenseId)
        {
            var expense = _expenseRepository.GetExpenseById(expenseId);
            if (expense == null)
            {
                throw new Exception("Chi tiêu không tồn tại.");
            }
            _expenseRepository.DeleteExpense(expense);
        }
        public void UpdateExpense(Expens updateExpense)
        {
            var existingExpense = _expenseRepository.GetExpenseById(updateExpense.ExpenseId);

            if (existingExpense == null)
            {
                throw new Exception("Chi tiêu không tồn tại.");
            }
            existingExpense.CategoryId = updateExpense.CategoryId;
            existingExpense.RecipientId = updateExpense.RecipientId ?? null;
            existingExpense.Amount = updateExpense.Amount;
            existingExpense.ExpenseDate = updateExpense.ExpenseDate;
            existingExpense.Note = updateExpense.Note;
            _expenseRepository.UpdateExpense(existingExpense);
        }
        public decimal GetTotalExpensesByUserId(string userId)
        {
            return _expenseRepository.GetTotalExpenseByUserId(userId);
        }
        public List<ExpenseDto> GetMonthlyExpenses(string userId)
        {
            var expenses = _expenseRepository.GetExpenseByUserId(userId);
            var monthlyExpenses = expenses
                .GroupBy(e => new DateTime(e.ExpenseDate.Year, e.ExpenseDate.Month, 1))  // Nhóm theo năm và tháng
                .Select(g => new ExpenseDto
                {
                    CategoryId = g.Key.ToString("MMMM yyyy"),  // Lấy chuỗi tháng và năm
                    Amount = g.Sum(e => e.Amount),
                })
                .OrderBy(e => DateTime.ParseExact(e.CategoryId, "MMMM yyyy", null)) 
                .ToList();

            return monthlyExpenses ?? null;
        }
        public List<ExpenseDto> GetDailyExpenses(string userId)
        {
            var expenses = _expenseRepository.GetDailyExpenses(userId);
            return expenses.Select(e => new ExpenseDto
            {
                Amount = e.Amount,
                CategoryId = e.CategoryId,
                Note = e.Note,
            }).ToList();
        }
        public List<string> GetMonthsWithExpenses(string userId)
        {
            var expenses = _expenseRepository.GetExpenseByUserId(userId);
            if (expenses == null || !expenses.Any())
            {
                return new List<string>(); // Trả về danh sách rỗng
            }
            var months = expenses
                .Select(e => new DateTime(e.ExpenseDate.Year, e.ExpenseDate.Month, 1))  
                .Distinct()
                .OrderBy(date => date) 
                .ToList();

           
            return months.Select(m => m.ToString("MMMM yyyy")).ToList() ?? null;
        }
        
        public decimal GetTotalAmountByMonthly(string userId, int month, int year)
        {
            return _expenseRepository.GetTotalAmountByMonthly(userId, month, year);
        }

        public int GetQuantityExpenses(string userId)
        {
            return _expenseRepository.GetQuantityExpenses(userId);
        }
    }
}
