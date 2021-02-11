using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant_other_activities
    {
        public long id { get; set; }
        public Guid applicant_id { get; set; }
        public string activity { get; set; }
        public string activity_type { get; set; }

        public virtual mp_applicant applicant_ { get; set; }
    }
}
