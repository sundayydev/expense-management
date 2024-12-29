using System;

namespace BLL.DTO.Reminder
{
    public class AddReminderDto
    {
        public string Title { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool? IsRecurring { get; set; }
        public string CategoryId { get; set; }
        public string RecurrenceType { get; set; }
        public bool? IsCompleted { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
