using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_progress_note
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public int appetite { get; set; }
        public int sleep { get; set; }
        public int side_effects { get; set; }
        public int meditation_efficacy { get; set; }
        public int medication_compliance { get; set; }
        public int orientation { get; set; }
        public int rapport { get; set; }
        public int appearance { get; set; }
        public int mood { get; set; }
        public int affect { get; set; }
        public int speech { get; set; }
        public int thought_process { get; set; }
        public int insight { get; set; }
        public int judgement { get; set; }
        public int cognitive { get; set; }
        public int psychomotor_activity { get; set; }
        public int memory_immediate { get; set; }
        public int assessment { get; set; }
        public string assessment_description { get; set; }
        public string plan { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public int? memory_recent { get; set; }
        public int? memory_past { get; set; }
        public string comment { get; set; }
        public Guid appointment_id { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
