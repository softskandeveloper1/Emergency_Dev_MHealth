using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_surgical_history
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public int surgery_type { get; set; }
        public DateTime date { get; set; }
        public string comments { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
