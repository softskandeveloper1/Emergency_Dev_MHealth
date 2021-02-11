using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IProviderCategoryService
    {
        void AddApplicantCategory(mp_applicant_category applicant_Category);
        void AddClinicianCategory(mp_clinician_category clinician_Category);
        IQueryable<mp_clinician_category> GetClinicianCategory();
        IQueryable<mp_applicant_category> GetApplicantCategory();
    }
}
