using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_lookup
    {
        public mp_lookup()
        {
            mp_applicant_language = new HashSet<mp_applicant_language>();
            mp_applicantmarital_statusNavigation = new HashSet<mp_applicant>();
            mp_applicantstatusNavigation = new HashSet<mp_applicant>();
            mp_clinician_language = new HashSet<mp_clinician_language>();
        }

        public int id { get; set; }
        public string category { get; set; }
        public string value { get; set; }
        public int deleted { get; set; }

        public virtual ICollection<mp_applicant_language> mp_applicant_language { get; set; }
        public virtual ICollection<mp_applicant> mp_applicantmarital_statusNavigation { get; set; }
        public virtual ICollection<mp_applicant> mp_applicantstatusNavigation { get; set; }
        public virtual ICollection<mp_clinician_language> mp_clinician_language { get; set; }
    }
}
