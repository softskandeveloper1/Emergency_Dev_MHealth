using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data;
using MHealth.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IProfileBankService _profileBankService;
        private readonly IProfileHMOService _profileHMOService;
        UserManager<ApplicationUser> _userManager;
        public SettingsController(IProfileBankService profileBankService, IProfileHMOService profileHMOService, UserManager<ApplicationUser> userManager)
        {
            _profileBankService = profileBankService;
            _profileHMOService = profileHMOService;
            _userManager = userManager;
        }

        public IActionResult GetBank(Guid profile_id)
        {
            var profile_bank = _profileBankService.Get().FirstOrDefault(e => e.profile_id == profile_id);
            if (profile_bank == null)
            {
                profile_bank = new mp_profile_bank();
            }
            return Ok(profile_bank);
        }

        public IActionResult GetHMO(Guid profile_id)
        {
            var profile_hmo = _profileHMOService.Get().FirstOrDefault(e => e.profile_id == profile_id);
            if (profile_hmo == null)
            {
                profile_hmo = new mp_profile_hmo();
            }
            return Ok(profile_hmo);
        }

        [HttpPost]
        public IActionResult PostBank(mp_profile_bank bank)
        {
            _profileBankService.AddOrUpdate(bank);
            return Ok(200);
        }

        [HttpPost]
        public IActionResult PostHMO(mp_profile_hmo hmo)
        {
            _profileHMOService.AddOrUpdate(hmo);
            return Ok(200);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return Ok(400);
            }

            //await _signInManager.RefreshSignInAsync(user);
            //_logger.LogInformation("User changed their password successfully.");
            // StatusMessage = "Your password has been changed.";

            return Ok(200);
        }



    }
}