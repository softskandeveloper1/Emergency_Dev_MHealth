using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using MHealth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers
{
    public class ConversationController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;
        private readonly IClinicianService _clinicianService;
        

        public ConversationController(IProfileService profileService, UserManager<ApplicationUser> userManager, IClinicianService clinicianService)
        {
            _profileService = profileService;
            _userManager = userManager;
            _clinicianService = clinicianService;
        }

        public IActionResult mconversations()
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
           
            if (User.IsInRole("client"))
            {
                var profile = _profileService.GetProfileByUserId(user_id);
                ViewBag.conversation_items = ConversationUtil.GetUserChannels(profile.id, "client");
            }
            else if (User.IsInRole("clinician"))
            {
                var profile = _clinicianService.GetByUserId(user_id);
                ViewBag.conversation_items = ConversationUtil.GetUserChannels(profile.id, "client");
            }
            return View();
        }

        public IQueryable<mp_conversation> GetUserConversations(string contact_id)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);

            return ConversationUtil.GetConversations(contact_id, user_id);
        }
    }
}