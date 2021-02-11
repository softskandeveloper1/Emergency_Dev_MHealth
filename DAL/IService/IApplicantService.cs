using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IApplicantService
    {
        Guid Add(mp_applicant profile);
        void AddApplicantToProfile(mp_link_applicant_clinician link);
        IQueryable<mp_applicant> Get();
        mp_applicant Get(Guid id);
        void Update(mp_applicant profile);
       
    }
}
