using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_appointment_log
    {
        public long id { get; set; }
        public Guid appointment_id { get; set; }
        public string role { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }

        public virtual mp_appointment appointment_ { get; set; }
    }
}
