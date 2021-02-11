using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_couple_screening
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public int relationship_status { get; set; }
        public string relationship_duration { get; set; }
        public string couple_counseling_expectation { get; set; }
        public int counselor_preference { get; set; }
        public int counselor_preference_age { get; set; }
        public int counselor_preference_religion { get; set; }
        public int domestic_violence { get; set; }
        public int relationship_type { get; set; }
        public DateTime created_at { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
