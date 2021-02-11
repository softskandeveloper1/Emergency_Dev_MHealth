using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_lk_appointment_type
    {
        public mp_lk_appointment_type()
        {
            mp_applicant_category = new HashSet<mp_applicant_category>();
            mp_appointment = new HashSet<mp_appointment>();
            mp_clinician_category = new HashSet<mp_clinician_category>();
            mp_profile_match = new HashSet<mp_profile_match>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public int active { get; set; }

        public virtual ICollection<mp_applicant_category> mp_applicant_category { get; set; }
        public virtual ICollection<mp_appointment> mp_appointment { get; set; }
        public virtual ICollection<mp_clinician_category> mp_clinician_category { get; set; }
        public virtual ICollection<mp_profile_match> mp_profile_match { get; set; }
    }
}
