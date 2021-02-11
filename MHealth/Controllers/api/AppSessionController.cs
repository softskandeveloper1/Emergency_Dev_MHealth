using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MHealth.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace MHealth.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AppSessionController : ControllerBase
    {
        public IActionResult GetToken(string username,Guid appointment_id)
        {
            var rs = new JObject();
            rs.Add("token",new TwilioHelper().GenerateCode(username));
            return Ok(rs.ToString());
        }
    }
}