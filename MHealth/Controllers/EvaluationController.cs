using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using MHealth.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers
{
    public class EvaluationController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClinicianService _clinicianService;
        private readonly IEvaluationService _evaluationService;

        public EvaluationController(UserManager<ApplicationUser> userManager, IClinicianService clinicianService, IEvaluationService evaluationService)
        {
            _userManager = userManager;
            _clinicianService = clinicianService;
            _evaluationService = evaluationService;
        }

        public IActionResult PediatricEvaluation()
        {
            return View();
        }

        public IActionResult PediatricEvaluationPartial(Guid profile_id, Guid appointment_id)
        {
            ViewBag.appointment_id = appointment_id;
            ViewBag.profile_id = profile_id;

            mp_pediatric_evaluation evaluation = _evaluationService.GetPediatricEvaluation().Include(e=>e.mp_ped_evaluation_history).Include(e=>e.mp_ped_symptomps).FirstOrDefault(e => e.profile_id == profile_id && e.appointment_id == appointment_id);
            if (evaluation == null)
            {
                evaluation = new mp_pediatric_evaluation();
            }

            return PartialView(evaluation);
        }

        [HttpPost]
        public IActionResult PediatricEvaluation(mp_pediatric_evaluation evaluation, IFormCollection collection)
        {
            var symptoms = collection["symptoms"];
            //save the eval
            var user_id = _userManager.GetUserId(HttpContext.User);
            evaluation.created_by = user_id;
            evaluation.created_date = DateTime.Now;
            int id = _evaluationService.AddPediatricEvaluation(evaluation);
            //save the history
            _evaluationService.AddPedEvaluationHistory(new mp_ped_evaluation_history
            {
                ped_eval_id = id,
                profile_id = Guid.Parse(collection["profile_id"]),
                appointment_id = Guid.Parse(collection["appointment_id"])
            });
            //save the symptoms
            List<mp_ped_symptomps> ped_Symptomps = new List<mp_ped_symptomps>();
            foreach (var s in symptoms)
            {
                ped_Symptomps.Add(new mp_ped_symptomps
                {
                    symptom_id = int.Parse(s),
                    ped_evaluation_id = id
                });
            }
            _evaluationService.AddSymptoms(ped_Symptomps);
            return View();
        }

        [HttpPost]
        public string PediatricEvaluationPartial(mp_pediatric_evaluation evaluation, IFormCollection collection)
        {
            try
            {
                var symptoms = collection["symptoms"];
                //save the eval
                var user_id = _userManager.GetUserId(HttpContext.User);
                evaluation.created_by = user_id;
                evaluation.created_date = DateTime.Now;
                int id = _evaluationService.AddPediatricEvaluation(evaluation);
                //save the history
                _evaluationService.AddPedEvaluationHistory(new mp_ped_evaluation_history
                {
                    ped_eval_id = id,
                    profile_id = Guid.Parse(collection["profile_id"]),
                    appointment_id = Guid.Parse(collection["appointment_id"])
                });
                //save the symptoms
                List<mp_ped_symptomps> ped_Symptomps = new List<mp_ped_symptomps>();
                foreach (var s in symptoms)
                {
                    ped_Symptomps.Add(new mp_ped_symptomps
                    {
                        symptom_id = int.Parse(s),
                        ped_evaluation_id = id
                    });
                }
                _evaluationService.AddSymptoms(ped_Symptomps);
                return "success";
            }
            catch (Exception e)
            {
                return "error";
            }
        }

        [HttpGet]
        public IActionResult PrimaryCareMedicalPartial(Guid profile_id, Guid appointment_id)
        {
            ViewBag.appointment_id = appointment_id;
            ViewBag.profile_id = profile_id;

            mp_phc_health_history med_history = _evaluationService.GetHealthHistory().FirstOrDefault(e => e.profile_id == profile_id && e.appointment_id == appointment_id);
            if (med_history == null)
            {
                med_history = new mp_phc_health_history();
            }

            mp_phc_mental_status mental_status = _evaluationService.GetMentalStatus().FirstOrDefault(e => e.profile_id == profile_id && e.appointment_id == appointment_id);
            if (mental_status == null)
            {
                mental_status = new mp_phc_mental_status();
            }

            mp_phc_social_history social_history = _evaluationService.GetSocialHistory().FirstOrDefault(e => e.profile_id == profile_id && e.appointment_id == appointment_id);
            if (social_history == null)
            {
                social_history = new mp_phc_social_history();
            }

            ViewBag.med_history = med_history;
            ViewBag.mental_status = mental_status;
            ViewBag.social_history = social_history;

            return PartialView();
        }

        [HttpGet]
        public IActionResult PrimaryCareMedical(int id)
        {
            // mp_phc_health_history med_history = _evaluationService.GetHealthHistory().FirstOrDefault(e => e.profile_id == profile_id && e.appointment_id == appointment_id);
            // if (med_history == null)
            // {
            //     med_history = new mp_phc_health_history();
            // }

            // mp_phc_mental_status mental_status = _evaluationService.GetMentalStatus().FirstOrDefault(e => e.profile_id == profile_id && e.appointment_id == appointment_id);
            // if (mental_status == null)
            // {
            //     mental_status = new mp_phc_mental_status();
            // }

            // mp_phc_social_history social_history = _evaluationService.GetSocialHistory().FirstOrDefault(e => e.profile_id == profile_id && e.appointment_id == appointment_id);
            // if (social_history == null)
            // {
            //     social_history = new mp_phc_social_history();
            // }

            // ViewBag.med_history = med_history;
            // ViewBag.mental_status = mental_status;
            // ViewBag.social_history = social_history;

            return View();
        }

        [HttpPost]
        public string PrimaryCareMedicalPartial(mp_phc_health_history med_history, mp_phc_mental_status mental_status, mp_phc_social_history social_history)
        {
            try
            {
                med_history.created_at = DateTime.Now;
                med_history.created_by = _userManager.GetUserId(HttpContext.User);
                _evaluationService.AddHealthHistory(med_history);

                mental_status.create_by = _userManager.GetUserId(HttpContext.User);
                mental_status.created_at = DateTime.Now;
                _evaluationService.AddMentalStatus(mental_status);

                social_history.created_at = DateTime.Now;
                social_history.created_by = _userManager.GetUserId(HttpContext.User);
                _evaluationService.AddSocialHistory(social_history);

                return "success";
            }
            catch (System.Exception e)
            {
                return "error";
            }
        }

        [HttpGet]
        public IActionResult MedicalHistory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult MedicalHistory(mp_phc_health_history health_History, mp_phc_medical_history medical_History, mp_phc_mental_status mental_Status,
        mp_phc_social_history social_History, mp_phc_system_review system_Review)
        {
            //save all the information
            _evaluationService.AddHealthHistory(health_History);
            _evaluationService.AddMedicaHistory(medical_History);
            _evaluationService.AddMentalStatus(mental_Status);
            _evaluationService.AddSocialHistory(social_History);
            _evaluationService.AddSystemReview(system_Review);
            return View();
        }

        [HttpPost]
        public IActionResult ChildQuestionaire(mp_child_screening screening)
        {
            _evaluationService.AddChildScreening(screening);
            return View();
        }

        [HttpGet]
        public IActionResult ChildQuestionaire()
        {
            return View();
        }
    }
}