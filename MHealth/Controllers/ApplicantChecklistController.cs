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
    public class ApplicantChecklistController : Controller
    {
        private readonly IApplicantChecklistService _applicantChecklistService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicantChecklistController(IApplicantChecklistService applicantChecklistService, UserManager<ApplicationUser> userManager)
        {
            _applicantChecklistService = applicantChecklistService;
            _userManager = userManager;
        }

        public IActionResult LoadPartial(Guid applicant_id)
        {
            var applicant_checklist = _applicantChecklistService.Get().FirstOrDefault(e => e.clinician_id == applicant_id);
            if (applicant_checklist != null)
            {
                return PartialView(applicant_checklist);
            }
            applicant_checklist = new mp_applicant_checklist
            {
                clinician_id = applicant_id
            };
            return PartialView(applicant_checklist);
        }

        [HttpPost]
        public IActionResult postchecklist(mp_applicant_checklist applicant_checklist)
        {
            var user_id = _userManager.GetUserId(HttpContext.User);
            applicant_checklist.created_by = user_id;
            _applicantChecklistService.AddOrUpdate(applicant_checklist);

            return Redirect(Request.Headers["Referer"].ToString());

        }
    }
}