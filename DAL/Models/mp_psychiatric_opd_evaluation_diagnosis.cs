using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_psychiatric_opd_evaluation_diagnosis
    {
        public long id { get; set; }
        public int diagnosis_type { get; set; }
        public int? diagnosis_id { get; set; }
        public string description { get; set; }
        public long psychiatric_evaluation_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }

        public virtual mp_psychiatric_opd_evaluation psychiatric_evaluation_ { get; set; }
    }
}
