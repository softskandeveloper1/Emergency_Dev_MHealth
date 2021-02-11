using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_family_history
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public string family_description { get; set; }
        public string current_family_relationship { get; set; }
        public int family_mental_health { get; set; }
        public string mental_health_descripiton { get; set; }
        public int childhood_trauma { get; set; }
        public string trauma_description { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public Guid appointment_id { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
