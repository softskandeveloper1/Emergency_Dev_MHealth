using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ProviderCategoryService: IProviderCategoryService
    {
        private readonly HContext _context = new HContext();

        public void AddApplicantCategory(mp_applicant_category applicant_Category)
        {
            _context.mp_applicant_category.Add(applicant_Category);
            _context.SaveChanges();
        }

        public void AddClinicianCategory(mp_clinician_category clinician_Category)
        {
            _context.mp_clinician_category.Add(clinician_Category);
            _context.SaveChanges();
        }

        public IQueryable<mp_clinician_category> GetClinicianCategory()
        {
            return _context.mp_clinician_category.AsQueryable();
        }

        public IQueryable<mp_applicant_category> GetApplicantCategory()
        {
            return _context.mp_applicant_category.AsQueryable();
        }
    }
}
