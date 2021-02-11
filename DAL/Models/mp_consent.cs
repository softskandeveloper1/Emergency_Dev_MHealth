using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_consent
    {
        public long id { get; set; }
        public string consent { get; set; }
        public string consent_type { get; set; }
        public int version { get; set; }
    }
}
