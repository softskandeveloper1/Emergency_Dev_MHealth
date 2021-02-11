using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        // GET: api/Profile
        [HttpGet]
        public IEnumerable<mp_profile> Get()
        {
            return _profileService.Get();
        }

        // GET: api/Profile/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(Guid id)
        {
            return Ok(_profileService.Get(id));
        }

        //get the route of a particular user
        [HttpGet("{user_id}")]
        public IActionResult GetUser(Guid user_id)
        {
            return Ok(_profileService.GetByUserId(user_id));
        }

        // POST: api/Profile
        [HttpPost]
        public void Post(mp_profile profile)
        {
            _profileService.Add(profile);
        }

       
        // PUT: api/Profile/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

      
    }
}
