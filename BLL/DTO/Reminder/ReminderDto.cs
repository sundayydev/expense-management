using System;
using System.Windows.Media;

namespace BLL.DTO.Reminder
{
    public class ReminderDto
    {
        private readonly BrushConverter _brushConverter = new BrushConverter();
        public string ReminderId { get; set; }
        public string Title { get; set; }
        public DateTime ReminderDate { get; set; }
        public bool? IsRecurring { get; set; }
        public string CategoryId { get; set; }
        public string RecurrenceType { get; set; }
        public bool? IsCompleted { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        
        public Brush ColorBgCompleted
        {
            get
            {
                return IsCompleted switch
                {
                    true => (Brush)_brushConverter.ConvertFromString("#04a08b"),
                    false => (Brush)_brushConverter.ConvertFromString("#ff562f"),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        public Brush BgCompleted
        {
            get
            {
                return IsCompleted switch
                {
                    true => (Brush)_brushConverter.ConvertFromString("#d1f5f0"),
                    false => (Brush)_brushConverter.ConvertFromString("#fff2e2"),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        public string CompletedText
        {
            get
            {
                return IsCompleted switch
                {
                    true => "Đã hoàn thành",
                    false => "Chưa hoàn thành",
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}
