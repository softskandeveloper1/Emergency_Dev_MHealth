using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Utils;
using MHealth.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers.api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessagingController : ControllerBase
    {
        UserManager<ApplicationUser> _userManager;
        public MessagingController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Get()
        {
            var email = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByEmailAsync(email);
            var notifications = NotificationUtil.Get().Where(e => e.user_id == user.Id);
            //if (!string.IsNullOrEmpty(query))
            //{
            //   
            //}

            if (!notifications.Any())
            {
                return Ok(new List<mp_notification>());
            }

            return Ok(notifications);

        }
    }
}