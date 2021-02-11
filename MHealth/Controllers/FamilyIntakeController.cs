using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data;
using MHealth.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MHealth.Controllers
{
    public class FamilyIntakeController : Controller
    {
        private readonly IFamilyIntakeService _familyIntakeService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppointmentFormService _appointmentFormService;
        private readonly IAppointmentService _appointmentService;

        public FamilyIntakeController(IFamilyIntakeService familyIntakeService, UserManager<ApplicationUser> userManager, IAppointmentFormService appointmentFormService, IAppointmentService appointmentService)
        {
            _familyIntakeService = familyIntakeService;
            _userManager = userManager;
            _appointmentFormService = appointmentFormService;
            _appointmentService = appointmentService;
        }

        // GET: FamilyIntake
        public ActionResult Index()
        {
            return View();
        }

        // GET: FamilyIntake/Details/5
        public ActionResult Details(int id)
        {
            return View(_familyIntakeService.Get().Include(e => e.profile_).FirstOrDefault(e => e.id == id));
        }

        // GET: FamilyIntake/Create
        public ActionResult Create()
        {
            return View(new mp_family_intake());
        }

        // POST: FamilyIntake/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var appointment = _appointmentService.Get(Guid.Parse(collection["appointment_id"]));

                var form = FormHelper.ColletionToJSON(collection);
                // TODO: Add insert logic here

                var intake = JsonConvert.DeserializeObject<mp_family_intake>(form.ToString());
                intake.created_by = _userManager.GetUserId(HttpContext.User);
                intake.profile_id = appointment.client_id;

                _familyIntakeService.Add(intake);

                var appointment_form = new mp_appointment_form
                {
                    appointment_id = intake.appointment_id,
                    form_id = 8
                };
                _appointmentFormService.Add(appointment_form);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public string CreatePartial(IFormCollection collection)
        {
            try
            {
                var appointment = _appointmentService.Get(Guid.Parse(collection["appointment_id"]));

                var form = FormHelper.ColletionToJSON(collection);
                // TODO: Add insert logic here

                var intake = JsonConvert.DeserializeObject<mp_family_intake>(form.ToString());
                intake.created_by = _userManager.GetUserId(HttpContext.User);
                intake.profile_id = appointment.client_id;

                _familyIntakeService.Add(intake);

                var appointment_form = new mp_appointment_form
                {
                    appointment_id = intake.appointment_id,
                    form_id = 8
                };
                _appointmentFormService.Add(appointment_form);

                return "success";
            }
            catch (Exception ex)
            {
                return "error";
            }
        }

        public IActionResult Partial(Guid appointment_id, int? id)
        {
            if (id.HasValue)
            {
                var intake = _familyIntakeService.Get(id.Value);
                return PartialView(intake);
            }
            else
            {
                var intake = new mp_family_intake
                {
                    appointment_id = appointment_id
                };
                return PartialView(intake);
            }

        }

        public IActionResult PartialList(Guid profile_id)
        {
            return PartialView(_familyIntakeService.GetClientIntkakes(profile_id));
        }

        // GET: FamilyIntake/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FamilyIntake/Edit/5
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

        // GET: FamilyIntake/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FamilyIntake/Delete/5
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
    }
}