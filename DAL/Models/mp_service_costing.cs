using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_service_costing
    {
        public long id { get; set; }
        public Guid clinician_id { get; set; }
        public int appointment_activity_sub_id { get; set; }
        public int appointment_service_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
        public decimal cost { get; set; }

        public virtual mp_lk_appointment_activity_sub appointment_activity_sub_ { get; set; }
        public virtual mp_lk_appointment_service appointment_service_ { get; set; }
        public virtual mp_clinician clinician_ { get; set; }
    }
}
