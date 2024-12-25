using BLL.DTO.Category;
using BLL.DTO.Income;
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
        public void DeleteIncome(string incomeId)
        {
            var income = _incomeRepository.GetIncomesByUserId(incomeId);
            if (income == null)
            {
                throw new Exception("Thu nhập không tồn tại.");
            }
            _incomeRepository.DeleteIncomeById(incomeId);
        }
    }

}
