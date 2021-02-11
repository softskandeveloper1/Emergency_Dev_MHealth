using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers
{
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService _pharmacyService;

        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }
        public async Task<IActionResult> Manage(int? page)
        {
            int pageSize = 25;
            var pharmacies = _pharmacyService.Get();
            return View(await PaginatedList<mp_pharmacy>.CreateAsync(pharmacies.OrderByDescending(e => e.name).AsNoTracking(), page ?? 1, pageSize));
        }

        [HttpPost]
        public IActionResult Post(mp_pharmacy pharmacy)
        {
            _pharmacyService.Add(pharmacy);
            return RedirectToAction("Manage");
        }
    }
}