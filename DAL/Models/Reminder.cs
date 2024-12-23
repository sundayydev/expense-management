namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Reminder
    {
        [StringLength(14)]
        public string ReminderId { get; set; }

        public Guid UserId { get; set; }

        [StringLength(10)]
        public string CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime ReminderDate { get; set; }

        public bool? IsRecurring { get; set; }

        [StringLength(50)]
        public string RecurrenceType { get; set; }

        public bool? IsCompleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual Category Category { get; set; }

        public virtual User User { get; set; }
    }
}
