using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IPracticeService
    {
        void AddApplicantPractice(List<mp_applicant_practice> practices);
        void AddClinicianPractice(List<mp_clinician_practice> practices);
        void DeleteClinicianPractice(Guid clinician_id);
        IQueryable<mp_clinician_practice> GetClinicianPractice(Guid clinician_id);
        IQueryable<mp_clinician_practice> GetApplicantPractice(Guid applicant_id);
    }
}
