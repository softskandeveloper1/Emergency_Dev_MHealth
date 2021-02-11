using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using MHealth.Areas.Identity.Pages.Account;
using MHealth.Data;
using MHealth.Entities;
using MHealth.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using System.Net.Mail;

namespace MHealth.Controllers
{
    public class UserController : Controller
    {

        UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly IProfileService _profileService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly IProfileMatchService _profileMatchService;
        private readonly ICoupleScreeningService _coupleScreeningService;
        private readonly IClinicianService _clinicianService;
        private readonly IEvaluationService _evaluationService;
        private readonly IEmergencyContactService _emergencyContactService;
        private IWebHostEnvironment _environment;

        public UserController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger,
            IEmailSender emailSender, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleMgr, IProfileService profileService, IEnrollmentService enrollmentService, IProfileMatchService profileMatchService, ICoupleScreeningService coupleScreeningService, IClinicianService clinicianService, IEvaluationService evaluationService, IEmergencyContactService emergencyContactService, IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _signInManager = signInManager;
            roleManager = roleMgr;
            _profileService = profileService;
            _enrollmentService = enrollmentService;
            _profileMatchService = profileMatchService;
            _coupleScreeningService = coupleScreeningService;
            _clinicianService = clinicianService;
            _evaluationService = evaluationService;
            _emergencyContactService = emergencyContactService;
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult reg_provider()
        {
            return View();
        }

        public IActionResult reg_member()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult c_register_mobile(int user_type)
        {
            ViewBag.user_type = user_type;
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        public IActionResult RegisterMobile()
        {
            return View();
        }

        public IActionResult RegisterMob(int appointment_type, int appointment_category, int appointment_category_sub)
        {
            ViewBag.appointment_type = appointment_type;
            ViewBag.appointment_category = appointment_category;
            ViewBag.appointment_category_sub = appointment_category_sub;
            return View();
        }
        public IActionResult Register(int appointment_type, int appointment_category, int appointment_category_sub)
        {
            ViewBag.appointment_type = appointment_type;
            ViewBag.appointment_category = appointment_category;
            ViewBag.appointment_category_sub = appointment_category_sub;
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> Create(IFormCollection collection)
        //{


        //    var username = collection["uname"];
        //    var roles = collection["roles"].ToString().Split(",");
        //    var password = collection["pwd"];
        //    var phone_number = collection["Mobile"];

        //    var jsonMsg = FormHelper.ColletionToJSON(collection);


        //    var UserManager = _userManager;


        //    var user = new ApplicationUser { UserName = username, Email = username, PhoneNumber = phone_number };

        //    var result = await UserManager.CreateAsync(user, password);
        //    if (result.Succeeded)
        //    {
        //        foreach (var role in roles)
        //        {
        //            await UserManager.AddToRoleAsync(user, role);
        //        }

        //        jsonMsg["user_id"] = user.Id;


        //        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);


        //        var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";



        //        var callbackUrl = url;




        //        await _emailSender.SendEmailAsync(collection["uname"].ToString(), "Welcome to MHealth - Confirm your email",
        //            $"Your account has successfully being created. Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");


        //    }
        //    return RedirectToAction("ManageUsers");
        //}

        //public IActionResult c_register()
        //{
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "questions", "questions.json");
        //    var JSON = System.IO.File.ReadAllText(path);
        //    var array = JArray.Parse(JSON);
        //    var questions = new List<QuestionItem>();

        //    foreach(var value in array)
        //    {
        //        var question_item = new QuestionItem
        //        {
        //            question = value["question"].ToString(),
        //            comment = value["comment"].ToString(),
        //           // model_name = value["model_name"].ToString(),
        //            q_type = value["q_type"].ToString(),
        //            id = value["id"].ToString()
        //        };

        //        if (!string.IsNullOrEmpty(value["lookup_category"].ToString()))
        //        {
        //            question_item.lookup_values = Options.Getlookups(value["lookup_category"].ToString()).ToList();
        //        }

        //        if (!string.IsNullOrEmpty(value["q_type"].ToString()) && value["q_type"].ToString()=="static_list")
        //        {
        //            question_item.lookup_values = JsonConvert.DeserializeObject<List<mp_lookup>>(value["lookup_values"].ToString()); 
        //        }

        //        questions.Add(question_item);
        //    }

        //    ViewBag.questions = JsonConvert.SerializeObject(questions);
        //    return View();
        //}

        [HttpPost]
        public async Task<ActionResult> RegisterClient(IFormCollection collection, mp_profile profile, mp_enrollment enrollment, mp_couple_screening couple_screening, mp_child_screening child_Screening)
        {
            if (!string.IsNullOrWhiteSpace(collection["religion_other"]))
            {
                enrollment.religion = Options.AddLookup(new mp_lookup { category = "religion", value = collection["religion_other"] }).id;
            }
            if (!string.IsNullOrWhiteSpace(collection["preferred_language_other"]))
            {
                profile.language = Options.AddLookup(new mp_lookup { category = "preferred_language", value = collection["preferred_language_other"] }).id;
            }
            if (!string.IsNullOrWhiteSpace(collection["tribe_other"]))
            {
                profile.tribe = Options.AddLookup(new mp_lookup { category = "tribe", value = collection["tribe_other"] }).id;
            }
            if (!string.IsNullOrWhiteSpace(collection["child_preferred_language_other"]))
            {
                child_Screening.child_preferred_language = Options.AddLookup(new mp_lookup { category = "preferred_language", value = collection["child_preferred_language_other"] }).id;
            }
            var appointment_type_id = Convert.ToInt32(collection["appointment_type"]);
            var appointment_activity_id = Convert.ToInt32(collection["appointment_category_id"]);
            var appointment_activity_sub_id = Convert.ToInt32(collection["appointment_category_sub_id"]);
            //auth data
            var username = collection["username"];
            //var roles = collection["roles"].ToString().Split(",");
            var password = collection["password"];
            //end of auth data

            //step-1
            int marital_status = Convert.ToInt32(collection["marital_status"]);
            int religious = Convert.ToInt32(collection["religious"]);
            int religion = Convert.ToInt32(collection["religion"]);
            int help_reason = Convert.ToInt32(collection["help_reason"]);
            int earlier_counseling = Convert.ToInt32(collection["earlier_counseling"]);
            int physical_health = Convert.ToInt32(collection["physical_health"]);
            int eating_habit = Convert.ToInt32(collection["eating_habit"]);
            int sleeping = Convert.ToInt32(collection["sleeping"]);
            int depression = Convert.ToInt32(collection["depression"]);
            int reduced_interest = Convert.ToInt32(collection["reduced_interest"]);
            int recent_depression = Convert.ToInt32(collection["recent_depression"]);
            int sleeping_trouble = Convert.ToInt32(collection["sleeping_trouble"]);
            int tiredness = Convert.ToInt32(collection["tiredness"]);
            int appetite = Convert.ToInt32(collection["appetite"]);
            int feeling_bad = Convert.ToInt32(collection["feeling_bad"]);
            int concentration_issue = Convert.ToInt32(collection["concentration_issue"]);
            int fidgety = Convert.ToInt32(collection["fidgety"]);
            int suicidal = Convert.ToInt32(collection["suicidal"]);
            int today_feeling = Convert.ToInt32(collection["today_feeling"]);
            int employed = Convert.ToInt32(collection["employed"]);
            int alcohol = Convert.ToInt32(collection["alcohol"]);
            int anxiety = Convert.ToInt32(collection["anxiety"]);
            bool? clinicalTherapyFaith = Convert.ToBoolean(collection["clinicalTherapyFaith"]);
            //end step-1

            //step-2
            var last_name = collection["last_name"];
            var first_name = collection["first_name"];
            var phone = collection["phone"];
            var email = collection["email"];
            var preferred_name = collection["preferred_name"];
            var address = collection["address"];
            var city = collection["city"];
            var area = collection["area"];
            var school_name = collection["school_name"];
            int state = Convert.ToInt32(collection["state"]);
            int country = Convert.ToInt32(collection["country"]);
            var dob = DateTime.Parse(collection["dob"]);
            int education_level = Convert.ToInt32(collection["education_level"]);
            //end step-2

            //emergency contact
            var emergency_last_name = collection["emergency_last_name"];
            var emergency_first_name = collection["emergency_first_name"];
            var emergency_phone = collection["emergency_phone"];
            var emergency_email = collection["emergency_email"];

            //var jsonMsg = FormHelper.ColletionToJSON(collection);
            var UserManager = _userManager;
            var user = new ApplicationUser { UserName = username, Email = username, PhoneNumber = profile.phone, UserType = 1, RegistrationDate = DateTime.Now };

            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user, "client");
                try
                {
                    Guid profile_id = _profileService.Add(new mp_profile
                    {
                        last_name = last_name,
                        first_name = first_name,
                        phone = phone,
                        email = email,
                        preferred_name = preferred_name,
                        address = address,
                        area = area,
                        city = city,
                        state = state,
                        country = country,
                        dob = dob,
                        user_id = user.Id,
                        profile_type = 1,
                        education_level = education_level,
                        school_name = school_name,
                        status = 0,
                        marital_status = marital_status
                    });
                    _emergencyContactService.Add(new mp_emergency_contact
                    {
                        first_name = emergency_first_name,
                        last_name = emergency_last_name,
                        email = emergency_email,
                        phone = emergency_phone,
                        profile_id = profile_id,
                        created_by = username
                    });
                    _enrollmentService.Add(new mp_enrollment
                    {
                        profile_id = profile_id,
                        religious = religious,
                        religion = religion,
                        help_reason = help_reason,
                        earlier_counseling = earlier_counseling,
                        physical_health = physical_health,
                        eating_habit = eating_habit,
                        sleeping = sleeping,
                        depression = depression,
                        reduced_interest = reduced_interest,
                        recent_depression = recent_depression,
                        sleeping_trouble = sleeping_trouble,
                        tiredness = tiredness,
                        appetite = appetite,
                        feeling_bad = feeling_bad,
                        concentration_issue = concentration_issue,
                        fidgety = fidgety,
                        suicidal = suicidal,
                        today_feeling = today_feeling,
                        employed = employed,
                        alcohol = alcohol,
                        anxiety = anxiety,
                        clinicalTherapyFaith = clinicalTherapyFaith,
                        created_by = username
                    });
                    if (appointment_type_id == 1 && appointment_activity_sub_id == 1)
                    {
                        //individual counselling
                        enrollment.created_by = user.Id;
                        enrollment.profile_id = profile_id;
                        _enrollmentService.Add(enrollment);
                    }
                    else if (appointment_type_id == 1 && appointment_activity_sub_id == 2)
                    {
                        //couples counselling
                        enrollment.created_by = user.Id;
                        enrollment.profile_id = profile_id;
                        _enrollmentService.Add(enrollment);

                        couple_screening.profile_id = profile_id;
                        _coupleScreeningService.Add(couple_screening);
                    }
                    else if (appointment_type_id == 1 && appointment_activity_sub_id == 3)
                    {
                        //child counselling
                        enrollment.created_by = user.Id;
                        enrollment.profile_id = profile_id;
                        _enrollmentService.Add(enrollment);

                        child_Screening.profile_id = profile_id;
                        _evaluationService.AddChildScreening(child_Screening);
                    }
                    else if (appointment_type_id == 1 && appointment_activity_sub_id == 4)
                    {
                        //familiy counselling
                        enrollment.created_by = user.Id;
                        enrollment.profile_id = profile_id;
                        _enrollmentService.Add(enrollment);
                    }

                    //var profile_match = new mp_profile_match
                    //{
                    //    appointment_type_id = appointment_type_id,
                    //    appointment_activity_id = appointment_activity_id,
                    //    appointment_activity_sub_id = appointment_activity_sub_id,
                    //    clinician_id = Guid.Parse(collection["clinician_id"]),
                    //    profile_id = profile_id
                    //};


                    //_profileMatchService.Add(profile_match);


                    await _emailSender.SendEmailAsync(user.Email, "Registration successful - MySpace MyTime",
                          $"Thanks you " + last_name + " " + first_name + " for creating a profile with us.");

                    //Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>

                    //return LocalRedirect("/Identity/Account/Login");
                    return Ok(200);

                }
                catch (Exception ex)
                {
                    //error message handling
                    var errMsg = "There was an error creating your account. " + ex.Message;
                    return Ok(errMsg);
                }


                //                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var url = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";

                //var callbackUrl = url;

                // await _signInManager.SignInAsync(user, isPersistent: false);

                //await _emailSender.SendEmailAsync(collection["username"].ToString(), "Welcome to MySpace MyTime",
                //$"Your account has successfully being created.");

                //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                //var callbackUrl = Url.Page(
                //    "/Account/ConfirmEmail",
                //    pageHandler: null,
                //    values: new { area = "Identity", userId = user.Id, code = code },
                //    protocol: Request.Scheme);

                //await _emailSender.SendEmailAsync(user.Email, "Your account has successfully being created. <br>",
                //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
            }
            // If we got this far, something failed, redisplay form
            var errors = "There was an error creating your account. Please ensure that the you use a strong password";
            if (result.Errors != null && result.Errors.Count() > 0)
            {
                errors = string.Join(',', result.Errors.Select(x => x.Description).ToList());
            }
            return Ok(errors);
            //return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        public async Task<ActionResult> RegisterClientMobile(IFormCollection collection, mp_profile profile, mp_enrollment enrollment, mp_couple_screening couple_screening, mp_child_screening child_Screening)
        {
            if (!string.IsNullOrWhiteSpace(collection["religion_other"]))
            {
                enrollment.religion = Options.AddLookup(new mp_lookup { category = "religion", value = collection["religion_other"] }).id;
            }

            if (!string.IsNullOrWhiteSpace(collection["preferred_language_other"]))
            {
                profile.language = Options.AddLookup(new mp_lookup { category = "preferred_language", value = collection["preferred_language_other"] }).id;
            }
            var appointment_type_id = Convert.ToInt32(collection["appointment_type"]);
            var appointment_activity_id = Convert.ToInt32(collection["appointment_category_id"]);
            var appointment_activity_sub_id = Convert.ToInt32(collection["appointment_category_sub_id"]);
            //auth data
            var username = collection["username"];
            //var roles = collection["roles"].ToString().Split(",");
            var password = collection["password"];
            //end of auth data

            //step-1
            //int marital_status = Convert.ToInt32(collection["marital_status"]);
            //int religious = Convert.ToInt32(collection["religious"]);
            //int religion = Convert.ToInt32(collection["religion"]);
            //int help_reason = Convert.ToInt32(collection["help_reason"]);
            //int earlier_counseling = Convert.ToInt32(collection["earlier_counseling"]);
            //int physical_health = Convert.ToInt32(collection["physical_health"]);
            //int eating_habit = Convert.ToInt32(collection["eating_habit"]);
            //int sleeping = Convert.ToInt32(collection["sleeping"]);
            //int depression = Convert.ToInt32(collection["depression"]);
            //int reduced_interest = Convert.ToInt32(collection["reduced_interest"]);
            //int recent_depression = Convert.ToInt32(collection["recent_depression"]);
            //int sleeping_trouble = Convert.ToInt32(collection["sleeping_trouble"]);
            //int tiredness = Convert.ToInt32(collection["tiredness"]);
            //int appetite = Convert.ToInt32(collection["appetite"]);
            //int feeling_bad = Convert.ToInt32(collection["feeling_bad"]);
            //int concentration_issue = Convert.ToInt32(collection["concentration_issue"]);
            //int fidgety = Convert.ToInt32(collection["fidgety"]);
            //int suicidal = Convert.ToInt32(collection["suicidal"]);
            //int today_feeling = Convert.ToInt32(collection["today_feeling"]);
            //int employed = Convert.ToInt32(collection["employed"]);
            //int alcohol = Convert.ToInt32(collection["alcohol"]);
            //int anxiety = Convert.ToInt32(collection["anxiety"]);
            //end step-1

            //step-2
            //var last_name = collection["last_name"];
            //var first_name = collection["first_name"];
            //var phone = collection["phone"];
            //var email = collection["email"];
            //var preferred_name = collection["preferred_name"];
            //var address = collection["address"];
            //var city = collection["city"];
            //int state = Convert.ToInt32(collection["state"]);
            //int country = Convert.ToInt32(collection["country"]);
            //var dob = DateTime.Parse(collection["dob"]);

            //end step-2

            //var jsonMsg = FormHelper.ColletionToJSON(collection);


            var UserManager = _userManager;


            var user = new ApplicationUser { UserName = username, Email = username, PhoneNumber = profile.phone, UserType = 1, RegistrationDate = DateTime.Now };

            var result = await UserManager.CreateAsync(user, password);
            if (result.Succeeded)
            {

                await UserManager.AddToRoleAsync(user, "client");

                //jsonMsg["user_id"] = user.Id;

                try
                {
                    profile.status = 0;
                    profile.profile_type = 1;
                    profile.user_id = user.Id;
                    Guid profile_id = _profileService.Add(profile);
                    //    _profileService.Add(new mp_profile
                    //{
                    //    last_name = last_name,
                    //    first_name = first_name,
                    //    phone = phone,
                    //    email = email,
                    //    preferred_name = preferred_name,
                    //    address = address,
                    //    city = city,
                    //    state = state,
                    //    country = country,
                    //    dob = dob,
                    //    user_id = user.Id,
                    //    profile_type = 1,
                    //    education_level = 0,
                    //    status = 0,
                    //    marital_status = marital_status
                    //});

                    //_enrollmentService.Add(new mp_enrollment
                    //{
                    //    profile_id = profile_id,
                    //    religious = religious,
                    //    religion = religion,
                    //    help_reason = help_reason,
                    //    earlier_counseling = earlier_counseling,
                    //    physical_health = physical_health,
                    //    eating_habit = eating_habit,
                    //    sleeping = sleeping,
                    //    depression = depression,
                    //    reduced_interest = reduced_interest,
                    //    recent_depression = recent_depression,
                    //    sleeping_trouble = sleeping_trouble,
                    //    tiredness = tiredness,
                    //    appetite = appetite,
                    //    feeling_bad = feeling_bad,
                    //    concentration_issue = concentration_issue,
                    //    fidgety = fidgety,
                    //    suicidal = suicidal,
                    //    today_feeling = today_feeling,
                    //    employed = employed,
                    //    alcohol = alcohol,
                    //    anxiety = anxiety,
                    //    created_by = username
                    //});
                    if (appointment_type_id == 1 && appointment_activity_sub_id == 1)
                    {
                        //individual counselling
                        enrollment.created_by = user.Id;
                        enrollment.profile_id = profile_id;
                        _enrollmentService.Add(enrollment);
                    }
                    else if (appointment_type_id == 1 && appointment_activity_sub_id == 2)
                    {
                        //couples counselling
                        enrollment.created_by = user.Id;
                        enrollment.profile_id = profile_id;
                        _enrollmentService.Add(enrollment);

                        couple_screening.profile_id = profile_id;
                        _coupleScreeningService.Add(couple_screening);
                    }
                    else if (appointment_type_id == 1 && appointment_activity_sub_id == 3)
                    {
                        //child counselling
                        enrollment.created_by = user.Id;
                        enrollment.profile_id = profile_id;
                        _enrollmentService.Add(enrollment);

                        child_Screening.profile_id = profile_id;
                        _evaluationService.AddChildScreening(child_Screening);
                    }
                    else if (appointment_type_id == 1 && appointment_activity_sub_id == 4)
                    {
                        //familiy counselling
                        enrollment.created_by = user.Id;
                        enrollment.profile_id = profile_id;
                        _enrollmentService.Add(enrollment);
                    }

                    //var profile_match = new mp_profile_match
                    //{
                    //    appointment_type_id = appointment_type_id,
                    //    appointment_activity_id = appointment_activity_id,
                    //    appointment_activity_sub_id = appointment_activity_sub_id,
                    //    clinician_id = Guid.Parse(collection["clinician_id"]),
                    //    profile_id = profile_id
                    //};


                    //_profileMatchService.Add(profile_match);
                }
                catch (Exception ex)
                {
                    //error message handling
                }

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = user.Id, code = code },
                    protocol: Request.Scheme);

                await _emailSender.SendEmailAsync(user.Email, "Your account has successfully being created. <br>",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                return RedirectToAction("MemberCompletedMobile");
                //return Ok(200);
            }

            // If we got this far, something failed, redisplay form

            return Redirect(Request.Headers["Referer"].ToString());// return RedirectToAction("Create");
            //return Ok(400);
        }

        [Authorize]
        public async Task<IActionResult> get_profile_image(string user_id = null)
        {
            if (string.IsNullOrEmpty(user_id))
            {
                user_id = _userManager.GetUserId(HttpContext.User);
            }

            var file_path = Path.Combine("wwwroot", "images", "profile", user_id + ".jpg");

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


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProfileImage(IFormFile profile_image)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);

            if (profile_image != null)
            {
                string uploadsFolder = Path.Combine("wwwroot", "images", "profile");
                string filePath = Path.Combine(uploadsFolder, user_id + ".jpg");
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profile_image.CopyToAsync(fileStream);
                }
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(IFormCollection collection)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, collection["OldPassword"], collection["NewPassword"]);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            // StatusMessage = "Your password has been changed.";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult MemberCompletedMobile()
        {
            return View();
        }

        public string Test()
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("support@myspace-mytime.com", "Change2401@!");

                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("support@myspace-mytime.com", "MySpace MyTime"),
                    Subject = "Test Subject",
                    Body = "Test Body",
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress("faisalmpathan.vision@gmail.com"));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Host = "smtpout.secureserver.net",
                    EnableSsl = true,
                    Credentials = credentials,
                };

                // Send it...         
                client.Send(mail);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}