using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Utils
{
    public enum NotificationType{
        appointment_scheduled_client=1,
        appointment_scheduled_clinician=2,
        account_created_client=3,
        account_created_clinician=4,
        appointment_cancelled=5
    }
    public static class NotificationUtil
    {
        private static readonly HContext _context = new HContext();

        public static void AddNotification(mp_notification notification)
        {
            notification.title = ConstructTitle(notification.notification_type);
            notification.notification = ConstructMessage(notification.notification_type);
            notification.created_at = DateTime.Now;
            _context.mp_notification.Add(notification);
            _context.SaveChanges();
        }

        public static void Add(mp_notification notification)
        {
            notification.created_at = DateTime.Now;
            _context.mp_notification.Add(notification);
            _context.SaveChanges();
        }

        public static IQueryable<mp_notification> Get()
        {
            return _context.mp_notification.AsQueryable();
        }

        public static string ConstructMessage(int notificationType)
        {

            var result = "";
            if (notificationType == 1)
            {
                result = AppSetting.GetValue("account_created_client");
            }
            else if (notificationType ==2)
            {
                result = AppSetting.GetValue("account_created_clinician"); 
            }
            else if (notificationType == 3)
            {
                result = AppSetting.GetValue("appointment_scheduled_client");
            }
            else if (notificationType == 4)
            {
                result = AppSetting.GetValue("appointment_scheduled_clinician");
            }


            return result;
        }

        public static string ConstructTitle(int notificationType)
        {

            var result = "";
            if (notificationType == 1)
            {
                result = "Account creation successful"; 
            }
            else if (notificationType == 2)
            {
                result = "Account creation successful"; 
            }
            else if (notificationType == 3)
            {
                result = "Appointment scheduled successfully";
            }
            else if (notificationType == 4)
            {
                result = "Appointment scheduled successfully";
            }


            return result;
        }

        public static void MarkAsRead(long notification_id)
        {
            var old = _context.mp_notification.FirstOrDefault(e => e.id == notification_id);

            var notification = _context.mp_notification.FirstOrDefault(e => e.id == notification_id);
            notification.read = 1;

            _context.Entry(old).CurrentValues.SetValues(notification);
            _context.SaveChanges();
        }

        public static int PendingMessages(string user_id)
        {
            return _context.mp_notification.Count(e => e.user_id == user_id && e.read == 0);
        }
    }
}
