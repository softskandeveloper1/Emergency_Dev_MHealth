using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class mp_notification
    {
        public long id { get; set; }
        public string notification { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public string user_id { get; set; }
        public int read { get; set; }
        public string updated_by { get; set; }
        public DateTime? updated_at { get; set; }
        public string created_by_name { get; set; }
        public string title { get; set; }
        public int notification_type { get; set; }
    }
}
