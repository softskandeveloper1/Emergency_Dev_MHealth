using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using MHealth.Data;
using MHealth.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers
{
    public class ReferralController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClinicianService _clinicianService;
        private readonly IProfileService _profileService;
        private readonly IProfileMatchService _profileMatchService;
        private readonly IReferralService _referralService;
        private readonly IEmailSender _emailSender;

        public ReferralController(IClinicianService clinicianService, UserManager<ApplicationUser> userManager, IProfileService profileService, IProfileMatchService profileMatchService, IReferralService referralService, IEmailSender emailSender)
        {
            _clinicianService = clinicianService;
            _userManager = userManager;
            _profileService = profileService;
            _profileMatchService = profileMatchService;
            _referralService = referralService;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Referrals(int? page, string query = null)
        {
            int pageSize = 25;
            ViewBag.query = query;
            var referrals = _referralService.Get();
            if (User.IsInRole("client"))
            {
                var user_id = _userManager.GetUserId(HttpContext.User);
                var profile = _profileService.Get().FirstOrDefault(e => e.user_id == user_id);

                referrals = referrals.Where(e => e.profile_id == profile.id);
            }
            else if (User.IsInRole("clinician"))
            {
                var user_id = _userManager.GetUserId(HttpContext.User);
                var clinician = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);

                referrals = referrals.Where(e => e.clinician_id == clinician.id);
            }
            if (!string.IsNullOrEmpty(query))
            {
                referrals = referrals.Where(e => e.id.ToString().Contains(query) || (e.clinician_.first_name != null && e.clinician_.first_name.Contains(query)) || (e.clinician_.last_name != null && e.clinician_.last_name.Contains(query)) || (e.profile_.first_name != null && e.profile_.first_name.Contains(query)) || (e.profile_.last_name != null && e.profile_.last_name.Contains(query)));
            }
            return View(await PaginatedList<mp_referral>.CreateAsync(referrals.Include(e=>e.profile_).Include(e=>e.clinician_).Include(e=>e.profile_match_).OrderByDescending(e => e.created_at).AsNoTracking(), page ?? 1, pageSize));
            
        }

        [Authorize(Roles ="clinician")]
        public IActionResult NewReferral(Guid profile_id)
        {
            var profile = _profileService.Get().FirstOrDefault(e => e.id == profile_id);
            return View(profile);
        }

        [HttpPost]
        [Authorize(Roles = "clinician")]
        public async Task<IActionResult> PostReferral(IFormCollection collection)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var clinician = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);
            

            var appointment_type_id = Convert.ToInt32(collection["appointment_type"]);
            var appointment_activity_id = Convert.ToInt32(collection["appointment_category"]);
            var appointment_activity_sub_id = Convert.ToInt32(collection["appointment_category_sub"]);

            var profile_id = Guid.Parse(collection["profile_id"]);
            var clinician_id = Guid.Parse(collection["clinician_id"]);

            var profile = _profileService.Get(profile_id);

            var profile_match = new mp_profile_match
            {
                appointment_type_id = appointment_type_id,
                appointment_activity_id = appointment_activity_id,
                appointment_activity_sub_id = appointment_activity_sub_id,
                clinician_id = clinician_id,
                profile_id = profile_id
            };
            var profile_match_id=_profileMatchService.Add(profile_match);

            var referral = new mp_referral
            {
                profile_id = profile_id,
                clinician_id = clinician.id,
                profile_match_id = profile_match_id,
                created_by=user_id
            };

            _referralService.Add(referral);

            //notify all the parties involved


            var notification = new mp_notification
            {
                created_by = "sys_admin",
                created_by_name = "System Admin",
                notification_type = 5,
                read = 0,
                user_id = profile.user_id,
                notification = "Hi " + profile.last_name + " " + profile.first_name + ", You have been referred to a provider for some services, check your referrals for more information" ,
                title = "New Referral"
            };

            NotificationUtil.Add(notification);

            await _emailSender.SendEmailAsync(profile.email, "New Referral - MySpace MyTime",
                  $"Hi " + profile.last_name + " " + profile.first_name + ", You have been referred to a provider for some services, login to your account and check your referrals for more information");


            notification = new mp_notification
            {
                created_by = "sys_admin",
                created_by_name = "System Admin",
                notification_type = 5,
                read = 0,
                user_id = clinician.user_id,
                notification = "Hi " + clinician.last_name + " " + clinician.first_name + ", you have successfully referred"+ profile.last_name + " " + profile.first_name + " to another provider for additional services. More information about this is available in your referrals.",
                title = "New Referral"
            };



            NotificationUtil.Add(notification);

            await _emailSender.SendEmailAsync(clinician.email, "New Referral - MySpace MyTime",
                $"Hi " + clinician.last_name + " " + clinician.first_name + ", you have successfully referred" + profile.last_name + " " + profile.first_name + " to another provider for additional services. More information about this is available in your referrals when you login to your account");

            return Ok(200);
        }
    }
}