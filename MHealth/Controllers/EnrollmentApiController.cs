using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.BL;
using DAL.IService;
using MHealth.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class EnrollmentApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;
        public EnrollmentApiController(IProfileService profileService, UserManager<ApplicationUser> userManager)
        {
            _profileService = profileService;
            _userManager = userManager;
        }
        public IActionResult GetQuestionaireStatus(int appointment_type)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);
            var status = new ProviderSelection().isQuestionaireCompleted(appointment_type, profile.id);
            return Ok(status);
        }
    }
}