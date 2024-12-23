namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Loan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Loan()
        {
            Payments = new HashSet<Payment>();
        }

        [StringLength(10)]
        public string LoanId { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string RecipientId { get; set; }

        [StringLength(50)]
        public string LoanType { get; set; }

        public decimal Amount { get; set; }

        [Column(TypeName = "date")]
        public DateTime LoanDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DueDate { get; set; }

        public bool? IsSettled { get; set; }

        [StringLength(255)]
        public string Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Recipient Recipient { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
