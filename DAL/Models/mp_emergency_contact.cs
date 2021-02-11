using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_emergency_contact
    {
        public int id { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public Guid profile_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
