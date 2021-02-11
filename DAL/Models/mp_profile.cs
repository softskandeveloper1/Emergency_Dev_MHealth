using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_profile
    {
        public mp_profile()
        {
            mp_appointment = new HashSet<mp_appointment>();
            mp_child_screening = new HashSet<mp_child_screening>();
            mp_children = new HashSet<mp_children>();
            mp_couple_intake = new HashSet<mp_couple_intake>();
            mp_emergency_contact = new HashSet<mp_emergency_contact>();
            mp_employment = new HashSet<mp_employment>();
            mp_enrollment = new HashSet<mp_enrollment>();
            mp_family_history = new HashSet<mp_family_history>();
            mp_family_intake = new HashSet<mp_family_intake>();
            mp_major_illness_surgery = new HashSet<mp_major_illness_surgery>();
            mp_medical_history = new HashSet<mp_medical_history>();
            mp_medication = new HashSet<mp_medication>();
            mp_medication_information = new HashSet<mp_medication_information>();
            mp_mental_health_plan = new HashSet<mp_mental_health_plan>();
            mp_ped_evaluation_history = new HashSet<mp_ped_evaluation_history>();
            mp_prescription = new HashSet<mp_prescription>();
            mp_profile_hmo = new HashSet<mp_profile_hmo>();
            mp_profile_match = new HashSet<mp_profile_match>();
            mp_progress_note = new HashSet<mp_progress_note>();
            mp_psychiatric_opd_evaluation = new HashSet<mp_psychiatric_opd_evaluation>();
            mp_psychiatric_progress_note = new HashSet<mp_psychiatric_progress_note>();
            mp_referral = new HashSet<mp_referral>();
            mp_social_relationship = new HashSet<mp_social_relationship>();
            mp_summary_treatment_history = new HashSet<mp_summary_treatment_history>();
            mp_surgical_history = new HashSet<mp_surgical_history>();
        }

        public Guid id { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string preferred_name { get; set; }
        public string address { get; set; }
        public string area { get; set; }
        public string city { get; set; }
        public string school_name { get; set; }
        public int state { get; set; }
        public int country { get; set; }
        public DateTime dob { get; set; }
        public string user_id { get; set; }
        public int profile_type { get; set; }
        public int? education_level { get; set; }
        public int status { get; set; }
        public int marital_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string about { get; set; }
        public int gender { get; set; }
        public int unique_id { get; set; }
        public int? tribe { get; set; }
        public int? language { get; set; }
        public int? counselor_preference { get; set; }
        public int? years_of_experience { get; set; }
        public int? employed { get; set; }
        public int consent_signed { get; set; }
        public bool? is_one_hour_reminder_mail_send { get; set; }
        public bool? is_24_hour_reminder_mail_send { get; set; }

        public virtual ICollection<mp_appointment> mp_appointment { get; set; }
        public virtual ICollection<mp_child_screening> mp_child_screening { get; set; }
        public virtual ICollection<mp_children> mp_children { get; set; }
        public virtual ICollection<mp_couple_intake> mp_couple_intake { get; set; }
        public virtual ICollection<mp_emergency_contact> mp_emergency_contact { get; set; }
        public virtual ICollection<mp_employment> mp_employment { get; set; }
        public virtual ICollection<mp_enrollment> mp_enrollment { get; set; }
        public virtual ICollection<mp_family_history> mp_family_history { get; set; }
        public virtual ICollection<mp_family_intake> mp_family_intake { get; set; }
        public virtual ICollection<mp_major_illness_surgery> mp_major_illness_surgery { get; set; }
        public virtual ICollection<mp_medical_history> mp_medical_history { get; set; }
        public virtual ICollection<mp_medication> mp_medication { get; set; }
        public virtual ICollection<mp_medication_information> mp_medication_information { get; set; }
        public virtual ICollection<mp_mental_health_plan> mp_mental_health_plan { get; set; }
        public virtual ICollection<mp_ped_evaluation_history> mp_ped_evaluation_history { get; set; }
        public virtual ICollection<mp_prescription> mp_prescription { get; set; }
        public virtual ICollection<mp_profile_hmo> mp_profile_hmo { get; set; }
        public virtual ICollection<mp_profile_match> mp_profile_match { get; set; }
        public virtual ICollection<mp_progress_note> mp_progress_note { get; set; }
        public virtual ICollection<mp_psychiatric_opd_evaluation> mp_psychiatric_opd_evaluation { get; set; }
        public virtual ICollection<mp_psychiatric_progress_note> mp_psychiatric_progress_note { get; set; }
        public virtual ICollection<mp_referral> mp_referral { get; set; }
        public virtual ICollection<mp_social_relationship> mp_social_relationship { get; set; }
        public virtual ICollection<mp_summary_treatment_history> mp_summary_treatment_history { get; set; }
        public virtual ICollection<mp_surgical_history> mp_surgical_history { get; set; }
    }
}
