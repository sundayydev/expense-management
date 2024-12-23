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
        { }
        public void AddIncome(Income income)
        {
            _context.Incomes.Add(income);
            _context.SaveChanges();
        }
        public void DeleteIncome(Income income)
        {
            _context.Incomes.Remove(income);
            _context.SaveChanges();
        }
        public void AddRecipient(Recipient recipient)
        {
            _context.Recipients.Add(recipient);
            _context.SaveChanges();
        }
        public List<Income> GetAllIncomes()
        {
            return _context.Incomes.ToList();
        }
    }
}
