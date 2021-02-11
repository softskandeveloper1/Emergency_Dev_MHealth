using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IAppointmentLogService
    {
        void Add(mp_appointment_log log);
        IQueryable<mp_appointment_log> Get();
    }
}
