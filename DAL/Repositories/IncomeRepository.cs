using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class IncomeRepository
    {
        private readonly MyDBContext _context = new MyDBContext();
        public IncomeRepository()
        {
            _context = new MyDBContext();
        }
        public void AddIncome(Income income)
        {
            _context.Incomes.Add(income);
            _context.SaveChanges();
        }
        public Income GetIncomeById(string id)
        {
            return _context.Incomes.FirstOrDefault(e => e.IncomeId == id);
        }
        public List<Income> Getall(string userId)
        {
            return _context.Incomes.AsNoTracking().Where(c => c.UserId.ToString() == userId).ToList();
        }
        public List<Income> GetIncomesByUserId(string userId)
        {
            return _context.Incomes.Include("Category").Where(i => i.UserId.ToString() == userId).OrderByDescending(i => i.IncomeDate).ToList();
        }
        public void UpdateIncome(Income income)
        {
            var entry = _context.Entry(income);
            if (entry.State == EntityState.Detached)
            {
                var existingIncome = _context.Incomes.Find(income.IncomeId);
                if (existingIncome != null)
                {
                    _context.Entry(existingIncome).CurrentValues.SetValues(income);
                }
            }
            _context.SaveChanges();
        }
        public void DeleteIncomeById(string incomeId)
        {
            var income = _context.Incomes.Find(incomeId);
            if (income != null)
            {
                _context.Incomes.Remove(income);
                _context.SaveChanges();
            }
        }
        public List<Income> GetIncomesByCategoryId(string categoryId)
        {
            return _context.Incomes.Where(i => i.CategoryId == categoryId).ToList();
        }
        public decimal GetTotalIncomeByUserId(string userId)
        {
            var userGuid = Guid.Parse(userId);
            var total = _context.Incomes
              .Where(e => e.UserId == userGuid)
              .Sum(e => (decimal?)e.Amount) ?? 0;

            return total;
        }
       
        public List<Income> GetMonthlyIncome(string userId, int month, int year)
        {
            var userGuid = Guid.Parse(userId);
            return _context.Incomes
                .Where(e => e.UserId == userGuid && e.IncomeDate.Month == month && e.IncomeDate.Year == year).ToList()
                .ToList();
        }
        
        // Lấy thu nhập hàng ngày của người dùng
        public List<Income> GetDailyIncome(string userId)
        {
            var userGuid = Guid.Parse(userId);
            return _context.Incomes
                .Where(i => i.UserId == userGuid && i.IncomeDate.Day == DateTime.Now.Day)
                .ToList();
        }

        //Lấy tổng tiền theo tháng và năm
        public decimal GetTotalAmountByMonthly(string userId, int month, int year)
        {
            var userGuid = Guid.Parse(userId);
            var total = _context.Incomes
              .Where(i => i.UserId == userGuid && i.IncomeDate.Month == month && i.IncomeDate.Year == year)
              .Sum(e => (decimal?)e.Amount) ?? 0;
            return total;
        }

        public decimal GetTotalAmountByDate(string userId, DateTime date)
        {
            var userGuid = Guid.Parse(userId);
            var total = _context.Incomes
              .Where(i => i.UserId == userGuid && i.IncomeDate.Day == date.Day &&
              i.IncomeDate.Month == date.Month && i.IncomeDate.Year == date.Year)
              .Sum(e => (decimal?)e.Amount) ?? 0;
            return total;
        }

        public Income GetIncomesByIncomesId(string incomeId)
        {
            return _context.Incomes.FirstOrDefault(r => r.IncomeId == incomeId);
        }

        public int GetQuantityIncomes(string userId)
        {
            return _context.Incomes.Where(i => i.UserId.ToString() == userId).Count();
        }
        public List<int> GetMonths(string userId)
        {
            var userGuid = Guid.Parse(userId);
            return _context.Incomes
                    .Where(e => e.UserId.ToString() == userId && e.IncomeDate != null)
                    .Select(e => e.IncomeDate.Month)
                    .Distinct()
                    .OrderBy(m => m)
                    .ToList();
        }
        public List<int> GetYear(string userId)
        {
            var userGuid = Guid.Parse(userId);
            return _context.Incomes
                    .Where(e => e.UserId.ToString() == userId)
                    .Select(e => e.IncomeDate.Year)
                    .Distinct()
                    .OrderBy(m => m)
                    .ToList();
        }
        public List<Income> GetIncomeByDate(string userId, DateTime date)
        {
            var userGuid = Guid.Parse(userId);
            var total = _context.Incomes
                .Where(e => e.UserId == userGuid && e.IncomeDate.Day == date.Day &&
                e.IncomeDate.Month == date.Month && e.IncomeDate.Year == date.Year).ToList();
            return total;
        }
    }
}
