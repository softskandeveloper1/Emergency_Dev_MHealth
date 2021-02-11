using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_link_applicant_clinician
    {
        public long id { get; set; }
        public Guid clinician_id { get; set; }
        public Guid applicant_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }

        public virtual mp_applicant applicant_ { get; set; }
        public virtual mp_clinician clinician_ { get; set; }
    }
}
