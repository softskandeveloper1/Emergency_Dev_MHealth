using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_psychiatric_opd_evaluation
    {
        public mp_psychiatric_opd_evaluation()
        {
            mp_psychiatric_opd_evaluation_diagnosis = new HashSet<mp_psychiatric_opd_evaluation_diagnosis>();
            mp_summary_treatment_history = new HashSet<mp_summary_treatment_history>();
        }

        public long id { get; set; }
        public Guid profile_id { get; set; }
        public string problem { get; set; }
        public string history_of_problem { get; set; }
        public string social_history { get; set; }
        public string family_history { get; set; }
        public string developmental_history { get; set; }
        public string substance_abuse_history { get; set; }
        public string medical_history { get; set; }
        public string medical_issues { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public string summary_of_recommendation { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
        public Guid appointment_id { get; set; }

        public virtual mp_profile profile_ { get; set; }
        public virtual ICollection<mp_psychiatric_opd_evaluation_diagnosis> mp_psychiatric_opd_evaluation_diagnosis { get; set; }
        public virtual ICollection<mp_summary_treatment_history> mp_summary_treatment_history { get; set; }
    }
}
