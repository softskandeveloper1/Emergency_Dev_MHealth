using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Utils;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace MHealth.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            //var token = new TwilioHelper().GenerateCode();

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
                return View();
            }
            else
            {
                return NotFound();
            }
        }
    }
}