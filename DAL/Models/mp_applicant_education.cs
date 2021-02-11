using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant_education
    {
        public string school { get; set; }
        public string address { get; set; }
        public int certification { get; set; }
        public Guid applicant_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public long id { get; set; }

        public virtual mp_applicant applicant_ { get; set; }
    }
}
