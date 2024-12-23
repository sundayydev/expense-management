using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class IncomeService
    {
        private readonly IncomeRepository _incomeRepository;
        public IncomeService()
        {
            _incomeRepository = new IncomeRepository();
            
        }
        public void AddNewIncome(string RecipientName,decimal Amount , string Note)
        {
            var recipient = new Recipient { RecipientName = RecipientName };

            var newIncome = new Income
            {
                IncomeId = null,
                Recipient = recipient,
                Amount = Amount,
                IncomeDate = DateTime.Now,
                Note = Note,
                CreatedAt = DateTime.Now
            };
            _incomeRepository.AddIncome(newIncome);
        }
        public List<Income> GetAllIncomes()
        {
            return _incomeRepository.GetAllIncomes();
        }

        public void DeleteIncome(Income income)
        {
            _incomeRepository.DeleteIncome(income);
        }

    }
}
