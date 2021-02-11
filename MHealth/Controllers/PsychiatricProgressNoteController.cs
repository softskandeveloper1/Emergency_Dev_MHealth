using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.IService;
using Microsoft.AspNetCore.Authorization;

namespace MHealth.Controllers
{
    [Authorize]
    public class PsychiatricProgressNoteController : Controller
    {
        private readonly IPsychiatricProgressNoteService _psychiatricProgressNote;

        public PsychiatricProgressNoteController(IPsychiatricProgressNoteService psychiatricProgressNote)
        {
            _psychiatricProgressNote = psychiatricProgressNote;
        }

        // GET: PsychiatricProgressNote
        public async Task<IActionResult> Index()
        {
            return View(await _psychiatricProgressNote.Get());
        }

        // GET: PsychiatricProgressNote/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_psychiatric_progress_note = await _psychiatricProgressNote.Get(id.Value);

            if (mp_psychiatric_progress_note == null)
            {
                return NotFound();
            }

            return View(mp_psychiatric_progress_note);
        }

        // GET: PsychiatricProgressNote/Create
        public IActionResult Create()
        {
            //ViewData["appointment_id"] = new Guid(); // test sample
            //ViewData["profile_id"] = new Guid(); //test
            return View();
        }

        public IActionResult CreatePartial(string profile_id, string appointment_id)
        {
            ViewBag.appointment_id = Guid.Parse(appointment_id);
            ViewBag.profile_id = Guid.Parse(profile_id);
            return PartialView();
        }

        // POST: PsychiatricProgressNote/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,profile_id,appointment_id,date_of_service,server,start_time,stop_time,length_of_session,type_of_session,service_code,visit_reason,past_history,family_history,symptoms_review,height,weight,blood_pressure,pulse,psychiatric_exam_note,allergies,additional_comment,risk_and_benefit_note,created_by,created_at")] mp_psychiatric_progress_note mp_psychiatric_progress_note)
        {
            if (ModelState.IsValid)
            {
                await _psychiatricProgressNote.Add(mp_psychiatric_progress_note);
                return RedirectToAction(nameof(Index));
            }
            return View(mp_psychiatric_progress_note);
        }

        // GET: PsychiatricProgressNote/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_psychiatric_progress_note = await _psychiatricProgressNote.Get(id.Value);
            if (mp_psychiatric_progress_note == null)
            {
                return NotFound();
            }
            ViewData["appointment_id"] = new Guid(); // test sample
            ViewData["profile_id"] = new Guid(); //test

            return View(mp_psychiatric_progress_note);
        }

        // POST: PsychiatricProgressNote/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("id,profile_id,appointment_id,date_of_service,server,start_time,stop_time,length_of_session,type_of_session,service_code,visit_reason,past_history,family_history,symptoms_review,height,weight,blood_pressure,pulse,psychiatric_exam_note,allergies,additional_comment,risk_and_benefit_note,created_by,created_at")] mp_psychiatric_progress_note mp_psychiatric_progress_note)
        {
            if (id != mp_psychiatric_progress_note.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _psychiatricProgressNote.Update(mp_psychiatric_progress_note);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mp_psychiatric_progress_noteExists(mp_psychiatric_progress_note.id))
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
            ViewData["appointment_id"] = new Guid(); // test sample
            ViewData["profile_id"] = new Guid(); //test
            return View(mp_psychiatric_progress_note);
        }

        // GET: PsychiatricProgressNote/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_psychiatric_progress_note = await _psychiatricProgressNote.Get(id.Value);
            if (mp_psychiatric_progress_note == null)
            {
                return NotFound();
            }

            return View(mp_psychiatric_progress_note);
        }

        // POST: PsychiatricProgressNote/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var mp_psychiatric_progress_note = await _psychiatricProgressNote.Get(id);
            if (mp_psychiatric_progress_note != null)
            {
                await _psychiatricProgressNote.Remove(id);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool mp_psychiatric_progress_noteExists(long id)
        {
            return _psychiatricProgressNote.Exists(id);
        }
    }
}
