using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant_specialty
    {
        public long id { get; set; }
        public Guid applicant_id { get; set; }
        public int specialty_id { get; set; }

        public virtual mp_applicant applicant_ { get; set; }
        public virtual mp_lk_specialty specialty_ { get; set; }
    }
}
