using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_lk_psychiatric_diagnosis
    {
        public int id { get; set; }
        public string icd_10_code { get; set; }
        public string icd_9_code { get; set; }
        public string description { get; set; }
    }
}
