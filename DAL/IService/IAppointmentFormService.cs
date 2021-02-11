using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IAppointmentFormService
    {
        void Add(mp_appointment_form form);
        IQueryable<mp_appointment_form> Get();
        IQueryable<mp_appointment_form> GetAppointmentForms(Guid appointment_id);
    }
}
