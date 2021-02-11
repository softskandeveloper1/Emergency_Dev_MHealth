using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_lk_specialty
    {
        public mp_lk_specialty()
        {
            mp_applicant_specialty = new HashSet<mp_applicant_specialty>();
            mp_clinician_specialty = new HashSet<mp_clinician_specialty>();
        }

        public int id { get; set; }
        public string name { get; set; }

        public virtual ICollection<mp_applicant_specialty> mp_applicant_specialty { get; set; }
        public virtual ICollection<mp_clinician_specialty> mp_clinician_specialty { get; set; }
    }
}
