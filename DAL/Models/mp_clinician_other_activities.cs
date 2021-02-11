using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinician_other_activities
    {
        public long id { get; set; }
        public Guid clinician_id { get; set; }
        public string activity { get; set; }
        public string activity_type { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
    }
}
