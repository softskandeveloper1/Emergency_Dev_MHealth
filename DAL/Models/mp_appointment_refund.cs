using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_appointment_refund
    {
        public long id { get; set; }
        public Guid appointment_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public int cancelled_by { get; set; }
        public int status { get; set; }
        public decimal amount { get; set; }

        public virtual mp_appointment appointment_ { get; set; }
    }
}
