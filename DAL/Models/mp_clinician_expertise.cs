using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinician_expertise
    {
        public Guid clinician_id { get; set; }
        public int expertise_id { get; set; }
        public long id { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
        public virtual mp_lk_expertise expertise_ { get; set; }
    }
}
