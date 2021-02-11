using DAL.Models;
using DAL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.BL
{
    public static class AppointmentBL
    {
        public static bool IsAppointmentClashing(Guid clinician_id,mp_appointment appointment)
        {
            DateTime new_apt_start = appointment.start_date, new_apt_end = appointment.end_date;
            var appointments = new AppointmentService().Get().Where(e => e.clinician_id == clinician_id && e.start_date.Date == appointment.start_date.Date);
            var result = true;
            foreach(var appt in appointments)
            {
                DateTime old_apt_start=appt.start_date, old_apt_end=appt.end_date;
                if ((old_apt_start < new_apt_start && old_apt_end < new_apt_start) || (old_apt_start > new_apt_start && old_apt_end > new_apt_end))
                {
                    result = true;
                }
                else
                {
                    return false;
                }
            }
           

            return result;
        }

        public static bool IsClinicianAvailable(DateTime clinician_start, DateTime clinician_end, DateTime new_apt_start, DateTime new_apt_end)
        {
            var result = false;
            if (clinician_start.TimeOfDay <= new_apt_start.TimeOfDay && clinician_end.TimeOfDay >= new_apt_end.TimeOfDay)
            {
                result = true;
            }
            return result;
        }
    }
}
