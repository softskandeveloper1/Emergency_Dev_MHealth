using DAL.Models;
using DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IClinicianService
    {
        Guid Add(mp_clinician profile);
        IQueryable<mp_clinician> GetAll();
        IQueryable<mp_clinician> Get();
        IQueryable<mp_clinician> GetClinicians();
        IQueryable<mp_clinician> Get(SearchDoctorModel model);
        mp_clinician Get(Guid id);
        void Update(mp_clinician profile);
        int Remove(Guid id);
        bool ProfileExists(Guid id);
        mp_clinician GetByUserId(string id);
       
    }
}
