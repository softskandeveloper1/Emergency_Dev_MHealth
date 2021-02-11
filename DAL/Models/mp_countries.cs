using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_countries
    {
        public mp_countries()
        {
            mp_applicant = new HashSet<mp_applicant>();
            mp_states = new HashSet<mp_states>();
        }

        public int id { get; set; }
        public string sortname { get; set; }
        public string name { get; set; }
        public int phonecode { get; set; }
        public int active { get; set; }

        public virtual ICollection<mp_applicant> mp_applicant { get; set; }
        public virtual ICollection<mp_states> mp_states { get; set; }
    }
}
