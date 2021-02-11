using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DAL.BL;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using MHealth.Data;
using MHealth.Entities;
using MHealth.Entities.ViewModel;
using MHealth.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;

namespace MHealth.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppointmentService _appointmentService;
        private readonly IProfileService _profileService;
        private readonly IClinicianAvailabilityService _clinicianAvailabilityService;
        private readonly ICreditService _creditService;
        private readonly IClinicianService _clinicianService;
        private readonly IProfileMatchService _profileMatchService;
        private readonly IEmailSender _emailSender;
        private readonly IProfileBankService _profileBankService;
        private readonly IAppointmentRefundService _appointmentRefundService;
        private readonly IServiceCostService _serviceCostService;
        private readonly PayStackSettings _payStackSettings;


        public AppointmentController(IProfileService profileService, UserManager<ApplicationUser> userManager, IAppointmentService appointmentService, IClinicianAvailabilityService clinicianAvailabilityService, ICreditService creditService, IClinicianService clinicianService, IProfileMatchService profileMatchService, IEmailSender emailSender, IProfileBankService profileBankService, IAppointmentRefundService appointmentRefundService, IServiceCostService serviceCostService, IOptions<PayStackSettings> payStackSettings)
        {
            _userManager = userManager;
            _profileService = profileService;
            _appointmentService = appointmentService;
            _clinicianAvailabilityService = clinicianAvailabilityService;
            _creditService = creditService;
            _clinicianService = clinicianService;
            _profileMatchService = profileMatchService;
            _emailSender = emailSender;
            _profileBankService = profileBankService;
            _appointmentRefundService = appointmentRefundService;
            _serviceCostService = serviceCostService;
            _payStackSettings = payStackSettings.Value;
        }

        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        // GET: Appointment/Details/5
        public ActionResult Details(Guid id)
        {
            var appointment = _appointmentService.Get().Include(e => e.client_).Include(e => e.clinician_).Include(e => e.mp_credit).FirstOrDefault(e => e.id == id);

            //ViewBag.schedules = _clinicianAvailabilityService.Get(appointment.clinician_id);
            return View(appointment);
        }

        public ActionResult Cancel(Guid id)
        {
            var appointment = _appointmentService.Get().Include(e => e.client_).Include(e => e.clinician_).Include(e => e.mp_credit).FirstOrDefault(e => e.id == id);
            if (User.IsInRole("client"))
            {
                Guid logged_user_id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
                mp_profile user_profile = _profileService.GetByUserId(logged_user_id);
                //check if the user currently has a bank information
                var profile_bank = _profileBankService.GetProfileBank(user_profile.id);
                if (profile_bank == null)
                {
                    profile_bank = new mp_profile_bank();
                }
                ViewBag.bank = profile_bank;

            }

            return View(appointment);
        }


        public IActionResult CancelConfirmation(Guid id)
        {
            return View(_appointmentService.Get().FirstOrDefault(e => e.id == id));
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(mp_profile_bank bank)
        {
            var collection = Request.Form;
            var appointment_id = Guid.Parse(collection["appointment_id"]);
            var appointment = _appointmentService.Get().Include(e => e.mp_credit).Include(e => e.client_).Include(e => e.clinician_).FirstOrDefault(e => e.id == appointment_id);

            if (appointment.status != 169 && appointment.status != 234)
            {
                TempData["AlertType"] = "alert-warning";
                TempData["AlertMessage"] = "This appointment cannot be cancelled.";
                return RedirectToAction("Details", new { id = appointment_id });
            }

            var user_notification = "An appointment that you created was cancelled.";


            Guid logged_user_id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            var cancelled_by = 298;
            var profile = _profileService.GetByUserId(logged_user_id);
            Guid? cancelById = null;
            if (User.IsInRole("client"))
            {
                cancelled_by = 297;
                bank.updated_by = logged_user_id.ToString();
                bank.created_by = logged_user_id.ToString();
                bank.profile_id = profile.id;
                cancelById = appointment.client_id;
                _profileBankService.AddOrUpdate(bank);
            }
            if (User.IsInRole("clinician"))
            {
                cancelById = appointment.clinician_id;
            }

            //check if payment has been made for the appointment
            if (appointment.mp_credit.Any() && appointment.status != 171)
            {
                var creditInfo = appointment.mp_credit.FirstOrDefault(x => x.appointment_id == appointment.id);
                if (creditInfo != null)
                {
                    var resundResponse = new PayStackHelper(_payStackSettings).Refund(creditInfo.transaction_reference);
                    if (resundResponse)
                    {
                        user_notification += " Your payment will be processed shortly and you will get it back in the next 24 - 48 hours.";
                        //refund the client
                        var refund = new mp_appointment_refund
                        {
                            appointment_id = appointment_id,
                            created_by = logged_user_id.ToString(),
                            amount = appointment.mp_credit.FirstOrDefault().amount,
                            cancelled_by = cancelled_by,
                            status = 296
                        };

                        _appointmentRefundService.AddRefund(refund);
                    }
                }

            }

            appointment.status = 171;
            appointment.cancelled_by = cancelById;
            appointment.cancel_reason = collection["comment"];

            _appointmentService.Update(appointment);
            //notifications to the client and the clinician

            var notification = new mp_notification
            {
                created_by = "sys_admin",
                created_by_name = "System Admin",
                notification_type = 5,
                read = 0,
                user_id = appointment.client_.user_id,
                notification = "Hi " + appointment.client_.last_name + " " + appointment.client_.first_name + ", " + user_notification,
                title = "Appointment cancelled"
            };

            NotificationUtil.Add(notification);

            await _emailSender.SendEmailAsync(appointment.client_.email, "Appointment cancelled - MySpace MyTime",
                  $"Hi " + appointment.client_.last_name + " " + appointment.client_.first_name + ", " + user_notification);


            notification = new mp_notification
            {
                created_by = "sys_admin",
                created_by_name = "System Admin",
                notification_type = 5,
                read = 0,
                user_id = appointment.clinician_.user_id,
                notification = "Hi " + appointment.clinician_.last_name + " " + appointment.clinician_.first_name + ", an appointment scheduled with you have been cancelled.",
                title = "Appointment cancelled"
            };



            NotificationUtil.Add(notification);

            await _emailSender.SendEmailAsync(appointment.clinician_.email, "Appointment cancelled - MySpace MyTime",
                $"Hi " + appointment.clinician_.last_name + " " + appointment.clinician_.first_name + ", an appointment scheduled with you have been cancelled.");



            return RedirectToAction("CancelConfirmation");
        }

        [HttpPost]
        public async Task CancelBySystem(string appointmentid)
        {
            Guid logged_user_id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            var cancelReason = string.Empty;
            var appointment_id = Guid.Parse(appointmentid);
            var appointment = _appointmentService.Get().Include(e => e.mp_credit)
                .Include(e => e.client_).Include(e => e.clinician_).FirstOrDefault(e => e.id == appointment_id);

            if (appointment.status == 234)
            {
                var user_notification = "An appointment that you created was cancelled.";
                var cancelled_by = 298;
                var profile = _profileService.GetByUserId(logged_user_id);
                Guid? cancelById = null;
                if (User.IsInRole("client"))
                {
                    var user_profile = _profileService.GetByUserId(logged_user_id);
                    var bank = _profileBankService.GetProfileBank(user_profile.id);
                    if (bank == null)
                    {
                        bank = new mp_profile_bank();
                    }
                    cancelled_by = 297;
                    bank.updated_by = logged_user_id.ToString();
                    bank.created_by = logged_user_id.ToString();
                    bank.profile_id = profile.id;
                    cancelById = appointment.client_id;
                    _profileBankService.AddOrUpdate(bank);
                    cancelReason = "Clinician not avilable on time.";
                }
                if (User.IsInRole("clinician"))
                {
                    cancelById = appointment.clinician_id;
                    cancelReason = "Client not avilable on time.";
                }

                //check if payment has been made for the appointment
                if (appointment.mp_credit.Any() && appointment.status != 171 && User.IsInRole("clinician"))
                {
                    var creditInfo = appointment.mp_credit.FirstOrDefault(x => x.appointment_id == appointment.id);
                    if (creditInfo != null)
                    {
                        var resundResponse = new PayStackHelper(_payStackSettings).Refund(creditInfo.transaction_reference);
                        if (resundResponse)
                        {
                            user_notification += " Your payment will be processed shortly and you will get it back in the next 24 - 48 hours.";
                            //refund the client
                            var refund = new mp_appointment_refund
                            {
                                appointment_id = appointment_id,
                                created_by = logged_user_id.ToString(),
                                amount = appointment.mp_credit.FirstOrDefault().amount,
                                cancelled_by = cancelled_by,
                                status = 296
                            };

                            _appointmentRefundService.AddRefund(refund);
                        }
                    }
                }

                appointment.status = 171;
                appointment.cancelled_by = cancelById;
                appointment.cancel_reason = cancelReason;
                _appointmentService.Update(appointment);
                //notifications to the client and the clinician

                var notification = new mp_notification
                {
                    created_by = "sys_admin",
                    created_by_name = "System Admin",
                    notification_type = 5,
                    read = 0,
                    user_id = appointment.client_.user_id,
                    notification = "Hi " + appointment.client_.last_name + " " + appointment.client_.first_name + ", " + user_notification + ", due to " + cancelReason,
                    title = "Appointment cancelled"
                };

                NotificationUtil.Add(notification);

                await _emailSender.SendEmailAsync(appointment.client_.email, "Appointment cancelled - MySpace MyTime",
                      $"Hi " + appointment.client_.last_name + " " + appointment.client_.first_name + ", " + user_notification + ", due to " + cancelReason);


                notification = new mp_notification
                {
                    created_by = "sys_admin",
                    created_by_name = "System Admin",
                    notification_type = 5,
                    read = 0,
                    user_id = appointment.clinician_.user_id,
                    notification = "Hi " + appointment.clinician_.last_name + " " + appointment.clinician_.first_name + ", an appointment scheduled with you have been cancelled." + ", due to " + cancelReason,
                    title = "Appointment cancelled"
                };

                NotificationUtil.Add(notification);
                await _emailSender.SendEmailAsync(appointment.clinician_.email, "Appointment cancelled - MySpace MyTime",
                    $"Hi " + appointment.clinician_.last_name + " " + appointment.clinician_.first_name + ", an appointment scheduled with you have been cancelled" + ", due to " + cancelReason);
            }
        }

        public DataTable GetAppointmentTrend()
        {
            var sql = "SELECT * from public.get_appointment_trend()";

            var cmd = new NpgsqlCommand(sql);
            var appointment_trend = DataAccess.GetDataTable(cmd);

            return appointment_trend;
        }


        // GET: Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(mp_appointment appointment)
        {
            var service = DAL.Utils.Options.GetAppointmentServices().FirstOrDefault(e => e.id == appointment.appointment_service);
            var collection = Request.Form;
            var date = collection["date"] + " " + collection["time"];
            var start_date = DateTime.Parse(date);

            appointment.start_date = start_date;
            appointment.end_date = start_date.AddMinutes(service.time_minutes);


            Guid logged_user_id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            mp_profile user_profile = _profileService.GetByUserId(logged_user_id);

            appointment.client_id = user_profile.id;
            Guid appointment_id;

            var clinician_available = _clinicianAvailabilityService.GetClinicianAvailabilityByDateRange(appointment);
            if (clinician_available != null)
            {
                //clinician is available by settings
                //now check if clinician already has appointment fixed
                var clinician_appointments = _appointmentService.GetClinicianAppointmentsByDateRange(appointment);
                if (clinician_appointments.Count() < 1)
                {
                    //clinician does not have appointments set for that time
                    //fix appointment
                    appointment_id = _appointmentService.Add(appointment);
                    TempData["appointment_id"] = appointment_id;
                    TempData["AlertType"] = "alert-success";
                    TempData["AlertMessage"] = "We have found an available clinician for you. Please make payment to confirm your booking";
                    return RedirectToAction(nameof(ConfirmAppointment));
                }
                //else
                //{
                //    //clinician is already occupied
                //    alternative_clinician = GetAvailableClinician(appointment);
                //    if (alternative_clinician != null)
                //    {
                //        appointment.clinician_id = alternative_clinician.id;
                //        appointment_id = _appointmentService.Add(appointment);
                //        TempData["appointment_id"] = appointment_id;
                //        TempData["AlertType"] = "alert-success";
                //        TempData["AlertMessage"] = "We have found an available clinician for you. Please make payment to confirm your booking";
                //        return RedirectToAction(nameof(ConfirmAppointment));
                //    }
                //    //check for other available clinicians
                //}
            }
            //else
            //{
            //    alternative_clinician = GetAvailableClinician(appointment);
            //    if (alternative_clinician != null)
            //    {
            //        appointment.clinician_id = alternative_clinician.id;
            //        appointment_id = _appointmentService.Add(appointment);
            //        TempData["appointment_id"] = appointment_id;
            //        TempData["AlertType"] = "alert-success";
            //        TempData["AlertMessage"] = "We have found an available clinician for you. Please make payment to confirm your booking";
            //        return RedirectToAction(nameof(ConfirmAppointment));
            //    }
            //    //check for other available clinicians
            //}

            //if we got here, then no clinician was available
            TempData["AlertType"] = "alert-warning";
            TempData["AlertMessage"] = "Sorry, we couldn't get an available clinician. Please change your dates and try again";
            return RedirectToAction("Clinicians", "Clinician");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewAppointment(mp_appointment appointment)
        {
            var service = DAL.Utils.Options.GetAppointmentServices().FirstOrDefault(e => e.id == appointment.appointment_service);
            var collection = Request.Form;
            var date = collection["date"] + " " + collection["time"];
            var start_date = DateTime.Parse(date);

            appointment.start_date = start_date;
            appointment.end_date = start_date.AddMinutes(service.time_minutes);


            Guid logged_user_id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            mp_profile user_profile = _profileService.GetByUserId(logged_user_id);

            appointment.client_id = user_profile.id;
            Guid appointment_id;

            var day_available = _clinicianAvailabilityService.Get().FirstOrDefault(e => e.day_name == start_date.DayOfWeek.ToString() && e.clinician_id == appointment.clinician_id);

            if (day_available != null)
            {


                // var clinician_available = _clinicianAvailabilityService.GetClinicianAvailabilityByDateRange(appointment);
                if (1 == 1)//if (AppointmentBL.IsClinicianAvailable(day_available.start_time,day_available.end_time,appointment.start_date,appointment.end_date))
                {
                    //clinician is available by settings
                    //now check if clinician already has appointment fixed
                    // var clinician_appointments = _appointmentService.GetClinicianAppointmentsByDateRange(appointment);
                    if (1 == 1)//if (AppointmentBL.IsAppointmentClashing(appointment.clinician_id,appointment))
                    {
                        //clinician does not have appointments set for that time
                        //fix appointment
                        appointment.status = 169;
                        appointment_id = _appointmentService.Add(appointment);
                        TempData["appointment_id"] = appointment_id;
                        TempData["AlertType"] = "alert-success";
                        TempData["AlertMessage"] = "We have found an available clinician for you. Please make payment to confirm your booking";
                        return RedirectToAction(nameof(ConfirmAppointment));
                    }

                }

            }
            //if we got here, then no clinician was available
            TempData["AlertType"] = "alert-warning";
            TempData["AlertMessage"] = "Sorry, the clinician will not be available at the time you chose. Please can you review their availability and adjust.";
            return RedirectToAction("NewAppointment", "Appointment", new { id = collection["profile_match"] });
        }

        public ActionResult ConfirmAppointment(Guid? id)
        {
            string appointment_id_str = TempData["appointment_id"]?.ToString();
            mp_appointment appointment = null;
            if (!String.IsNullOrEmpty(appointment_id_str))
            {
                Guid appointment_id = Guid.Parse(appointment_id_str);
                appointment = _appointmentService.Get(appointment_id);
            }
            else if (String.IsNullOrEmpty(appointment_id_str) && id.HasValue)
            {
                appointment = _appointmentService.Get(id.Value);
            }

            var service_cost = _serviceCostService.Get().FirstOrDefault(e => e.clinician_id == appointment.clinician_id && e.appointment_service_id == appointment.appointment_service && e.appointment_activity_sub_id == appointment.appointment_activity_sub_id);

            ViewBag.service_cost = service_cost;

            return View(appointment);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ConfirmAppointment(IFormCollection collection)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var appointment_id = collection["appointment_id"];
            try
            {
                mp_appointment appointment = _appointmentService.Get().Include(x => x.client_).Include(x => x.clinician_).FirstOrDefault(e => e.id == Guid.Parse(appointment_id));
                mp_credit credit = new mp_credit
                {
                    amount = Convert.ToDecimal(collection["amount"]),
                    profile_id = appointment.client_id,
                    created_by = user_id,
                    appointment_id = appointment.id,
                    mode_of_payment = 1,
                    transaction_reference = collection["transaction_reference"]
                };

                _creditService.Add(credit);

                appointment.status = 234;

                _appointmentService.Update(appointment);

                var admins = await _userManager.GetUsersInRoleAsync("super_admin");
                await new NotificationHelper(_emailSender).AppointmentScheduled(appointment, admins);


                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Successfully booked an appointment";
                return RedirectToAction("CompletedBooking", "Appointment");
            }
            catch (Exception ex)
            {
                TempData["AlertType"] = "alert-warning";
                TempData["AlertMessage"] = "Payment for appointment was not successful, Kindly select the appoinment and go to payment";
                return RedirectToAction("ConfirmAppointment", "Appointment", new { id = appointment_id });
            }
        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Appointment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Appointment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult NewAppointment(Guid id)
        {
            var profile_match = _profileMatchService.Get().Include(e => e.clinician_).Include(e => e.appointment_type_).Include(e => e.appointment_activity_).Include(e => e.appointment_activity_sub_).FirstOrDefault(e => e.id == id);

            ViewBag.schedules = _clinicianAvailabilityService.Get(profile_match.clinician_id).Where(x => x.start_time != null && x.start_time.Date >= DateTime.Now.Date);
            return View(profile_match);
        }

        private mp_profile GetAvailableClinician(mp_appointment appointment)
        {
            List<Guid> available_clinician_ids = _clinicianAvailabilityService.GetOtherCliniciansAvailabilityByDateRange(appointment).Select(e => e.clinician_id).ToList();

            List<Guid> taken_slots = _appointmentService.GetOtherCliniciansAppointmentsByDateRange(appointment).Where(e => available_clinician_ids.Contains(e.clinician_id)).Select(e => e.clinician_id).ToList();

            List<mp_profile> profiles = _profileService.Get().Where(e => available_clinician_ids.Contains(e.id) && !taken_slots.Contains(e.id)).ToList();

            if (profiles.Count() > 0)
            {
                return profiles.First();
            }

            return null;
        }

        public string CheckUserHadAppointment(int appointment_type)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            var appointments = _appointmentService.Get().Where(e => e.appointment_type == appointment_type && e.client_id == profile.id);
            var json = new JObject
            {
                { "appointments", appointments.Count() }
            };

            return json.ToString();
        }

        public List<CalenderItem> GetUserAppointments()
        {
            var appointments = new List<mp_appointment>();
            var user_id = _userManager.GetUserId(HttpContext.User);
            if (User.IsInRole("clinician"))
            {
                var profile = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);
                //get all the appointments of the profile
                appointments = _appointmentService.GetProfileAppointments(profile.id).ToList();
            }
            else if (User.IsInRole("client"))
            {
                var profile = _profileService.GetProfileByUserId(user_id);
                //get all the appointments of the profile
                appointments = _appointmentService.GetProfileAppointments(profile.id).ToList();
            }

            var calendar_items = new List<CalenderItem>();
            foreach (var appointment in appointments)
            {
                var calendar_item = new CalenderItem
                {
                    id = appointment.id,
                    title = appointment.appointment_typeNavigation.name,
                    start = appointment.start_date.ToString("yyyy-MM-ddThh:mm")
                };

                calendar_items.Add(calendar_item);
            }

            return calendar_items;
        }


        public async Task<IActionResult> MyAppointments(int? page)
        {
            int pageSize = 25;
            var user_id = _userManager.GetUserId(HttpContext.User);
            if (User.IsInRole("clinician"))
            {
                var profile = _clinicianService.Get().FirstOrDefault(e => e.user_id == user_id);
                //get all the appointments of the profile
                var appointments = _appointmentService.GetProfileAppointments(profile.id);

                return View(await PaginatedList<mp_appointment>.CreateAsync(appointments.OrderByDescending(e => e.created_at).AsNoTracking(), page ?? 1, pageSize));
            }
            else if (User.IsInRole("client"))
            {
                var profile = _profileService.GetProfileByUserId(user_id);
                //get all the appointments of the profile
                var appointments = _appointmentService.GetProfileAppointments(profile.id);
                return View(await PaginatedList<mp_appointment>.CreateAsync(appointments.OrderByDescending(e => e.created_at).AsNoTracking(), page ?? 1, pageSize));
            }


            return View();
        }

        public Appointment GetAppointment(Guid appointment_id)
        {
            var mp_apt = _appointmentService.Get().FirstOrDefault(e => e.id == appointment_id);
            var appointment = new Appointment(mp_apt);
            appointment.clinician = _clinicianService.Get(mp_apt.clinician_id);
            appointment.client = _profileService.Get().FirstOrDefault(e => e.id == mp_apt.client_id);
            appointment.appointment_serviceNavigation = DAL.Utils.Options.GetAppointmentServices().FirstOrDefault(e => e.id == mp_apt.appointment_service);

            return appointment;
        }


        public IActionResult CompletedBooking()
        {
            return View();
        }


        public async Task<IActionResult> MySessions(int? apttype, int? page)
        {
            int pageSize = 25;
            var user_id = _userManager.GetUserId(HttpContext.User);
            if (User.IsInRole("client"))
            {
                var profile = _profileService.GetProfileByUserId(user_id);
                //get all the appointments of the profile
                var appointments = _appointmentService.GetProfileAppointments(profile.id).Where(e => e.status == 170);
                if (apttype.HasValue)
                {
                    appointments = appointments.Where(e => e.appointment_type == apttype).Include(e => e.mp_credit);
                }
                //return View(appointments);

                return View(await PaginatedList<mp_appointment>.CreateAsync(appointments.OrderByDescending(e => e.created_at).AsNoTracking(), page ?? 1, pageSize));

            }
            else if (User.IsInRole("clinician"))
            {
                var profile = _clinicianService.GetByUserId(user_id);
                //get all the appointments of the profile
                var appointments = _appointmentService.GetProfileAppointments(profile.id).Where(e => e.status == 170);
                if (apttype.HasValue)
                {
                    appointments = appointments.Where(e => e.appointment_type == apttype).Include(e => e.mp_credit);
                }
                var tt = appointments.ToList();
                //return View(appointments);
                return View(await PaginatedList<mp_appointment>.CreateAsync(appointments.OrderByDescending(e => e.created_at).AsNoTracking(), page ?? 1, pageSize));
            }
            return NotFound();

            ////var sql = "get_client_form_timeline";
            //var sql = string.Format("SELECT * from public.get_client_form_timeline('{0}')", profile.id);

            //var cmd = new NpgsqlCommand(sql);
            ////cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("pro_id", NpgsqlDbType.Uuid, profile.id);
            //var dt = DataAccess.GetDataTable(cmd);

            //ViewBag.timelines = DataUtil.DataTableToList<TimelineItem>(dt);


        }

        [HttpPost]
        public async Task<ActionResult> Refund(string appointmentid)
        {
            try
            {
                var appointment_id = Guid.Parse(appointmentid);
                var appointment = _appointmentService.Get().Include(e => e.mp_credit)
                    .Include(e => e.client_).Include(e => e.clinician_).FirstOrDefault(e => e.id == appointment_id);

                if (appointment.status != 169 && appointment.status != 234)
                {
                    return Ok("This appointment cannot be cancelled.");
                }

                var user_notification = "An appointment that you created was cancelled.";

                Guid logged_user_id = Guid.Parse(_userManager.GetUserId(HttpContext.User));
                var cancelled_by = 298;
                var profile = _profileService.GetByUserId(logged_user_id);

                //check if payment has been made for the appointment
                if (appointment.mp_credit.Any() && appointment.status != 171)
                {
                    var creditInfo = appointment.mp_credit.FirstOrDefault(x => x.appointment_id == appointment.id);
                    if (creditInfo != null)
                    {
                        var resundResponse = new PayStackHelper(_payStackSettings).Refund(creditInfo.transaction_reference);
                        if (resundResponse)
                        {
                            user_notification += " Your payment will be processed shortly and you will get it back in the next 24 - 48 hours.";
                            //refund the client
                            var refund = new mp_appointment_refund
                            {
                                appointment_id = appointment_id,
                                created_by = logged_user_id.ToString(),
                                amount = appointment.mp_credit.FirstOrDefault().amount,
                                cancelled_by = cancelled_by,
                                status = 296
                            };

                            _appointmentRefundService.AddRefund(refund);
                        }

                    }

                }

                appointment.status = 171;
                appointment.cancelled_by = appointment.clinician_id;
                appointment.cancel_reason = "Cancel and Refund";

                _appointmentService.Update(appointment);
                //notifications to the client and the clinician

                var notification = new mp_notification
                {
                    created_by = "sys_admin",
                    created_by_name = "System Admin",
                    notification_type = 5,
                    read = 0,
                    user_id = appointment.client_.user_id,
                    notification = "Hi " + appointment.client_.last_name + " " + appointment.client_.first_name + ", " + user_notification,
                    title = "Appointment cancelled"
                };

                NotificationUtil.Add(notification);

                await _emailSender.SendEmailAsync(appointment.client_.email, "Appointment cancelled - MySpace MyTime",
                      $"Hi " + appointment.client_.last_name + " " + appointment.client_.first_name + ", " + user_notification);


                notification = new mp_notification
                {
                    created_by = "sys_admin",
                    created_by_name = "System Admin",
                    notification_type = 5,
                    read = 0,
                    user_id = appointment.clinician_.user_id,
                    notification = "Hi " + appointment.clinician_.last_name + " " + appointment.clinician_.first_name + ", an appointment scheduled with you have been cancelled.",
                    title = "Appointment cancelled"
                };

                NotificationUtil.Add(notification);
                await _emailSender.SendEmailAsync(appointment.clinician_.email, "Appointment cancelled - MySpace MyTime",
                    $"Hi " + appointment.clinician_.last_name + " " + appointment.clinician_.first_name + ", an appointment scheduled with you have been cancelled.");
                return Ok(200);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }


        public async Task<IActionResult> Sessions(int? page)
        {
            int pageSize = 25;
            var appointments = _appointmentService.Get().Where(e => e.status == 170).Include(e => e.clinician_)
                .Include(e => e.client_)
                .Include(e => e.appointment_typeNavigation)
                .Include(e => e.appointment_serviceNavigation);

            return View(await PaginatedList<mp_appointment>.CreateAsync(appointments.OrderByDescending(e => e.created_at).AsNoTracking(), page ?? 1, pageSize));

        }

        [HttpPost]
        public ActionResult RescheduleAppointment()
        {
            var collection = Request.Form;
            var mp_apt = _appointmentService.Get(Guid.Parse(collection["id"]));
            var service = DAL.Utils.Options.GetAppointmentServices().FirstOrDefault(e => e.id == mp_apt.appointment_service);
            DateTime.TryParseExact(collection["date"], "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime start_date);
            start_date += DateTime.Parse(collection["time"]).TimeOfDay;

            mp_apt.start_date = start_date;
            mp_apt.end_date = start_date.AddMinutes(service.time_minutes);

            var day_available = _clinicianAvailabilityService.Get().FirstOrDefault(e => e.day_name == start_date.DayOfWeek.ToString() && e.clinician_id == mp_apt.clinician_id);

            if (day_available != null)
            {
                _appointmentService.Update(mp_apt);
                TempData["appointment_id"] = mp_apt.id;
                TempData["AlertType"] = "alert-success";
                TempData["AlertMessage"] = "Appointment scheduled successfully!";
                return RedirectToAction("Details", "Appointment", new { id = collection["id"] });
            }
            //if we got here, then no clinician was available
            TempData["AlertType"] = "alert-warning";
            TempData["AlertMessage"] = "Sorry, the clinician will not be available at the time you chose. Please can you review their availability and adjust.";
            return RedirectToAction("Details", "Appointment", new { id = collection["id"] });
        }
    }
}