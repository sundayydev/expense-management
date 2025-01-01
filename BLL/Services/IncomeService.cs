using BLL.DTO.Category;
using BLL.DTO.Expenses;
using BLL.DTO.Income;
using BLL.DTO.Recipient;
using DAL.Models;
using DAL.Repositories;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BLL.Services
{
    public class IncomeService
    {
        private readonly IncomeRepository _incomeRepository = new();
        private readonly CategoryRepository _categoryRepository = new();
        public IncomeService()
        {
            _incomeRepository = new IncomeRepository();
            _categoryRepository = new CategoryRepository();
            
        }
        public void AddIncome(AddIncomeDto addIncomeDto)
        {
            var income = new Income
            {
                IncomeId = new CodeGenerator().GenerateCodeIncome(),
                UserId = Guid.Parse(addIncomeDto.UserId),
                CategoryId = addIncomeDto.CategoryId,
                RecipientId = addIncomeDto.RecipientId,
                IncomeDate = addIncomeDto.IncomeDate,
                Amount = addIncomeDto.Amount,
                Note = addIncomeDto.Note,
                CreatedAt = DateTime.Now
            };
            _incomeRepository.AddIncome(income);
        }
        public List<IncomeDto> GetIncomeByUserId(string userId)
        {
            try
            {
                var incomes = _incomeRepository.GetIncomesByUserId(userId);

                return incomes.Select(i => new IncomeDto
                {
                    IncomeId = i.IncomeId,
                    CategoryId = i.CategoryId,
                    CategoryName = i.Category?.CategoryName,
                    RecipientId = i.RecipientId,
                    RecipientName = i.Recipient?.RecipientName,
                    IncomeDate = i.IncomeDate,
                    Amount = i.Amount,
                    Note = i.Note,
                    CreatedAt =i.CreatedAt ?? DateTime.Now
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách thu nhập: {ex.Message}");
            }
        }
        public void UpdateIncome(Income updateIncome)
        {
            var existingIncome = _incomeRepository.GetIncomeById(updateIncome.IncomeId);

            if (existingIncome == null)
            {
                throw new Exception("Chi tiêu không tồn tại.");
            }
            existingIncome.CategoryId = updateIncome.CategoryId;
            existingIncome.RecipientId = updateIncome.RecipientId ?? null;
            existingIncome.Amount = updateIncome.Amount;
            existingIncome.IncomeDate = updateIncome.IncomeDate;
            existingIncome.Note = updateIncome.Note;
            _incomeRepository.UpdateIncome(existingIncome);
        }
        public Income GetIncomeById(string incomeId)
        {
            return _incomeRepository.GetIncomeById(incomeId);
        }
        public void DeleteIncome(string incomeId)
        {
            var income = _incomeRepository.GetIncomesByUserId(incomeId);
            if (income == null)
            {
                throw new Exception("Thu nhập không tồn tại.");
            }
            _incomeRepository.DeleteIncomeById(incomeId);
        }
        public decimal GetTotalIncomeByUserId(string userId)
        {
            return _incomeRepository.GetTotalIncomeByUserId(userId);
        }

        public List<IncomeDto> GetMonthlyIncome(string userId)
        {
            var income = _incomeRepository.GetIncomesByUserId(userId);

            // Nhóm chi tiêu theo tháng và năm
            var monthlyIncome = income
                .GroupBy(e => new DateTime(e.IncomeDate.Year, e.IncomeDate.Month, 1))  // Nhóm theo năm và tháng
                .Select(g => new IncomeDto
                {
                    CategoryId = g.Key.ToString("MMMM yyyy"),  // Lấy chuỗi tháng và năm
                    Amount = g.Sum(e => e.Amount),  // Tính tổng chi tiêu của tháng đó
                })
                .OrderBy(e => DateTime.ParseExact(e.CategoryId, "MMMM yyyy", null))  // Sắp xếp theo tháng năm
                .ToList();

            return monthlyIncome;
        }

        public decimal GetTotalAmountByMonthly(string userId, int month, int year)
        {
            return _incomeRepository.GetTotalAmountByMonthly(userId, month, year);
        }

        public List<IncomeDto> GetDailyIncome(string userId)
        {
            var incomes = _incomeRepository.GetDailyIncome(userId);
            return incomes.Select(i => new IncomeDto
            {
                Amount = i.Amount,
                CategoryId = i.CategoryId
            }).ToList();
        }

        public IncomeDto GetIncomesByIncomesId(string incomeId)
        {
            Income income = _incomeRepository.GetIncomesByIncomesId(incomeId);
            return new IncomeDto()
            {
                IncomeId = income.IncomeId,
                CategoryName = income.Category.CategoryName,
                Amount = income.Amount,
                Note = income.Note,
            };
        }
        public int GetQuantityIncomes(string userId)
        {
            return _incomeRepository.GetQuantityIncomes(userId);
        }

        public decimal GetTotalAmountByDate(string userId, DateTime date)
        {
            return _incomeRepository.GetTotalAmountByDate(userId, date);
        }
    }

}
