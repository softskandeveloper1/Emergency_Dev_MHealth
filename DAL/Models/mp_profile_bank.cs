using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_profile_bank
    {
        public long id { get; set; }
        public Guid profile_id { get; set; }
        public string bank_name { get; set; }
        public string account_number { get; set; }
        public string account_name { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_at { get; set; }
        public string updated_by { get; set; }
    }
}
