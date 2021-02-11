using DAL.IService;
using DAL.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.ViewModels;

namespace DAL.Service
{
    public class ClinicianService : IClinicianService
    {
        private readonly HContext _context = new HContext();

        public Guid Add(mp_clinician profile)
        {
            profile.id = Guid.NewGuid();
            profile.created_at = DateTime.Now;
            _context.mp_clinician.Add(profile);
            _context.SaveChanges();

            return profile.id;
        }

        public IQueryable<mp_clinician> GetAll()
        {
            return _context.mp_clinician.AsQueryable();
        }

        public IQueryable<mp_clinician> Get()
        {
            return _context.mp_clinician.Where(e => e.status == 5).Include(e => e.mp_clinician_availability).AsQueryable();
        }

        public IQueryable<mp_clinician> GetClinicians()
        {
            return _context.mp_clinician.Where(e => e.status == 5).AsQueryable();
        }

        public IQueryable<mp_clinician> Get(SearchDoctorModel model)
        {
            IQueryable<mp_clinician> clinicians = _context.mp_clinician.Include(e => e.mp_clinician_category).Include(e => e.mp_clinician_specialty).Include(e => e.mp_clinician_other_activities).AsQueryable();
            return clinicians.Where(e => e.mp_clinician_category.Any(e => e.id == model.appointmentCategory) && e.mp_clinician_other_activities.Any(e => e.id == model.appointmentActivity));
        }

        public mp_clinician Get(Guid id)
        {
            return _context.mp_clinician.FirstOrDefault(e => e.id == id);
        }

        public void Update(mp_clinician profile)
        {
            var old = _context.mp_clinician.FirstOrDefault(e => e.id == profile.id);
            profile.created_at = old.created_at;

            _context.Entry(old).CurrentValues.SetValues(profile);
            _context.SaveChanges();
        }

        public int Remove(Guid id)
        {
            var existing = _context.mp_clinician.Find(id);
            _context.mp_clinician.Remove(existing);
            return _context.SaveChanges();
        }

        public bool ProfileExists(Guid id)
        {
            return _context.mp_clinician.Any(e => e.id == id);
        }

        public mp_clinician GetByUserId(string user_id)
        {
            return _context.mp_clinician.FirstOrDefault(e => e.user_id == user_id);
        }

 
    }
}
