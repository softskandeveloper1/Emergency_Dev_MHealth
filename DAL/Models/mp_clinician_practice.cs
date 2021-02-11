using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinician_practice
    {
        public long id { get; set; }
        public Guid clinician_id { get; set; }
        public string hospital { get; set; }
        public string city { get; set; }
        public string duration { get; set; }
        public string role { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
    }
}
