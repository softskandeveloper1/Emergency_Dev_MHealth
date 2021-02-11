using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_substance_use
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public int tobacco { get; set; }
        public int? tobacco_amount { get; set; }
        public int? tobacco_years { get; set; }
        public int alcohol { get; set; }
        public string alcohol_type { get; set; }
        public string alcohol_freq { get; set; }
        public string last_drink { get; set; }
        public string drink_amount { get; set; }
        public int withdrawal_symptons { get; set; }
        public string withdrawal_symptons_description { get; set; }
        public int blackouts { get; set; }
        public int? blackouts_freq { get; set; }
        public int illicit_drugs { get; set; }
        public string drug_type { get; set; }
        public int? drug_freq { get; set; }
        public DateTime? date_of_last_use { get; set; }
        public int substance_abuse_treatment { get; set; }
        public string agency { get; set; }
        public string treatment_type { get; set; }
        public DateTime? treatment_date { get; set; }
        public int support_programs { get; set; }
        public string support_programs_description { get; set; }
        public int triggers { get; set; }
        public string triggers_description { get; set; }
        public int? use_legal_issues { get; set; }
        public string legal_issues_description { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public Guid? appointment_id { get; set; }
    }
}
