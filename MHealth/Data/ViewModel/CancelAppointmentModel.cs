using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class CancelAppointmentModel
    {
        public Guid appointment_id { get; set; }
        public string cancel_reason { set; get; }
    }
}
