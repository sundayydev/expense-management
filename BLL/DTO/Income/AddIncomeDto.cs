using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO.Income
{
    public class AddIncomeDto
    {
        public string CategoryId { get; set; }
        public string RecipientId { get; set; }
        public string Source { get; set; }    
        public DateTime IncomeDate { get; set; }
        public decimal Amount { get; set; }
        public string Note { get; set; }
        public string UserId { get; set; }
    }
}
