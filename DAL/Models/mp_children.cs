using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_children
    {
        public long id { get; set; }
        public string name { get; set; }
        public int? gender { get; set; }
        public Guid profile_id { get; set; }
        public string created_by { get; set; }
        public DateTime dob { get; set; }
        public DateTime created_at { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
