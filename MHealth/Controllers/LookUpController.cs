using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using DAL.IService;

namespace MHealth.Controllers
{
    [Authorize]
    public class LookUpController : Controller
    {
        private readonly ILookUpService _lookUpService;

        public LookUpController(ILookUpService lookUpService)
        {
            _lookUpService = lookUpService;
        }

        // GET: LookUp
        public async Task<IActionResult> Index()
        {
            return View(await _lookUpService.GetLookups());
        }

        // GET: LookUp/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_lookup = await _lookUpService.GetLookUpById((int)id);
            if (mp_lookup == null)
            {
                return NotFound();
            }

            return View(mp_lookup);
        }

        // GET: LookUp/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LookUp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("category,value")] mp_lookup mp_lookup)
        {
            if (ModelState.IsValid)
            {
                mp_lookup.deleted = 0;
                await _lookUpService.AddLookUp(mp_lookup);
                return RedirectToAction(nameof(Index));
            }
            return View(mp_lookup);
        }

        // GET: LookUp/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_lookup = await _lookUpService.GetLookUpById((int)id);
            if (mp_lookup == null)
            {
                return NotFound();
            }
            return View(mp_lookup);
        }

        // POST: LookUp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,category,value")] mp_lookup mp_lookup)
        {
            if (id != mp_lookup.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _lookUpService.UpdateLookUp(mp_lookup);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_lookUpService.LookUpExists(mp_lookup.id))
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
            return View(mp_lookup);
        }

        // GET: LookUp/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_lookup = await _lookUpService.GetLookUpById((int)id);
            if (mp_lookup == null)
            {
                return NotFound();
            }

            return View(mp_lookup);
        }

        // POST: LookUp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _lookUpService.RemoveLookUp(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
