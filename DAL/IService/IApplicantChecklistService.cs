using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IApplicantChecklistService
    {
        void AddOrUpdate(mp_applicant_checklist applicant_checklist);
        IQueryable<mp_applicant_checklist> Get();
    }
}
