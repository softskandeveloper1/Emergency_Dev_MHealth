using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers
{
    public class ProfileMatchController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;
        private readonly IClinicianService _clinicianService;
        private readonly IProfileMatchService _profileMatchService;


        public ProfileMatchController(IProfileService profileService, UserManager<ApplicationUser> userManager, IClinicianService clinicianService, IProfileMatchService profileMatchService)
        {
            _userManager = userManager;
            _profileService = profileService;
            _clinicianService = clinicianService;
            _profileMatchService = profileMatchService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public Guid MatchProfile(mp_profile_match profile_match)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            profile_match.profile_id = profile.id;
            var match_id = _profileMatchService.Add(profile_match);

            return match_id;
        }

        public string GetMatch(int appointment_type, int appointment_category, int appointment_category_sub)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            var match = _profileMatchService.Get().FirstOrDefault(e => e.appointment_type_id == appointment_type && e.appointment_activity_id == appointment_category && e.appointment_activity_sub_id == appointment_category_sub && e.profile_id==profile.id);

            if (match != null)
            {
                return match.id.ToString();
            }
            return null;
        }
    }
}