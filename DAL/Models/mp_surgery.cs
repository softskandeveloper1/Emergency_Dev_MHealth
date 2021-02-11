using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_surgery
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string short_name { get; set; }
        public int deleted { get; set; }
    }
}
