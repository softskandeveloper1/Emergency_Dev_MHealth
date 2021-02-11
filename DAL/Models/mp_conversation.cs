using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_conversation
    {
        public long id { get; set; }
        public string message { get; set; }
        public DateTime created_at { get; set; }
        public string to { get; set; }
        public string from { get; set; }
    }
}
