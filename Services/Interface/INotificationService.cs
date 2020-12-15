using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;

namespace WebApi.Services
{
    public interface INotificationService
    {
        IEnumerable<dynamic> GetNotificationList();
        dynamic GetNotificationById(int id);
        IEnumerable<dynamic> GetReminderTypeSelection();
        IEnumerable<dynamic> GetFrequencyTypeSelection();
        int EditNotification(notification_setting data);
        int AddNotification(notification_setting data);
        IEnumerable<dynamic> GetNotificationSelection();
    }
}
