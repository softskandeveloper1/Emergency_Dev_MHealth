using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IOtherActivitiesService
    {
        void AddApplicantActivities(List<mp_applicant_other_activities> other_Activities);
        void AddClinicianActivities(List<mp_clinician_other_activities> other_Activities);
        void DeleteClinicianActivities(Guid clinician_id);
        IQueryable<mp_clinician_other_activities> GetClinicianActivities(Guid clinician_id);
        IQueryable<mp_applicant_other_activities> GetApplicantActivities(Guid applicant_id);
    }
}
