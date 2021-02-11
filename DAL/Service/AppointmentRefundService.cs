using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class AppointmentRefundService: IAppointmentRefundService
    {
        private readonly HContext _context = new HContext();

        public void AddRefund(mp_appointment_refund refund)
        {
            refund.created_at = DateTime.Now;
            _context.mp_appointment_refund.Add(refund);
            _context.SaveChanges();
        }

        public IQueryable<mp_appointment_refund> Get()
        {
            return _context.mp_appointment_refund.AsQueryable();
        }

        public void Update(mp_appointment_refund refund)
        {
            var old = _context.mp_appointment_refund.FirstOrDefault(e => e.id == refund.id);
            refund.created_at = DateTime.Now;
            refund.created_by = refund.created_by;

            _context.Entry(old).CurrentValues.SetValues(refund);
            _context.SaveChanges();
        }
    }
}
