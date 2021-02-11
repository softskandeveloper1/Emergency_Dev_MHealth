using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly HContext _context = new HContext();

        public Guid Add(mp_appointment appointment)
        {
            appointment.id = Guid.NewGuid();
            appointment.created_by = "Self";
            appointment.created_at = DateTime.Now;
            _context.mp_appointment.Add(appointment);
            _context.SaveChanges();

            return appointment.id;
        }

        public mp_appointment Get(Guid id)
        {
            return _context.mp_appointment.Include(e => e.clinician_).Include(e => e.client_).FirstOrDefault(e => e.id == id);
        }

        public IQueryable<mp_appointment> Get()
        {
            return _context.mp_appointment.AsQueryable();
        }

        public IQueryable<mp_appointment> GetProfileAppointments(Guid profile_id)
        {
            return _context.mp_appointment
                .Include(e => e.clinician_)
                .Include(e => e.client_)
                .Include(e => e.appointment_typeNavigation)
                .Include(e => e.appointment_serviceNavigation)
                .Where(e => e.client_id == profile_id || e.clinician_id == profile_id).AsQueryable();
        }

        public void Update(mp_appointment appointment)
        {
            var old = _context.mp_appointment.FirstOrDefault(e => e.id == appointment.id);
            appointment.created_at = old.created_at;
            appointment.created_by = old.created_by;

            _context.Entry(old).CurrentValues.SetValues(appointment);
            _context.SaveChanges();
        }

        public IQueryable<mp_appointment> GetClinicianAppointmentsByDateRange(mp_appointment appointment)
        {
            return _context.mp_appointment.Where(e => e.start_date >= appointment.start_date && e.end_date <= appointment.end_date && e.clinician_id == appointment.clinician_id).AsQueryable();
        }

        public IQueryable<mp_appointment> GetOtherCliniciansAppointmentsByDateRange(mp_appointment appointment)
        {
            return _context.mp_appointment.Where(e => e.start_date >= appointment.start_date && e.end_date <= appointment.end_date && e.clinician_id != appointment.clinician_id).AsQueryable();
        }

    }
}
