using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_pediatric_evaluation
    {
        public mp_pediatric_evaluation()
        {
            mp_ped_evaluation_history = new HashSet<mp_ped_evaluation_history>();
            mp_ped_symptomps = new HashSet<mp_ped_symptomps>();
        }

        public int id { get; set; }
        public DateTime? visit_date { get; set; }
        public string complaiant { get; set; }
        public string family_history { get; set; }
        public string past_medical_probelms { get; set; }
        public string illness_since_last_visit { get; set; }
        public string drug_allergy { get; set; }
        public double? temp { get; set; }
        public double? pulse { get; set; }
        public double? bp { get; set; }
        public double? weight { get; set; }
        public double? head_circumference { get; set; }
        public double? mid_upper_arm { get; set; }
        public double? ht_lt { get; set; }
        public string visit_summary { get; set; }
        public string medication { get; set; }
        public string referral { get; set; }
        public string additional_information { get; set; }
        public DateTime? next_visit { get; set; }
        public DateTime? created_date { get; set; }
        public Guid appointment_id { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
        public Guid profile_id { get; set; }
        public string created_by { get; set; }

        public virtual ICollection<mp_ped_evaluation_history> mp_ped_evaluation_history { get; set; }
        public virtual ICollection<mp_ped_symptomps> mp_ped_symptomps { get; set; }
    }
}
