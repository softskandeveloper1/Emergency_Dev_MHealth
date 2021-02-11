using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IPopulationService
    {
        void AddApplicantPopulationGroup(List<mp_applicant_population> populations);
        IQueryable<mp_applicant_population> GetApplicantPopulations(Guid applicant_id);
        IQueryable<mp_clinician_population> GetClinicianPopulations(Guid clinician_id);
        void AddClinicianPopulationGroup(List<mp_clinician_population> populations);
    }
}
