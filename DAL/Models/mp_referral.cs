using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_referral
    {
        public long id { get; set; }
        public Guid clinician_id { get; set; }
        public Guid profile_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public Guid profile_match_id { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
        public virtual mp_profile profile_ { get; set; }
        public virtual mp_profile_match profile_match_ { get; set; }
    }
}
