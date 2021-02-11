using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_profile_match
    {
        public mp_profile_match()
        {
            mp_referral = new HashSet<mp_referral>();
        }

        public Guid profile_id { get; set; }
        public Guid clinician_id { get; set; }
        public int appointment_type_id { get; set; }
        public int appointment_activity_id { get; set; }
        public int appointment_activity_sub_id { get; set; }
        public DateTime created_at { get; set; }
        public Guid id { get; set; }

        public virtual mp_lk_appointment_activity appointment_activity_ { get; set; }
        public virtual mp_lk_appointment_activity_sub appointment_activity_sub_ { get; set; }
        public virtual mp_lk_appointment_type appointment_type_ { get; set; }
        public virtual mp_clinician clinician_ { get; set; }
        public virtual mp_profile profile_ { get; set; }
        public virtual ICollection<mp_referral> mp_referral { get; set; }
    }
}
