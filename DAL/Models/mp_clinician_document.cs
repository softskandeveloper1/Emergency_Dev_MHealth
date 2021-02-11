using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinician_document
    {
        public int document_type { get; set; }
        public Guid clinician_id { get; set; }
        public string path { get; set; }
        public DateTime created_at { get; set; }
        public long id { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
    }
}
