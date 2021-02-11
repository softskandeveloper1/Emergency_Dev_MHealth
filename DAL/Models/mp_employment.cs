using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_employment
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public string employer { get; set; }
        public string employment_length { get; set; }
        public int like_job { get; set; }
        public string reason { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
