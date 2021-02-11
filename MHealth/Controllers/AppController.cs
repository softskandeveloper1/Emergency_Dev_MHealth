using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Utils;
using MHealth.Entities;
using MHealth.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Twilio.Jwt.AccessToken;

namespace MHealth.Controllers
{
    [Authorize]
    public class AppController : Controller
    {
        IAppointmentService _appointmentService;
        private readonly TwilioSettings _twilioSettings;

        public AppController(IAppointmentService appointmentService, IOptions<TwilioSettings> twilioSettings)
        {
            _appointmentService = appointmentService;
            _twilioSettings = twilioSettings.Value;
        }

        public IActionResult Session(Guid appointment_id)
        {

            //var app_id = AppSetting.GetValue("comet_app_id");

            //var obj = new JObject();
            //obj.Add("appid", app_id);
            //obj.Add("apikey", "1b2b8e46ff8cf3eabe275e952dc3a77d221fe613");

            //get the appointment
            ViewBag.appointment_id = appointment_id;

            var appointment = _appointmentService.Get(appointment_id);
            ViewBag.token = new TwilioHelper().GenerateCode("");


            //var token = new HttpUtil().PostMessageCurl(string.Format("https://api-us.cometchat.io/v2.0/users/{0}/auth_tokens", comet_user), obj.ToString());
            return View(appointment);
        }

        public IActionResult CSession(Guid appointment_id)
        {
            ViewBag.appointment_id = appointment_id;
            var appointment = _appointmentService.Get(appointment_id);
            return View(appointment);
        }

        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public string post_token(string appointmentId)
        {
            var appointment = _appointmentService.Get(Guid.Parse(appointmentId));
            if (appointment.status == 171)
            {
                var ca = new JObject
                {
                    { "token", "Appointment was canceled" }
                };
                return ca.ToString();
            }

            var rs = new JObject
            {
                { "token", GenerateCode(appointmentId) }
            };
            return rs.ToString();
        }

        private string GenerateCode(string appointmentId)
        {
            // Create a Video grant for this token
            var grant = new VideoGrant
            {
                Room = appointmentId
            };

            var grants = new HashSet<IGrant> { grant };

            // Create an Access Token generator
            var token = new Token(
                _twilioSettings.TwilioAccountSid,
                _twilioSettings.TwilioApiKey,
                _twilioSettings.TwilioApiSecret,
                identity: User.Identity.Name.ToString(),
                grants: grants);

            return token.ToJwt();
        }
    }
}