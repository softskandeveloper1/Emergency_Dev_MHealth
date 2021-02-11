using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinician_education
    {
        public string school { get; set; }
        public string address { get; set; }
        public string certification { get; set; }
        public Guid clinician_id { get; set; }
        public DateTime created_at { get; set; }
        public long id { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
    }
}
