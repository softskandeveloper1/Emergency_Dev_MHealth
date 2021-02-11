using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.IService;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace MHealth.Controllers
{
    [Authorize]
    public class PsychosocialController : Controller
    {
        private readonly IPsychosocialService _pschosocialService;
        private readonly IProfileService _profileService;

        public PsychosocialController(IPsychosocialService pschosocialService, IProfileService profileService)
        {
            _pschosocialService = pschosocialService;
            _profileService = profileService;
        }

        // GET: Psychosocial
        public async Task<IActionResult> Index()
        {
            return View(await _pschosocialService.GetPsychosocials());
        }

        // GET: Psychosocial/Details/5
        public async Task<IActionResult> Adult(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_psychosocial = await _pschosocialService.GetPsychosocial((int)id);
            if (mp_psychosocial == null)
            {
                return NotFound();
            }

            return View(mp_psychosocial);
        }

        // GET: Psychosocial/Create
        public IActionResult Create()
        {
            return View();
        }

        // public IActionResult Adult()
        // {
        //     return View();
        // }

        public IActionResult AdultPartial(string profile_id, string appointment_id)
        {
            ViewBag.appointment_id = Guid.Parse(appointment_id);
            ViewBag.profile_id = Guid.Parse(profile_id);
            return PartialView();
        }

        // POST: Psychosocial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Adult([Bind("problem,depression,anxiety,mood_swing,appetite_changes,sleep_changes,hallucinations,work_problems,racing_thoughts,confusion,memory_problems,loss_interest,irritability,excessive_worry,suicidal_ideation,relationship_issues,low_energy,panic_attacks,obsessive_thoughts,ritualistic_behaviour,checking,counting,self_injury,difficulty_concentrating,hyperactivity,history,effect_symptons,mental_problem,mental_problem_description,mental_hospitalization,mental_hospitalization_description")] mp_psychosocial mp_psychosocial, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                mp_psychosocial.created_at = DateTime.Now;
                mp_psychosocial.created_by = User.FindFirstValue(ClaimTypes.NameIdentifier);
                mp_psychosocial.profile_id = Guid.Parse(collection["profile_id"]); //_profileService.GetByUserId(Guid.Parse(mp_psychosocial.created_by)).id;
                mp_psychosocial.appointment_id = Guid.Parse(collection["appointment_id"]);
                _pschosocialService.AddPsychosocial(mp_psychosocial);
                return "success";
            }
            return "failure";
        }

        [HttpPost]
        public string Medication(mp_medical_history mp_medical_history, IFormCollection collections)
        {
            if (ModelState.IsValid)
            {
                //var profile = _profileService.GetByUserId(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                mp_medical_history.created_at = DateTime.Now;
                mp_medical_history.created_by = User.FindFirstValue(ClaimTypes.NameIdentifier);
                mp_medical_history.profile_id = Guid.Parse(collections["profile_id"]);
                mp_medical_history.appointment_id = Guid.Parse(collections["appointment_id"]);
                _pschosocialService.AddMedicalHistory(mp_medical_history);

                List<mp_medication> mp_medications = new List<mp_medication>();
                var medication = collections["medication"];
                //save medication
                if (medication.Count > 0 && medication[0].Trim() != "")
                {
                    for (int i = 0; i < medication.Count; i++)
                    {
                        mp_medications.Add(new mp_medication
                        {
                            medication = medication[i],
                            dosage = collections["dosage"][i],
                            //last_dose = DateTime.Parse(collections["last_dose"][i]),
                            physician = collections["physician"][i],
                            taking_as_prescribed = int.Parse(collections["taking_as_prescribed"][i]),
                            created_at = DateTime.Now,
                            created_by = User.FindFirstValue(ClaimTypes.NameIdentifier),
                            profile_id = Guid.Parse(collections["profile_id"])
                        });
                    }
                    if (mp_medications.Count() > 0) _pschosocialService.AddMedication(mp_medications);
                }


                List<mp_surgical_history> surgical_Histories = new List<mp_surgical_history>();
                var surgical_type = collections["surgery_type"];
                //save medication
                if (surgical_type.Count > 0 && surgical_type[0].Trim() != "")
                {
                    for (int i = 0; i < surgical_type.Count; i++)
                    {
                        surgical_Histories.Add(new mp_surgical_history
                        {
                            surgery_type = int.Parse(surgical_type[i]),
                            date = DateTime.Parse(collections["date"][i]),
                            comments = collections["comment"],
                            created_at = DateTime.Now,
                            created_by = User.FindFirstValue(ClaimTypes.NameIdentifier),
                            profile_id = Guid.Parse(collections["profile_id"])
                        });
                    }
                    if (surgical_Histories.Count() > 0) _pschosocialService.AddSurgicalHistories(surgical_Histories);
                }
                return "success";
            }

            return "failure";
        }

        [HttpPost]
        public string MaritalRelationship(mp_social_relationship relationship, IFormCollection collections)
        {
            if (ModelState.IsValid)
            {
                var profile = _profileService.GetByUserId(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                relationship.created_at = DateTime.Now;
                relationship.created_by = User.FindFirstValue(ClaimTypes.NameIdentifier);
                relationship.profile_id = Guid.Parse(collections["profile_id"]);
                relationship.appointment_id = Guid.Parse(collections["appointment_id"]);
                _pschosocialService.AddSocialRelationship(relationship);

                List<mp_children> mp_childrens = new List<mp_children>();
                var name = collections["name"];
                //save medication
                if (name.Count > 0 & name[0].Trim() != "")
                {
                    for (int i = 0; i < name.Count; i++)
                    {
                        mp_childrens.Add(new mp_children
                        {
                            name = name[i],
                            dob = DateTime.Parse(collections["dob"][i]),
                            gender = int.Parse(collections["gender"][i]),
                            profile_id = profile.id,
                            created_by = User.FindFirstValue(ClaimTypes.NameIdentifier),
                            created_at = DateTime.Now
                        });
                    }
                    if (mp_childrens.Count > 0) _pschosocialService.AddChildren(mp_childrens);
                }

                return "success";
            }
            return "failure";
        }


        [HttpPost]
        public string FamilyHistory(mp_family_history history, IFormCollection collections)
        {
            if (ModelState.IsValid)
            {
                var profile = _profileService.GetByUserId(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                history.created_at = DateTime.Now;
                history.created_by = User.FindFirstValue(ClaimTypes.NameIdentifier);
                history.profile_id = Guid.Parse(collections["profile_id"]);
                history.appointment_id = Guid.Parse(collections["appointment_id"]);
                _pschosocialService.AddFamilyHistory(history);
                return "success";
            }
            return "failure";
        }

        [HttpPost]
        public string EducationHistory(mp_education_history education, IFormCollection collections)
        {
            if (ModelState.IsValid)
            {
                var profile = _profileService.GetByUserId(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                education.created_at = DateTime.Now;
                education.created_by = User.FindFirstValue(ClaimTypes.NameIdentifier);
                education.profile_id = Guid.Parse(collections["profile_id"]);
                education.appointment_id = Guid.Parse(collections["appointment_id"]);
                _pschosocialService.AddEducationHistory(education);
                return "success";
            }
            return "failure";
        }


        [HttpPost]
        public string WorkHistory(mp_employment employment, IFormCollection collections)
        {
            if (ModelState.IsValid)
            {
                var profile = _profileService.GetByUserId(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                employment.created_at = DateTime.Now;
                employment.created_by = User.FindFirstValue(ClaimTypes.NameIdentifier);
                employment.profile_id = Guid.Parse(collections["profile_id"]);
                //employment.appointment_id = Guid.Parse(collections["appointment_id"]);
                _pschosocialService.AddEmployment(employment);
                return "success";
            }
            return "failure";
        }

        [HttpPost]
        public string SubstanceUse(mp_substance_use mp_substance_use, IFormCollection collections)
        {
            if (ModelState.IsValid)
            {
                var profile = _profileService.GetByUserId(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
                mp_substance_use.created_at = DateTime.Now;
                mp_substance_use.created_by = User.FindFirstValue(ClaimTypes.NameIdentifier);
                mp_substance_use.profile_id = Guid.Parse(collections["profile_id"]);
                mp_substance_use.appointment_id = Guid.Parse(collections["appointment_id"]);

                _pschosocialService.AddSubstanceUse(mp_substance_use);
                return "success";
            }
            return "failure";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("profile_id,problem,depression,anxiety,mood_swing,appetite_changes,sleep_changes,hallucinations,work_problems,racing_thoughts,confusion,memory_problems,loss_interest,irritability,excessive_worry,suicidal_ideation,relationship_issues,low_energy,panic_attacks,obsessive_thoughts,ritualistic_behaviour,checking,counting,self_injury,difficulty_concentrating,hyperactivity,history,effect_symptons,mental_problem,mental_problem_description,mental_hospitalization,mental_hospitalization_description")] mp_psychosocial mp_psychosocial)
        {
            if (ModelState.IsValid)
            {
                _pschosocialService.AddPsychosocial(mp_psychosocial);
                return RedirectToAction(nameof(Index));
            }
            return View(mp_psychosocial);
        }

        // GET: Psychosocial/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_psychosocial = await _pschosocialService.GetPsychosocial((int)id);
            if (mp_psychosocial == null)
            {
                return NotFound();
            }
            return View(mp_psychosocial);
        }

        // POST: Psychosocial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,profile_id,problem,depression,anxiety,mood_swing,appetite_changes,sleep_changes,hallucinations,work_problems,racing_thoughts,confusion,memory_problems,loss_interest,irritability,excessive_worry,suicidal_ideation,relationship_issues,low_energy,panic_attacks,obsessive_thoughts,ritualistic_behaviour,checking,counting,self_injury,difficulty_concentrating,hyperactivity,history,effect_symptons,mental_problem,mental_problem_description,mental_hospitalization,mental_hospitalization_description")] mp_psychosocial mp_psychosocial)
        {
            if (id != mp_psychosocial.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _pschosocialService.UpdatePsychosocial(mp_psychosocial);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_pschosocialService.PsychosocialExists(mp_psychosocial.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mp_psychosocial);
        }

        // GET: Psychosocial/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_psychosocial = await _pschosocialService.GetPsychosocial((int)id);
            if (mp_psychosocial == null)
            {
                return NotFound();
            }

            return View(mp_psychosocial);
        }

        // POST: Psychosocial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pschosocialService.RemovePsychosocial(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
