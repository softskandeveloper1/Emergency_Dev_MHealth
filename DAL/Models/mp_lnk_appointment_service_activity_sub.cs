using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_lnk_appointment_service_activity_sub
    {
        public int activity_sub_id { get; set; }
        public int appointment_service_id { get; set; }

        public virtual mp_lk_appointment_activity_sub activity_sub_ { get; set; }
        public virtual mp_lk_appointment_service appointment_service_ { get; set; }
    }
}
