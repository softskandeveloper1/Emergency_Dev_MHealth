using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant
    {
        public mp_applicant()
        {
            mp_applicant_category = new HashSet<mp_applicant_category>();
            mp_applicant_document = new HashSet<mp_applicant_document>();
            mp_applicant_education = new HashSet<mp_applicant_education>();
            mp_applicant_expertise = new HashSet<mp_applicant_expertise>();
            mp_applicant_language = new HashSet<mp_applicant_language>();
            mp_applicant_other_activities = new HashSet<mp_applicant_other_activities>();
            mp_applicant_population = new HashSet<mp_applicant_population>();
            mp_applicant_practice = new HashSet<mp_applicant_practice>();
            mp_applicant_specialty = new HashSet<mp_applicant_specialty>();
            mp_link_applicant_clinician = new HashSet<mp_link_applicant_clinician>();
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
        public int gender { get; set; }
        public int country { get; set; }
        public DateTime dob { get; set; }
        public int status { get; set; }
        public int marital_status { get; set; }
        public DateTime created_at { get; set; }
        public string expertise { get; set; }
        public string about { get; set; }
        public int? area_of_interest { get; set; }
        public string contact_name { get; set; }
        public string contact_phone { get; set; }
        public string contact_email { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
        public int years_of_experience { get; set; }
        public DateTime? year_qualified_doctor { get; set; }
        public DateTime? year_qualified_specialist { get; set; }

        public virtual mp_countries countryNavigation { get; set; }
        public virtual mp_lookup marital_statusNavigation { get; set; }
        public virtual mp_states stateNavigation { get; set; }
        public virtual mp_lookup statusNavigation { get; set; }
        public virtual ICollection<mp_applicant_category> mp_applicant_category { get; set; }
        public virtual ICollection<mp_applicant_document> mp_applicant_document { get; set; }
        public virtual ICollection<mp_applicant_education> mp_applicant_education { get; set; }
        public virtual ICollection<mp_applicant_expertise> mp_applicant_expertise { get; set; }
        public virtual ICollection<mp_applicant_language> mp_applicant_language { get; set; }
        public virtual ICollection<mp_applicant_other_activities> mp_applicant_other_activities { get; set; }
        public virtual ICollection<mp_applicant_population> mp_applicant_population { get; set; }
        public virtual ICollection<mp_applicant_practice> mp_applicant_practice { get; set; }
        public virtual ICollection<mp_applicant_specialty> mp_applicant_specialty { get; set; }
        public virtual ICollection<mp_link_applicant_clinician> mp_link_applicant_clinician { get; set; }
    }
}
