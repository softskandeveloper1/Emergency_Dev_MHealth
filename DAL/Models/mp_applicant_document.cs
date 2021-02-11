using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant_document
    {
        public int id { get; set; }
        public int document_type { get; set; }
        public string path { get; set; }
        public Guid applicant_id { get; set; }

        public virtual mp_applicant applicant_ { get; set; }
    }
}
