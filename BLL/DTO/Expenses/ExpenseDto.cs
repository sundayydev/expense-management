using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Expenses
{
    public class ExpenseDto
    {
        public string ExpenseId { get; set; }
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public string RecipientId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string Note { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public bool IsSelected { get; set; }
    }
}
