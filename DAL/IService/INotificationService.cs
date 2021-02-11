using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface INotificationService
    {
        void Add(mp_notification notification);
        IQueryable<mp_notification> GetAllUserNotifications(string user_id);
        IQueryable<mp_notification> GetPendingUserNotifications(string user_id);
        void Update(mp_notification notification);
    }
}
