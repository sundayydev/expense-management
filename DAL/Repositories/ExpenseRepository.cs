using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
            return _context.Expenses.AsNoTracking().Where(c => c.UserId.ToString() == userId).ToList();
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
        public void UpdateExpense(Expens ex)
        {
            _context.Expenses.AddOrUpdate(ex);
            _context.SaveChanges();
        }
        public decimal GetTotalExpense()
        {
            return _context.Expenses.Sum(e => e.Amount);
        }
        public decimal GetTotalExpenseByUserId(string userId)
        {
            var userGuid = Guid.Parse(userId);
            var total = _context.Expenses
              .Where(e => e.UserId == userGuid)
              .Sum(e => (decimal?)e.Amount) ?? 0;

            return total;
        }
        public List<Expens> GetMonthlyExpenses(string userId)
        {
            var userGuid = Guid.Parse(userId);
            return _context.Expenses
                .Where(e => e.UserId == userGuid && e.ExpenseDate.Month == DateTime.Now.Month)
                .ToList();
        }

        // Lấy chi tiêu hàng ngày của người dùng
        public List<Expens> GetDailyExpenses(string userId)
        {
            var userGuid = Guid.Parse(userId);
            return _context.Expenses
                .Where(e => e.UserId == userGuid && e.ExpenseDate.Day == DateTime.Now.Day)
                .ToList();
        }

        //Lấy tổng tiền theo tháng và năm
        public decimal GetTotalAmountByMonthly(string userId, int month, int year)
        {
            var userGuid = Guid.Parse(userId);
            var total = _context.Expenses
                .Where(e => e.UserId == userGuid && e.ExpenseDate.Month == month && e.ExpenseDate.Year == year)
                .Sum(e => (decimal?)e.Amount) ?? 0;
            return total;
        }
    }
}
