using DAL.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MHealth.Helper
{
    public class ConsentAttribute : ActionFilterAttribute, IActionFilter
    {
        public string roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            UserManager<IdentityUser> userManager = (UserManager<IdentityUser>)context.HttpContext.RequestServices.GetService(typeof(UserManager<IdentityUser>));
            var username = context.HttpContext.User.Identity.Name;


            var userId = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var user = await userManager.FindByEmailAsync(username);
            var consented = ProfileUtil.IsConsented(userId,roles);


            if (!consented && roles=="client")
            {
                context.Result = new RedirectToRouteResult(
            new RouteValueDictionary {{ "Controller", "Profile" },
                                          { "Action", "Consent" } });

            }
            else if (!consented && roles == "clinician")
            {
                context.Result = new RedirectToRouteResult(
           new RouteValueDictionary {{ "Controller", "Clinician" },
                                          { "Action", "Consent" } });
            }


            base.OnActionExecuting(context);
        }
    }

    public class RoleFilters : ActionFilterAttribute, IActionFilter
    {

     

        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    // do something after the action executes
        //}
    }
}
