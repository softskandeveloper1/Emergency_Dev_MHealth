using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_child_screening
    {
        public int id { get; set; }
        public int child_gender { get; set; }
        public int child_relationship_to { get; set; }
        public int child_age { get; set; }
        public int where_child_lives { get; set; }
        public int does_child_school { get; set; }
        public int child_in_counselling { get; set; }
        public int child_physical_health_rating { get; set; }
        public int child_eating_rating { get; set; }
        public int child_sleep_rating { get; set; }
        public int child_experiencing_anxiety { get; set; }
        public int child_experiencing_sadness { get; set; }
        public int little_pleasure { get; set; }
        public int have_outburst_anger { get; set; }
        public int trouble_concentrating { get; set; }
        public int feeling_down { get; set; }
        public int feeling_tired { get; set; }
        public int child_feeling_bad { get; set; }
        public int child_being_suicidal { get; set; }
        public string child_issue_concerns { get; set; }
        public int child_relationship_rating { get; set; }
        public int financial_status { get; set; }
        public int child_preferred_language { get; set; }
        public Guid profile_id { get; set; }
        public int child_fidgety { get; set; }
        public int counselor_preference { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
