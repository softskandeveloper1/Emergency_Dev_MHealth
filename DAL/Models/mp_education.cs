using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_education
    {
        public int id { get; set; }
        public string school { get; set; }
        public string address { get; set; }
        public int certification { get; set; }
        public Guid clinician_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
    }
}
