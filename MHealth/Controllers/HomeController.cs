using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using DAL.Utils;
using MHealth.Helper;
using DAL.IService;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MHealth.Controllers
{
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class HomeController : Controller
    {
        private readonly IClinicianService _clinicianService;

        public HomeController(IClinicianService clinicianService)
        {
            _clinicianService = clinicianService;
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {

            if (User.IsInRole("clinician"))
            {
                return RedirectToAction("MyProfile", "Clinician");
            }
            else if (User.IsInRole("client"))
            {
                return RedirectToAction("MyProfile", "Profile");
            }
            else if (User.IsInRole("admin") || User.IsInRole("super_admin"))
            {
                var sql = "select (select count(id) from mp_profile where profile_type=1) as \"clients\",(select count(id) from mp_clinician) as \"clinicians\",(select count(id) from mp_appointment) as \"appointments\"  ";

                var cmd = new NpgsqlCommand(sql);
                ViewBag.dt = DataAccess.GetDataTable(cmd);

                sql = "SELECT * from public.get_appointment_monthly_summary()";

                cmd = new NpgsqlCommand(sql);
                ViewBag.appointment_summary = DataAccess.GetDataTable(cmd);

                var provider_summary = _clinicianService.Get().GroupBy(e => e.area_of_interest).Select(e => new[] { e.Key.Value, e.Count() }).ToList();

                ViewBag.provider_summary = provider_summary;

                sql = "SELECT * from public.get_appointment_trend()";

                cmd = new NpgsqlCommand(sql);
                var appointment_trend = DataAccess.GetDataTable(cmd);

                ViewBag.appointment_trend = appointment_trend;



                return View();
            }
            else
            {
                return NotFound();
            }

            return View();
          
        }

        public IActionResult Advise()
        {
            return View();
        }

        public IActionResult faq()
        {
            return View();
        }

        public IActionResult career()
        {
            return View();
        }

        public IActionResult contact()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult test()
        {
            return View();
        }
    }
}