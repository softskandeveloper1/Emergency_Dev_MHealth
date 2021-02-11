using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_couple_intake
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public string partner_name { get; set; }
        public int relationship { get; set; }
        public string length_of_relationship { get; set; }
        public string counseling_reason { get; set; }
        public string counselling_expectation { get; set; }
        public string action_towards_issues { get; set; }
        public string biggest_strength { get; set; }
        public int relationship_happiness_rating { get; set; }
        public string improvement_suggestion { get; set; }
        public int earlier_counselling { get; set; }
        public DateTime? earlier_counselling_when { get; set; }
        public string earlier_counselling_where { get; set; }
        public string earlier_counselling_whom { get; set; }
        public string earlier_counselling_length { get; set; }
        public string earlier_counselling_problems_treated { get; set; }
        public int? earlier_counselling_outcome { get; set; }
        public int individual_counselling { get; set; }
        public string individual_counselling_summary { get; set; }
        public int drink_alcohol { get; set; }
        public string drink_alcohol_summary { get; set; }
        public int violence { get; set; }
        public string violence_summary { get; set; }
        public int threatened_divorce { get; set; }
        public int? threatened_divorce_who { get; set; }
        public int consulted_lawyer { get; set; }
        public int? consulted_lawyer_who { get; set; }
        public int withdrawal_from_relationship { get; set; }
        public int? withdrawal_from_relationship_who { get; set; }
        public int sexual_relationship { get; set; }
        public int? sexual_rel_enjoyment { get; set; }
        public int? satified_with_sexual_rel_freq { get; set; }
        public int? stress_level { get; set; }
        public string rank_relationship_concerns { get; set; }
        public int? relationship_satisfaction_start { get; set; }
        public int? relationship_satisfaction_now { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
        public Guid appointment_id { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
