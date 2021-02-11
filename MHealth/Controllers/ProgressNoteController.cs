using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.IService;
using MHealth.Data;
using Microsoft.AspNetCore.Identity;

namespace MHealth.Controllers
{
    public class ProgressNoteController : Controller
    {
        private readonly IProgressNoteService _progressNoteService;
        private readonly IAppointmentService _appointmentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProgressNoteController(IProgressNoteService progressNoteService, IAppointmentService appointmentService, UserManager<ApplicationUser> userManager)
        {
            _progressNoteService = progressNoteService;
            _appointmentService = appointmentService;
            _userManager = userManager;
        }

        // GET: ProgressNote
        public async Task<IActionResult> Index()
        {
            return View(await _progressNoteService.GetProgressNotes());
        }

        // GET: ProgressNote/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_progress_note = await _progressNoteService.GetProgressNote((int)id);
            if (mp_progress_note == null)
            {
                return NotFound();
            }

            return View(mp_progress_note);
        }

        public async Task<IActionResult> Partial(string appointment_id, int? id, string profile_id)
        {
            var progress_note = new mp_progress_note();
            //get progress not with the appointment_id
            if (id.HasValue)
            {
                progress_note = await _progressNoteService.GetProgressNote(id.Value);
                return View(progress_note);
            }
            else
            {
                progress_note = (await _progressNoteService.GetProgressNotes()).FirstOrDefault(e => e.appointment_id == Guid.Parse(appointment_id));

                if (progress_note == null)
                {
                    progress_note = new mp_progress_note { appointment_id = Guid.Parse(appointment_id) };
                }
                ViewBag.appointment_id = Guid.Parse(appointment_id);
                ViewBag.profile_id = Guid.Parse(profile_id);
            }

            return PartialView(progress_note);
        }

        // GET: ProgressNote/Create
        public IActionResult Create()
        {
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name");
            return View();
        }

        // POST: ProgressNote/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("profile_id,appetite,sleep,side_effects,meditation_efficacy,medication_compliance,orientation,rapport,appearance,mood,affect,speech,thought_process,insight,judgement,cognitive,psychomotor_activity,memory,assessment,assessment_descriptioni,plan,appointment_id")] mp_progress_note mp_progress_note)
        {

            var appointment = _appointmentService.Get(mp_progress_note.appointment_id);
            mp_progress_note.profile_id = appointment.client_id;
            mp_progress_note.created_by = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                await _progressNoteService.AddProgressNote(mp_progress_note);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_progress_note.profile_id);
            return View(mp_progress_note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> CreatePartial([Bind("profile_id,appetite,sleep,side_effects,meditation_efficacy,medication_compliance,orientation,rapport,appearance,mood,affect,speech,thought_process,insight,judgement,cognitive,psychomotor_activity,memory,assessment,assessment_descriptioni,plan,appointment_id")] mp_progress_note mp_progress_note)
        {

            var appointment = _appointmentService.Get(mp_progress_note.appointment_id);
            mp_progress_note.profile_id = appointment.client_id;
            mp_progress_note.created_by = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                await _progressNoteService.AddProgressNote(mp_progress_note);
                return "success";
            }
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_progress_note.profile_id);
            return "error";
        }

        // GET: ProgressNote/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_progress_note = await _progressNoteService.GetProgressNote((int)id);
            if (mp_progress_note == null)
            {
                return NotFound();
            }
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_progress_note.profile_id);
            return View(mp_progress_note);
        }

        // POST: ProgressNote/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("id,profile_id,appetite,sleep,side_effects,meditation_efficacy,medication_compliance,orientation,rapport,appearance,mood,affect,speech,thought_process,insight,judgement,cognitive,psychomotor_activity,memory,assessment,assessment_descriptioni,plan")] mp_progress_note mp_progress_note)
        {
            if (id != mp_progress_note.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _progressNoteService.UpdateProgressNote(mp_progress_note);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_progressNoteService.ProgressNoteExists(mp_progress_note.id))
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
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_progress_note.profile_id);
            return View(mp_progress_note);
        }

        // GET: ProgressNote/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_progress_note = await _progressNoteService.GetProgressNote((int)id);
            if (mp_progress_note == null)
            {
                return NotFound();
            }

            return View(mp_progress_note);
        }

        // POST: ProgressNote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _progressNoteService.RemoveProgressNote(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
