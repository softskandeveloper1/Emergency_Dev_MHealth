using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_states
    {
        public mp_states()
        {
            mp_applicant = new HashSet<mp_applicant>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public int country_id { get; set; }

        public virtual mp_countries country_ { get; set; }
        public virtual ICollection<mp_applicant> mp_applicant { get; set; }
    }
}
