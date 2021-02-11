using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Entities.ViewModel
{
    public class Appointment
    {
        public Appointment()
        {

        }

        public Appointment(mp_appointment appointment)
        {
            id = appointment.id;
            client_id = appointment.client_id;
            clinician_id = appointment.clinician_id;
            start_date = appointment.start_date;
            appointment_type = appointment.appointment_type;
            appointment_service = appointment.appointment_service;
            clinician_id = appointment.clinician_id;
            client_id = appointment.client_id;
            appointment_serviceNavigation = appointment.appointment_serviceNavigation;
            appointment_typeNavigation = appointment.appointment_typeNavigation;
            client = appointment.client_;
            clinician = appointment.clinician_;
        }

        public Guid id { get; set; }
        public Guid client_id { get; set; }
        public Guid clinician_id { get; set; }
        public int status { get; set; }
        public int appointment_type { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public int appointment_service { get; set; }


        public mp_lk_appointment_service appointment_serviceNavigation { get; set; }
        public mp_lk_appointment_type appointment_typeNavigation { get; set; }
        public mp_profile client { get; set; }
        public mp_clinician clinician { get; set; }


    }
}
