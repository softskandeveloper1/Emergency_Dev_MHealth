using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_medical_history
    {
        public int id { get; set; }
        public Guid profile_id { get; set; }
        public int pain { get; set; }
        public string pain_description { get; set; }
        public int? binged_on_food { get; set; }
        public int? gone_without_eating { get; set; }
        public int? vomitted_on_purpose { get; set; }
        public int? laxatives_to_purge { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public Guid appointment_id { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
