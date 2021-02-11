using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_education_history
    {
        public int id { get; set; }
        public string how_far_school { get; set; }
        public int behavioural_issues { get; set; }
        public string behavioural_issues_describe { get; set; }
        public Guid profile_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public Guid? appointment_id { get; set; }
    }
}
