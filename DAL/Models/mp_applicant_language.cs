using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_applicant_language
    {
        public long id { get; set; }
        public Guid applicant_id { get; set; }
        public int language { get; set; }

        public virtual mp_applicant applicant_ { get; set; }
        public virtual mp_lookup languageNavigation { get; set; }
    }
}
