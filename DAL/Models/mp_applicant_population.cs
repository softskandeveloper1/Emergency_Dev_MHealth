using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant_population
    {
        public long id { get; set; }
        public Guid applicant_id { get; set; }
        public int population_id { get; set; }

        public virtual mp_applicant applicant_ { get; set; }
        public virtual mp_population_group population_ { get; set; }
    }
}
