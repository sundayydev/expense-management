namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Income")]
    public partial class Income
    {
        [StringLength(14)]
        public string IncomeId { get; set; }

        public Guid UserId { get; set; }

        [StringLength(10)]
        public string CategoryId { get; set; }

        [StringLength(10)]
        public string RecipientId { get; set; }

        public decimal Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime IncomeDate { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Category Category { get; set; }

        public virtual Recipient Recipient { get; set; }

        public virtual User User { get; set; }
    }
}
