namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment
    {
        public int PaymentId { get; set; }

        public int LoanId { get; set; }

        [Column(TypeName = "date")]
        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Loan Loan { get; set; }
    }
}
