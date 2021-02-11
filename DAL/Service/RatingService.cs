using DAL.IService;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Service
{
    public class RatingService: IRatingService
    {
        private readonly HContext _context = new HContext();

        public void AddOrUpdate(mp_clinician_rating rating)
        {
            var old = _context.mp_clinician_rating.FirstOrDefault(e => e.id == rating.id);
            if (old != null)
            {
                rating.created_at = old.created_at;
                rating.created_by = old.created_by;
                _context.Entry(old).CurrentValues.SetValues(rating);
            }
            else
            {
                rating.created_at = DateTime.Now;
                _context.mp_clinician_rating.Add(rating);
            }
           
            _context.SaveChanges();
        }

        public IQueryable<mp_clinician_rating> Get()
        {
            return _context.mp_clinician_rating.AsQueryable();
        }
    }
}
