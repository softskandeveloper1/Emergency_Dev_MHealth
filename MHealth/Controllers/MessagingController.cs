using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Utils;
using MHealth.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers
{
    [Authorize]
    public class MessagingController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        public MessagingController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Inbox(int? pageNumber, string query = null)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            int pageSize = 25;
            var notifications = NotificationUtil.Get().Where(e=>e.user_id==user_id);
            if (!string.IsNullOrEmpty(query))
            {
                notifications = notifications.Where(e => e.notification.Contains(query));
            }
            return View(await PaginatedList<mp_notification>.CreateAsync(notifications.OrderByDescending(e => e.created_at).AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        public IActionResult LoadMessagePartial(long message_id)
        {
            NotificationUtil.MarkAsRead(message_id);
            return PartialView(NotificationUtil.Get().FirstOrDefault(e => e.id == message_id));
        }

        public void MarkAsRead(long message_id)
        {
            NotificationUtil.MarkAsRead(message_id);
        }
    }
}