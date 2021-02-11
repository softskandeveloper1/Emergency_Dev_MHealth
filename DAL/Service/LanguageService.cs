using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly HContext _context = new HContext();

        public void AddApplicantLanguage(List<mp_applicant_language> languages)
        {
            foreach (var language in languages)
            {
                _context.mp_applicant_language.Add(language);
            }

            _context.SaveChanges();
        }

        public void AddClinicianLanguage(List<mp_clinician_language> languages)
        {
            foreach (var language in languages)
            {
                _context.mp_clinician_language.Add(language);
            }
            _context.SaveChanges();
        }

        public void DeleteClinicianLanguage(Guid clinician_id)
        {
            var langs = GetClinicianLanguage(clinician_id);
            _context.mp_clinician_language.RemoveRange(langs);
            _context.SaveChanges();
        }

        public IQueryable<mp_clinician_language> GetClinicianLanguage(Guid clinician_id)
        {
            return _context.mp_clinician_language.Where(e => e.clinician_id == clinician_id);
        }


        public IQueryable<mp_applicant_language> GetApplicantLanguage(Guid applicant_id)
        {
            return _context.mp_applicant_language.Where(e => e.applicant_id == applicant_id);
        }
    }
}
