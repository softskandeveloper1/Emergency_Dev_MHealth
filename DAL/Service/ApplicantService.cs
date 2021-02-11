using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Service
{
    public class ApplicantService : IApplicantService
    {
        private readonly HContext _context = new HContext();

        public Guid Add(mp_applicant profile)
        {
            profile.id = Guid.NewGuid();
            profile.created_at = DateTime.Now;
            _context.mp_applicant.Add(profile);
            _context.SaveChanges();

            return profile.id;
        }

        public IQueryable<mp_applicant> Get()
        {
            return _context.mp_applicant.Include(m => m.countryNavigation)
            .Include(m => m.statusNavigation)
            .Include(e => e.mp_applicant_population)
            .Include(e => e.mp_applicant_specialty)
            .AsQueryable();
        }

        public void AddApplicantToProfile(mp_link_applicant_clinician link)
        {
            link.created_at = DateTime.Now;
            _context.mp_link_applicant_clinician.Add(link);
            _context.SaveChanges();
        }

        public mp_applicant Get(Guid id)
        {
            return _context.mp_applicant.Include(m => m.countryNavigation)
            .Include(m => m.statusNavigation)
            .Include(e => e.mp_applicant_expertise)
            .Include(e => e.mp_applicant_document)
            .Include(e=>e.mp_applicant_population)
            .Include(e=>e.mp_applicant_specialty)
            .Include(e=>e.mp_applicant_education)
            .FirstOrDefault(e => e.id == id);
        }

        public void Update(mp_applicant profile)
        {
            var old = _context.mp_applicant.FirstOrDefault(e => e.id == profile.id);
            profile.created_at = old.created_at;

            _context.Entry(old).CurrentValues.SetValues(profile);
            _context.SaveChanges();
        }



    }
}
