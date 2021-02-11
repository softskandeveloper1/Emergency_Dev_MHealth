using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_mental_health_plan
    {
        public mp_mental_health_plan()
        {
            mp_mental_health_plan_objective = new HashSet<mp_mental_health_plan_objective>();
            mp_mental_health_plan_review_period = new HashSet<mp_mental_health_plan_review_period>();
        }

        public long id { get; set; }
        public Guid profile_id { get; set; }
        public int service_type { get; set; }
        public DateTime start_date { get; set; }
        public int duration { get; set; }
        public string problem_area { get; set; }
        public string history { get; set; }
        public string long_term_goal { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public Guid appointment_id { get; set; }

        public virtual mp_profile profile_ { get; set; }
        public virtual ICollection<mp_mental_health_plan_objective> mp_mental_health_plan_objective { get; set; }
        public virtual ICollection<mp_mental_health_plan_review_period> mp_mental_health_plan_review_period { get; set; }
    }
}
