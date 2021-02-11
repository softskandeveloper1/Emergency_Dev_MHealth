using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_population_group
    {
        public mp_population_group()
        {
            mp_applicant_population = new HashSet<mp_applicant_population>();
            mp_clinician_population = new HashSet<mp_clinician_population>();
        }

        public int id { get; set; }
        public string name { get; set; }

        public virtual ICollection<mp_applicant_population> mp_applicant_population { get; set; }
        public virtual ICollection<mp_clinician_population> mp_clinician_population { get; set; }
    }
}
