using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ExpenseRepository
    {
        private readonly MyDBContext _context = new MyDBContext();
        public void AddExpense(Expens ex)
        {
            _context.Expenses.Add(ex);
            _context.SaveChanges();
        }
        public List<Expens> GetExpenseByUserId(string userId)
        {
            return _context.Expenses.Where(c => c.UserId.ToString() == userId).ToList();
        }
        public Expens GetExpenseById(string id)
        {
            return _context.Expenses.FirstOrDefault(e => e.ExpenseId == id);
        }

        public void DeleteExpense(Expens expense)
        {
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }
    }
}
