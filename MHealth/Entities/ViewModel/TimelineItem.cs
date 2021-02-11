using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Entities.ViewModel
{
    public class TimelineItem
    {
        public long id { set; get; }
        public Guid profile_id { set; get; }
        public string form_name { set; get; }
        public DateTime created_at { set; get; }
        public string created_by { set; get; }
        public Guid appointment_id { set; get; }
        public string appointment_type { set; get; }
        public string appointment_service { set; get; }
        public string view_url { set; get; }
    }
}
