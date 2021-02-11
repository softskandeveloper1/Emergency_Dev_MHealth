using DAL.IService;
using DAL.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ProfileService : IProfileService
    {
        private readonly HContext _context = new HContext();

        public Guid Add(mp_profile profile)
        {
            profile.id = Guid.NewGuid();
            profile.created_at = DateTime.Now;
            _context.mp_profile.Add(profile);
            _context.SaveChanges();

            return profile.id;
        }

        public IQueryable<mp_profile> Get()
        {
            return _context.mp_profile.AsQueryable();
        }

        public mp_profile Get(Guid id)
        {
            return _context.mp_profile
                .Include(e=>e.mp_enrollment)
                .FirstOrDefault(e => e.id == id);
        }

        public mp_profile GetProfileByUserId(string user_id)
        {
            return _context.mp_profile
                .Include(e => e.mp_enrollment)
                .FirstOrDefault(e => e.user_id == user_id);
        }

        public void Update(mp_profile profile)
        {
            var old = _context.mp_profile.FirstOrDefault(e => e.id == profile.id);
            profile.created_at = old.created_at;

            _context.Entry(old).CurrentValues.SetValues(profile);
            _context.SaveChanges();
        }

        public int Remove(Guid id)
        {
            var existing = _context.mp_profile.Find(id);
            _context.mp_profile.Remove(existing);
            return _context.SaveChanges();
        }

        public bool ProfileExists(Guid id)
        {
            return _context.mp_profile.Any(e => e.id == id);
        }

        public mp_profile GetByUserId(Guid id)
        {
            string user_id = id.ToString();
            return _context.mp_profile.FirstOrDefault(e => e.user_id == user_id);
        }
    }
}
