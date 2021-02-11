using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class AppointmentLogService: IAppointmentLogService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_appointment_log log)
        {
            log.created_at = DateTime.Now;
            _context.mp_appointment_log.Add(log);
            _context.SaveChanges();
        }

        public IQueryable<mp_appointment_log> Get()
        {
            return _context.mp_appointment_log.AsQueryable();
        }
    }
}
