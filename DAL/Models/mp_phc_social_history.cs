using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_phc_social_history
    {
        public int id { get; set; }
        public int? marital_status { get; set; }
        public string smoking { get; set; }
        public string alchohol { get; set; }
        public int? past_drug_use { get; set; }
        public int? current_drug_use { get; set; }
        public Guid profile_id { get; set; }
        public string created_by { get; set; }
        public DateTime? created_at { get; set; }
        public Guid? appointment_id { get; set; }
    }
}
