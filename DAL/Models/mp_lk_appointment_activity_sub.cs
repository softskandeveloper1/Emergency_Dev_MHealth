using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_lk_appointment_activity_sub
    {
        public mp_lk_appointment_activity_sub()
        {
            mp_applicant_category = new HashSet<mp_applicant_category>();
            mp_clinician_category = new HashSet<mp_clinician_category>();
            mp_lnk_appointment_service_activity_sub = new HashSet<mp_lnk_appointment_service_activity_sub>();
            mp_profile_match = new HashSet<mp_profile_match>();
            mp_service_costing = new HashSet<mp_service_costing>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public int appointment_activity_id { get; set; }
        public int active { get; set; }

        public virtual ICollection<mp_applicant_category> mp_applicant_category { get; set; }
        public virtual ICollection<mp_appointment> mp_appointment { get; set; }
        public virtual ICollection<mp_clinician_category> mp_clinician_category { get; set; }
        public virtual ICollection<mp_lnk_appointment_service_activity_sub> mp_lnk_appointment_service_activity_sub { get; set; }
        public virtual ICollection<mp_profile_match> mp_profile_match { get; set; }
        public virtual ICollection<mp_service_costing> mp_service_costing { get; set; }
    }
}
