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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        public AppointmentController(IAppointmentService appointmentService, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _appointmentService = appointmentService;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public IActionResult GetAppointments()
        {
            var appointments = _appointmentService.Get().Include(e => e.clinician_).Include(e => e.appointment_typeNavigation).Include(e => e.client_).Include(e => e.appointment_serviceNavigation);
            var appointment_models = new List<AppointmentModel>();
            foreach (var appointment in appointments)
            {
                appointment_models.Add(new AppointmentModel(appointment));
            }

            return Ok(appointment_models);
        }

        public IActionResult GetProfileAppointments(Guid profile_id)
        {
            var appointments = _appointmentService.GetProfileAppointments(profile_id);
            var appointment_models = new List<AppointmentModel>();
            foreach (var appointment in appointments)
            {
                appointment_models.Add(new AppointmentModel(appointment));
            }

            return Ok(appointment_models);
        }


        public IActionResult GetAppointment(Guid appointment_id)
        {
            var appointment = _appointmentService.Get().Include(e => e.clinician_).Include(e => e.appointment_typeNavigation).Include(e => e.client_).Include(e => e.appointment_serviceNavigation).FirstOrDefault(e => e.id == appointment_id);
            var appointment_model = new AppointmentModel(appointment);

            return Ok(appointment_model);
        }

        public async Task<IActionResult> PostAppointment(mp_appointment appointment)
        {
            //  var email = _userManager.GetUserId(HttpContext.User);
            //  var user = await _userManager.FindByEmailAsync(email);

            appointment.created_by = appointment.client_id.ToString();
            _appointmentService.Add(appointment);

            return Ok(appointment.id);
        }

        public async Task<IActionResult> CompleteAppointment(Guid appointment_id)
        {
            var email = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByEmailAsync(email);
            var appointment = _appointmentService.Get().Include(x => x.client_).Include(x => x.clinician_).FirstOrDefault(e => e.id == appointment_id);
            appointment.status = 170;
            _appointmentService.Update(appointment);
            //await new NotificationHelper(_emailSender).AppointmentScheduled(appointment);
            return Ok(200);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteAppointmentWithRating(Guid appointment_id, string star, string feedback)
        {
            //var email = _userManager.GetUserId(HttpContext.User);
            //var user = await _userManager.FindByEmailAsync(email);
            var appointment = _appointmentService.Get().Include(x => x.client_).Include(x => x.clinician_).FirstOrDefault(e => e.id == appointment_id);
            appointment.status = 170;
            appointment.feedback_star = star;
            appointment.feedback = feedback;
            _appointmentService.Update(appointment);
            //await new NotificationHelper(_emailSender).AppointmentScheduled(appointment);
            return Ok(200);
        }

        [HttpPost]
        public async Task<IActionResult> CancelAppointment(CancelAppointmentModel cancel)
        {
            //var email = _userManager.GetUserId(HttpContext.User);
            //var user = await _userManager.FindByEmailAsync(email);
            var appointment = _appointmentService.Get().FirstOrDefault(e => e.id == cancel.appointment_id);
            appointment.status = 171;
            appointment.cancel_reason = cancel.cancel_reason;
            _appointmentService.Update(appointment);
            return Ok(200);
        }

        public IActionResult GetDayAppointments(string day)
        {
            var appointment_models = new List<AppointmentModel>();
            var date = DateTime.Now;
            DateTime.TryParse(day, out date);

            var appointments = _appointmentService.Get().Where(e => e.start_date.Date == date.Date).Include(e => e.clinician_).Include(e => e.appointment_typeNavigation).Include(e => e.client_).Include(e => e.appointment_serviceNavigation);

            foreach (var appointment in appointments)
            {
                appointment_models.Add(new AppointmentModel(appointment));
            }

            return Ok(appointment_models);
        }

        public IActionResult GetAppointmentRating(Guid appointment_id)
        {
            var appointment = _appointmentService.Get(appointment_id);
            if (appointment == null)
            {
                return NotFound();
            }

            var response = new
            {
                appointment_id = appointment.id,
                appointment.feedback_star,
                appointment.feedback
            };

            return Ok(response);
        }
    }
}