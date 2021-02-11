using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.IService
{
    public interface IAppointmentRefundService
    {
        void AddRefund(mp_appointment_refund refund);
        IQueryable<mp_appointment_refund> Get();
        void Update(mp_appointment_refund refund);
    }
}
