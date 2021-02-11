using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_psychiatric_progress_note
    {
        public Guid profile_id { get; set; }
        public Guid appointment_id { get; set; }
        public DateTime date_of_service { get; set; }
        public string server { get; set; }
        public DateTime start_time { get; set; }
        public string length_of_session { get; set; }
        public string type_of_session { get; set; }
        public string service_code { get; set; }
        public string visit_reason { get; set; }
        public string past_history { get; set; }
        public string family_history { get; set; }
        public string symptoms_review { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string blood_pressure { get; set; }
        public string pulse { get; set; }
        public string psychiatric_exam_note { get; set; }
        public string allergies { get; set; }
        public string additional_comment { get; set; }
        public string risk_and_benefit_note { get; set; }
        public string created_by { get; set; }
        public DateTime? created_at { get; set; }
        public long id { get; set; }
        public DateTime stop_time { get; set; }

        public virtual mp_appointment appointment_ { get; set; }
        public virtual mp_profile profile_ { get; set; }
    }
}
