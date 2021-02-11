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
using MHealth.Data;
using Microsoft.AspNetCore.Http;
using MHealth.Data.ViewModel;
using MHealth.Helper;
using System.IO;
using Microsoft.Extensions.Logging;

namespace MHealth.Controllers
{
    [Authorize]
    public class ClinicianController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClinicianService _clinicianService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IAppointmentService _appointmentService;
        private readonly IClinicianAvailabilityService _clinicianAvailabilityService;
        private readonly IProfileService _profileService;
        private readonly IServiceCostService _serviceCostService;
        private readonly IProviderCategoryService _providerCategoryService;
        private readonly IEmailSender _emailSender;
        private readonly IApplicantService _applicantService;
        private readonly ILookUpService _lookUpService;
        private readonly IApplicantDocumentService _applicantDocumentService;
        private readonly IExpertiseService _expertiseService;
        private readonly IClinicianDocumentService _profileDocumentService;
        private readonly IPopulationService _populationService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IEducationService _educationService;
        private readonly ILanguageService _languageService;
        private readonly IPracticeService _practiceService;
        private readonly IOtherActivitiesService _otherActivitiesService;
        private readonly IClinicService _clinicService;

        public ClinicianController(IClinicianService clinicianService, UserManager<ApplicationUser> userManager, IEnrollmentService enrollmentService, IAppointmentService appointmentService, IClinicianAvailabilityService clinicianAvailabilityService, IProfileService profileService, IServiceCostService serviceCostService, IProviderCategoryService providerCategoryService, IApplicantService applicantService, ILookUpService lookUpService, IApplicantDocumentService applicantDocumentService, IExpertiseService expertiseService, IClinicianDocumentService profileDocumentService, IEmailSender emailSender, IPopulationService populationService, ISpecialtyService specialtyService, IEducationService educationService, ILanguageService languageService, IPracticeService practiceService, IOtherActivitiesService otherActivitiesService, ILogger<ApplicantController> log, IClinicService clinicService)
        {
            _clinicianService = clinicianService;
            _enrollmentService = enrollmentService;
            _userManager = userManager;
            _appointmentService = appointmentService;
            _clinicianAvailabilityService = clinicianAvailabilityService;
            _profileService = profileService;
            _serviceCostService = serviceCostService;
            _providerCategoryService = providerCategoryService;
            _applicantService = applicantService;
            _lookUpService = lookUpService;
            _expertiseService = expertiseService;
            _applicantDocumentService = applicantDocumentService;
            _profileDocumentService = profileDocumentService;
            _emailSender = emailSender;
            _specialtyService = specialtyService;
            _populationService = populationService;
            _educationService = educationService;
            _languageService = languageService;
            _practiceService = practiceService;
            _otherActivitiesService = otherActivitiesService;
            _clinicService = clinicService;
        }

        public IActionResult Clinicians(int appointment_type, int appointment_category, int appointment_category_sub)
        {
            ViewBag.appointment_type = appointment_type;
            ViewBag.appointment_category = appointment_category;
            ViewBag.appointment_category_sub = appointment_category_sub;
            var profiles = _clinicianService.Get();

            if (appointment_type == 1)
            {
                profiles = profiles.Where(e => e.area_of_interest == 165 || e.area_of_interest == 166);
            }
            else
            {
                profiles = profiles.Where(e => e.area_of_interest == 167);
            }

            return View(profiles);
        }

        public async Task<IActionResult> AllClinicians(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var profiles = _clinicianService.Get();
            if (!string.IsNullOrEmpty(query))
            {
                profiles.Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.phone.Contains(query) || e.preferred_name.Contains(query) || e.email.Contains(query));
            }
            return View(await PaginatedList<mp_clinician>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "clinician")]
        [ConsentAttribute(roles = "clinician")]
        public IActionResult MyProfile()
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);
            //get all the appointments of the profile
            ViewBag.appointment = _appointmentService.GetProfileAppointments(profile.id);
            ViewBag.schedules = _clinicianAvailabilityService.Get(profile.id);
            //get the clients of the clinician
            ViewBag.profiles = _profileService.Get().Where(e => e.profile_type == 1);
            return View("CProfile", profile);

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

            var profile = _clinicianService.Get().FirstOrDefault(e => e.id == id);
            //get all the appointments of the profile
            //ViewBag.appointment = _appointmentService.GetProfileAppointments(id);

            ViewBag.appointment = _appointmentService.GetProfileAppointments(profile.id);
            ViewBag.schedules = _clinicianAvailabilityService.Get(profile.id);

            return View("CProfile", profile);



        }

        // GET: Profile
        public IActionResult Index()
        {
            return View(_clinicianService.Get());
        }

        // GET: Profile/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_profile = _clinicianService.Get((Guid)id);
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
        public IActionResult Create([Bind("last_name,first_name,phone,email,preferred_name,address,city,state,country,dob,user_id,profile_type,education_level,status,marital_status")] mp_clinician mp_profile)
        {
            if (ModelState.IsValid)
            {
                _clinicianService.Add(mp_profile);
                return RedirectToAction(nameof(Index));
            }
            return View(mp_profile);
        }

        // GET: Profile/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                var user_id = _userManager.GetUserId(HttpContext.User);
                var profile = _clinicianService.Get().Include(x => x.mp_clinician_category).Include(x => x.mp_clinician_language)
                    .Include(x => x.mp_clinician_education).Include(x => x.mp_clinician_practice).Include(x => x.mp_clinician_other_activities).FirstOrDefault(e => e.user_id == user_id);
                if (profile != null)
                {
                    return View(profile);
                }
                return NotFound();
            }

            var mp_profile = _clinicianService.Get((Guid)id);
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
        public async Task<IActionResult> Edit(Guid id, mp_clinician mp_profile)
        {
            if (id != mp_profile.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var collection = Request.Form;
                try
                {
                    //add languages
                    if (!string.IsNullOrEmpty(collection["language_spoken"]))
                    {
                        var languages_spoken = collection["language_spoken"].ToString().Split(',');
                        var applicant_languages = new List<mp_clinician_language>();
                        _languageService.DeleteClinicianLanguage(id);
                        foreach (var language in languages_spoken)
                        {
                            applicant_languages.Add(new mp_clinician_language { clinician_id = id, language = Convert.ToInt32(language) });
                        }
                        _languageService.AddClinicianLanguage(applicant_languages);
                    }                    

                    string rootDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "") + "\\Uploads\\";
                    if (!Directory.Exists(rootDir))
                        Directory.CreateDirectory(rootDir);
                    var doc_type = collection["doc_type"].ToString().Split(',');

                    var clinicianDocuments = _profileDocumentService.GetByClinician(id);
                    var profilePhoto = collection.Files["profile_img"];
                    var resume = collection.Files["resume"];
                    var certificate = collection.Files["certificate"];
                    if (profilePhoto != null)
                    {
                        string uploadsFolder = Path.Combine("wwwroot", "images", "applicant");
                        string filePath = Path.Combine(uploadsFolder, id + ".jpg");
                        using var fileStream = new FileStream(filePath, FileMode.Create);
                        await profilePhoto.CopyToAsync(fileStream);
                    }
                    if (resume != null)
                    {
                        //save the file
                        var path = rootDir + "/" + resume.FileName;
                        using (var stream = System.IO.File.Create(path))
                        {
                            await resume.CopyToAsync(stream);
                        }
                        var docType = Convert.ToInt32(collection["Resume_doc_type"]);

                        mp_clinician_document applicant_Documents = new mp_clinician_document
                        {

                            clinician_id = id,
                            document_type = docType,
                            path = path
                        };

                        var document = clinicianDocuments.FirstOrDefault(x => x.document_type == docType);
                        if (document != null)
                        {
                            _profileDocumentService.RemoveProfileDocument(document.id);
                        }
                        _profileDocumentService.Add(applicant_Documents);
                    }
                    if (certificate != null)
                    {
                        //save the file
                        var path = rootDir + "/" + certificate.FileName;
                        using (var stream = System.IO.File.Create(path))
                        {
                            await certificate.CopyToAsync(stream);
                        }
                        var docType = Convert.ToInt32(collection["Certificate_doc_type"]);
                        mp_clinician_document applicant_Documents = new mp_clinician_document
                        {
                            clinician_id = id,
                            document_type = docType,
                            path = path
                        };

                        var document = clinicianDocuments.FirstOrDefault(x => x.document_type == docType);
                        if (document != null)
                        {
                            _profileDocumentService.RemoveProfileDocument(document.id);
                        }
                        _profileDocumentService.Add(applicant_Documents);
                    }

                    //update the education information of the applicant

                    var name_of_school = collection["name_of_school"].ToList();
                    var address_of_school = collection["address_of_school"].ToList();
                    var certifications = collection["certification"].ToList();

                    var applicant_educations = new List<mp_clinician_education>();
                    _educationService.DeleteClinicianEducation(id);
                    for (var j = 0; j < name_of_school.Count(); j++)
                    {
                        applicant_educations.Add(new mp_clinician_education { clinician_id = id, address = address_of_school[j], school = name_of_school[j], certification = certifications[j] });
                    }

                    _educationService.AddClinicianEducation(applicant_educations);

                    var practice_hospital = collection["practice_hospital"].ToList();
                    var practice_city = collection["practice_city"].ToList();
                    var practice_role = collection["practice_role"].ToList();
                    var practice_duration = collection["practice_duration"].ToList();

                    var applicant_practices = new List<mp_clinician_practice>();
                    for (var j = 0; j < practice_hospital.Count(); j++)
                    {
                        if (!string.IsNullOrEmpty(practice_hospital[j]) && !string.IsNullOrEmpty(practice_city[j]) && !string.IsNullOrEmpty(practice_duration[j]) && !string.IsNullOrEmpty(practice_role[j]))
                        {
                            applicant_practices.Add(new mp_clinician_practice { clinician_id = id, hospital = practice_hospital[j], city = practice_city[j], duration = practice_duration[j], role = practice_role[j] });
                        }

                    }
                    _practiceService.DeleteClinicianPractice(id);
                    _practiceService.AddClinicianPractice(applicant_practices);

                    //add other activities

                    var activities = collection["activity_outstanding_work"].ToList();
                    var activity_outstanding_work = new List<mp_clinician_other_activities>();
                    _otherActivitiesService.DeleteClinicianActivities(id);
                    foreach (var activity in activities)
                    {
                        activity_outstanding_work.Add(new mp_clinician_other_activities { clinician_id = id, activity = activity, activity_type = "activity_outstanding_work" });
                    }
                    _otherActivitiesService.AddClinicianActivities(activity_outstanding_work);

                    activities = collection["activity_scholary"].ToList();
                    activity_outstanding_work = new List<mp_clinician_other_activities>();

                    foreach (var activity in activities)
                    {
                        activity_outstanding_work.Add(new mp_clinician_other_activities { clinician_id = id, activity = activity, activity_type = "activity_scholary" });
                    }
                    _otherActivitiesService.AddClinicianActivities(activity_outstanding_work);

                    activities = collection["activity_social"].ToList();
                    activity_outstanding_work = new List<mp_clinician_other_activities>();

                    foreach (var activity in activities)
                    {
                        activity_outstanding_work.Add(new mp_clinician_other_activities { clinician_id = id, activity = activity, activity_type = "activity_social" });
                    }
                    _otherActivitiesService.AddClinicianActivities(activity_outstanding_work);

                    _clinicianService.Update(mp_profile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_clinicianService.ProfileExists(mp_profile.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit));
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

            var mp_profile = _clinicianService.Get((Guid)id);
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
            _clinicianService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult UpdateAvailability(IFormCollection collection)
        {
            try
            {
                var user_id = _userManager.GetUserId(HttpContext.User);
                Guid clinician_id = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id).id;

                var dayDate = DateTime.ParseExact(collection["day_name"], "MM/dd/yyyy", null);
                var startTime = DateTime.ParseExact(collection["start_time"], "h:mm tt", null).TimeOfDay;
                var endTime = DateTime.ParseExact(collection["end_time"], "h:mm tt", null).TimeOfDay;
                var dayName = dayDate.DayOfWeek.ToString();
                _clinicianAvailabilityService.AddOrUpdate(
                    new mp_clinician_availability
                    {
                        day_name = dayName,
                        start_time = dayDate.Add(startTime),
                        end_time = dayDate.Add(endTime),
                        status = bool.Parse(collection["status"]),
                        created_at = DateTime.Now,
                        clinician_id = clinician_id
                    });
                //return RedirectToAction("MyProfile");
                return Redirect(Request.Headers["Referer"].ToString());
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message + ex.InnerException;
                var user_id = _userManager.GetUserId(HttpContext.User);
                var profile = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);
                //get all the appointments of the profile
                ViewBag.appointment = _appointmentService.GetProfileAppointments(profile.id);
                ViewBag.schedules = _clinicianAvailabilityService.Get(profile.id);
                //get the clients of the clinician
                ViewBag.profiles = _profileService.Get().Where(e => e.profile_type == 1);
                return View("CProfile", profile);
            }
        }

        public IActionResult UpdateServiceCost(IFormCollection collection)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var clinician = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);

            var services = Options.GetAppointmentServices();

            var sub_ids = collection["sub_id"].ToList();
            var service_ids = collection["service_id"].ToList();
            var amounts = collection["amount"].ToList();

            for (var i = 0; i < sub_ids.Count; i++)
            {
                var service_cost = new mp_service_costing
                {
                    //appointment_type_id = 1,
                    clinician_id = clinician.id,
                    created_by = user_id
                };

                var cost = amounts[i].ToString();
                var appointment_activity_sub = sub_ids[i].ToString();
                var service_id = Convert.ToInt32(service_ids[i]);
                if (!string.IsNullOrEmpty(cost))
                {
                    service_cost.cost = Convert.ToDecimal(cost);
                    service_cost.appointment_service_id = service_id;
                    service_cost.appointment_activity_sub_id = Convert.ToInt32(appointment_activity_sub);

                    _serviceCostService.AddOrUpdate(service_cost);
                }
            }


            //foreach (var sv in services)
            //{
            //    var service_cost = new mp_service_costing
            //    {
            //        //appointment_type_id = 1,
            //        clinician_id = clinician.id,
            //        created_by = user_id
            //    };

            //    var cost = collection["apt_" + sv.id].ToString();
            //    var appointment_activity_sub = collection["sub_" + sv.id].ToString();
            //    if (!string.IsNullOrEmpty(cost))
            //    {
            //        service_cost.cost = Convert.ToDecimal(cost);
            //        service_cost.appointment_service_id = sv.id;
            //        service_cost.appointment_activity_sub_id =Convert.ToInt32(appointment_activity_sub);

            //        _serviceCostService.AddOrUpdate(service_cost);
            //    }

            //}
            return Redirect(Request.Headers["Referer"].ToString());
        }


        public IActionResult UpdateID(IFormCollection collection)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var clinician = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);

            clinician.bank_name = collection["bank_name"];
            clinician.account_number = collection["account_number"];
            clinician.account_name = collection["account_name"];
            clinician.mode_of_identification = collection["mode_of_identification"];
            clinician.identification_number = collection["identification_number"];

            _clinicianService.Update(clinician);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> RemoveAvailability(long id)
        {
            await _clinicianAvailabilityService.Remove(id);

            return RedirectToAction("MyProfile");
        }

        [AllowAnonymous]
        public IActionResult MatchClinicians()
        {
            //var clinicians = _clinicianService.Get();
            return PartialView(_clinicianService.Get());
        }

        public IActionResult Settings()
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);
            var costing_models = new List<CostingModel>();

            var provider_categories = _providerCategoryService.GetClinicianCategory().Where(e => e.clinician_id == profile.id).Include(e => e.appointment_category_subNavigation);

            foreach (var provider_category in provider_categories)
            {
                var appointment_sub_services = Options.GetAppointmentSubServices().Where(e => e.activity_sub_id == provider_category.appointment_category_sub).Select(e => e.appointment_service_id);

                var appointment_services = Options.GetAppointmentServices().Where(e => appointment_sub_services.Contains(e.id));

                foreach (var appointment_service in appointment_services)
                {
                    costing_models.Add(new CostingModel { service_id = appointment_service.id, service_name = appointment_service.name, sub_id = provider_category.appointment_category_sub, sub_name = provider_category.appointment_category_subNavigation.name });
                }
            }

            ViewBag.costing_models = costing_models;

            //var appointment_services = Options.GetAppointmentServices().Where(e => appointment_sub_services.Contains(e.id));


            ViewBag.service_costs = _serviceCostService.Get().Where(e => e.clinician_id == profile.id);
            ViewBag.schedules = _clinicianAvailabilityService.Get(profile.id);
            return View(profile);
        }

        public async Task<IActionResult> Providers(int? page, string query = null)
        {
            int pageSize = 25;
            ViewBag.query = query;
            var profiles = _clinicianService.Get();
            if (!string.IsNullOrEmpty(query))
            {
                profiles = profiles.Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.phone.Contains(query) || e.preferred_name.Contains(query) || e.email.Contains(query));
            }
            return View(await PaginatedList<mp_clinician>.CreateAsync(profiles.Include(e => e.mp_appointment).ThenInclude(e => e.mp_credit).OrderByDescending(e => e.last_name).AsNoTracking(), page ?? 1, pageSize));

        }

        public IActionResult Consent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Consent(IFormCollection collection)
        {


            if (collection["consent_checkbox"].ToString().ToLower() == "on")
            {
                var user_id = _userManager.GetUserId(HttpContext.User);
                var profile = _clinicianService.GetByUserId(user_id);
                profile.consent_signed = 1;
                _clinicianService.Update(profile);

                return RedirectToAction("MyProfile");
            }

            return View();
        }
    }
}
