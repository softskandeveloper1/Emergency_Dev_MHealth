using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_cities
    {
        public mp_cities()
        {
            mp_applicant = new HashSet<mp_applicant>();
        }

        public string id { get; set; }
        public string name { get; set; }
        public int state_id { get; set; }

        public virtual mp_states state_ { get; set; }
        public virtual ICollection<mp_applicant> mp_applicant { get; set; }
    }
}
