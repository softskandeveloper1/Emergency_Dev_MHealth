using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_ped_evaluation_history
    {
        public int id { get; set; }
        public Guid profile_id { get; set; }
        public Guid appointment_id { get; set; }
        public int ped_eval_id { get; set; }

        public virtual mp_appointment appointment_ { get; set; }
        public virtual mp_pediatric_evaluation ped_eval_ { get; set; }
        public virtual mp_profile profile_ { get; set; }
    }
}
