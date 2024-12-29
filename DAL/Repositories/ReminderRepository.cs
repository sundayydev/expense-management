using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ReminderRepository
    {
        private readonly MyDBContext _context = new MyDBContext();
        
        public ReminderRepository()
        { }
        
        public void AddReminder(Reminder reminder)
        {
            _context.Reminders.Add(reminder);
            _context.SaveChanges();
        }
        
        public List<Reminder> GetRemindersByUserId(string userId)
        {
            return _context.Reminders
                .AsNoTracking()
                .Where(r => r.UserId.ToString() == userId)
                .ToList();
        }
        
        public Reminder GetReminderByReminderId(string userId, string reminderId)
        {
            return _context.Reminders.FirstOrDefault(r => r.UserId.ToString() == userId && r.ReminderId == reminderId);
        }
        
        public void UpdateReminder(Reminder reminder)
        {
            var reminderUpdate = _context.Reminders.FirstOrDefault(r => r.ReminderId == reminder.ReminderId);
            
            if (reminderUpdate != null)
            {
                reminderUpdate.Title = reminder.Title;
                reminderUpdate.ReminderDate = reminder.ReminderDate;
                reminderUpdate.IsRecurring = reminder.IsRecurring;
                reminderUpdate.CategoryId = reminder.CategoryId;
                reminderUpdate.RecurrenceType = reminder.RecurrenceType;
                reminderUpdate.IsCompleted = reminder.IsCompleted;
                reminderUpdate.Description = reminder.Description;
            }
            
            _context.Reminders.AddOrUpdate(reminderUpdate);
            _context.SaveChanges();
        }
        
        public void DeleteReminder(Reminder reminder)
        {
            _context.Reminders.Remove(reminder);
            _context.SaveChanges();
        }
    }
}
