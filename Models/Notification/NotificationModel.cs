using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models.Notification
{  
    public class NotificationModel
    {
        public int id { get; set; }
        public string notificationName { get; set; }
        public int notificationReminder { get; set; }
        public int notificationReminderTypeId { get; set; }
        public string notificationReminderType { get; set; }
        public int notificationFrequency { get; set; }
        public int notificationFrequencyTypeId { get; set; }
        public string notificationFrequencyType { get; set; }
        public int remindBeforeAfter { get; set; }
        public string notificationStatus { get; set; }
    }
}
