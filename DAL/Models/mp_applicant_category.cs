using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant_category
    {
        public long id { get; set; }
        public Guid applicant_id { get; set; }
        public int appointment_type { get; set; }
        public int appointment_category { get; set; }
        public int appointment_category_sub { get; set; }

        public virtual mp_applicant applicant_ { get; set; }
        public virtual mp_lk_appointment_activity appointment_categoryNavigation { get; set; }
        public virtual mp_lk_appointment_activity_sub appointment_category_subNavigation { get; set; }
        public virtual mp_lk_appointment_type appointment_typeNavigation { get; set; }
    }
}
