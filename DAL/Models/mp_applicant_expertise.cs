using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant_expertise
    {
        public long id { get; set; }
        public Guid applicant_id { get; set; }
        public int expertise_id { get; set; }

        public virtual mp_applicant applicant_ { get; set; }
        public virtual mp_lk_expertise expertise_ { get; set; }
    }
}
