using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_profile_mental_status
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public int mental_status_id { get; set; }
        public int status { get; set; }
        public string comment { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
    }
}
