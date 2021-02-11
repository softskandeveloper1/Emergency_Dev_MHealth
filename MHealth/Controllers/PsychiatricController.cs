using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using DAL.IService;
using MHealth.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers
{
    public class PsychiatricController : Controller
    {
        private readonly IPsychiatricProgressNoteService _psychiatricService;
        private readonly UserManager<ApplicationUser> _userManager;
        public PsychiatricController(IPsychiatricProgressNoteService psychiatricService, UserManager<ApplicationUser> userManager)
        {
            _psychiatricService = psychiatricService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreatePartial(Guid profile_id, Guid appointment_id)
        {
            ViewBag.appointment_id = appointment_id;
            ViewBag.profile_id = profile_id;

            var evaluation =  _psychiatricService.GetPsychiatricOpd().Include(e=>e.mp_summary_treatment_history).Include(e=>e.mp_psychiatric_opd_evaluation_diagnosis).FirstOrDefault(e => e.appointment_id == appointment_id && e.profile_id == profile_id);

            if (evaluation == null)
            {
                evaluation = new mp_psychiatric_opd_evaluation
                {
                    appointment_id = appointment_id,
                    profile_id = profile_id
                };
            }

            return PartialView(evaluation);
        }

        [HttpPost]
        public string CreatePartial(mp_psychiatric_opd_evaluation evaluation)
        {
            if (ModelState.IsValid)
            {
                evaluation.created_at = DateTime.Now;
                evaluation.created_by = _userManager.GetUserId(HttpContext.User);
                _psychiatricService.AddPsychiatricOpd(evaluation);
                return "success";
            }
            return "error";
        }
    }
}