using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.IService;

namespace MHealth.Controllers
{
    public class SubstanceUseController : Controller
    {
        private ISubstanceUseService _substanceUseService;

        public SubstanceUseController(ISubstanceUseService substanceUseService)
        {
            _substanceUseService = substanceUseService;
        }

        // GET: SubstanceUse
        public async Task<IActionResult> Index()
        {
            return View(await _substanceUseService.GetSubstanceUses());
        }

        // GET: SubstanceUse/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_substance_use = await _substanceUseService.GetSubstanceUse((int)id);
            if (mp_substance_use == null)
            {
                return NotFound();
            }

            return View(mp_substance_use);
        }

        // GET: SubstanceUse/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubstanceUse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("profile_id,tobacco,tobacco_amount,tobacco_years,alcohol,alcohol_type,alcohol_freq,last_drink,drink_amount,withdrawal_symptons,withdrawal_symptons_description,blackouts,blackouts_freq,illicit_drugs,drug_type,drug_freq,date_of_last_use,substance_abuse_treatment,agency,treatment_type,treatment_date,support_programs,support_programs_description,triggers,triggers_description,use_legal_issues,legal_issues_description")] mp_substance_use mp_substance_use)
        {
            if (ModelState.IsValid)
            {
                await _substanceUseService.AddSubstanceUse(mp_substance_use);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index));
        }

        // GET: SubstanceUse/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_substance_use = await _substanceUseService.GetSubstanceUse((int)id);
            if (mp_substance_use == null)
            {
                return NotFound();
            }
            return View(mp_substance_use);
        }

        // POST: SubstanceUse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("id,profile_id,tobacco,tobacco_amount,tobacco_years,alcohol,alcohol_type,alcohol_freq,last_drink,drink_amount,withdrawal_symptons,withdrawal_symptons_description,blackouts,blackouts_freq,illicit_drugs,drug_type,drug_freq,date_of_last_use,substance_abuse_treatment,agency,treatment_type,treatment_date,support_programs,support_programs_description,triggers,triggers_description,use_legal_issues,legal_issues_description")] mp_substance_use mp_substance_use)
        {
            if (id != mp_substance_use.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _substanceUseService.UpdateSubstanceUse(mp_substance_use);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_substanceUseService.substanceUseExists(mp_substance_use.id))
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
            return View(mp_substance_use);
        }

        // GET: SubstanceUse/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_substance_use = await _substanceUseService.GetSubstanceUse((int)id);
            if (mp_substance_use == null)
            {
                return NotFound();
            }

            return View(mp_substance_use);
        }

        // POST: SubstanceUse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            await _substanceUseService.RemoveSubstanceUse((int)id);
            return RedirectToAction(nameof(Index));
        }
    }
}
