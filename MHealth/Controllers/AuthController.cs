using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL.IService;
using MHealth.Data;
using MHealth.Data.ViewModel;
using MHealth.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace MHealth.Controllers
{
    public class AuthController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _config;
        private readonly IProfileService _profileService;
        private readonly IClinicianService _clinicianService;

        public AuthController(UserManager<ApplicationUser> userManager, ILogger<AuthController> logger,
            IEmailSender emailSender, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleMgr, IConfiguration config, IProfileService profileService, IClinicianService clinicianService)
        {
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _signInManager = signInManager;
            roleManager = roleMgr;
            _config = config;
            _profileService = profileService;
            _clinicianService = clinicianService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] LoginViewModel model)
        {
            //var collection = Request.Form;
            //var model = new LoginViewModel();
            //model.Email = email;
            //model.Password = password;
            //var username = collection["Email"].ToString();
            //model.Email = username;
            //model.Password = collection["Password"].ToString();
            //if (ModelState.IsValid)
            //{

            var personModel = new PersonModel();

            var user = await _userManager.FindByEmailAsync(model.Email);
           
            
            if (user != null)
            {
                //var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains("client"))
                    {
                        var profile = _profileService.GetProfileByUserId(user.Id);
                        personModel = new PersonModel(profile);
                    }
                    else if (roles.Contains("clinician"))
                    {
                        var clinician = _clinicianService.GetByUserId(user.Id);
                        personModel = new PersonModel(clinician);
                    }
                    else
                    {
                        return BadRequest(new { response = 202 });
                    }

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                        _config["Tokens:Issuer"],
                        claims,
                        expires: DateTime.Now.AddMinutes(180),
                        signingCredentials: creds);

                    personModel.token =  new JwtSecurityTokenHandler().WriteToken(token);
                    personModel.response = 200;
                    personModel.user_id = user.Id;

                    // return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                    return Ok(personModel);
                }
            }
            //}

            return BadRequest(new { response = 202 });
            //return BadRequest("Could not create token");
        }
   
    }


    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}