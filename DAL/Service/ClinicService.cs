using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ClinicService : IClinicService
    {
        private readonly HContext _context = new HContext();

        public Guid AddClinic(mp_clinic clinic)
        {
            clinic.id = Guid.NewGuid();
            clinic.created_at = DateTime.Now;
            _context.mp_clinic.Add(clinic);
            _context.SaveChanges();

            return clinic.id;
        }

        public IQueryable<mp_clinic> Get()
        {
            return _context.mp_clinic.AsQueryable();
        }

        public mp_clinic Get(Guid id)
        {
            return _context.mp_clinic.FirstOrDefault(e => e.id == id);
        }

        public void AddClinicianToClinic(mp_clinic_clinician clinic_Clinician)
        {
            clinic_Clinician.created_at = DateTime.Now;
            _context.mp_clinic_clinician.Add(clinic_Clinician);
            _context.SaveChanges();
        }

        public IQueryable<mp_clinic_clinician> GetClinicClinicians(Guid clinic_id)
        {
            return _context.mp_clinic_clinician.Where(e => e.clinic_id == clinic_id);
        }

        public void Update(mp_clinic clinic)
        {
            var old = _context.mp_clinic.FirstOrDefault(e => e.id == clinic.id);
            clinic.created_at = old.created_at;
            _context.Entry(old).CurrentValues.SetValues(clinic);
            _context.SaveChanges();
        }
    }
}
