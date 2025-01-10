using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Models
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
            : base("name=MyDBContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Expens> Expenses { get; set; }
        public virtual DbSet<Income> Incomes { get; set; }
        public virtual DbSet<Recipient> Recipients { get; set; }
        public virtual DbSet<Reminder> Reminders { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .Property(e => e.CategoryId)
                .IsUnicode(false);

            modelBuilder.Entity<Expens>()
                .Property(e => e.ExpenseId)
                .IsUnicode(false);

            modelBuilder.Entity<Expens>()
                .Property(e => e.CategoryId)
                .IsUnicode(false);

            modelBuilder.Entity<Expens>()
                .Property(e => e.RecipientId)
                .IsUnicode(false);

            modelBuilder.Entity<Income>()
                .Property(e => e.IncomeId)
                .IsUnicode(false);

            modelBuilder.Entity<Income>()
                .Property(e => e.CategoryId)
                .IsUnicode(false);

            modelBuilder.Entity<Income>()
                .Property(e => e.RecipientId)
                .IsUnicode(false);

            modelBuilder.Entity<Recipient>()
                .Property(e => e.RecipientId)
                .IsUnicode(false);

            modelBuilder.Entity<Reminder>()
                .Property(e => e.ReminderId)
                .IsUnicode(false);

            modelBuilder.Entity<Reminder>()
                .Property(e => e.CategoryId)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Categories)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Expenses)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Incomes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Recipients)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Reminders)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
