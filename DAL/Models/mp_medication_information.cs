using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_medication_information
    {
        public Guid appointment_id { get; set; }
        public string dosage { get; set; }
        public string frequency { get; set; }
        public long id { get; set; }
        public string medication { get; set; }
        public Guid profile_id { get; set; }
        public string side_effects { get; set; }

        public virtual mp_appointment appointment_ { get; set; }
        public virtual mp_profile profile_ { get; set; }
    }
}
