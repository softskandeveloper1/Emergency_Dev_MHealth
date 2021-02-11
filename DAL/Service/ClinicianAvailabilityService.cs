using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Service
{
    public class ClinicianAvailabilityService : IClinicianAvailabilityService
    {
        private readonly HContext _context = new HContext();

        public IEnumerable<mp_clinician_availability> Get(Guid clinician_id)
        {
            return _context.mp_clinician_availability.AsEnumerable().Where(e => e.clinician_id == clinician_id).OrderBy(e => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), e.day_name));
        }

        public IQueryable<mp_clinician_availability> Get()
        {
            return _context.mp_clinician_availability.AsQueryable();
        }

        public void AddOrUpdate(mp_clinician_availability availability)
        {
            if (_context.mp_clinician_availability.Any(e => e.clinician_id == availability.clinician_id && e.day_name == availability.day_name))
            {
                // run update
                var old = _context.mp_clinician_availability.FirstOrDefault(e => e.clinician_id == availability.clinician_id && e.day_name == availability.day_name);
                availability.id = old.id;
                _context.Entry(old).CurrentValues.SetValues(availability);
            }
            else
            {
                _context.mp_clinician_availability.Add(availability);
            }

            _context.SaveChanges();
        }

        public async Task<int> Remove(long id)
        {
            //TODO add delete flags instead of removing the object entirely
            var mp_clinician_availability = await _context.mp_clinician_availability.FindAsync(id);
            _context.mp_clinician_availability.Remove(mp_clinician_availability);
            return await _context.SaveChangesAsync();
        }

        public mp_clinician_availability GetClinicianAvailabilityByDateRange(mp_appointment appointment)
        {
            return _context.mp_clinician_availability.AsEnumerable().Where(e => e.status.Value && e.day_name == appointment.start_date.DayOfWeek.ToString() &&  e.end_time.TimeOfDay > appointment.start_date.TimeOfDay && appointment.end_date.TimeOfDay < e.end_time.TimeOfDay && e.clinician_id == appointment.clinician_id).FirstOrDefault();
        }

        public IQueryable<mp_clinician_availability> GetOtherCliniciansAvailabilityByDateRange(mp_appointment appointment)
        {
            return _context.mp_clinician_availability.Where(e => e.status.Value && e.day_name == appointment.start_date.DayOfWeek.ToString() && e.start_time.TimeOfDay >= appointment.start_date.TimeOfDay && e.end_time.TimeOfDay <= appointment.end_date.TimeOfDay && e.clinician_id != appointment.clinician_id).AsQueryable();
        }

        public List<mp_clinician> GetAvailableClinicianByAppointmentDate(List<mp_clinician> clinicians, DateTime appointmentDate)
        {
            List<mp_clinician> available_clinicians = new List<mp_clinician>();
            foreach (mp_clinician clinician in clinicians)
            {
                if (clinician.mp_clinician_availability.Where(e => e.status.Value && e.day_name == appointmentDate.DayOfWeek.ToString() && e.end_time.TimeOfDay > appointmentDate.TimeOfDay) != null)
                {
                    //something is actually wrong here
                    if (_context.mp_appointment.Where(e => e.start_date == appointmentDate && e.clinician_id == clinician.id) == null)
                    {
                        available_clinicians.Add(clinician);
                    }
                }
            }

            return available_clinicians;
        }

    }
}
