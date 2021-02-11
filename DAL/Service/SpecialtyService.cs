using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly HContext _context = new HContext();

        public void AddApplicantSpecialty(List<mp_applicant_specialty> specialties)
        {
            foreach(var specialty in specialties)
            {
                _context.mp_applicant_specialty.Add(specialty);
            }
            _context.SaveChanges();
        }

        public void AddClinicianSpecialty(List<mp_clinician_specialty> specialties)
        {
            foreach (var specialty in specialties)
            {
                _context.mp_clinician_specialty.Add(specialty);
            }
            _context.SaveChanges();
        }


        public IQueryable<mp_applicant_specialty> GetApplicantSpecialties(Guid applicant_id)
        {
            return _context.mp_applicant_specialty.Where(e=>e.applicant_id==applicant_id).Include(e=>e.specialty_).AsQueryable();
        }

        public IQueryable<mp_clinician_specialty> GetClinicianSpecialties(Guid clinician_id)
        {
            return _context.mp_clinician_specialty.Where(e => e.clinician_id == clinician_id).Include(e => e.specialty_).AsQueryable();
        }
    }
}
