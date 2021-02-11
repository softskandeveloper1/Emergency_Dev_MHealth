using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ProfileMatchService: IProfileMatchService
    {
        private readonly HContext _context = new HContext();

        public Guid Add(mp_profile_match profile_match)
        {
            //check if the match already exists

            var old = _context.mp_profile_match.FirstOrDefault(e => e.profile_id == profile_match.profile_id && e.clinician_id == profile_match.clinician_id && e.appointment_type_id == profile_match.appointment_type_id && e.appointment_activity_id == profile_match.appointment_activity_id && e.appointment_activity_sub_id == profile_match.appointment_activity_sub_id);

            if (old != null) return old.id;

            profile_match.id = Guid.NewGuid();
            profile_match.created_at = DateTime.Now;
            _context.mp_profile_match.Add(profile_match);
            _context.SaveChanges();

            return profile_match.id;
        }

        public IQueryable<mp_profile_match> Get()
        {
            return _context.mp_profile_match.AsQueryable();
        }
    }
}
