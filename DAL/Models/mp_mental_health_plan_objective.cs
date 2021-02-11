using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_mental_health_plan_objective
    {
        public long id { get; set; }
        public long health_plan_id { get; set; }
        public string objective { get; set; }
        public int tool { get; set; }
        public DateTime date { get; set; }
        public int outcome { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }

        public virtual mp_mental_health_plan health_plan_ { get; set; }
    }
}
