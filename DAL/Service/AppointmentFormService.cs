using DAL.IService;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class AppointmentFormService: IAppointmentFormService
    {
        private readonly HContext _context = new HContext();

        public void Add(mp_appointment_form form)
        {
            _context.mp_appointment_form.Add(form);
            _context.SaveChanges();
        }

        public IQueryable<mp_appointment_form> Get()
        {
            return _context.mp_appointment_form
                .Include(e=>e.form_)
                .AsQueryable();
        }

        public IQueryable<mp_appointment_form> GetAppointmentForms(Guid appointment_id)
        {
            return _context.mp_appointment_form.Where(e=>e.appointment_id==appointment_id)
               .Include(e => e.form_)
               .AsQueryable();
        }
    }
}
