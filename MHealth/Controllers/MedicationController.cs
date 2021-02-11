using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;

namespace MHealth.Controllers
{
    public class MedicationController : Controller
    {
        private readonly HContext _context;

        public MedicationController(HContext context)
        {
            _context = context;
        }

        // GET: Medication
        public async Task<IActionResult> Index()
        {
            var hContext = _context.mp_medication.Include(m => m.profile_);
            return View(await hContext.ToListAsync());
        }

        // GET: Medication/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_medication = await _context.mp_medication
                .Include(m => m.profile_)
                .FirstOrDefaultAsync(m => m.id == id);
            if (mp_medication == null)
            {
                return NotFound();
            }

            return View(mp_medication);
        }

        // GET: Medication/Create
        public IActionResult Create()
        {
            ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name");
            return View();
        }

        // POST: Medication/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,profile_id,medication,dosage,physician,last_dose,taking_as_prescribed,created_at,created_by")] mp_medication mp_medication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mp_medication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_medication.profile_id);
            return View(mp_medication);
        }

        // GET: Medication/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_medication = await _context.mp_medication.FindAsync(id);
            if (mp_medication == null)
            {
                return NotFound();
            }
            ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_medication.profile_id);
            return View(mp_medication);
        }

        // POST: Medication/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,profile_id,medication,dosage,physician,last_dose,taking_as_prescribed,created_at,created_by")] mp_medication mp_medication)
        {
            if (id != mp_medication.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mp_medication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mp_medicationExists(mp_medication.id))
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
            ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_medication.profile_id);
            return View(mp_medication);
        }

        // GET: Medication/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_medication = await _context.mp_medication
                .Include(m => m.profile_)
                .FirstOrDefaultAsync(m => m.id == id);
            if (mp_medication == null)
            {
                return NotFound();
            }

            return View(mp_medication);
        }

        // POST: Medication/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mp_medication = await _context.mp_medication.FindAsync(id);
            _context.mp_medication.Remove(mp_medication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mp_medicationExists(int id)
        {
            return _context.mp_medication.Any(e => e.id == id);
        }
    }
}
