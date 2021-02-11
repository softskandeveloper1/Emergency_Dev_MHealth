using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_psychosocial
    {
        public int id { get; set; }
        public Guid profile_id { get; set; }
        public string problem { get; set; }
        public int depression { get; set; }
        public int anxiety { get; set; }
        public int mood_swing { get; set; }
        public int appetite_changes { get; set; }
        public int sleep_changes { get; set; }
        public int hallucinations { get; set; }
        public int work_problems { get; set; }
        public int racing_thoughts { get; set; }
        public int confusion { get; set; }
        public int memory_problems { get; set; }
        public int loss_interest { get; set; }
        public int irritability { get; set; }
        public int excessive_worry { get; set; }
        public int suicidal_ideation { get; set; }
        public int relationship_issues { get; set; }
        public int low_energy { get; set; }
        public int panic_attacks { get; set; }
        public int obsessive_thoughts { get; set; }
        public int ritualistic_behaviour { get; set; }
        public int checking { get; set; }
        public int counting { get; set; }
        public int self_injury { get; set; }
        public int difficulty_concentrating { get; set; }
        public int hyperactivity { get; set; }
        public string history { get; set; }
        public string effect_symptons { get; set; }
        public int mental_problem { get; set; }
        public string mental_problem_description { get; set; }
        public int mental_hospitalization { get; set; }
        public string mental_hospitalization_description { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public Guid appointment_id { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
    }
}
