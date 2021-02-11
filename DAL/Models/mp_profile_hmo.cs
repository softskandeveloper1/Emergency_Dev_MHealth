using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_profile_hmo
    {
        public long id { get; set; }
        public string hmo_name { get; set; }
        public string organization_name { get; set; }
        public string insurance_number { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
        public Guid profile_id { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
