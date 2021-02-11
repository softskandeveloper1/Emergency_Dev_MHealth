using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class CreditController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ICreditService _creditService;
        UserManager<ApplicationUser> _userManager;

        public CreditController(UserManager<ApplicationUser> userManager, ICreditService creditService)
        {
            _userManager = userManager;
            _creditService = creditService;
        }

        [HttpPost]
        public async Task<IActionResult> PostCredit(mp_credit credit)
        {
            // var email = _userManager.GetUserId(HttpContext.User);
            // var user = await _userManager.FindByEmailAsync(email);
            credit.created_by = "self"; //user.Id;

            try
            {
                _creditService.Add(credit);
            }
            catch (Exception ex)
            {
                return Ok(500);
            }

            return Ok(200);
        }


    }
}