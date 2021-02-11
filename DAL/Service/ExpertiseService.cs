using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ExpertiseService : IExpertiseService
    {
        private readonly HContext _context = new HContext();

        public void AddApplicantExpertise(List<mp_applicant_expertise> expertises)
        {
            foreach (var exp in expertises)
            {
                // _context.mp_applicant_expertise.Add(exp);
            }

            _context.SaveChanges();
        }


        public IQueryable<mp_applicant_expertise> GetApplicantExpertise(Guid applicant_id)
        {
            return _context.mp_applicant_expertise.Where(e => e.applicant_id == applicant_id)
                .Include(e => e.expertise_).AsQueryable();
        }

        public IQueryable<mp_clinician_expertise> GetProfileExpertise(Guid profile_id)
        {
            return _context.mp_clinician_expertise.Where(e => e.clinician_id == profile_id)
                .Include(e => e.expertise_).AsQueryable();
        }

        public void AddProfileExpertise(List<mp_clinician_expertise> expertises)
        {
            foreach (var exp in expertises)
            {
                _context.mp_clinician_expertise.Add(exp);
            }

            _context.SaveChanges();
        }

    }
}
