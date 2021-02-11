using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant_practice
    {
        public long id { get; set; }
        public Guid applicant_id { get; set; }
        public string hospital { get; set; }
        public string city { get; set; }
        public string duration { get; set; }
        public string role { get; set; }

        public virtual mp_applicant applicant_ { get; set; }
    }
}
