using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class NotificationService : INotificationService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_notification notification)
        {
            notification.created_at = DateTime.Now;
            _context.mp_notification.Add(notification);
            _context.SaveChanges();
        }

        public IQueryable<mp_notification> GetAllUserNotifications(string user_id)
        {
            return _context.mp_notification.Where(e => e.user_id == user_id);
        }

        public IQueryable<mp_notification> GetPendingUserNotifications(string user_id)
        {
            return _context.mp_notification.Where(e => e.user_id == user_id && e.read == 1);
        }


        public void Update(mp_notification notification)
        {
            var old = _context.mp_notification.FirstOrDefault(e => e.id == notification.id);
            notification.created_at = old.created_at;
            notification.created_by = old.created_by;
            notification.updated_at = DateTime.Now;

            _context.Entry(old).CurrentValues.SetValues(notification);
            _context.SaveChanges();
        }
    }
}
