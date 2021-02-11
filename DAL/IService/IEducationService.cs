using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IEducationService
    {
        void AddApplicantEducation(List<mp_applicant_education> educations);
        void AddClinicianEducation(List<mp_clinician_education> educations);
        void DeleteClinicianEducation(Guid clinician_id);
        IQueryable<mp_clinician_education> GetClinicianEducation(Guid clinician_id);
        IQueryable<mp_applicant_education> GetApplicantEducation(Guid applicant_id);
    }
}
