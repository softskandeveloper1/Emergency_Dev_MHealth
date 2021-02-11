using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_social_relationship
    {
        public int id { get; set; }
        public Guid profile_id { get; set; }
        public int? num_marriages { get; set; }
        public string relationship_concern { get; set; }
        public int? social_activities { get; set; }
        public int? support_network { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public Guid appointment_id { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
