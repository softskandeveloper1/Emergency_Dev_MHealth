using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinician_availability
    {
        public Guid clinician_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public DateTime? created_at { get; set; }
        public long id { get; set; }
        public bool? status { get; set; }
        public string day_name { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
    }
}
