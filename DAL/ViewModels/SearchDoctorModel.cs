using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.ViewModels
{
    public class SearchDoctorModel
    {
        public DateTime? appointmentDate { get; set; }
        public int? appointmentType { get; set; }
        public long? appointmentCategory { get; set; }
        public long? appointmentActivity { get; set; }
        public long? appointmentService { get; set; }
        public string name { set; get; }
        public string clinician_id { get; set; }
    }
}
