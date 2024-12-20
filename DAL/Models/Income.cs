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
        public int IncomeId { get; set; }

        public Guid UserId { get; set; }

        public int? RecipientId { get; set; }

        [StringLength(100)]
        public string Source { get; set; }

        public decimal Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime IncomeDate { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Recipient Recipient { get; set; }

        public virtual User User { get; set; }
    }
}
