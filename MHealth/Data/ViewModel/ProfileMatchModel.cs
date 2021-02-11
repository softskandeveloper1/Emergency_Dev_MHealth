using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class ProfileMatchModel
    {
        public ProfileMatchModel()
        {

        }

        public ProfileMatchModel(mp_profile_match match)
        {
            profile_id = match.profile_id;
            clinician_id = match.clinician_id;
            appointment_type_id = match.appointment_type_id;
            appointment_type = match.appointment_type_.name;
            appointment_activity_id = match.appointment_activity_id;
            appointment_activity = match.appointment_activity_.name;
            appointment_activity_sub_id = match.appointment_activity_sub_id;
            appointment_activity_sub = match.appointment_activity_sub_.name;

            created_at = match.created_at;

            clinician = new DoctorModel(match.clinician_);
            profile = new MemberModel(match.profile_);
        }

        public Guid profile_id { get; set; }
        public Guid clinician_id { get; set; }
        public int appointment_type_id { get; set; }
        public string appointment_type { set; get; }
        public int appointment_activity_id { get; set; }
        public string appointment_activity { get; set; }
        public int appointment_activity_sub_id { get; set; }
        public string appointment_activity_sub { get; set; }
        public DateTime created_at { get; set; }
        public Guid id { get; set; }


        public  DoctorModel clinician { get; set; }
        public MemberModel profile { get; set; }


    }
}
