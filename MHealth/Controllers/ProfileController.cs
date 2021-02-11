using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.IService;
using Microsoft.AspNetCore.Identity;
using DAL.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MHealth.Data;
using Npgsql;
using MHealth.Entities.ViewModel;
using NpgsqlTypes;
using System.IO;
using MHealth.Helper;

namespace MHealth.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IAppointmentService _appointmentService;
        private readonly IClinicianAvailabilityService _clinicianAvailabilityService;
        private readonly IChildrenService _childrenService;
        private readonly IProfileMatchService _profileMatchService;
        private readonly IProfileBankService _profileBankService;
        private readonly IProfileHMOService _profileHMOService;

        public ProfileController(IProfileService profileService, UserManager<ApplicationUser> userManager, IEnrollmentService enrollmentService,IAppointmentService appointmentService, IClinicianAvailabilityService clinicianAvailabilityService, IChildrenService childrenService, IProfileMatchService profileMatchService, IProfileBankService profileBankService, IProfileHMOService profileHMOService)
        {
            _profileService = profileService;
            _enrollmentService = enrollmentService;
            _userManager = userManager;
            _appointmentService = appointmentService;
            _clinicianAvailabilityService = clinicianAvailabilityService;
            _childrenService = childrenService;
            _profileMatchService = profileMatchService;
            _profileBankService = profileBankService;
            _profileHMOService = profileHMOService;
        }

       

        public async Task<IActionResult> Clients(int? pageNumber, string query=null)
        {
            int pageSize = 25;
            var profiles = _profileService.Get().Where(e => e.profile_type == 1);
            if (!string.IsNullOrEmpty(query))
            {
                profiles.Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.phone.Contains(query) || e.preferred_name.Contains(query) || e.email.Contains(query));
            }
            return View(await PaginatedList<mp_profile>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult Consent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Consent(IFormCollection collection)
        {
            

            if (collection["consent_checkbox"].ToString().ToLower() == "on" )
            {
                var user_id = _userManager.GetUserId(HttpContext.User);
                var profile = _profileService.GetProfileByUserId(user_id);
                profile.consent_signed = 1;
                _profileService.Update(profile);

                return RedirectToAction("MyProfile", "Profile");
            }
       
            return View();
        }

        public IActionResult ProfileInfo(Guid? id)
        {
            if (id.HasValue)
            {
                //get all the session forms of a client
                //var profile = _profileService.Get(id.Value);
                //var appointments = _appointmentService.GetProfileAppointments(profile.id);
            }

            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);
            //get all the appointments of the profile
            ViewBag.appointment = _appointmentService.GetProfileAppointments(profile.id);

            //var sql = "get_client_form_timeline";
            var sql = string.Format("SELECT * from public.get_client_form_timeline('{0}')",profile.id);

            var cmd = new NpgsqlCommand(sql);
            //cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("pro_id", NpgsqlDbType.Uuid, profile.id);
            var dt = DataAccess.GetDataTable(cmd);

            ViewBag.timelines = DataUtil.DataTableToList<TimelineItem>(dt);

            return View(profile);
        }

        public IActionResult PartialProfile(Guid profile_id)
        {
            return PartialView(_profileService.Get(profile_id));
        }
        
      
        [Authorize(Roles ="client")]
        [ConsentAttribute(roles ="client")]
        public IActionResult MyProfile()
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            //if (profile.consent_signed == 0)
            //{
            //    return RedirectToAction("Consent");
            //}
            //get all the appointments of the profile
            ViewBag.appointment = _appointmentService.GetProfileAppointments(profile.id);
            return View("MyProfile", profile);
            
        }

        //public IActionResult CProfile()
        //{
        //    var user_id = _userManager.GetUserId(HttpContext.User);
        //    var profile = _profileService.Get().FirstOrDefault(e => e.user_id == user_id);
        //    //get all the appointments of the profile
        //    ViewBag.appointment = _appointmentService.GetProfileAppointments(profile.id);
        //    return View(profile);
        //}

        public IActionResult Profile(Guid id)
        {

            var profile = _profileService.Get().FirstOrDefault(e => e.id == id);
            //get all the appointments of the profile
            ViewBag.appointment = _appointmentService.GetProfileAppointments(id);
            return View("MyProfile", profile);
            

            
        }

        // GET: Profile
        public IActionResult Index()
        {
            return View(_profileService.Get());
        }

        // GET: Profile/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_profile = _profileService.Get((Guid)id);
            if (mp_profile == null)
            {
                return NotFound();
            }

            return View(mp_profile);
        }

        // GET: Profile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("last_name,first_name,phone,email,preferred_name,address,city,state,country,dob,user_id,profile_type,education_level,status,marital_status")] mp_profile mp_profile)
        {
            if (ModelState.IsValid)
            {
                _profileService.Add(mp_profile);
                return RedirectToAction(nameof(Index));
            }
            return View(mp_profile);
        }

        // GET: Profile/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                //check if the user is logged in
                var user_id = _userManager.GetUserId(HttpContext.User);
                var profile = _profileService.GetProfileByUserId(user_id);
                if (profile != null)
                {
                    return View(profile);
                }
                return NotFound();
            }

            var mp_profile = _profileService.Get((Guid)id);
            if (mp_profile == null)
            {
                return NotFound();
            }
            return View(mp_profile);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("id,last_name,first_name,phone,email,preferred_name,address,city,state,country,dob,user_id,profile_type,education_level,status,marital_status")] mp_profile mp_profile)
        {
            if (id != mp_profile.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _profileService.Update(mp_profile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_profileService.ProfileExists(mp_profile.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mp_profile);
        }

        // GET: Profile/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_profile = _profileService.Get((Guid)id);
            if (mp_profile == null)
            {
                return NotFound();
            }

            return View(mp_profile);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            //_profileService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateAvailability(IFormCollection collection)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            Guid clinician_id = _profileService.Get().FirstOrDefault(e => e.user_id == user_id).id;

            var start_date = collection["start_date"];

            _clinicianAvailabilityService.AddOrUpdate(
                new mp_clinician_availability
                {
                    start_time = DateTime.Parse(collection["start_time"]),
                    end_time = DateTime.Parse(collection["end_time"]),
                    created_at = DateTime.Now,
                    clinician_id = clinician_id
                });
            return RedirectToAction("MyProfile");
        }

        public IActionResult Settings()
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);
            ViewBag.children = _childrenService.Get().Where(e => e.profile_id == profile.id);

            var profile_bank = _profileBankService.GetProfileBank(profile.id);
            if (profile_bank == null)
            {
                profile_bank = new mp_profile_bank();
            }
            ViewBag.bank = profile_bank;

            var profile_hmo = _profileHMOService.GetProfileHMO(profile.id);
            if (profile_hmo == null)
            {
                profile_hmo = new mp_profile_hmo();
            }
            ViewBag.profile_hmo = profile_hmo;

            return View();
        }

        public IActionResult EditEnrollment(int appointment_type, int appointment_category, int appointment_category_sub)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);
            ViewBag.appointment_type = appointment_type;
            ViewBag.appointment_category = appointment_category;
            ViewBag.appointment_category_sub = appointment_category_sub;

            var enrollment = _enrollmentService.Get(profile.id);

            if (enrollment == null)
            {
                enrollment = new mp_enrollment
                {
                    profile_id = profile.id
                };
            }

            return View(enrollment);
        }

        [HttpPost]
        public IActionResult EditEnrollment(mp_enrollment enrollment)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            var collection = Request.Form;
            var appointment_type = collection["appointment_type"];
            var appointment_category = collection["appointment_category"];
            var appointment_category_sub = collection["appointment_category_sub"];

            if (_enrollmentService.Get(enrollment.profile_id) != null)
            {
                _enrollmentService.Update(enrollment);
            }
            else
            {
                enrollment.created_by = user_id;
                enrollment.profile_id = profile.id;
                _enrollmentService.Add(enrollment);
            }
           

           
            return RedirectToAction("Clinicians", "Clinician",new {  appointment_type, appointment_category , appointment_category_sub });
        }


        public IActionResult EditQ(int appointment_type, int appointment_category, int appointment_category_sub)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            ViewBag.appointment_type = appointment_type;
            ViewBag.appointment_category = appointment_category;
            ViewBag.appointment_category_sub = appointment_category_sub;

            return View(profile);
        }

        [HttpPost]
        public IActionResult EditQ(mp_profile mp_profile)
        {
            var collection = Request.Form;

            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            var appointment_type = collection["appointment_type"];
            var appointment_category = collection["appointment_category"];
            var appointment_category_sub = collection["appointment_category_sub"];

            profile.gender = mp_profile.gender;
            profile.tribe = mp_profile.tribe;

            if (appointment_type == 2)
            {
                profile.years_of_experience = mp_profile.years_of_experience;
                profile.counselor_preference = mp_profile.counselor_preference;
            }

            _profileService.Update(profile);

            return RedirectToAction("Clinicians", "Clinician", new { appointment_type, appointment_category, appointment_category_sub });
        }


        public IActionResult Providers()
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            var profile_matches = _profileMatchService.Get().Where(e => e.profile_id == profile.id).Include(e => e.clinician_).Include(e => e.appointment_type_).Include(e => e.appointment_activity_).Include(e => e.appointment_activity_sub_);
            return View(profile_matches);
        }

        [HttpPost]
        public IActionResult UpdateBank(mp_profile_bank bank)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            bank.updated_by = user_id.ToString();
            bank.created_by = user_id.ToString();
            bank.profile_id = profile.id;
            _profileBankService.AddOrUpdate(bank);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public IActionResult UpdateHMO(mp_profile_hmo hmo)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            hmo.updated_by = user_id.ToString();
            hmo.created_by = user_id.ToString();
            hmo.profile_id = profile.id;
            _profileHMOService.AddOrUpdate(hmo);

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
