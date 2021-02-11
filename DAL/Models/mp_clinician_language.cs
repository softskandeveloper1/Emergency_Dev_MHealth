using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinician_language
    {
        public long id { get; set; }
        public Guid clinician_id { get; set; }
        public int language { get; set; }

        public virtual mp_clinician clinician_ { get; set; }
        public virtual mp_lookup languageNavigation { get; set; }
    }
}
