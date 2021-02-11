using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data.ViewModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;

namespace MHealth.Controllers.api
{
    //[Authorize(AuthenticationSchemes = AuthSchemes)]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private const string AuthSchemes =
        CookieAuthenticationDefaults.AuthenticationScheme + "," +
        JwtBearerDefaults.AuthenticationScheme;
        private readonly IProfileService _profileService;
        private readonly IClinicianService _clinicianService;
        public ProfileController(IProfileService profileService, IClinicianService clinicianService)
        {
            _profileService = profileService;
            _clinicianService = clinicianService;
        }
        // GET: api/Profile
        public IActionResult GetProfiles(string query)
        {
            return Ok(_profileService.Get().Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.unique_id.ToString().Contains(query)));
        }

        public IActionResult GetSearch(string query)
        {
            var profiles = _profileService.Get().Where(e => e.last_name.Contains(query) || e.first_name.Contains(query) || e.unique_id.ToString().Contains(query));
            var member_models = new List<MemberModel>();

            foreach(var profile in profiles)
            {
                member_models.Add(new MemberModel(profile));
            }
            return Ok(member_models);
        }

        // POST: api/Profile
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Profile/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }


        [HttpPost]
        public IActionResult UpdateProfile(PersonModel personModel)
        {
            if (personModel !=null)
            {
                if (personModel.role == "client")
                {
                    var profile = _profileService.Get(personModel.id);
                    if (profile != null)
                    {
                        profile.first_name = personModel.first_name;
                        //continue
                        _profileService.Update(profile);

                        return Ok(200);
                    }

                }
                else if (personModel.role == "clinician")
                {
                    var clinician = _clinicianService.Get(personModel.id);
                    if (clinician != null)
                    {
                        clinician.first_name = personModel.first_name;
                        //continue
                        _clinicianService.Update(clinician);
                        return Ok(200);
                    }
                }

            }

            return Ok(400);
        }
    }
}
