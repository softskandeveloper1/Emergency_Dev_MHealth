using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_form
    {
        public mp_form()
        {
            mp_appointment_form = new HashSet<mp_appointment_form>();
        }

        public int id { get; set; }
        public string name { get; set; }
        public string write { get; set; }
        public string read { get; set; }
        public int? clinician_type { get; set; }
        public int active { get; set; }

        public virtual ICollection<mp_appointment_form> mp_appointment_form { get; set; }
    }
}
