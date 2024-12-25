using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Expense
{
    public class ExpenseDto
    {
        public string ExpenseId { get; set; }
        public Guid UserId { get; set; }
        public string CategoryId { get; set; } 
        public int? RecipientId { get; set; } 
        public decimal Amount { get; set; } 
        public DateTime ExpenseDate { get; set; }
        public string Note { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
