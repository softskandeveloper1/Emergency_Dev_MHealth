using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_prescription
    {
        public mp_prescription()
        {
            mp_prescription_drug = new HashSet<mp_prescription_drug>();
        }

        public long id { get; set; }
        public Guid profile_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public string comment { get; set; }
        public Guid clinician_id { get; set; }
        public int? pharmacy_id { get; set; }
        public int? viewed { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
        public virtual mp_profile profile_ { get; set; }
        public virtual ICollection<mp_prescription_drug> mp_prescription_drug { get; set; }
    }
}
