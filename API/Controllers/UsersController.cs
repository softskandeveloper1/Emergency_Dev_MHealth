using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using MHealth.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Utils;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private ILookUpService _lookUpService;

        public UsersController(IUserService userService, SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager, ILookUpService lookUpService)
        {
            _userService = userService;
            _signInManager = signInManager;
            _userManager = userManager;
            _lookUpService = lookUpService;
        }

        [AllowAnonymous]
        //[HttpPost("authenticate")]
        [Route("/api/[controller]/authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateModel model)
        {
            var username = model.Username;
            var result = await _signInManager.PasswordSignInAsync(username, model.Password, false, lockoutOnFailure: false);
            //var user = _userService.Authenticate(collection["Username"], collection["Password"]);

            if (result.Succeeded)
            {
                //get user information
                var user = await _userManager.FindByNameAsync(username);
                var token = _userService.GetToken(user);

                return Ok(new { message = "Success", token });
            }

            return BadRequest(new { message = "Username or password is incorrect" });

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/[controller]/get-all-reg-dropdowns")]
        public async Task<IActionResult> GetAllRegDropdowns()
        {
            var provider_type = await _lookUpService.GetLookUpByCategory("provider_type");
            var appointment_types = Options.GetAppointmentTypes().ToList();
            return Ok(new { provider_type, appointment_types });
        }
    }
}
