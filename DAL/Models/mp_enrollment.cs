using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_enrollment
    {
        public int id { get; set; }
        public int religious { get; set; }
        public int? religion { get; set; }
        public int help_reason { get; set; }
        public int earlier_counseling { get; set; }
        public int physical_health { get; set; }
        public int eating_habit { get; set; }
        public int sleeping { get; set; }
        public int depression { get; set; }
        public int reduced_interest { get; set; }
        public int recent_depression { get; set; }
        public int sleeping_trouble { get; set; }
        public int tiredness { get; set; }
        public int appetite { get; set; }
        public int feeling_bad { get; set; }
        public int concentration_issue { get; set; }
        public int fidgety { get; set; }
        public int suicidal { get; set; }
        public int today_feeling { get; set; }
        public int employed { get; set; }
        public int alcohol { get; set; }
        public int anxiety { get; set; }
        public bool? clinicalTherapyFaith { get; set; }
        public Guid profile_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public int? drug_use { get; set; }
        public int? anger_burst { get; set; }

        public virtual mp_profile profile_ { get; set; }
    }
}
