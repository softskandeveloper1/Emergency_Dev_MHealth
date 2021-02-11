using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DAL.Utils;

namespace MHealth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        public IActionResult GetAppointmentActivities(int parent_id)
        {
            return Ok(Options.GetAppointmentActivities(parent_id));
        }

        public IActionResult GetAppointmentSubActivities(int parent_id)
        {
            return Ok(Options.GetAppointmentSubActivities(parent_id));
        }

        public IActionResult GetAppointmentTypes()
        {
            return Ok(Options.GetAppointmentTypes());
        }

        public IActionResult GetLookups()
        {
            return Ok(Options.GetAllLookUps());
        }
    }
}