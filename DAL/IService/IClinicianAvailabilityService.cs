using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.IService
{
    public interface IClinicianAvailabilityService
    {
        IEnumerable<mp_clinician_availability> Get(Guid clinician_id);
        void AddOrUpdate(mp_clinician_availability availability);
        Task<int> Remove(long id);
        mp_clinician_availability GetClinicianAvailabilityByDateRange(mp_appointment appointment);
        IQueryable<mp_clinician_availability> GetOtherCliniciansAvailabilityByDateRange(mp_appointment appointment);
        List<mp_clinician> GetAvailableClinicianByAppointmentDate(List<mp_clinician> clinicians, DateTime appointmentDate);
        IQueryable<mp_clinician_availability> Get();
    }
}
