using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace MHealth.Helper
{
    public static class UserHelper
    {
        public static string get_profile_image()
        {
            //var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            //var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            //var userId = claim.Value;

            //var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (!File.Exists(""))
            //{
            //    return Path.Combine("wwwroot", "images", "profile", "anonymous.jpg");
            //}
            return Path.Combine("wwwroot", "images", "profile", "anonymous.jpg");
        }

        public static string GetUserId(this IPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;
            var claim = claimsIdentity.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
            return claim.Value;
        }
    }
}
