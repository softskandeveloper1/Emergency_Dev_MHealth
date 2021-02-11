using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinician
    {
        public mp_clinician()
        {
            mp_applicant_checklist = new HashSet<mp_applicant_checklist>();
            mp_appointment = new HashSet<mp_appointment>();
            mp_clinic_clinician = new HashSet<mp_clinic_clinician>();
            mp_clinician_availability = new HashSet<mp_clinician_availability>();
            mp_clinician_category = new HashSet<mp_clinician_category>();
            mp_clinician_document = new HashSet<mp_clinician_document>();
            mp_clinician_education = new HashSet<mp_clinician_education>();
            mp_clinician_expertise = new HashSet<mp_clinician_expertise>();
            mp_clinician_language = new HashSet<mp_clinician_language>();
            mp_clinician_other_activities = new HashSet<mp_clinician_other_activities>();
            mp_clinician_population = new HashSet<mp_clinician_population>();
            mp_clinician_practice = new HashSet<mp_clinician_practice>();
            mp_clinician_specialty = new HashSet<mp_clinician_specialty>();
            mp_link_applicant_clinician = new HashSet<mp_link_applicant_clinician>();
            mp_prescription = new HashSet<mp_prescription>();
            mp_profile_match = new HashSet<mp_profile_match>();
            mp_referral = new HashSet<mp_referral>();
            mp_service_costing = new HashSet<mp_service_costing>();
        }

        public Guid id { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string preferred_name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int state { get; set; }
        public int country { get; set; }
        public DateTime dob { get; set; }
        public string user_id { get; set; }
        public int? education_level { get; set; }
        public int status { get; set; }
        public int marital_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string about { get; set; }
        public int? area_of_interest { get; set; }
        public string contact_name { get; set; }
        public string contact_phone { get; set; }
        public string contact_email { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public int consent_signed { get; set; }
        public int years_of_experience { get; set; }
        public DateTime? year_qualified_doctor { get; set; }
        public DateTime? year_qualified_specialist { get; set; }
        public int provider_type { get; set; }
        public int gender { get; set; }
        public string bank_name { get; set; }
        public string account_name { get; set; }
        public string account_number { get; set; }
        public string association { set; get; }
        public string member_since { set; get; }
        public string membership_number { set; get; }
        public string mode_of_identification { set; get; }
        public string identification_number { set; get; }
        public string licensure { set; get; }

        public virtual ICollection<mp_applicant_checklist> mp_applicant_checklist { get; set; }
        public virtual ICollection<mp_appointment> mp_appointment { get; set; }
        public virtual ICollection<mp_clinic_clinician> mp_clinic_clinician { get; set; }
        public virtual ICollection<mp_clinician_availability> mp_clinician_availability { get; set; }
        public virtual ICollection<mp_clinician_category> mp_clinician_category { get; set; }
        public virtual ICollection<mp_clinician_document> mp_clinician_document { get; set; }
        public virtual ICollection<mp_clinician_education> mp_clinician_education { get; set; }
        public virtual ICollection<mp_clinician_expertise> mp_clinician_expertise { get; set; }
        public virtual ICollection<mp_clinician_language> mp_clinician_language { get; set; }
        public virtual ICollection<mp_clinician_other_activities> mp_clinician_other_activities { get; set; }
        public virtual ICollection<mp_clinician_population> mp_clinician_population { get; set; }
        public virtual ICollection<mp_clinician_practice> mp_clinician_practice { get; set; }
        public virtual ICollection<mp_clinician_specialty> mp_clinician_specialty { get; set; }
        public virtual ICollection<mp_link_applicant_clinician> mp_link_applicant_clinician { get; set; }
        public virtual ICollection<mp_prescription> mp_prescription { get; set; }
        public virtual ICollection<mp_profile_match> mp_profile_match { get; set; }
        public virtual ICollection<mp_referral> mp_referral { get; set; }
        public virtual ICollection<mp_service_costing> mp_service_costing { get; set; }
    }
}
