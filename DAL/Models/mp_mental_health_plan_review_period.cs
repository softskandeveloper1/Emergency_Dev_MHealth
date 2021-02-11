using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_mental_health_plan_review_period
    {
        public long id { get; set; }
        public long health_plan_id { get; set; }
        public DateTime review_date { get; set; }
        public string goal { get; set; }
        public int sufficient_to_goal { get; set; }

        public virtual mp_mental_health_plan health_plan_ { get; set; }
    }
}
