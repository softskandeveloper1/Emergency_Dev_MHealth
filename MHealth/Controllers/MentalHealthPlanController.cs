using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers
{
    public class MentalHealthPlanController : Controller
    {
        private readonly IMentalHealthPlanService _mentalHealthPlanService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MentalHealthPlanController(IMentalHealthPlanService mentalHealthPlanService, UserManager<ApplicationUser> userManager)
        {
            _mentalHealthPlanService = mentalHealthPlanService;
            _userManager = userManager;
        }
        // GET: MentalHealthPlan
        public ActionResult Index()
        {
            return View();
        }

        // GET: MentalHealthPlan/Details/5
        public ActionResult Details(int id)
        {
            var mental_health_plan = _mentalHealthPlanService.Get(id);
            return View(mental_health_plan);
        }

        // GET: MentalHealthPlan/Create
        public ActionResult Create()
        {
            return View(new mp_mental_health_plan());
        }

        public ActionResult Partial(string profile_id, string appointment_id)
        {
            ViewBag.appointment_id = Guid.Parse(appointment_id);
            ViewBag.profile_id = Guid.Parse(profile_id);
            return PartialView(new mp_mental_health_plan());
        }

        [HttpPost]
        public string Partial(mp_mental_health_plan health_Plan)
        {
            if (ModelState.IsValid)
            {
                health_Plan.created_by = _userManager.GetUserId(HttpContext.User);
                _mentalHealthPlanService.AddPlan(health_Plan);
                return "success";
            }
            return "error";
        }

        // POST: MentalHealthPlan/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MentalHealthPlan/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MentalHealthPlan/Edit/5
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

        // GET: MentalHealthPlan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MentalHealthPlan/Delete/5
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