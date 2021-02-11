using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class OtherActivitiesService : IOtherActivitiesService
    {
        private readonly HContext _context = new HContext();

        public void AddApplicantActivities(List<mp_applicant_other_activities> other_Activities)
        {
            foreach (var activity in other_Activities)
            {
                _context.mp_applicant_other_activities.Add(activity);
            }

            _context.SaveChanges();
        }

        public void AddClinicianActivities(List<mp_clinician_other_activities> other_Activities)
        {
            foreach (var activity in other_Activities)
            {
                _context.mp_clinician_other_activities.Add(activity);
            }
            _context.SaveChanges();
        }

        public void DeleteClinicianActivities(Guid clinician_id)
        {
            var items = GetClinicianActivities(clinician_id);
            _context.mp_clinician_other_activities.RemoveRange(items);
            _context.SaveChanges();
        }

        public IQueryable<mp_clinician_other_activities> GetClinicianActivities(Guid clinician_id)
        {
            return _context.mp_clinician_other_activities.Where(e => e.clinician_id == clinician_id);
        }


        public IQueryable<mp_applicant_other_activities> GetApplicantActivities(Guid applicant_id)
        {
            return _context.mp_applicant_other_activities.Where(e => e.applicant_id == applicant_id);
        }
    }
}
