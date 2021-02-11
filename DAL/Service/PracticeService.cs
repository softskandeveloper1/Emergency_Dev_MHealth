using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class PracticeService: IPracticeService
    {
        private readonly HContext _context = new HContext();

        public void AddApplicantPractice(List<mp_applicant_practice> practices)
        {
            foreach (var practice in practices)
            {
                _context.mp_applicant_practice.Add(practice);
            }

            _context.SaveChanges();
        }

        public void AddClinicianPractice(List<mp_clinician_practice> practices)
        {
            foreach (var practice in practices)
            {
                _context.mp_clinician_practice.Add(practice);
            }
            _context.SaveChanges();
        }

        public void DeleteClinicianPractice(Guid clinician_id)
        {
            var items = GetClinicianPractice(clinician_id);
            _context.mp_clinician_practice.RemoveRange(items);
            _context.SaveChanges();
        }

        public IQueryable<mp_clinician_practice> GetClinicianPractice(Guid clinician_id)
        {
            return _context.mp_clinician_practice.Where(e => e.clinician_id == clinician_id);
        }


        public IQueryable<mp_clinician_practice> GetApplicantPractice(Guid applicant_id)
        {
            return _context.mp_clinician_practice.Where(e => e.clinician_id == applicant_id);
        }
    }
}
