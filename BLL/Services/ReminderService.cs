using BLL.DTO.Reminder;
using DAL.Models;
using DAL.Repositories;
using DAL.Utils;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class ReminderService
    {
        private readonly ReminderRepository _reminderRepository = new ReminderRepository();

        public ReminderService()
        { }

        public void AddReminder(AddReminderDto addReminderDto)
        {
            var reminder = new Reminder
            {
                ReminderId = new CodeGenerator().GenerateCodeReminder(),
                Title = addReminderDto.Title,
                ReminderDate = addReminderDto.ReminderDate,
                IsRecurring = addReminderDto.IsRecurring,
                CategoryId = addReminderDto.CategoryId,
                RecurrenceType = addReminderDto.RecurrenceType,
                IsCompleted = addReminderDto.IsCompleted,
                Description = addReminderDto.Description,
                UserId = Guid.Parse(addReminderDto.UserId),
                CreatedAt = DateTime.Now
            };

            _reminderRepository.AddReminder(reminder);
        }

        public void UpdateReminder(Reminder updateReminder)
        {
            _reminderRepository.UpdateReminder(updateReminder);
        }

        public ReminderDto GetReminderByReminderId(string userId, string reminderId)
        {
            Reminder reminder = _reminderRepository.GetReminderByReminderId(userId, reminderId);
            return new ReminderDto()
            {
                ReminderId = reminder.ReminderId,
                Title = reminder.Title,
                ReminderDate = reminder.ReminderDate,
                IsRecurring = reminder.IsRecurring,
                CategoryId = reminder.CategoryId,
                RecurrenceType = reminder.RecurrenceType,
                IsCompleted = reminder.IsCompleted,
                Description = reminder.Description,
            };
        }

        public List<ReminderDto> GetRemindersByUserId(string userId)
        {
            List<Reminder> list = _reminderRepository.GetRemindersByUserId(userId);
            List<ReminderDto> listDto = new List<ReminderDto>();

            foreach (var item in list)
            {
                ReminderDto reminderDto = new ReminderDto
                {
                    ReminderId = item.ReminderId,
                    Title = item.Title,
                    ReminderDate = item.ReminderDate,
                    IsRecurring = item.IsRecurring,
                    CategoryId = item.CategoryId,
                    RecurrenceType = item.RecurrenceType,
                    IsCompleted = item.IsCompleted,
                    Description = item.Description,
                    CategoryName = item.Category.CategoryName
                };

                listDto.Add(reminderDto);
            }

            return listDto;
        }

        public void DeleteReminder(string userId, string reminderId)
        {
            Reminder reminder = _reminderRepository.GetReminderByReminderId(userId, reminderId);
            _reminderRepository.DeleteReminder(reminder);
        }
    
    }
}
