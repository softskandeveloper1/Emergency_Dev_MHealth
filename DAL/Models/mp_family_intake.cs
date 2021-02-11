using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_family_intake
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public string partner_name { get; set; }
        public string partner_phone { get; set; }
        public int? relationship { get; set; }
        public string partner_email { get; set; }
        public DateTime? partner_dob { get; set; }
        public int? suicidal { get; set; }
        public string suicidal_details { get; set; }
        public int? hospitalized_mental { get; set; }
        public string hospitalized_mental_summary { get; set; }
        public int? member_receiving_counseling { get; set; }
        public string member_receiving_counseling_summary { get; set; }
        public string counseling_reason { get; set; }
        public string success_determination { get; set; }
        public string family_strength { get; set; }
        public string family_weakness { get; set; }
        public string deal_with_conflict { get; set; }
        public string how_family_celebrate { get; set; }
        public string together_activities { get; set; }
        public string family_reaction_to_issues { get; set; }
        public int? family_violence { get; set; }
        public string family_violence_summary { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
        public int? partner_gender { get; set; }
        public Guid appointment_id { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
