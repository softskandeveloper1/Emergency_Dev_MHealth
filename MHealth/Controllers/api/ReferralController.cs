using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using MHealth.Data;
using MHealth.Data.ViewModel;
using MHealth.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers.api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReferralController : ControllerBase
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetReferrals()
        {
            var email = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByEmailAsync(email);

            var referral_models = new List<ReferralModel>();

            if (await _userManager.IsInRoleAsync(user, "client"))
            {
                
                var profile = _profileService.Get().FirstOrDefault(e => e.user_id == user.Id);
                var referrals = _referralService.Get().Where(e => e.profile_id == profile.id)
                .Include(e => e.clinician_)
                .Include(e => e.profile_)
                .Include(e => e.profile_match_).ThenInclude(e => e.appointment_type_)
                .Include(e => e.profile_match_).ThenInclude(e => e.appointment_activity_)
                .Include(e => e.profile_match_).ThenInclude(e => e.appointment_activity_sub_);

                

                foreach (var referral in referrals)
                {
                    referral_models.Add(new ReferralModel(referral));
                }

                return Ok(referral_models);
            }
            else if (await _userManager.IsInRoleAsync(user, "clinician"))
            {
       
                var clinician = _clinicianService.Get().FirstOrDefault(e => e.user_id == user.Id);

                var referrals = _referralService.Get().Where(e => e.clinician_id == clinician.id)
                .Include(e => e.clinician_)
                .Include(e => e.profile_)
                .Include(e => e.profile_match_).ThenInclude(e => e.appointment_type_)
                .Include(e => e.profile_match_).ThenInclude(e => e.appointment_activity_)
                .Include(e => e.profile_match_).ThenInclude(e => e.appointment_activity_sub_);

                foreach (var referral in referrals)
                {
                    referral_models.Add(new ReferralModel(referral));
                }

                return Ok(referral_models);
            }

          

            return Ok(referral_models);

        }

        [HttpPost]
        public async Task<IActionResult> Post(ReferralModel model)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var clinician = _clinicianService.Get().FirstOrDefault(e => e.id == model.clinician_id);


            var appointment_type_id =model.profile_match.appointment_type_id;
            var appointment_activity_id = model.profile_match.appointment_activity_id;
            var appointment_activity_sub_id = model.profile_match.appointment_activity_sub_id;

            var profile_id = model.profile_id;
            var clinician_id = model.clinician_id;

            var profile = _profileService.Get(profile_id);

            var profile_match = new mp_profile_match
            {
                appointment_type_id = appointment_type_id,
                appointment_activity_id = appointment_activity_id,
                appointment_activity_sub_id = appointment_activity_sub_id,
                clinician_id = clinician_id,
                profile_id = profile_id
            };
            var profile_match_id = _profileMatchService.Add(profile_match);

            var referral = new mp_referral
            {
                profile_id = profile_id,
                clinician_id = clinician.id,
                profile_match_id = profile_match_id,
                created_by = model.created_by
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
                notification = "Hi " + profile.last_name + " " + profile.first_name + ", You have been referred to a provider for some services, check your referrals for more information",
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
                notification = "Hi " + clinician.last_name + " " + clinician.first_name + ", you have successfully referred" + profile.last_name + " " + profile.first_name + " to another provider for additional services. More information about this is available in your referrals.",
                title = "New Referral"
            };



            NotificationUtil.Add(notification);

            await _emailSender.SendEmailAsync(clinician.email, "New Referral - MySpace MyTime",
                $"Hi " + clinician.last_name + " " + clinician.first_name + ", you have successfully referred" + profile.last_name + " " + profile.first_name + " to another provider for additional services. More information about this is available in your referrals when you login to your account");

            return Ok(200);
        }
    }
}