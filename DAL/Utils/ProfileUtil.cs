using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Utils
{
    public static class ProfileUtil
    {
        private static readonly HContext _context = new HContext();

        public static bool IsConsented(string user_id,string role)
        {
            if (role == "client")
            {
                var profile = _context.mp_profile.AsNoTracking().FirstOrDefault(e => e.user_id == user_id);
                if (profile.consent_signed == 0)
                {
                    return false;
                }
            }
            else if (role == "clinician")
            {
                var profile = _context.mp_clinician.AsNoTracking().FirstOrDefault(e => e.user_id == user_id);
                if (profile.consent_signed == 0)
                {
                    return false;
                }

            }

            return true;
        }

        public static string get_profile_name(string user_id, string role)
        {
            if (role == "client")
            {
                var profile = _context.mp_profile.FirstOrDefault(e => e.user_id == user_id);
                return string.Format("{0} {1}", profile.last_name, profile.first_name);
            }
            else if (role == "clinician")
            {
                var profile = _context.mp_clinician.FirstOrDefault(e => e.user_id == user_id);
                return string.Format("{0} {1}", profile.last_name, profile.first_name);
            }

            return "";
        }

       
    }
}