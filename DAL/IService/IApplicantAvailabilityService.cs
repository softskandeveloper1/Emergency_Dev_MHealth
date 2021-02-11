using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IApplicantAvailabilityService
    {
        void Add(mp_clinician_availability availability);
        void Update(mp_clinician_availability availability);
        IQueryable<mp_clinician_availability> GetClinicianAvailabilityByDateRange(mp_appointment appointment);
        IQueryable<mp_clinician_availability> GetOtherCliniciansAvailabilityByDateRange(mp_appointment appointment);
    }
}
