using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_medication
    {
        public int id { get; set; }
        public Guid profile_id { get; set; }
        public string medication { get; set; }
        public string dosage { get; set; }
        public string physician { get; set; }
        public TimeSpan last_dose { get; set; }
        public int taking_as_prescribed { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
