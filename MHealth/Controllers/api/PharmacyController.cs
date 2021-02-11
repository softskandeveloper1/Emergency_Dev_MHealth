using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        IPharmacyService _pharmacyService;
        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }

        public IActionResult GetPharmacies(string query)
        {
            if (!string.IsNullOrEmpty(query) && query != "undefined")
            {
                return Ok(_pharmacyService.Get().Take(5));
            }
            query = query.Trim();
            return Ok(_pharmacyService.Get().Where(e => e.name.Contains(query, StringComparison.OrdinalIgnoreCase) || e.address.Contains(query, StringComparison.OrdinalIgnoreCase)));
        }

        public IActionResult GetById(int id)
        {
            return Ok(_pharmacyService.Get().FirstOrDefault(e => e.id == id));
        }

        public IActionResult Get()
        {
            return Ok(_pharmacyService.Get());
        }
    }
}