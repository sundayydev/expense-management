using BLL.DTO.Category;
using BLL.DTO.Expenses;
using DAL.Models;
using DAL.Repositories;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
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
                RecipientId = addExpense.RecipientId ?? null,
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
    }
}
