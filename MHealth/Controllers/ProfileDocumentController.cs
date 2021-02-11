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
    public class ClinianDocumentController : Controller
    {
        private readonly IClinicianDocumentService _profileDocumentService;

        public ClinianDocumentController(IClinicianDocumentService profileDocumentService)
        {
            _profileDocumentService = profileDocumentService;
        }

        // GET: ProfileDocument
        public IActionResult Index()
        {
            return View(_profileDocumentService.Get());
        }

        // GET: ProfileDocument/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_profile_document = _profileDocumentService.Get((int)id);
            if (mp_profile_document == null)
            {
                return NotFound();
            }

            return View(mp_profile_document);
        }

        // GET: ProfileDocument/Create
        public IActionResult Create()
        {
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name");
            return View();
        }

        // POST: ProfileDocument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("document_type,profile_id,path")] mp_clinician_document mp_profile_document)
        {
            if (ModelState.IsValid)
            {
                _profileDocumentService.Add(mp_profile_document);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_profile_document.profile_id);
            return View(mp_profile_document);
        }

        // GET: ProfileDocument/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_profile_document = _profileDocumentService.Get((int)id);
            if (mp_profile_document == null)
            {
                return NotFound();
            }
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_profile_document.profile_id);
            return View(mp_profile_document);
        }

        // POST: ProfileDocument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,document_type,profile_id,path")] mp_clinician_document mp_profile_document)
        {
            if (id != mp_profile_document.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _profileDocumentService.UpdateProfileDocument(mp_profile_document);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_profileDocumentService.profileDocumentExists((int)mp_profile_document.id))
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
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_profile_document.profile_id);
            return View(mp_profile_document);
        }

        // GET: ProfileDocument/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_profile_document = _profileDocumentService.Get((int)id);
            if (mp_profile_document == null)
            {
                return NotFound();
            }

            return View(mp_profile_document);
        }

        // POST: ProfileDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _profileDocumentService.RemoveProfileDocument(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
