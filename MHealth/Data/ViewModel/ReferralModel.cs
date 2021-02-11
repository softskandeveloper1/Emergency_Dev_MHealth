using DAL.Models;
using DAL.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MHealth.Data.ViewModel
{
    public class ReferralModel
    {
        public ReferralModel()
        {

        }

        public ReferralModel(mp_referral referral)
        {
            id = referral.id;
            clinician_id = referral.clinician_id;
            profile_id = referral.profile_id;
            created_at = referral.created_at;
            created_by = referral.created_by;
            profile_match_id = referral.profile_match_id;
            referred_by = ProfileUtil.get_profile_name(referral.created_by, "role");

            clinician = new DoctorModel(referral.clinician_);
            profile = new MemberModel(referral.profile_);
            profile_match = new ProfileMatchModel(referral.profile_match_);
        }

        public long id { get; set; }
        public Guid clinician_id { get; set; }
        public Guid profile_id { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public Guid profile_match_id { get; set; }
        public string referred_by { get; set; }

        public DoctorModel clinician { get; set; }
        public MemberModel profile { get; set; }
        public ProfileMatchModel profile_match { get; set; }
    }
}
