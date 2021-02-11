using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data;
using MHealth.Data.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MHealth.Controllers.api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IProgressNoteService _progressNoteService;
        private readonly IFamilyIntakeService _familyIntakeService;
        private readonly IEvaluationService _evaluationService;
        private readonly IPsychiatricProgressNoteService _psychiatricProgressNote;
        private readonly UserManager<ApplicationUser> _userManager;

        public FormController(IFamilyIntakeService familyIntakeService, IProgressNoteService progressNoteService, IEvaluationService evaluationService, IPsychiatricProgressNoteService psychiatricProgressNote, UserManager<ApplicationUser> userManager)
        {
            _familyIntakeService = familyIntakeService;
            _progressNoteService = progressNoteService;
            _evaluationService = evaluationService;
            _psychiatricProgressNote = psychiatricProgressNote;
            _userManager = userManager;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetFamilyIntake(Guid appointment_id)
        {
            var intake = _familyIntakeService.Get().FirstOrDefault(e=>e.appointment_id==appointment_id);
            if (intake == null)
            {
                intake = new mp_family_intake
                {
                    appointment_id = appointment_id
                };
            }

            return Ok(intake);

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostFamilyIntake(mp_family_intake intake)
        {
            var old = _familyIntakeService.Get().FirstOrDefault(e => e.appointment_id == intake.appointment_id);

            var email = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByEmailAsync(email);
            if (old == null)
            {
                //this is a new form
                intake.created_by = user.Id;
                _familyIntakeService.Add(intake);
            }
            else
            {
                //update the existing form
                intake.created_by = old.created_by;
                intake.id = old.id;
                _familyIntakeService.Update(intake);
            }

            return Ok(200);
        }

        public IActionResult GetProgressNote(Guid appointment_id)
        {
            var progress_note = _progressNoteService.Get().FirstOrDefault(e => e.appointment_id == appointment_id);

            if (progress_note == null)
            {
                progress_note = new mp_progress_note { appointment_id = appointment_id };
            }
          
            return Ok(progress_note);
        }

        [HttpPost]
        public async Task<IActionResult> PostProgressNote(mp_progress_note note)
        {
            var old = _progressNoteService.Get().FirstOrDefault(e => e.appointment_id == note.appointment_id);

            var email = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByEmailAsync(email);
            if (old == null)
            {
                //this is a new form
                note.created_by = user.Id;
                await  _progressNoteService.AddProgressNote(note);
            }
            else
            {
                //update the existing form
                note.created_by = old.created_by;
                note.id = old.id;
                _progressNoteService.UpdateProgressNote(note);
            }

            return Ok(200);
        }

        public IActionResult GetPrimaryCare(Guid appointment_id)
        {
          
            mp_phc_health_history med_history = _evaluationService.GetHealthHistory().FirstOrDefault(e =>  e.appointment_id == appointment_id);
            if (med_history == null)
            {
                med_history = new mp_phc_health_history();
            }

            mp_phc_mental_status mental_status = _evaluationService.GetMentalStatus().FirstOrDefault(e =>e.appointment_id == appointment_id);
            if (mental_status == null)
            {
                mental_status = new mp_phc_mental_status();
            }

            mp_phc_social_history social_history = _evaluationService.GetSocialHistory().FirstOrDefault(e =>  e.appointment_id == appointment_id);
            if (social_history == null)
            {
                social_history = new mp_phc_social_history();
            }

            return Ok(new PrimaryCareModel(med_history,mental_status,social_history));
        }

        public IActionResult GetPediatricEvaluation(Guid appointment_id)
        {

            mp_pediatric_evaluation evaluation = _evaluationService.GetPediatricEvaluation().FirstOrDefault(e => e.appointment_id == appointment_id);
            if (evaluation == null)
            {
                evaluation = new mp_pediatric_evaluation();
            }

            return Ok(evaluation);
        }

        [HttpPost]
        public async Task<IActionResult> PostPediatricEvaluation(mp_pediatric_evaluation note)
        {
            var old = _evaluationService.GetPediatricEvaluation().FirstOrDefault(e => e.appointment_id == note.appointment_id);

            var email = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByEmailAsync(email);
            _evaluationService.AddPediatricEvaluation(note);

            return Ok(200);
        }


        public IActionResult GetPsychiatricProgress(Guid appointment_id)
        {
            var mp_psychiatric_progress_note = (_psychiatricProgressNote.GetNote()).FirstOrDefault(e=>e.appointment_id==appointment_id);
            if (mp_psychiatric_progress_note == null)
            {
                mp_psychiatric_progress_note = new mp_psychiatric_progress_note();
            }
            return Ok(mp_psychiatric_progress_note);
        }

        [HttpPost]
        public async Task<IActionResult> PostPsychiatricProgress(mp_psychiatric_progress_note note)
        {
            var old = _psychiatricProgressNote.GetNote().FirstOrDefault(e => e.appointment_id == note.appointment_id);

            var email = _userManager.GetUserId(HttpContext.User);
            var user = await _userManager.FindByEmailAsync(email);
            if (old == null)
            {
                //this is a new form
                note.created_by = user.Id;
                await _psychiatricProgressNote.Add(note);
            }
            else
            {
                //update the existing form
                note.created_by = old.created_by;
                note.id = old.id;
                _psychiatricProgressNote.Update(note);
            }

            return Ok(200);
        }
    }
}