using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class EducationService : IEducationService
    {
        private readonly HContext _context = new HContext();

        public void AddApplicantEducation(List<mp_applicant_education> educations)
        {
            foreach (var education in educations)
            {
                education.created_at = DateTime.Now;
                _context.mp_applicant_education.Add(education);
            }

            _context.SaveChanges();
        }

        public void AddClinicianEducation(List<mp_clinician_education> educations)
        {
            foreach (var education in educations)
            {
                education.created_at = DateTime.Now;
                _context.mp_clinician_education.Add(education);
            }
            _context.SaveChanges();
        }

        public void DeleteClinicianEducation(Guid clinician_id)
        {
            var educations = GetClinicianEducation(clinician_id);
            _context.mp_clinician_education.RemoveRange(educations);
            _context.SaveChanges();
        }

        public IQueryable<mp_clinician_education> GetClinicianEducation(Guid clinician_id)
        {
            return _context.mp_clinician_education.Where(e => e.clinician_id == clinician_id);
        }


        public IQueryable<mp_applicant_education> GetApplicantEducation(Guid applicant_id)
        {
            return _context.mp_applicant_education.Where(e => e.applicant_id == applicant_id);
        }


    }
}
