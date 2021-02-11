using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant_checklist
    {
        public long id { get; set; }
        public Guid clinician_id { get; set; }
        public int resume { get; set; }
        public int passport_photograph { get; set; }
        public int verification_of_employment { get; set; }
        public int verification_of_education { get; set; }
        public int verification_of_license { get; set; }
        public int offer_signed { get; set; }
        public int bank_information { get; set; }
        public int orientation { get; set; }
        public int work_email { get; set; }
        public int badge { get; set; }
        public int access_setup { get; set; }
        public int login_setup { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
    }
}
