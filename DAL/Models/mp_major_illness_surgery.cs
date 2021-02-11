using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_major_illness_surgery
    {
        public int id { get; set; }
        public Guid profile_id { get; set; }
        public string diagnosis { get; set; }
        public string reason { get; set; }
        public int? year { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
