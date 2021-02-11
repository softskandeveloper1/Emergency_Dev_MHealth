using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public  interface ILanguageService
    {
        void AddApplicantLanguage(List<mp_applicant_language> languages);
        void AddClinicianLanguage(List<mp_clinician_language> languages);
        void DeleteClinicianLanguage(Guid clinician_id);
        IQueryable<mp_clinician_language> GetClinicianLanguage(Guid clinician_id);
        IQueryable<mp_applicant_language> GetApplicantLanguage(Guid applicant_id);
    }
}
