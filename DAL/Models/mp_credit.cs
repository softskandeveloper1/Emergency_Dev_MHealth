using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_credit
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public decimal amount { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public int mode_of_payment { get; set; }
        public Guid appointment_id { get; set; }
        public string transaction_reference { get; set; }

        public virtual mp_appointment appointment_ { get; set; }
    }
}
