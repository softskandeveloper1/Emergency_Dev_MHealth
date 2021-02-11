﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_select_clinician
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public Guid clinician_id { get; set; }
        public int appointment_type_id { get; set; }
        public int appointment_activity_id { get; set; }
        public int appointment_activity_sub_id { get; set; }
        public DateTime created_at { get; set; }

        public virtual mp_lk_appointment_activity appointment_activity_ { get; set; }
        public virtual mp_lk_appointment_activity_sub appointment_activity_sub_ { get; set; }
        public virtual mp_lk_appointment_type appointment_type_ { get; set; }
        public virtual mp_clinician clinician_ { get; set; }
        public virtual mp_profile profile_ { get; set; }
    }
}
