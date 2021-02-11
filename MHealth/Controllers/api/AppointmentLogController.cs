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
    public class AppointmentLogController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppointmentLogService _appointmentLogService;

        public AppointmentLogController(IAppointmentLogService appointmentLogService, UserManager<ApplicationUser> userManager)
        {
            _appointmentLogService = appointmentLogService;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult PostLog([FromBody]mp_appointment_log log)
        {
            try
            {
                var user_id = _userManager.GetUserId(HttpContext.User);
                log.created_by = user_id;

                if (User.IsInRole("client"))
                {
                    log.role = "Member";
                }
                else if (User.IsInRole("clinician"))
                {
                    log.role = "Provider";
                }

                _appointmentLogService.Add(log);

                return Ok(200);
            }
            catch (Exception ex)
            {
                return Ok(400);
            }
           
        }
    }
}