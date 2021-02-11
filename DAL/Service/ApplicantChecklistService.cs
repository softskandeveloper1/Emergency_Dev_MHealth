using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ApplicantChecklistService: IApplicantChecklistService
    {
        private readonly HContext _context = new HContext();

        public void AddOrUpdate(mp_applicant_checklist applicant_checklist)
        {
            var old = _context.mp_applicant_checklist.FirstOrDefault(e => e.clinician_id == applicant_checklist.clinician_id);
            if (old != null)
            {
                applicant_checklist.created_at = old.created_at;
                applicant_checklist.updated_at = DateTime.Now;
                applicant_checklist.created_by = old.created_by;

                _context.Entry(old).CurrentValues.SetValues(applicant_checklist);
            }
            else
            {
                applicant_checklist.created_at = DateTime.Now;

                _context.mp_applicant_checklist.Add(applicant_checklist);
            }
            _context.SaveChanges();
        }

        public IQueryable<mp_applicant_checklist> Get()
        {
            return _context.mp_applicant_checklist.AsQueryable();
        }
    }
}
