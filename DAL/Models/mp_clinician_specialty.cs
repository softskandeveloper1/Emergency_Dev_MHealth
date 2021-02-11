using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinician_specialty
    {
        public long id { get; set; }
        public Guid clinician_id { get; set; }
        public int specialty_id { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
        public virtual mp_lk_specialty specialty_ { get; set; }
    }
}
