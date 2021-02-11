using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_lk_appointment_service
    {
        public mp_lk_appointment_service()
        {
            mp_appointment = new HashSet<mp_appointment>();
            mp_lnk_appointment_service_activity_sub = new HashSet<mp_lnk_appointment_service_activity_sub>();
            mp_service_costing = new HashSet<mp_service_costing>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public int active { get; set; }
        public int time_minutes { get; set; }

        public virtual ICollection<mp_appointment> mp_appointment { get; set; }
        public virtual ICollection<mp_lnk_appointment_service_activity_sub> mp_lnk_appointment_service_activity_sub { get; set; }
        public virtual ICollection<mp_service_costing> mp_service_costing { get; set; }
    }
}
