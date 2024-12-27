using DAL.Models;
using System;
using System.Collections.Generic;
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
        public List<Income> GetIncomesByUserId(string userId)
        {
            return _context.Incomes.Include("Category") .Where(i => i.UserId.ToString() == userId).OrderByDescending(i => i.IncomeDate).ToList();
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
        public List<Income> GetMonthlyIncome(string userId)
        {
            var userGuid = Guid.Parse(userId);
            return _context.Incomes
                .Where(i => i.UserId == userGuid && i.IncomeDate.Month == DateTime.Now.Month)
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
    }
}
