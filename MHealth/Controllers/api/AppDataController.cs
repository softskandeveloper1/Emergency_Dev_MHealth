using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppDataController : ControllerBase
    {
        //// GET: api/AppData
        [HttpGet]
        public dynamic Get()
        {
            var appointment_type =  Options.GetAppointmentTypes().Select(e => new { e.id, e.name}).ToList();
            var appointment_services = Options.GetAppointmentServices().Select(e => new { e.id, e.name }).ToList();
            var appointment_activities = Options.GetAppointmentActivities().Select(e => new { e.id, e.name, e.appointment_service_id }).ToList();
            var appointment_sub_activities  = Options.GetAppointmentSubActivities().Select(e => new { e.id, e.name, e.appointment_activity_id }).ToList();
            
            return new { appointment_type, appointment_services, appointment_activities, appointment_sub_activities };
        }

        [HttpGet]
        public dynamic GetProfileSelectData()
        {
            var states = Options.GetStates().Select(e => new { e.id, e.name }).ToList();
            var countries = Options.GetCountries().Select(e => new { e.id, e.name }).ToList();
            var marital_statuses = Options.Getlookups("marital_status").Select(e => new { e.id, e.value}).ToList();
            var education_levels = Options.Getlookups("education_level").Select(e => new { e.id, e.value }).ToList();     
            return new { states, countries, marital_statuses, education_levels };
        }


    }
}