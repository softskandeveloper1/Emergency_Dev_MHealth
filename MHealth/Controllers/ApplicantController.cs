using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.IService;
using Newtonsoft.Json;
using MHealth.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Identity;
using MHealth.Helper;
using Microsoft.AspNetCore.Authorization;
using MHealth.Data;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Encodings.Web;
using DAL.Utils;

namespace MHealth.Controllers
{
    [Authorize]
    public class ApplicantController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IApplicantService _applicantService;
        private readonly ILookUpService _lookUpService;
        private readonly IApplicantDocumentService _applicantDocumentService;
        private readonly IExpertiseService _expertiseService;
        private readonly IClinicianDocumentService _profileDocumentService;
        private readonly IClinicianService _profileService;
        private readonly IPopulationService _populationService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IEducationService _educationService;
        private readonly ILanguageService _languageService;
        private readonly IPracticeService _practiceService;
        private readonly IOtherActivitiesService _otherActivitiesService;
        private readonly IProviderCategoryService _providerCategoryService;
        private readonly IClinicianService _clinicianService;
        private readonly IClinicService _clinicService;

        readonly ILogger<ApplicantController> _log;

        public ApplicantController(IApplicantService applicantService, ILookUpService lookUpService, IApplicantDocumentService applicantDocumentService, IExpertiseService expertiseService, IClinicianDocumentService profileDocumentService, IClinicianService profileService, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IPopulationService populationService, ISpecialtyService specialtyService, IEducationService educationService, ILanguageService languageService, IPracticeService practiceService, IOtherActivitiesService otherActivitiesService, ILogger<ApplicantController> log, IProviderCategoryService providerCategoryService, IClinicianService clinicianService, IClinicService clinicService)
        {
            _applicantService = applicantService;
            _lookUpService = lookUpService;
            _expertiseService = expertiseService;
            _applicantDocumentService = applicantDocumentService;
            _profileDocumentService = profileDocumentService;
            _profileService = profileService;
            _userManager = userManager;
            _emailSender = emailSender;
            _specialtyService = specialtyService;
            _populationService = populationService;
            _educationService = educationService;
            _log = log;
            _languageService = languageService;
            _practiceService = practiceService;
            _otherActivitiesService = otherActivitiesService;
            _providerCategoryService = providerCategoryService;
            _clinicianService = clinicianService;
            _clinicService = clinicService;
        }

        //GET: Applicant
        public async Task<IActionResult> Index()
        {
            return View(await _applicantService.Get().ToListAsync());
        }

        public async Task<IActionResult> Providers(int? page, int? application_status, string query = null)
        {
            //return View(await _clinicianService.GetAll().Where(e => e.status == 3 && e.provider_type == 177).ToListAsync());

            int pageSize = 25;
            ViewBag.query = query;
            var profiles = _clinicianService.GetAll();
            if (application_status.HasValue)
            {
                profiles = profiles.Where(e => e.status == application_status.Value);
            }
            if (!string.IsNullOrEmpty(query))
            {
                profiles = profiles.Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.phone.Contains(query) || e.preferred_name.Contains(query) || e.email.Contains(query));
            }
            return View(await PaginatedList<mp_clinician>.CreateAsync(profiles.OrderByDescending(e => e.last_name).AsNoTracking(), page ?? 1, pageSize));

        }

        public IActionResult Provider(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_applicant = _clinicianService.GetAll()
                .Include(e => e.mp_clinician_education)
                .Include(e => e.mp_clinician_document)
                .Include(e => e.mp_clinician_language)
                .Include(e => e.mp_clinician_other_activities)
                .Include(e => e.mp_clinician_practice)
                .FirstOrDefault(e => e.id == id.Value);

            if (mp_applicant == null)
            {
                return NotFound();
            }

            return View(mp_applicant);
        }


        [AllowAnonymous]
        public IActionResult Completed(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_applicant = _clinicianService.Get((Guid)id);
            if (mp_applicant == null)
            {
                return NotFound();
            }

            return View(mp_applicant);
        }

        public IActionResult CompletedMobile(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_applicant = _clinicianService.Get((Guid)id);
            if (mp_applicant == null)
            {
                return NotFound();
            }

            return View(mp_applicant);
        }

        [AllowAnonymous]
        public IActionResult ContinueRegistration(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_applicant = _applicantService.Get((Guid)id);
            if (mp_applicant == null)
            {
                return NotFound();
            }

            if (mp_applicant.statusNavigation.value == "Pending")
            {
                return RedirectToAction("Completed", new { id = id });
            }

            return View(mp_applicant);
        }

        // GET: Applicant/Details/5
        public IActionResult Applicant(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_applicant = _applicantService.Get((Guid)id);

            if (mp_applicant == null)
            {
                return NotFound();
            }

            return View(mp_applicant);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult NewApplicant(IFormCollection collection)
        {
            ViewBag.appointment_type = collection["appointment_type_id"];
            ViewBag.appointment_category = collection["appointment_category_id"];
            ViewBag.appointment_category_sub = collection["appointment_category_sub_id"];
            ViewBag.provider_type = Convert.ToInt32(collection["provider_type"]);
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult NewApplicantMobile(IFormCollection collection)
        {
            ViewBag.appointment_type = collection["appointment_type_id"];
            ViewBag.appointment_category = collection["appointment_category_id"];
            ViewBag.appointment_category_sub = collection["appointment_category_sub_id"];
            ViewBag.provider_type = Convert.ToInt32(collection["provider_type"]);
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ContinueRegistration(IFormCollection collection)
        {
            var applicant_id = Guid.Parse(collection["applicant_id"]);
            var applicant = _applicantService.Get(Guid.Parse(collection["applicant_id"]));


            var doc_type = collection["doc_type"].ToString().Split(',');

            string rootDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "") + "\\Uploads\\";
            if (!Directory.Exists(rootDir))
                Directory.CreateDirectory(rootDir);

            int count = 0;
            List<mp_applicant_document> applicant_Documents = new List<mp_applicant_document>();
            foreach (var item in collection.Files)
            {
                if (item.Length > 0)
                {
                    //save the file
                    var path = rootDir + "/" + item.FileName;
                    using (var stream = System.IO.File.Create(path))
                    {
                        await item.CopyToAsync(stream);
                    }
                    applicant_Documents.Add(new mp_applicant_document
                    {
                        applicant_id = applicant_id,
                        document_type = Convert.ToInt32(doc_type[count]),//get this from lookup table
                        path = path
                    });
                }

                count += 1;
            }
            _applicantDocumentService.AddApplicantDocuments(applicant_Documents);

            var applicant_expertise = collection["expertise"].ToString().Split(',');

            var expertises = new List<mp_applicant_expertise>();
            foreach (var app_expertise in applicant_expertise)
            {
                expertises.Add(new mp_applicant_expertise { applicant_id = applicant_id, expertise_id = Convert.ToInt32(app_expertise) });
            }

            _expertiseService.AddApplicantExpertise(expertises);


            var applicant_populations = collection["population"].ToString().Split(',');
            var populations = new List<mp_applicant_population>();

            foreach (var population in applicant_populations)
            {
                populations.Add(new mp_applicant_population { applicant_id = applicant_id, population_id = Convert.ToInt32(population) });
            }

            _populationService.AddApplicantPopulationGroup(populations);


            var applicant_specialties = collection["specialty"].ToString().Split(',');
            var specialties = new List<mp_applicant_specialty>();

            foreach (var specialty in applicant_specialties)
            {
                specialties.Add(new mp_applicant_specialty { applicant_id = applicant_id, specialty_id = Convert.ToInt32(specialty) });
            }
            _specialtyService.AddApplicantSpecialty(specialties);

            //update the education information of the applicant

            var name_of_school = collection["name_of_school"].ToList();
            var address_of_school = collection["address_of_school"].ToList();
            var certifications = collection["certification"].ToList();

            var applicant_educations = new List<mp_applicant_education>();
            for (var j = 0; j < name_of_school.Count(); j++)
            {
                applicant_educations.Add(new mp_applicant_education { applicant_id = applicant_id, address = address_of_school[j], school = name_of_school[j], certification = Convert.ToInt32(certifications[j]), created_by = "" });
            }

            _educationService.AddApplicantEducation(applicant_educations);
            //update the applicant and also change the status to awaiting interview

            applicant.status = (await _lookUpService.GetLookUpByValueAndCategory("Pending", "application_status")).id;
            applicant.expertise = collection["expertise"];
            applicant.about = collection["about"];
            applicant.contact_email = collection["contact_email"];
            applicant.contact_name = collection["contact_name"];
            applicant.contact_phone = collection["contact_phone"];
            applicant.area_of_interest = Convert.ToInt32(collection["area_of_interest"]);

            _applicantService.Update(applicant);
            return RedirectToAction("Completed", new { id = collection["applicant_id"] });
        }

        // POST: Applicant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult NewApplicant1([Bind("last_name,first_name,phone,email,preferred_name,address,city,state,gender,country,dob,marital_status")] mp_applicant mp_applicant)
        {
            var look_up_status = _lookUpService.GetLookUpByValueAndCategory("Incomplete", "application_status").Result;
            if (ModelState.IsValid)
            {
                mp_applicant.id = Guid.NewGuid();
                mp_applicant.created_at = DateTime.Now;
                mp_applicant.status = look_up_status.id;
                var id = _applicantService.Add(mp_applicant);
                //send an email to the applicant
                return RedirectToAction("ContinueRegistration", new { id });
            }
            return View(mp_applicant);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> PostApplicant(mp_clinician mp_applicant, mp_clinic clinic)
        {
            var collection = Request.Form;
            try
            {
                var oot = collection["provider_type"];
                var look_up_status = await _lookUpService.GetLookUpByValueAndCategory("Pending", "application_status");
                if (ModelState.IsValid)
                {
                    mp_applicant.id = Guid.NewGuid();
                    mp_applicant.created_at = DateTime.Now;
                    mp_applicant.status = look_up_status.id;
                    var applicant_id = _clinicianService.Add(mp_applicant);
                    var appointment_type = Convert.ToInt32(collection["appointment_type"]);
                    var appointment_category = Convert.ToInt32(collection["appointment_category"]);
                    var category_subs = collection["appointment_category_sub"].ToString().Split(",");
                    foreach (var sub in category_subs)
                    {
                        var appointment_category_sub = Convert.ToInt32(sub);

                        _providerCategoryService.AddClinicianCategory(new mp_clinician_category { clinician_id = applicant_id, appointment_category = appointment_category, appointment_type = appointment_type, appointment_category_sub = appointment_category_sub });
                    }
                    var provider_type = Convert.ToInt32(collection["provider_type"]);
                    if (provider_type == 294 || provider_type == 178)
                    {
                        //collect the clinic information
                        clinic.status = 4;
                        clinic.clinic_type = provider_type;
                        var clinic_id = _clinicService.AddClinic(clinic);

                        var clinic_clinician = new mp_clinic_clinician
                        {
                            clinician_id = applicant_id,
                            clinic_id = clinic_id,
                            is_admin = 1,
                            status = 1
                        };
                        _clinicService.AddClinicianToClinic(clinic_clinician);
                    }
                    //send an email to the applicant
                    //return RedirectToAction("ContinueRegistration", new { id });
                    var doc_type = collection["doc_type"].ToString().Split(',');
                    string rootDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "") + "\\Uploads\\";
                    if (!Directory.Exists(rootDir))
                        Directory.CreateDirectory(rootDir);


                    //var applicant_expertise = collection["expertise"].ToString().Split(',');

                    //var expertises = new List<mp_applicant_expertise>();
                    //foreach (var app_expertise in applicant_expertise)
                    //{
                    //    expertises.Add(new mp_applicant_expertise { applicant_id = applicant_id, expertise_id = Convert.ToInt32(app_expertise) });
                    //}

                    //_expertiseService.AddApplicantExpertise(expertises);


                    //var applicant_populations = collection["population"].ToString().Split(',');
                    //var populations = new List<mp_applicant_population>();

                    //foreach (var population in applicant_populations)
                    //{
                    //    populations.Add(new mp_applicant_population { applicant_id = applicant_id, population_id = Convert.ToInt32(population) });
                    //}

                    //_populationService.AddApplicantPopulationGroup(populations);


                    //var applicant_specialties = collection["specialty"].ToString().Split(',');
                    //var specialties = new List<mp_applicant_specialty>();

                    //foreach (var specialty in applicant_specialties)
                    //{
                    //    specialties.Add(new mp_applicant_specialty { applicant_id = applicant_id, specialty_id = Convert.ToInt32(specialty) });
                    //}
                    //_specialtyService.AddApplicantSpecialty(specialties);

                    //update the applicant and also change the status to awaiting interview

                    //applicant.status = (await _lookUpService.GetLookUpByValueAndCategory("Pending", "application_status")).id;
                    //applicant.expertise = collection["expertise"];
                    //applicant.about = collection["about"];
                    //applicant.contact_email = collection["contact_email"];
                    //applicant.contact_name = collection["contact_name"];
                    //applicant.contact_phone = collection["contact_phone"];
                    //applicant.area_of_interest = Convert.ToInt32(collection["area_of_interest"]);

                    //_applicantService.Update(applicant);
                    await _emailSender.SendEmailAsync(mp_applicant.email, "Registration successful - MySpace MyTime",
                       $"Thanks you " + mp_applicant.last_name + " " + mp_applicant.first_name + " for creating a profile with us. We are reviewing your profile and will get back to you as soon as possible.");

                    return RedirectToAction("Completed", new { id = applicant_id });
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "There was an error completing the information of the applicant");
                // return ex.Message;
            }
            ViewBag.appointment_type = collection["appointment_type"];
            ViewBag.appointment_category = collection["appointment_category"];
            ViewBag.appointment_category_sub = collection["appointment_category_sub"];
            ViewBag.provider_type = Convert.ToInt32(collection["provider_type"]);
            return View("NewApplicant", mp_applicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> PostApplicantMobile(mp_clinician mp_applicant, mp_clinic clinic)
        {
            try
            {
                var collection = Request.Form;
                var oot = collection["provider_type"];
                // var look_up_status = await _lookUpService.GetLookUpByValueAndCategory("Incomplete", "application_status");
                var look_up_status = await _lookUpService.GetLookUpByValueAndCategory("Pending", "application_status");
                if (ModelState.IsValid)
                {
                    mp_applicant.id = Guid.NewGuid();
                    mp_applicant.created_at = DateTime.Now;
                    mp_applicant.status = look_up_status.id;
                    var applicant_id = _clinicianService.Add(mp_applicant);



                    var appointment_type = Convert.ToInt32(collection["appointment_type"]);
                    var appointment_category = Convert.ToInt32(collection["appointment_category"]);

                    var category_subs = collection["appointment_category_sub"].ToString().Split(",");
                    foreach (var sub in category_subs)
                    {
                        var appointment_category_sub = Convert.ToInt32(sub);

                        _providerCategoryService.AddClinicianCategory(new mp_clinician_category { clinician_id = applicant_id, appointment_category = appointment_category, appointment_type = appointment_type, appointment_category_sub = appointment_category_sub });
                    }



                    var provider_type = Convert.ToInt32(collection["provider_type"]);

                    if (provider_type == 294 || provider_type == 178)
                    {
                        //collect the clinic information
                        clinic.status = 4;
                        clinic.clinic_type = provider_type;
                        var clinic_id = _clinicService.AddClinic(clinic);

                        var clinic_clinician = new mp_clinic_clinician
                        {
                            clinician_id = applicant_id,
                            clinic_id = clinic_id,
                            is_admin = 1,
                            status = 1
                        };

                        _clinicService.AddClinicianToClinic(clinic_clinician);

                    }


                    //send an email to the applicant
                    //return RedirectToAction("ContinueRegistration", new { id });
                    var doc_type = collection["doc_type"].ToString().Split(',');

                    string rootDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "") + "\\Uploads\\";
                    if (!Directory.Exists(rootDir))
                        Directory.CreateDirectory(rootDir);

                    int count = 0;
                    List<mp_clinician_document> applicant_Documents = new List<mp_clinician_document>();
                    foreach (var item in collection.Files)
                    {
                        if (item.Name == "profile_img")
                        {
                            string uploadsFolder = Path.Combine("wwwroot", "images", "applicant");
                            string filePath = Path.Combine(uploadsFolder, applicant_id + ".jpg");
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await item.CopyToAsync(fileStream);
                            }
                        }
                        else
                        {
                            if (item.Length > 0)
                            {
                                //save the file
                                var path = rootDir + "/" + item.FileName;
                                using (var stream = System.IO.File.Create(path))
                                {
                                    await item.CopyToAsync(stream);
                                }
                                applicant_Documents.Add(new mp_clinician_document
                                {
                                    clinician_id = applicant_id,
                                    document_type = Convert.ToInt32(doc_type[count]),//get this from lookup table
                                    path = path
                                });
                            }

                            count += 1;
                        }

                    }
                    _profileDocumentService.AddADocuments(applicant_Documents);

                    //var applicant_expertise = collection["expertise"].ToString().Split(',');

                    //var expertises = new List<mp_applicant_expertise>();
                    //foreach (var app_expertise in applicant_expertise)
                    //{
                    //    expertises.Add(new mp_applicant_expertise { applicant_id = applicant_id, expertise_id = Convert.ToInt32(app_expertise) });
                    //}

                    //_expertiseService.AddApplicantExpertise(expertises);


                    //var applicant_populations = collection["population"].ToString().Split(',');
                    //var populations = new List<mp_applicant_population>();

                    //foreach (var population in applicant_populations)
                    //{
                    //    populations.Add(new mp_applicant_population { applicant_id = applicant_id, population_id = Convert.ToInt32(population) });
                    //}

                    //_populationService.AddApplicantPopulationGroup(populations);


                    //var applicant_specialties = collection["specialty"].ToString().Split(',');
                    //var specialties = new List<mp_applicant_specialty>();

                    //foreach (var specialty in applicant_specialties)
                    //{
                    //    specialties.Add(new mp_applicant_specialty { applicant_id = applicant_id, specialty_id = Convert.ToInt32(specialty) });
                    //}
                    //_specialtyService.AddApplicantSpecialty(specialties);

                    //add languages

                    var languages_spoken = collection["language_spoken"].ToString().Split(',');
                    var applicant_languages = new List<mp_clinician_language>();

                    foreach (var language in languages_spoken)
                    {
                        applicant_languages.Add(new mp_clinician_language { clinician_id = applicant_id, language = Convert.ToInt32(language) });
                    }
                    _languageService.AddClinicianLanguage(applicant_languages);

                    //update the education information of the applicant

                    var name_of_school = collection["name_of_school"].ToList();
                    var address_of_school = collection["address_of_school"].ToList();
                    var certifications = collection["certification"].ToList();

                    var applicant_educations = new List<mp_clinician_education>();
                    for (var j = 0; j < name_of_school.Count(); j++)
                    {
                        applicant_educations.Add(new mp_clinician_education { clinician_id = applicant_id, address = address_of_school[j], school = name_of_school[j], certification = certifications[j] });
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
                            applicant_practices.Add(new mp_clinician_practice { clinician_id = applicant_id, hospital = practice_hospital[j], city = practice_city[j], duration = practice_duration[j], role = practice_role[j] });
                        }

                    }

                    _practiceService.AddClinicianPractice(applicant_practices);

                    //add other activities

                    var activities = collection["activity_outstanding_work"].ToList();
                    var activity_outstanding_work = new List<mp_clinician_other_activities>();

                    foreach (var activity in activities)
                    {
                        activity_outstanding_work.Add(new mp_clinician_other_activities { clinician_id = applicant_id, activity = activity, activity_type = "activity_outstanding_work" });
                    }
                    _otherActivitiesService.AddClinicianActivities(activity_outstanding_work);

                    activities = collection["activity_scholary"].ToList();
                    activity_outstanding_work = new List<mp_clinician_other_activities>();

                    foreach (var activity in activities)
                    {
                        activity_outstanding_work.Add(new mp_clinician_other_activities { clinician_id = applicant_id, activity = activity, activity_type = "activity_scholary" });
                    }
                    _otherActivitiesService.AddClinicianActivities(activity_outstanding_work);

                    activities = collection["activity_social"].ToList();
                    activity_outstanding_work = new List<mp_clinician_other_activities>();

                    foreach (var activity in activities)
                    {
                        activity_outstanding_work.Add(new mp_clinician_other_activities { clinician_id = applicant_id, activity = activity, activity_type = "activity_social" });
                    }
                    _otherActivitiesService.AddClinicianActivities(activity_outstanding_work);

                    //update the applicant and also change the status to awaiting interview

                    //applicant.status = (await _lookUpService.GetLookUpByValueAndCategory("Pending", "application_status")).id;
                    //applicant.expertise = collection["expertise"];
                    //applicant.about = collection["about"];
                    //applicant.contact_email = collection["contact_email"];
                    //applicant.contact_name = collection["contact_name"];
                    //applicant.contact_phone = collection["contact_phone"];
                    //applicant.area_of_interest = Convert.ToInt32(collection["area_of_interest"]);

                    // _applicantService.Update(applicant);
                    await _emailSender.SendEmailAsync(mp_applicant.email, "Registration successful - MySpace MyTime",
                       $"Thanks you " + mp_applicant.last_name + " " + mp_applicant.first_name + " for creating a profile with us. We are reviewing your profile and will get back to you as soon as possible.");




                    return RedirectToAction("CompletedMobile", new { id = applicant_id });


                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "There was an error completing the information of the applicant");
                // return ex.Message;
            }

            return View("NewApplicant", mp_applicant);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> PostApplicant1(mp_applicant mp_applicant)
        {

            try
            {
                // var look_up_status = await _lookUpService.GetLookUpByValueAndCategory("Incomplete", "application_status");
                var look_up_status = await _lookUpService.GetLookUpByValueAndCategory("Pending", "application_status");
                if (ModelState.IsValid)
                {
                    mp_applicant.id = Guid.NewGuid();
                    mp_applicant.created_at = DateTime.Now;
                    mp_applicant.status = look_up_status.id;
                    var applicant_id = _applicantService.Add(mp_applicant);

                    var collection = Request.Form;

                    var appointment_type = Convert.ToInt32(collection["appointment_type"]);
                    var appointment_category = Convert.ToInt32(collection["appointment_category"]);
                    var appointment_category_sub = Convert.ToInt32(collection["appointment_category_sub"]);

                    _providerCategoryService.AddApplicantCategory(new mp_applicant_category { applicant_id = applicant_id, appointment_category = appointment_category, appointment_type = appointment_type, appointment_category_sub = appointment_category_sub });
                    //send an email to the applicant
                    //return RedirectToAction("ContinueRegistration", new { id });
                    var doc_type = collection["doc_type"].ToString().Split(',');

                    string rootDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Replace("file:\\", "") + "\\Uploads\\";
                    if (!Directory.Exists(rootDir))
                        Directory.CreateDirectory(rootDir);

                    int count = 0;
                    List<mp_applicant_document> applicant_Documents = new List<mp_applicant_document>();
                    foreach (var item in collection.Files)
                    {
                        if (item.Name == "profile_img")
                        {
                            string uploadsFolder = Path.Combine("wwwroot", "images", "applicant");
                            string filePath = Path.Combine(uploadsFolder, applicant_id + ".jpg");
                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await item.CopyToAsync(fileStream);
                            }
                        }
                        else
                        {
                            if (item.Length > 0)
                            {
                                //save the file
                                var path = rootDir + "/" + item.FileName;
                                using (var stream = System.IO.File.Create(path))
                                {
                                    await item.CopyToAsync(stream);
                                }
                                applicant_Documents.Add(new mp_applicant_document
                                {
                                    applicant_id = applicant_id,
                                    document_type = Convert.ToInt32(doc_type[count]),//get this from lookup table
                                    path = path
                                });
                            }

                            count += 1;
                        }

                    }
                    _applicantDocumentService.AddApplicantDocuments(applicant_Documents);

                    //var applicant_expertise = collection["expertise"].ToString().Split(',');

                    //var expertises = new List<mp_applicant_expertise>();
                    //foreach (var app_expertise in applicant_expertise)
                    //{
                    //    expertises.Add(new mp_applicant_expertise { applicant_id = applicant_id, expertise_id = Convert.ToInt32(app_expertise) });
                    //}

                    //_expertiseService.AddApplicantExpertise(expertises);


                    //var applicant_populations = collection["population"].ToString().Split(',');
                    //var populations = new List<mp_applicant_population>();

                    //foreach (var population in applicant_populations)
                    //{
                    //    populations.Add(new mp_applicant_population { applicant_id = applicant_id, population_id = Convert.ToInt32(population) });
                    //}

                    //_populationService.AddApplicantPopulationGroup(populations);


                    //var applicant_specialties = collection["specialty"].ToString().Split(',');
                    //var specialties = new List<mp_applicant_specialty>();

                    //foreach (var specialty in applicant_specialties)
                    //{
                    //    specialties.Add(new mp_applicant_specialty { applicant_id = applicant_id, specialty_id = Convert.ToInt32(specialty) });
                    //}
                    //_specialtyService.AddApplicantSpecialty(specialties);

                    //add languages

                    var languages_spoken = collection["language_spoken"].ToString().Split(',');
                    var applicant_languages = new List<mp_applicant_language>();

                    foreach (var language in languages_spoken)
                    {
                        applicant_languages.Add(new mp_applicant_language { applicant_id = applicant_id, language = Convert.ToInt32(language) });
                    }
                    _languageService.AddApplicantLanguage(applicant_languages);

                    //update the education information of the applicant

                    var name_of_school = collection["name_of_school"].ToList();
                    var address_of_school = collection["address_of_school"].ToList();
                    var certifications = collection["certification"].ToList();

                    var applicant_educations = new List<mp_applicant_education>();
                    for (var j = 0; j < name_of_school.Count(); j++)
                    {
                        applicant_educations.Add(new mp_applicant_education { applicant_id = applicant_id, address = address_of_school[j], school = name_of_school[j], certification = Convert.ToInt32(certifications[j]), created_by = "" });
                    }

                    _educationService.AddApplicantEducation(applicant_educations);

                    var practice_hospital = collection["practice_hospital"].ToList();
                    var practice_city = collection["practice_city"].ToList();
                    var practice_role = collection["practice_role"].ToList();
                    var practice_duration = collection["practice_duration"].ToList();

                    var applicant_practices = new List<mp_applicant_practice>();
                    for (var j = 0; j < practice_hospital.Count(); j++)
                    {
                        if (!string.IsNullOrEmpty(practice_hospital[j]) && !string.IsNullOrEmpty(practice_city[j]) && !string.IsNullOrEmpty(practice_duration[j]) && !string.IsNullOrEmpty(practice_role[j]))
                        {
                            applicant_practices.Add(new mp_applicant_practice { applicant_id = applicant_id, hospital = practice_hospital[j], city = practice_city[j], duration = practice_duration[j], role = practice_role[j] });
                        }

                    }

                    _practiceService.AddApplicantPractice(applicant_practices);

                    //add other activities

                    var activities = collection["activity_outstanding_work"].ToList();
                    var activity_outstanding_work = new List<mp_applicant_other_activities>();

                    foreach (var activity in activities)
                    {
                        activity_outstanding_work.Add(new mp_applicant_other_activities { applicant_id = applicant_id, activity = activity, activity_type = "activity_outstanding_work" });
                    }
                    _otherActivitiesService.AddApplicantActivities(activity_outstanding_work);

                    activities = collection["activity_scholary"].ToList();
                    activity_outstanding_work = new List<mp_applicant_other_activities>();

                    foreach (var activity in activities)
                    {
                        activity_outstanding_work.Add(new mp_applicant_other_activities { applicant_id = applicant_id, activity = activity, activity_type = "activity_scholary" });
                    }
                    _otherActivitiesService.AddApplicantActivities(activity_outstanding_work);

                    activities = collection["activity_social"].ToList();
                    activity_outstanding_work = new List<mp_applicant_other_activities>();

                    foreach (var activity in activities)
                    {
                        activity_outstanding_work.Add(new mp_applicant_other_activities { applicant_id = applicant_id, activity = activity, activity_type = "activity_social" });
                    }
                    _otherActivitiesService.AddApplicantActivities(activity_outstanding_work);

                    //update the applicant and also change the status to awaiting interview

                    //applicant.status = (await _lookUpService.GetLookUpByValueAndCategory("Pending", "application_status")).id;
                    //applicant.expertise = collection["expertise"];
                    //applicant.about = collection["about"];
                    //applicant.contact_email = collection["contact_email"];
                    //applicant.contact_name = collection["contact_name"];
                    //applicant.contact_phone = collection["contact_phone"];
                    //applicant.area_of_interest = Convert.ToInt32(collection["area_of_interest"]);

                    //_applicantService.Update(applicant);
                    await _emailSender.SendEmailAsync(mp_applicant.email, "Registration successful - MySpace MyTime",
                       $"Thanks you " + mp_applicant.last_name + " " + mp_applicant.first_name + " for creating a profile with us. We are reviewing your profile and will get back to you as soon as possible.");




                    return RedirectToAction("Completed", new { id = applicant_id });


                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "There was an error completing the information of the applicant");
                // return ex.Message;
            }

            //return View("NewApplicant", mp_applicant); //this throws an error as the view requires an applicant model not a clincian
            //therefore am redirecting to the index
            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> Update1(mp_applicant applicant)
        {

            var old = _applicantService.Get(applicant.id);
            old.status = applicant.status;


            //if applicant if hired, create their account and profile
            if (applicant.status == 5)
            {

                //create a new user
                var UserManager = _userManager;
                var user = new ApplicationUser { UserName = old.email, Email = old.email, PhoneNumber = old.phone, UserType = 2, RegistrationDate = DateTime.Now };
                var result = await UserManager.CreateAsync(user, "E4!nte123talk");

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "clinician");
                    var user_id = _userManager.GetUserId(HttpContext.User);

                    //move all the info on applicant table to the profile table
                    var profile = new mp_clinician();
                    profile.last_name = old.last_name;
                    profile.first_name = old.first_name;
                    profile.address = old.address;
                    profile.city = old.city;
                    profile.country = old.country;
                    profile.dob = old.dob;
                    profile.email = old.email;
                    profile.marital_status = old.marital_status;
                    profile.phone = old.phone;
                    profile.preferred_name = old.preferred_name;
                    //profile.profile_type = 2;
                    profile.state = old.state;
                    profile.status = 1;
                    profile.user_id = user.Id;
                    profile.about = old.about;
                    profile.contact_email = old.contact_email;
                    profile.contact_name = old.contact_name;
                    profile.contact_phone = old.contact_phone;
                    profile.area_of_interest = old.area_of_interest;

                    profile.years_of_experience = old.years_of_experience;
                    profile.year_qualified_doctor = profile.year_qualified_doctor;
                    profile.year_qualified_specialist = old.year_qualified_specialist;

                    _profileService.Add(profile);

                    //add the expertise
                    var expertises = new List<mp_clinician_expertise>();
                    foreach (var expertise in _expertiseService.GetApplicantExpertise(old.id))
                    {
                        expertises.Add(new mp_clinician_expertise { expertise_id = expertise.expertise_id, clinician_id = profile.id });
                    }
                    _expertiseService.AddProfileExpertise(expertises);

                    //add the language
                    var languages = new List<mp_clinician_language>();
                    foreach (var language in _languageService.GetApplicantLanguage(old.id))
                    {
                        languages.Add(new mp_clinician_language { language = language.language, clinician_id = profile.id });
                    }
                    _languageService.AddClinicianLanguage(languages);


                    //add other activities
                    var activities = new List<mp_clinician_other_activities>();
                    foreach (var activity in _otherActivitiesService.GetApplicantActivities(old.id))
                    {
                        activities.Add(new mp_clinician_other_activities { activity = activity.activity, clinician_id = profile.id, activity_type = activity.activity_type });
                    }
                    _otherActivitiesService.AddClinicianActivities(activities);

                    //add the population group
                    var population_groups = _populationService.GetApplicantPopulations(old.id);
                    var populations = new List<mp_clinician_population>();

                    foreach (var population in population_groups)
                    {
                        populations.Add(new mp_clinician_population { clinician_id = profile.id, population_id = population.population_id });
                    }
                    _populationService.AddClinicianPopulationGroup(populations);

                    var applicant_specialties = _specialtyService.GetApplicantSpecialties(old.id);
                    var clinician_specialties = new List<mp_clinician_specialty>();

                    foreach (var specialty in applicant_specialties)
                    {
                        clinician_specialties.Add(new mp_clinician_specialty { clinician_id = profile.id, specialty_id = specialty.specialty_id });
                    }
                    _specialtyService.AddClinicianSpecialty(clinician_specialties);

                    //add the documents
                    var docs = new List<mp_clinician_document>();
                    foreach (var doc in old.mp_applicant_document)
                    {
                        docs.Add(new mp_clinician_document { document_type = doc.document_type, path = doc.path, clinician_id = profile.id });
                    }
                    _profileDocumentService.AddADocuments(docs);

                    var educations = new List<mp_clinician_education>();
                    var applicant_educations = _educationService.GetApplicantEducation(old.id);
                    foreach (var education in applicant_educations)
                    {
                        educations.Add(new mp_clinician_education { school = education.school, clinician_id = profile.id, address = education.address });
                    }

                    _educationService.AddClinicianEducation(educations);


                    var practices = new List<mp_clinician_practice>();
                    var applicant_practices = _practiceService.GetApplicantPractice(old.id);
                    foreach (var practice in applicant_practices)
                    {
                        practices.Add(new mp_clinician_practice { clinician_id = profile.id, city = practice.city, duration = practice.duration, role = practice.role, hospital = practice.hospital });
                    }

                    _practiceService.AddClinicianPractice(practices);

                    var applicant_catgory = _providerCategoryService.GetApplicantCategory().FirstOrDefault(e => e.applicant_id == old.id);
                    _providerCategoryService.AddClinicianCategory(new mp_clinician_category { clinician_id = profile.id, appointment_category = applicant_catgory.appointment_category, appointment_type = applicant_catgory.appointment_type, appointment_category_sub = applicant_catgory.appointment_category_sub });

                    //link the applicant to the new clinician created
                    _applicantService.AddApplicantToProfile(new mp_link_applicant_clinician
                    {
                        clinician_id = profile.id,
                        applicant_id = old.id,
                        created_by = user_id
                    });
                }
            }

            _applicantService.Update(old);

            return Redirect(Request.Headers["Referer"].ToString());
        }



        [HttpPost]
        public async Task<IActionResult> Update(mp_clinician applicant)
        {

            var old = _clinicianService.Get(applicant.id);
            old.status = applicant.status;


            //if applicant if hired, create their account and profile
            if (applicant.status == 5)
            {
                var password = "E4!nte123talk";
                //create a new user
                var UserManager = _userManager;
                var user = new ApplicationUser { UserName = old.email, Email = old.email, PhoneNumber = old.phone, UserType = 2, RegistrationDate = DateTime.Now };
                var result = await UserManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "clinician");
                    var user_id = _userManager.GetUserId(HttpContext.User);
                    old.updated_by = user_id;
                    old.user_id = user.Id;
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    await _userManager.ConfirmEmailAsync(user, code);
                    //move all the info on applicant table to the profile table
                    await _emailSender.SendEmailAsync(old.email, "Welcome to MySpace MyTime",
                 $"Your account has successfully being created. Your initial password is " + password + ". Please log in and change your credentials.");

                    var notification = new mp_notification
                    {
                        created_by = "sys_admin",
                        created_by_name = "System Admin",
                        notification_type = 8,
                        read = 0,
                        user_id = user.Id,
                        notification = "Hi " + old.last_name + " " + old.first_name + ", Welcome to MySpace MyTime. We are really happy to have you here. Please go to the settings page and specify the times that you are available and the cost of your services",
                        title = "Welcome - MySpace MyTime"
                    };

                    NotificationUtil.Add(notification);

                }
            }
            else
            {
                var processStatus = Options.Getlookups("application_status").FirstOrDefault(x => x.id == applicant.status).value;
                await _emailSender.SendEmailAsync(old.email, "Registration Progress Updated - MySpace MyTime",
                   $"Hi " + old.last_name + " " + old.first_name + ", Your registration progress is updated as " + processStatus);
            }

            _clinicianService.Update(old);

            return Redirect(Request.Headers["Referer"].ToString());
        }



        [Authorize]
        public async Task<IActionResult> get_applicant_image(string applicant_id)
        {
            var file_path = Path.Combine("wwwroot", "images", "applicant", applicant_id + ".jpg");

            if (!System.IO.File.Exists(file_path))
            {
                file_path = Path.Combine("wwwroot", "images", "profile", "anonymous.jpg");
            }


            var memory = new MemoryStream();
            using (var stream = new FileStream(file_path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "image/jpeg", Path.GetFileName(file_path));
        }

    }
}
