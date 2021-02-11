using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_prescription_drug
    {
        public long id { get; set; }
        public long prescription_id { get; set; }
        public string dosage { get; set; }
        public string drug { get; set; }

        public virtual mp_prescription prescription_ { get; set; }
    }
}
