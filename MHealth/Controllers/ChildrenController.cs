using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers
{
    public class ChildrenController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileService _profileService;
               private readonly IChildrenService _childrenService;

        public ChildrenController(IProfileService profileService, UserManager<ApplicationUser> userManager,  IChildrenService childrenService)
        {
            _profileService = profileService;
            _userManager = userManager;
            _childrenService = childrenService;
        }


        public IActionResult Add(mp_children children)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            var profile = _profileService.GetProfileByUserId(user_id);

            children.profile_id = profile.id;
            children.created_by = user_id;

            _childrenService.Add(children);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Delete(int id)
        {      
            _childrenService.Delete(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}