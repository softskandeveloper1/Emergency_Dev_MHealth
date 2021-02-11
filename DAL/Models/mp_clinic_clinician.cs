using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinic_clinician
    {
        public long id { get; set; }
        public Guid clinic_id { get; set; }
        public Guid clinician_id { get; set; }
        public DateTime created_at { get; set; }
        public int is_admin { get; set; }
        public int status { get; set; }

        public virtual mp_clinic clinic_ { get; set; }
        public virtual mp_clinician clinician_ { get; set; }
    }
}
