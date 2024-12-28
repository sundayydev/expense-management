using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BLL.DTO
{
    public class CardInfoDto
    {
        public string CardType { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountLastMonth { get; set; }
        public decimal Percent {  get; set; }
        public Brush ColorBackground { get; set; }
        public Brush ColorForeground { get; set; }
    }
}
