using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers
{
    public class ClinicController : Controller
    {
        private readonly IClinicService _clinicService;
        public ClinicController(IClinicService clinicService)
        {
            _clinicService = clinicService;
        }

        public async Task<IActionResult> Clinics(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var clinics = _clinicService.Get().Where(e => e.status == 5);
            if (!string.IsNullOrEmpty(query))
            {
                clinics.Where(e => e.name.Contains(query) || e.first_name_c.Contains(query) || e.last_name_c.Contains(query));
            }
            return View(await PaginatedList<mp_clinic>.CreateAsync(clinics.OrderByDescending(e => e.name).AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> PClinics(int? pageNumber, string query = null)
        {
            int pageSize = 25;
            var clinics = _clinicService.Get().Where(e => e.status == 4);
            if (!string.IsNullOrEmpty(query))
            {
                clinics.Where(e => e.name.Contains(query) || e.first_name_c.Contains(query) || e.last_name_c.Contains(query));
            }
            return View(await PaginatedList<mp_clinic>.CreateAsync(clinics.OrderByDescending(e => e.name).AsNoTracking(), pageNumber ?? 1, pageSize));
           
        }

        public IActionResult Clinic(Guid id)
        {
            ViewBag.clinicians = _clinicService.GetClinicClinicians(id).Include(e => e.clinician_);
            return View(_clinicService.Get().FirstOrDefault(e => e.id == id));
        }

        public IActionResult Update(IFormCollection collection)
        {
            var id = Guid.Parse(collection["id"]);
            var clinic = _clinicService.Get(id);
            clinic.status = Convert.ToInt32(collection["status"]);
            _clinicService.Update(clinic);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}