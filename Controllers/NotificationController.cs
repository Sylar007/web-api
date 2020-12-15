using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Models.Notification;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private INotificationService _notificationService;

        public NotificationController(
            INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet]
        [Route("/Notification/GetNotificationList")]
        public string GetNotificationList()
        {
            IEnumerable<object> part = _notificationService.GetNotificationList();
            return JsonConvert.SerializeObject(part);
        }

        [HttpGet]
        [Route("/Notification/GetNotificationById/{id}")]
        public string GetNotificationById(int id)
        {
            object notification = _notificationService.GetNotificationById(id);
            return JsonConvert.SerializeObject(notification);
        }

        [HttpGet]
        [Route("/Notification/GetNotificationSelection")]
        public string GetNotificationSelection()
        {
            object notification = _notificationService.GetNotificationSelection();
            return JsonConvert.SerializeObject(notification);
        }

        [HttpGet]
        [Route("/Notification/GetReminderTypeSelection")]
        public string GetReminderTypeSelection()
        {
            object reminder = _notificationService.GetReminderTypeSelection();
            return JsonConvert.SerializeObject(reminder);
        }

        [HttpGet]
        [Route("/Notification/GetFrequencyTypeSelection")]
        public string GetFrequencyTypeSelection()
        {
            object frequency = _notificationService.GetFrequencyTypeSelection();
            return JsonConvert.SerializeObject(frequency);
        }

		[HttpPost]
		[Route("/Notification/UpdateNotification")]
		public int UpdateNotification([FromBody] NotificationModel model)
		{
			notification_setting notification = new notification_setting();

			notification.id = model.id;
			notification.name = model.notificationName;
			notification.frequency = model.notificationFrequency;
			notification.frequencyType = model.notificationFrequencyTypeId;
			notification.reminder = model.notificationReminder;
			notification.reminderType = model.notificationReminderTypeId;
			notification.remindBeforeAfter = model.remindBeforeAfter;
			return _notificationService.EditNotification(notification);
		}

		[HttpPost]
		[Route("/Notification/AddNotification")]
		public int AddNotification([FromBody] NotificationModel model)
		{
            notification_setting notification = new notification_setting();

            notification.name = model.notificationName;
            notification.frequency = model.notificationFrequency;
            notification.frequencyType = model.notificationFrequencyTypeId;
            notification.reminder = model.notificationReminder;
            notification.reminderType = model.notificationReminderTypeId;
            notification.remindBeforeAfter = model.remindBeforeAfter;
            notification.status = 1;
            return _notificationService.AddNotification(notification);
        }
	}
}