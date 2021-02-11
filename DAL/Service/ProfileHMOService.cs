using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class ProfileHMOService: IProfileHMOService
    {
        private readonly HContext _context = new HContext();

        public void AddOrUpdate(mp_profile_hmo hmo)
        {
            var old = _context.mp_profile_hmo.FirstOrDefault(e => e.id == hmo.id);
            if (old != null)
            {
                hmo.created_at = old.created_at;
                hmo.created_by = old.created_by;
                hmo.updated_at = DateTime.Now;

                _context.Entry(old).CurrentValues.SetValues(hmo);
            }
            else
            {
                hmo.created_at = DateTime.Now;
                _context.mp_profile_hmo.Add(hmo);
            }
            _context.SaveChanges();
        }


        public mp_profile_hmo GetProfileHMO(Guid profile_id)
        {
            return _context.mp_profile_hmo.FirstOrDefault(e => e.profile_id == profile_id);
        }

        public IQueryable<mp_profile_hmo> Get()
        {
            return _context.mp_profile_hmo.AsQueryable();
        }
    }
}
