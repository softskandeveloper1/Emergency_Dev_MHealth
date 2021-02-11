using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IExpertiseService
    {
        void AddApplicantExpertise(List<mp_applicant_expertise> expertises);
        IQueryable<mp_applicant_expertise> GetApplicantExpertise(Guid applicant_id);
        IQueryable<mp_clinician_expertise> GetProfileExpertise(Guid profile_id);
        void AddProfileExpertise(List<mp_clinician_expertise> expertises);
    }
}
