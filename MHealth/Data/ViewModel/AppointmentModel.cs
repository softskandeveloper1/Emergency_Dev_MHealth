using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class AppointmentModel
    {
        public AppointmentModel() { }

        public AppointmentModel(mp_appointment appointment) 
        {
            id = appointment.id;
            title = appointment.appointment_typeNavigation.name;
            doctor = new DoctorModel(appointment.clinician_);
            appointmentDate = appointment.start_date;
            member = new MemberModel(appointment.client_);
            service = appointment.appointment_serviceNavigation.name;
            duration = appointment.appointment_serviceNavigation.time_minutes;
        }

        public Guid id { set; get; }
        public string title { set; get; }
        public DoctorModel doctor { set; get; }
        public DateTime appointmentDate { set; get; }
        public MemberModel member { set; get; }
        public string service { set; get; }
        public int duration { set; get; }
       
    }
}
