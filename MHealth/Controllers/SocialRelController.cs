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
    public class SocialRelController : Controller
    {
        private readonly ISocialRelService _socialRelationshipService;

        public SocialRelController(ISocialRelService socialRelationshipService)
        {
            _socialRelationshipService = socialRelationshipService;
        }

        // GET: SocialRel
        public async Task<IActionResult> Index()
        {
            return View(await _socialRelationshipService.GetSocialRelationships());
        }

        // GET: SocialRel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_social_relationship = await _socialRelationshipService.GetSocialRelationship((int)id);
            if (mp_social_relationship == null)
            {
                return NotFound();
            }

            return View(mp_social_relationship);
        }

        // GET: SocialRel/Create
        public IActionResult Create()
        {
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name");
            return View();
        }

        // POST: SocialRel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("profile_id,num_marriages,relationship_concern,social_activities,support_network")] mp_social_relationship mp_social_relationship)
        {
            if (ModelState.IsValid)
            {
                await _socialRelationshipService.AddSocialRelationship(mp_social_relationship);
                return RedirectToAction(nameof(Index));
            }
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_social_relationship.profile_id);
            return View(nameof(Index));
        }

        // GET: SocialRel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_social_relationship = await _socialRelationshipService.GetSocialRelationship((int)id);
            if (mp_social_relationship == null)
            {
                return NotFound();
            }
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_social_relationship.profile_id);
            return View(mp_social_relationship);
        }

        // POST: SocialRel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("id,profile_id,num_marriages,relationship_concern,social_activities,support_network")] mp_social_relationship mp_social_relationship)
        {
            if (id != mp_social_relationship.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _socialRelationshipService.UpdateSocialRelationship(mp_social_relationship);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_socialRelationshipService.RelationshipExists(mp_social_relationship.id))
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
            //ViewData["profile_id"] = new SelectList(_context.mp_profile, "id", "first_name", mp_social_relationship.profile_id);
            return View(mp_social_relationship);
        }

        // GET: SocialRel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mp_social_relationship = await _socialRelationshipService.GetSocialRelationship((int)id);
            if (mp_social_relationship == null)
            {
                return NotFound();
            }

            return View(mp_social_relationship);
        }

        // POST: SocialRel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _socialRelationshipService.RemoveSocialRelationship(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
