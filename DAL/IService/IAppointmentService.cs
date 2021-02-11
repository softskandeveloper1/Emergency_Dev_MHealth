using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IAppointmentService
    {
        Guid Add(mp_appointment appointment);
        mp_appointment Get(Guid id);
        IQueryable<mp_appointment> GetProfileAppointments(Guid profile_id);
        void Update(mp_appointment appointment);
        IQueryable<mp_appointment> Get();
        IQueryable<mp_appointment> GetClinicianAppointmentsByDateRange(mp_appointment appointment);
        IQueryable<mp_appointment> GetOtherCliniciansAppointmentsByDateRange(mp_appointment appointment);
    }
}
