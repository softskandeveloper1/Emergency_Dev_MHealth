using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_appointment_form
    {
        public long id { get; set; }
        public int form_id { get; set; }
        public Guid appointment_id { get; set; }

        public virtual mp_appointment appointment_ { get; set; }
        public virtual mp_form form_ { get; set; }
    }
}
