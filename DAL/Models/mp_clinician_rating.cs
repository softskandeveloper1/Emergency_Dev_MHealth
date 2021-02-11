using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_clinician_rating
    {
        public long id { get; set; }
        public Guid appointment_id { get; set; }
        public Guid clinician_id { get; set; }
        public Guid client_id { get; set; }
        public int considerate { get; set; }
        public int professional { get; set; }
        public int understand_problems { get; set; }
        public int worked_together { get; set; }
        public int felt_safe { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
    }
}
