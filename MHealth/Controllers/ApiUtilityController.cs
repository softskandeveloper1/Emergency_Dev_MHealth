using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUtilityController : ControllerBase
    {
        private readonly IClinicianService _clinicianService;

        public ApiUtilityController(IClinicianService clinicianService)
        {
            _clinicianService = clinicianService;
        }

        //// GET: api/ApiUtility
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("/api/apiutility/getclinician/{guid}")]
        public dynamic GetClinician(Guid guid)
        {
           var clinician =  _clinicianService.Get(guid);
            if (clinician != null)
            {
                return new
                {
                    id = clinician.id,
                    phone = clinician.phone,
                    first_name = clinician.first_name,
                    last_name = clinician.last_name
                };

            }

            return "value";
        }

        // POST: api/ApiUtility
        [HttpPost("/api/apiutility/post_lookup")]
        public IActionResult Post([FromBody] string value,string category)
        {
            var lookup = new mp_lookup
            {
                category = category,
                value = value
            };

            lookup = Options.AddLookup(lookup);

            return Ok(lookup.id);
        }

        //// PUT: api/ApiUtility/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
