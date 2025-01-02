using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        public List<Expens> GetMonthlyExpenses(string userId, int month, int year)
        {
            var userGuid = Guid.Parse(userId);
            var total = _context.Expenses
                .Where(e => e.UserId == userGuid && e.ExpenseDate.Month == month && e.ExpenseDate.Year == year).ToList();
            return total;
            
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

        public decimal GetTotalAmountByDate(string userId, DateTime date)
        {
            var userGuid = Guid.Parse(userId);
            var total = _context.Expenses
                .Where(e => e.UserId == userGuid && e.ExpenseDate.Day == date.Day &&
                e.ExpenseDate.Month == date.Month && e.ExpenseDate.Year == date.Year)
                .Sum(e => (decimal?)e.Amount) ?? 0;
            return total;
        }
        public List<Expens> GetExpenseByDate(string userId, DateTime date)
        {
            var userGuid = Guid.Parse(userId);
            var total = _context.Expenses
                .Where(e => e.UserId == userGuid && e.ExpenseDate.Day == date.Day &&
                e.ExpenseDate.Month == date.Month && e.ExpenseDate.Year == date.Year).ToList();
            return total;
        }
        public int GetQuantityExpenses(string userId)
        {
            return _context.Expenses.Where(e => e.UserId.ToString() == userId).Count();
        }

        public List<int> GetMonths(string userId)
        {
            var userGuid = Guid.Parse(userId);
            return _context.Expenses
                    .Where(e => e.UserId.ToString() == userId && e.ExpenseDate != null) 
                    .Select(e => e.ExpenseDate.Month)  
                    .Distinct()  
                    .OrderBy(m => m)  
                    .ToList();
        }
        public List<int> GetYear(string userId)
        {
            var userGuid = Guid.Parse(userId);
            return _context.Expenses
                    .Where(e => e.UserId.ToString() == userId && e.ExpenseDate != null)
                    .Select(e => e.ExpenseDate.Year)
                    .Distinct()
                    .OrderBy(m => m)
                    .ToList();
        }
        public List<int> GetYearsByMonth(string userId, int month)
        {
            using (var context = new MyDBContext())
            {
                var expenseYears = context.Expenses
                    .Where(e => e.UserId.ToString() == userId && e.ExpenseDate.Month == month)
                    .Select(e => e.ExpenseDate.Year)
                    .Distinct()
                    .ToList();

                var incomeYears = context.Incomes
                    .Where(i => i.UserId.ToString() == userId && i.IncomeDate.Month == month)
                    .Select(i => i.IncomeDate.Year)
                    .Distinct()
                    .ToList();

                return expenseYears.Union(incomeYears).Distinct().OrderBy(y => y).ToList();
            }
        }
    }
}
