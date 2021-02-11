using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_lk_expertise
    {
        public mp_lk_expertise()
        {
            mp_applicant_expertise = new HashSet<mp_applicant_expertise>();
            mp_clinician_expertise = new HashSet<mp_clinician_expertise>();
        }

        public int id { get; set; }
        public string name { get; set; }

        public virtual ICollection<mp_applicant_expertise> mp_applicant_expertise { get; set; }
        public virtual ICollection<mp_clinician_expertise> mp_clinician_expertise { get; set; }
    }
}
