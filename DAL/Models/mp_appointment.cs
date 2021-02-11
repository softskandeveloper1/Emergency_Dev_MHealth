using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_appointment
    {
        public mp_appointment()
        {
            mp_appointment_form = new HashSet<mp_appointment_form>();
            mp_appointment_log = new HashSet<mp_appointment_log>();
            mp_appointment_refund = new HashSet<mp_appointment_refund>();
            mp_credit = new HashSet<mp_credit>();
            mp_medication_information = new HashSet<mp_medication_information>();
            mp_ped_evaluation_history = new HashSet<mp_ped_evaluation_history>();
            mp_psychiatric_progress_note = new HashSet<mp_psychiatric_progress_note>();
            mp_summary_treatment_history = new HashSet<mp_summary_treatment_history>();
        }

        public Guid id { get; set; }
        public Guid client_id { get; set; }
        public Guid clinician_id { get; set; }
        public int status { get; set; }
        public int appointment_type { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public int appointment_service { get; set; }
        public int appointment_activity_id { get; set; }
        public int appointment_activity_sub_id { get; set; }
        public string cancel_reason { get; set; }
        public bool? is_reminder_mail_send { get; set; }
        public Guid? cancelled_by { get; set; }
        public string feedback_star { get; set; }

        public string feedback { get; set; }


        public virtual mp_lk_appointment_service appointment_serviceNavigation { get; set; }
        public virtual mp_lk_appointment_activity_sub appointment_sub_typeNavigation { get; set; }
        public virtual mp_lk_appointment_type appointment_typeNavigation { get; set; }
        public virtual mp_profile client_ { get; set; }
        public virtual mp_clinician clinician_ { get; set; }
        public virtual ICollection<mp_appointment_form> mp_appointment_form { get; set; }
        public virtual ICollection<mp_appointment_log> mp_appointment_log { get; set; }
        public virtual ICollection<mp_appointment_refund> mp_appointment_refund { get; set; }
        public virtual ICollection<mp_credit> mp_credit { get; set; }
        public virtual ICollection<mp_medication_information> mp_medication_information { get; set; }
        public virtual ICollection<mp_ped_evaluation_history> mp_ped_evaluation_history { get; set; }
        public virtual ICollection<mp_psychiatric_progress_note> mp_psychiatric_progress_note { get; set; }
        public virtual ICollection<mp_summary_treatment_history> mp_summary_treatment_history { get; set; }
    }
}
