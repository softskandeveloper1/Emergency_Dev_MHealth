using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_phc_medical_history
    {
        public int id { get; set; }
        public string visit_reason { get; set; }
        public string allergy { get; set; }
        public string medication { get; set; }
        public string food { get; set; }
        public string complain { get; set; }
        public Guid profile_id { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
    }
}
