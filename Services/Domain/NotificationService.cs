using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class NotificationService : INotificationService
    {
        private DataContext _context;

        public NotificationService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<dynamic> GetNotificationList()
        {
            try
            {
                return (from notification in _context.notification_setting
                        join reminderType in _context.reminder_type on notification.reminderType equals reminderType.id
                        join frequencyType in _context.frequency_type on notification.frequencyType equals frequencyType.id
                        where notification.status != 0
                        select new
                        {
                            id = notification.id,
                            notificationName = notification.name,
                            notificationReminder = notification.reminder,
                            notificationReminderType = reminderType.name,
                            notificationFrequency = notification.frequency,
                            notificationFrequencyType = frequencyType.name,
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public dynamic GetNotificationById(int id)
        {
            try
            {
                return (from notification in _context.notification_setting
                        join reminderType in _context.reminder_type on notification.reminderType equals reminderType.id
                        join frequencyType in _context.frequency_type on notification.frequencyType equals frequencyType.id
                        where notification.status != 0 && notification.id == id
                        select new
                        {
                            id = notification.id,
                            notificationName = notification.name,
                            notificationReminder = notification.reminder,
                            notificationReminderTypeId = reminderType.id,
                            notificationReminderType = reminderType.name,
                            notificationFrequency = notification.frequency,
                            notificationFrequencyTypeId = frequencyType.id,
                            notificationFrequencyType = frequencyType.name,
                            notificationStatus = (notification.status != null || notification.status != 0  ? "Active" : "Inactive")
                        }).First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<dynamic> GetNotificationSelection()
        {
            try
            {
                return (from notification_setting in _context.notification_setting
                        select new
                        {
                            id = notification_setting.id,
                            name = notification_setting.name
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<dynamic> GetReminderTypeSelection()
        {
            try
            {
                return (from reminder_type in _context.reminder_type
                        select new
                        {
                            id = reminder_type.id,
                            name = reminder_type.name
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<dynamic> GetFrequencyTypeSelection()
        {
            try
            {
                return (from frequency_type in _context.frequency_type
                        select new
                        {
                            id = frequency_type.id,
                            name = frequency_type.name
                        }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditNotification(notification_setting data)
        {
            try
            {

                notification_setting notification = _context.notification_setting.Where(x => x.id == data.id).First();
                notification.name = data.name;
                notification.frequency = data.frequency;
                notification.frequencyType = data.frequencyType;
                notification.reminder = data.reminder;
                notification.reminderType = data.reminderType;
                notification.remindBeforeAfter = data.remindBeforeAfter;
                int num = _context.SaveChanges();
                if (num > 0)
                {
                    return data.id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data.id;
        }

        public int AddNotification(notification_setting data)
        {
            try
            {
                _context.notification_setting.Add(data);
                int num = _context.SaveChanges();
                _context.SaveChanges();
                if (num > 0)
                {
                    return data.id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data.id;
        }
    }
}
