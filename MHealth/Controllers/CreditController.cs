using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MHealth.Controllers
{
    public class CreditController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICreditService _creditService;
        private readonly IProfileService _profileService;
        private readonly ILogger<CreditController> _logger;

        public CreditController(UserManager<ApplicationUser> userManager, ICreditService creditService, IProfileService profileService, ILogger<CreditController> logger)
        {
            _userManager = userManager;
            _creditService = creditService;
            _profileService = profileService;
            _logger = logger;
        }

        public IActionResult PostCredit(mp_credit credit)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            credit.profile_id = profile.id;

            try
            {
                _creditService.Add(credit);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error crediting an account",ex);
                return Ok(500);
            }
            

            return Ok(200);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}