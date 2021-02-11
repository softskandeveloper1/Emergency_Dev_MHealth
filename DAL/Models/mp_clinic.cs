using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinic
    {
        public mp_clinic()
        {
            mp_clinic_clinician = new HashSet<mp_clinic_clinician>();
        }

        public Guid id { get; set; }
        public string name { get; set; }
        public string address_c { get; set; }
        public string first_name_c { get; set; }
        public string last_name_c { get; set; }
        public string landmark { get; set; }
        public string hours_of_operation { get; set; }
        public int? number_of_locations { get; set; }
        public int diagnostics_center { get; set; }
        public int clinic_type { get; set; }
        public DateTime created_at { get; set; }
        public int status { get; set; }
        public int? number_of_beds { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
        public string phone_c { get; set; }
        public string email_c { get; set; }

        public virtual ICollection<mp_clinic_clinician> mp_clinic_clinician { get; set; }
    }
}
