using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IClinicService
    {
        Guid AddClinic(mp_clinic clinic);
        IQueryable<mp_clinic> Get();
        mp_clinic Get(Guid id);
        void AddClinicianToClinic(mp_clinic_clinician clinic_Clinician);
        void Update(mp_clinic clinic);
        IQueryable<mp_clinic_clinician> GetClinicClinicians(Guid clinic_id);
    }
}
