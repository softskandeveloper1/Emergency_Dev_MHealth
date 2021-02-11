using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class AvailabilityModel
    {
        public Guid clinician_id { set; get; }
        public Dictionary<string,string> availability { set; get; }
    }
}
