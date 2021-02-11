using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers
{
    public class RatingsController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        IRatingService _ratingService;
        private readonly IProfileService _profileService;
        private readonly IAppointmentService _appointmentService;
        public RatingsController(UserManager<ApplicationUser> userManager, IRatingService ratingService, IProfileService profileService, IAppointmentService appointmentService)
        {
            _userManager = userManager;
            _ratingService = ratingService;
            _profileService = profileService;
            _appointmentService = appointmentService;
        }

        public IActionResult LoadPartial(Guid appointment_id)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
           
            var appointment = _appointmentService.Get(appointment_id);
            var rating = _ratingService.Get().FirstOrDefault(e => e.appointment_id == appointment_id && e.client_id==appointment.client_id);
            if (rating != null)
            {
                return PartialView(rating);
            }
            rating = new mp_clinician_rating
            {
                client_id = appointment.client_id,
                clinician_id = appointment.clinician_id,
                appointment_id=appointment_id
            };

            return PartialView(rating);
        }

        public IActionResult Post(mp_clinician_rating rating)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);
            rating.created_by = user_id;
            rating.client_id = profile.id;

            _ratingService.AddOrUpdate(rating);

            return Ok(200);
        }
    }
}