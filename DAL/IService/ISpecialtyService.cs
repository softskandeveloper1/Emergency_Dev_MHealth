using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface ISpecialtyService
    {
        void AddApplicantSpecialty(List<mp_applicant_specialty> specialties);
        void AddClinicianSpecialty(List<mp_clinician_specialty> specialties);
        IQueryable<mp_applicant_specialty> GetApplicantSpecialties(Guid applicant_id);
        IQueryable<mp_clinician_specialty> GetClinicianSpecialties(Guid clinician_id);
    }
}
