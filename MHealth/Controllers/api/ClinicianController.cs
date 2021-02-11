using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using DAL.ViewModels;
using MHealth.Data;
using MHealth.Data.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers.api
{
    //[Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClinicianController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClinicianService _clinicianService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IAppointmentService _appointmentService;
        private readonly IClinicianAvailabilityService _clinicianAvailabilityService;
        private readonly IProfileService _profileService;
        private readonly IServiceCostService _serviceCostService;
        private readonly IProviderCategoryService _providerCategoryService;

        public ClinicianController(IClinicianService clinicianService, UserManager<ApplicationUser> userManager, IEnrollmentService enrollmentService, IAppointmentService appointmentService, IClinicianAvailabilityService clinicianAvailabilityService, IProfileService profileService, IServiceCostService serviceCostService, IProviderCategoryService providerCategoryService)
        {
            _clinicianService = clinicianService;
            _enrollmentService = enrollmentService;
            _userManager = userManager;
            _appointmentService = appointmentService;
            _clinicianAvailabilityService = clinicianAvailabilityService;
            _profileService = profileService;
            _serviceCostService = serviceCostService;
            _providerCategoryService = providerCategoryService;
        }

        public IActionResult GetDoctor()
        {
            var clinician = _clinicianService.Get().FirstOrDefault();
            return Ok(new DoctorModel(clinician));
        }

        [HttpPost]
        public IActionResult GetClinicianAvailability(mp_appointment appointment)
        {
            //check if clinician has availability setting matching these criteria
            var availability = _clinicianAvailabilityService.GetClinicianAvailabilityByDateRange(appointment);

            if (availability == null)
            {
                return Ok(false);
            }

            //now check if the clinician already an appointment for the same schedule
            var matching_existing_appointments = _appointmentService.GetClinicianAppointmentsByDateRange(appointment);

            if (matching_existing_appointments.Count() > 0)
            {
                return Ok(false);
            }


            return Ok(true);
        }

        [HttpPost]
        public IActionResult GetClinicians(SearchDoctorModel search)
        {
            var provider_category = _providerCategoryService.GetClinicianCategory();
            var areas = Options.Getlookups("area_of_interest");
            var clinicians = _clinicianService.GetClinicians();
            List<DoctorModel> doctorModels = new List<DoctorModel>();

            if (search.appointmentType.HasValue)
            {
                provider_category = provider_category.Where(e => e.appointment_type == search.appointmentType.Value);
            }
            if (search.appointmentCategory.HasValue)
            {
                provider_category = provider_category.Where(e => e.appointment_category == search.appointmentCategory.Value);
            }
            if (search.appointmentActivity.HasValue)
            {
                provider_category = provider_category.Where(e => e.appointment_category_sub == search.appointmentActivity.Value);
            }

            if (provider_category.Any())
            {
                var ids = provider_category.Select(e => e.clinician_id).ToList();
                clinicians = clinicians.Where(e => ids.Contains(e.id));
            }

            //check if name is one of the search models
            if (!string.IsNullOrEmpty(search.name))
            {
                var category = areas.FirstOrDefault(x => x.value.ToLower() == search.name.ToLower());
                clinicians = clinicians.Where(e => e.last_name.ToLower().Contains(search.name.ToLower()) || e.first_name.ToLower().Contains(search.name.ToLower()) || (e.area_of_interest == (category == null ? -1 : category.id)));
            }

            if (clinicians.Count() > 0)
            {
                foreach (var clinician in clinicians)
                {
                    doctorModels.Add(new DoctorModel(clinician));
                }
            }



            return Ok(doctorModels);
        }

        [HttpPost]
        public IActionResult GetSearchClinicians(SearchDoctorModel search)
        {
            var provider_category = _providerCategoryService.GetClinicianCategory();
            var clinicians = _clinicianService.GetClinicians();

            if (search.appointmentType.HasValue)
            {
                provider_category = provider_category.Where(e => e.appointment_type == search.appointmentType.Value);
            }
            if (search.appointmentCategory.HasValue)
            {
                provider_category = provider_category.Where(e => e.appointment_category == search.appointmentCategory.Value);
            }
            if (search.appointmentActivity.HasValue)
            {
                provider_category = provider_category.Where(e => e.appointment_category_sub == search.appointmentActivity.Value);
            }

            if (provider_category.Any())
            {
                var ids = provider_category.Select(e => e.clinician_id).ToList();
                clinicians = clinicians.Where(e => ids.Contains(e.id));
            }

            //check if name is one of the search models
            if (!string.IsNullOrEmpty(search.name))
            {
                clinicians = clinicians.Where(e => e.last_name.ToLower().Contains(search.name.ToLower()) || e.first_name.ToLower().Contains(search.name.ToLower()));
            }

            var doctors = new List<DoctorModel>();
            foreach (var clinician in clinicians)
            {
                doctors.Add(new DoctorModel(clinician));
            }

            return Ok(doctors);
        }

        public IActionResult GetAvailability(Guid clinician_id)
        {
            return Ok(_clinicianAvailabilityService.Get().Where(e => e.clinician_id == clinician_id));
        }

        public IActionResult GetClinicianServices(Guid clinician_id)
        {
            var provider_categories = _providerCategoryService.GetClinicianCategory().Where(e => e.clinician_id == clinician_id).Select(e => e.appointment_category_sub).ToArray();

            var appointment_sub_services = Options.GetAppointmentSubServices().Where(e => provider_categories.Contains(e.activity_sub_id)).Select(e => e.appointment_service_id).ToArray();

            var appointment_services = Options.GetAppointmentServices().Where(e => appointment_sub_services.Contains(e.id));

            return Ok(appointment_services);
        }

        public IActionResult GetClinicianAvailability(Guid clinician_id)
        {
            var dictionary = new Dictionary<string, string>();
            var availabilities = _clinicianAvailabilityService.Get().Where(e => e.clinician_id == clinician_id);

            foreach (var availability in availabilities)
            {
                dictionary.Add("Start_" + availability.day_name, availability.start_time.ToString("hh:mm tt"));
                dictionary.Add("End_" + availability.day_name, availability.end_time.ToString("hh:mm tt"));
            }
            return Ok(dictionary);
        }

        [HttpPost]
        public IActionResult PostClinicianAvailability(AvailabilityModel model)
        {
            var days = new[] { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

            foreach (var day in days)
            {
                var availabilities = model.availability.Where(e => e.Key.EndsWith(day));
                if (availabilities.Any() && availabilities.Count() == 2)
                {
                    if (!string.IsNullOrEmpty(model.availability["End_" + day]) && !string.IsNullOrEmpty(model.availability["End_" + day]))
                    {
                        try
                        {
                            var end_time = Convert.ToDateTime(model.availability["End_" + day]);
                            var start_time = Convert.ToDateTime(model.availability["Start_" + day]);

                            _clinicianAvailabilityService.AddOrUpdate(
                                  new mp_clinician_availability
                                  {
                                      day_name = day,
                                      start_time = start_time,
                                      end_time = end_time,
                                      status = true,
                                      created_at = DateTime.Now,
                                      clinician_id = model.clinician_id
                                  });
                        }
                        catch (Exception)
                        {

                        }

                    }


                }
            }

            return Ok(200);
        }

        public IActionResult GetListClinicianAvailability(string date, Guid clinician_id)
        {
            var times = new List<string>();

            var selected_date = Convert.ToDateTime(date);
            var day = selected_date.ToString("dddd");

            var availability = _clinicianAvailabilityService.Get().FirstOrDefault(e => e.day_name == day && e.clinician_id == clinician_id);

            if (availability != null)
            {
                var split_date = TimeUtil.SplitDateRange(availability.start_time, availability.end_time, 30);

                foreach (var date_range in split_date)
                {
                    times.Add(date_range.Item2.ToString("hh:mm tt"));
                }
            }

            return Ok(times);
        }

        public IActionResult GetClinicianRatings(Guid clinician_id)
        {
            var appointment = _appointmentService.GetProfileAppointments(clinician_id)
                .Where(x => x.status == 170 && !string.IsNullOrEmpty(x.feedback_star))
                .OrderByDescending(z => z.created_at).Take(50);
            var appointmentTotalStar = appointment.Select(x => x.feedback_star).Select(int.Parse).Sum();
            var averageRating = appointmentTotalStar == 0 ? 0 : appointmentTotalStar / appointment.Count();

            var response = new
            {
                clinician_id,
                averageRating
            };

            return Ok(response);
        }
    }
}