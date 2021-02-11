using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_summary_treatment_history
    {
        public Guid appointment_id { get; set; }
        public DateTime? date_of_service { get; set; }
        public string treatment_type { get; set; }
        public long id { get; set; }
        public string treatment_provider { get; set; }
        public Guid profile_id { get; set; }
        public string comments { get; set; }
        public long psychiatric_evaluation_id { get; set; }

        public virtual mp_appointment appointment_ { get; set; }
        public virtual mp_profile profile_ { get; set; }
        public virtual mp_psychiatric_opd_evaluation psychiatric_evaluation_ { get; set; }
    }
}
